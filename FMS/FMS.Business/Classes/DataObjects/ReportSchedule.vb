Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml
Imports System.Reflection
Imports System.Linq.Expressions

Namespace DataObjects
    Public Class ReportSchedule

        Private Const REPORT_TYPES As String = "Oneoff,Daily,Weekly,Monthly"
        Private Const DAYS_OF_WEEK As String = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday"
        Private Const DATETIME_OPTIONS As String = "Now,Beginning of Day,Beginning of Week,Beginning of Year,Specific"
#Region "Misc"

        Public Function GetDate(datetime_option As String)

            Select Case datetime_option

                Case "Now" : Return Now
                Case "Beginning of Day" : Return New Date(Now.Year, Now.Month, Now.Day)
                Case "Beginning of Week" : Return Now.StartOfWeek(System.DayOfWeek.Monday)
                Case "Beginning of Month" : Return New Date(Now.Year, Now.Month, 1)
                Case "Beginning of Year" : Return New Date(Now.Year, 1, 1)

                Case Else : Throw New Exception(String.Format("unexpected datetime_option option: {0}", datetime_option))
            End Select

        End Function

        Public Shared Function GetReportTypes() As List(Of String)
            Return REPORT_TYPES.Split(","c).ToList()
        End Function

        Public Shared Function GetDaysOfWeek() As List(Of String)
            Return DAYS_OF_WEEK.Split(","c).ToList()
        End Function

        Public Shared Function GetDateTimeOptions() As List(Of String)
            Return DATETIME_OPTIONS.Split(","c).ToList
        End Function

        Public Shared Function GetMonthDays() As List(Of Integer)

            Dim retobj As New List(Of Integer)

            For i As Integer = 1 To 31
                retobj.Add(i)
            Next

            Return retobj

        End Function

#End Region

#Region "parameters"

        Public Property ReportscheduleID As Guid
        Public Property ApplicationId As Guid
        Public Property ReportName As String
        ''' <summary>
        ''' Daily, weekly etc
        ''' </summary>
        Public Property ReportType As String
        ''' <summary>
        ''' 1 or Wendesday etc
        ''' </summary>
        Public Property ReportTypeSpecific As String
        Public Property ReportTime As Date
        Public Property Enabled As Boolean
        Public Property DateCreated As Date
        Public Property Creator As String
        Public Property ReportParams As String 'for now it is a string anyway
        Public Property SubscriberID As Guid
#Region "Schedule Parameters"
        Public Property Schedule As String ' for insert schedule in XML Fromat   
        Public Property ScheduleDate As String
        Public Property ScheduleTime As String
        Public Property DayofWeek As String
        Public Property DayofMonth As String
        Public Property StartDate As String
        Public Property EndDate As String
        Public Property Vehicle As String
        Public Property Recipients As String
#End Region


#End Region

#Region "constructors"
        Public Sub New()

        End Sub
        Public Sub New(x As Business.ReportSchdeule)
            Me.ReportscheduleID = x.ReportScheduleID
            Me.ApplicationId = x.ApplicationID
            Me.Creator = x.Creator
            Me.DateCreated = x.DateCreated
            Me.Enabled = x.Enabled
            Me.ReportName = x.ReportName
            Me.ReportParams = x.ReportParams
            Me.ReportscheduleID = x.ReportScheduleID
            Me.ReportTime = x.ReportTime
            Me.ReportType = x.ReportType
            Me.ReportTypeSpecific = x.ReportTypeSpecific
            Me.SubscriberID = x.SubscriberID
            Me.Schedule = x.Schedule
            Me.Recipients = x.Recipients
            Me.ScheduleDate = ""
        End Sub

#End Region

#Region "GETS & SETS"


        Public Shared Function GetAllForApplication(appID As Guid) As List(Of DataObjects.ReportSchedule)

            Dim objDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            Dim objList As New List(Of DataObjects.ReportSchedule)

            Dim objResult = (From x In SingletonAccess.FMSDataContextNew.ReportSchdeules _
                     Where x.ApplicationID = appID _
                        Order By x.DateCreated
                        Select New DataObjects.ReportSchedule(x)).ToList()

            If Not objList Is Nothing Then
                For Each item In objResult

                    objList.Add(New ReportSchedule() With
                                {.ReportscheduleID = item.ReportscheduleID,
                                 .ApplicationId = item.ApplicationId,
                                 .ReportName = item.ReportName,
                                 .ReportType = item.ReportType,
                                 .ReportTypeSpecific = item.ReportTypeSpecific,
                                 .Enabled = item.Enabled,
                                 .DateCreated = item.DateCreated,
                                 .Creator = item.Creator,
                                 .ReportParams = DeserializeCustomValues(item.ReportParams, "Parm", ""),
                                 .SubscriberID = item.SubscriberID,
                                 .Schedule = DeserializeCustomValues(item.Schedule, "Schedule", Convert.ToString(item.ReportType)),
                                 .Recipients = item.Recipients
                                })
               
                Next
            End If
            Return objList

        End Function



