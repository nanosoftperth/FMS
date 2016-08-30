

Namespace DataObjects

    Public Class TimeZone

        Private Const PERTH_HOURS_OFFSET As Decimal = 8.0

#Region "properties"

        Public Property ID As String

        Public Property Offset As Decimal

        Public Property DST_Offset As Decimal

        Public Property Description As String

        Public Property CurrentOffset As Decimal



        ''' <summary>
        ''' this object can be created from either a Google web api call
        ''' or a microsoft system timezone. If it was made from a microsoft timezone, 
        ''' then the following string can be used to SAVE what timezone were talking about.
        ''' Pretty much just for reference.
        ''' </summary>
        Friend Property SerializedTimezoneObject As String

#End Region

#Region "calculated readonly properties"


        ''' <summary>
        ''' 'the devcies return GMT + 8, we have to reverse this
        ''' </summary>
        Public ReadOnly Property Offset_FromPerthToHQ As Double
            Get
                Dim rawOffset As Double = Me.Offset
                Dim dtOffset As Double = Me.DST_Offset
                Dim overallOffset As Double = rawOffset + dtOffset

                'the devcies return GMT + 8, we have to reverse this
                '-2 = -10
                '10 = 2
                '0 = -8
                '12 = 4 'looks like we just -8 from the result

                Return overallOffset - PERTH_HOURS_OFFSET '(8 being perths permanent offset, god help us if we get daylight savings back)

            End Get
        End Property

        ''' <summary>
        ''' 'the devcies return GMT + 8, we have to reverse this
        ''' </summary>
        Public ReadOnly Property Offset_FromHQToPerth As Double
            Get
                Dim rawOffset As Double = Me.Offset
                Dim dtOffset As Double = Me.DST_Offset
                Dim overallOffset As Double = rawOffset + dtOffset

                '+2 & +8 should be +6
                '+10 & +8 should be -2
                '-5 & +8 should be 13

                'this means PerthOffset - HQOffset = HTtoPerthOffset

                Return PERTH_HOURS_OFFSET - overallOffset
            End Get
        End Property

       

#End Region

#Region "consructors"
        ''' <summary>
        ''' for serialization purposes only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(googTZ As GoogleAPI.TimeZoneResponse)

            With googTZ

                Me.DST_Offset = .OffsetHoursDST
                Me.Offset = .OffsetHours
                Me.ID = .time_zone_id
                Me.Description = .time_zone_name

                Me.CurrentOffset = Me.Offset + Me.DST_Offset

            End With

        End Sub

        Public Sub New(serializedStr As String)

            Dim tzi As TimeZoneInfo = TimeZoneInfo.FromSerializedString(serializedStr)

            initializeFromTimeZoneInfo(tzi)

        End Sub

        Public Sub New(tzi As System.TimeZoneInfo)
            initializeFromTimeZoneInfo(tzi)
        End Sub

        Private Sub initializeFromTimeZoneInfo(tzi As System.TimeZoneInfo)

            With tzi
                Me.Description = .DisplayName
                Me.ID = .Id
                Me.Offset = .BaseUtcOffset.TotalHours
                Me.DST_Offset = 0
                Me.SerializedTimezoneObject = tzi.ToSerializedString
            End With

            'We should get the time (without DST rule applied) here.
            Dim comparisonDate As Date = Now.ToUniversalTime.AddHours(Me.Offset)


            For Each t In tzi.GetAdjustmentRules

                If t.DateStart <= comparisonDate AndAlso t.DateEnd >= comparisonDate Then

                    Dim year As Integer = comparisonDate.Year

                    Dim sd As Date = New Date(comparisonDate.Year, t.DaylightTransitionStart.Month, t.DaylightTransitionStart.Day, t.DaylightTransitionStart.TimeOfDay.Hour, t.DaylightTransitionStart.TimeOfDay.Minute, t.DaylightTransitionStart.TimeOfDay.Second)
                    Dim ed As Date = New Date(comparisonDate.Year, t.DaylightTransitionEnd.Month, t.DaylightTransitionEnd.Day, t.DaylightTransitionEnd.TimeOfDay.Hour, t.DaylightTransitionEnd.TimeOfDay.Minute, t.DaylightTransitionEnd.TimeOfDay.Second)

                    If sd <= comparisonDate AndAlso ed >= comparisonDate Then

                        Me.DST_Offset = t.DaylightDelta.TotalHours
                    End If
                End If
            Next

            Me.CurrentOffset = Me.Offset + Me.DST_Offset

        End Sub
#End Region

#Region "gets and sets"

        Public Shared Function GetCurrentClientTime() As Date
            Return Now.timezoneToClient
        End Function

        Public Shared Function GettimeZoneFromID(tzID As String) As DataObjects.TimeZone

                Return GetMSoftTZones.Where(Function(x) x.ID = tzID).Single
        End Function

        Public Shared Function GetMSoftTZones() As List(Of DataObjects.TimeZone)

            Return System.TimeZoneInfo.GetSystemTimeZones.Select(Function(x) New DataObjects.TimeZone(x)).ToList

        End Function

        Public Shared Function GetMSftTimeZonesAndCurrentIfGoogle() As List(Of DataObjects.TimeZone)

            Dim retlst = GetMSoftTZones()

            retlst.Insert(0, New DataObjects.TimeZone With {.Description = "Auto-calc from headquarters location"})

            Return retlst

        End Function

        Public Shared Function GetMSftTimeZonesAutoInheritOption() As List(Of DataObjects.TimeZone)

            Dim retlst = GetMSoftTZones()

            retlst.Insert(0, New DataObjects.TimeZone With {.Description = "Use application timezone", .ID = String.Empty})

            Return retlst

        End Function


#End Region

#Region "overrides"
        Public Overrides Function ToString() As String

            Dim str As String = "ID:{1}{0}Description:{2}{0}Offset:{3}{0}DST_Offset:{4}{0}CurrentOffset:{5}{0}"

            Return String.Format(str, vbNewLine, ID, Description, Offset, DST_Offset, CurrentOffset)

        End Function

#End Region

    End Class

End Namespace