#End Region
        Public Shared Sub insert(rpt As DataObjects.ReportSchedule)

            Dim x As New FMS.Business.ReportSchdeule

            x.ReportScheduleID = Guid.NewGuid
            x.ApplicationID = rpt.ApplicationId
            x.ReportType = rpt.ReportType 
            x.Enabled = True
            x.Creator = rpt.Creator
            x.DateCreated = DateAndTime.Now
            x.ReportName = rpt.ReportName
            x.ReportParams = ""
            x.ReportTime = DateAndTime.Now
            x.SubscriberID = Guid.NewGuid
            x.Schedule = rpt.SerializeCustomValues(rpt, rpt.ReportType, "Schedule")
            x.ReportParams = "" 'rpt.SerializeCustomValues(rpt, "", "Parm")
            x.Recipients = rpt.Recipients


            SingletonAccess.FMSDataContextContignous.ReportSchdeules.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
        Public Function SerializeCustomValues(rptObj As DataObjects.ReportSchedule, reporttype As String, type As String) As String
            Dim result As String = String.Empty

            Dim obj As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            If rptObj Is Nothing Then
                Return result
            Else
                If type = "Schedule" Then
                    If reporttype = "Oneoff" Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleDate), rptObj.ScheduleDate)
                    ElseIf reporttype = "Daily" Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                    ElseIf reporttype = "Weekly" Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                        obj.Add(GetPropertyName(Function() rptObj.DayofWeek), rptObj.DayofWeek)
                    ElseIf reporttype = "Monthly" Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                        obj.Add(GetPropertyName(Function() rptObj.DayofMonth), rptObj.DayofMonth)
                    End If
                ElseIf type = "Parm" Then
                    obj.Add(GetPropertyName(Function() rptObj.StartDate), rptObj.StartDate)
                    obj.Add(GetPropertyName(Function() rptObj.EndDate), rptObj.EndDate)
                    obj.Add(GetPropertyName(Function() rptObj.Vehicle), rptObj.Vehicle)
                End If

                Dim ds = New DictionarySerializer(obj)
                Dim xs = New XmlSerializer(GetType(DictionarySerializer))
                Dim textWriter = New StringWriter
                Dim xmlWriter1 = XmlWriter.Create(textWriter)
                xs.Serialize(xmlWriter1, ds)
                result = textWriter.ToString
            End If

            Return result
        End Function
        'Public Shared Function GetPropertyName(propertyName As String) As String
        '    Dim MyType As Type = Type.GetType("ReportSchedule")
        '    Dim Mypropertyinfo As PropertyInfo = MyType.GetProperty(GetType ())

        '    Return Mypropertyinfo.Name
        'End Function
        Public Shared Function GetPropertyName(Of T)(ByVal expression As Expressions.Expression(Of Func(Of T))) As String
            Dim memberExpression As Expressions.MemberExpression = DirectCast(expression.Body, Expressions.MemberExpression)
            Return memberExpression.Member.Name
        End Function
        Public Shared Function DeserializeCustomValues(ByVal customValuesXml As String, ByVal _type As String, _reportType As String) As String
            Dim returnString As String = String.Empty

            If String.IsNullOrWhiteSpace(customValuesXml) Then
                Return returnString
            End If

            Dim serializer = New XmlSerializer(GetType(DictionarySerializer))

            Dim textReader = New StringReader(customValuesXml)
            Dim xmlReaderobj = XmlReader.Create(textReader)

            Dim ds = CType(serializer.Deserialize(xmlReaderobj), DictionarySerializer)

            If _type = "Parm" Then
                returnString = String.Empty

                returnString = returnString + "Time  Period =" + Convert.ToString(ds.Dictionary.Item("StartDate")) + "to" + Convert.ToString(ds.Dictionary.Item("EndDate")) + Environment.NewLine

                returnString = returnString + "Vehicle = " + Convert.ToString(ds.Dictionary.Item("Vehicle"))

            ElseIf _type = "Schedule" Then

                returnString = String.Empty

                ' returnString = returnString + Convert.ToString(_reportType) + +"" + String.Format("{0:HH:MM}", If(ds.Dictionary.ContainsKey("ScheduleTime"), Convert.ToString(ds.Dictionary.Item("ScheduleTime")), ""))

                'Dim KeyExist As String = String.Empty

                'If ds.Dictionary.TryGetValue(ds.Dictionary.Item("ScheduleTime"), KeyExist) Then
                '    returnString = returnString + "" + String.Format("{0:HH:MM}", Convert.ToString(ds.Dictionary.Item("ScheduleTime")))
                'End If

            End If

            Return returnString
        End Function

    End Class
    Public Class ReportParam
        Public Property StartDate As String
        Public Property EndDate As String
        Public Property Vehicle As String 
    End Class

End Namespace