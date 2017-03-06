Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml
Imports System.Reflection
Imports System.Linq.Expressions

Namespace DataObjects
    Public Class ReportSchedule

        Private Const REPORT_TYPES As String = "OneOff,Daily,Weekly,Monthly"
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
        Public Property Driver As String
        Public Property Recipients As Guid

        Public Property NativeID As String
        Public Property RecipientName As String
        Public Property StartDateSpecific As String
        Public Property EndDateSpecific As String
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

            Dim objList1 As New List(Of DataObjects.ReportSchedule)

            Dim objDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            Dim objList As New List(Of DataObjects.ReportSchedule)
            Dim rpt As New DataObjects.ReportSchedule

            Dim objResult = (From x In SingletonAccess.FMSDataContextNew.ReportSchdeules _
                     Where x.ApplicationID = appID _
                        Order By x.DateCreated
                        Select New DataObjects.ReportSchedule(x)).ToList()

            If Not objList Is Nothing Then
                For Each item In objResult
                    objList.Add(New ReportSchedule() With
                                {.ReportscheduleID = item.ReportscheduleID,
                                 .ReportName = item.ReportName,
                                 .ReportType = item.ReportType,
                                 .ReportTypeSpecific = item.ReportTypeSpecific,
                                 .Enabled = item.Enabled,
                                 .DateCreated = item.DateCreated,
                                 .Creator = item.Creator,
                                 .ReportParams = DeserializeCustomValues(item.ReportParams, "Parm", "", Convert.ToString(item.ReportName)),
                                 .SubscriberID = item.SubscriberID,
                                 .Schedule = DeserializeCustomValues(item.Schedule, "Schedule", Convert.ToString(item.ReportType), ""),
                                 .ScheduleDate = GetScheduleParameter(item.Schedule, GetPropertyName(Function() rpt.ScheduleDate)),
                                 .ScheduleTime = GetScheduleParameter(item.Schedule, GetPropertyName(Function() rpt.ScheduleTime)),
                                 .DayofWeek = GetScheduleParameter(item.Schedule, GetPropertyName(Function() rpt.DayofWeek)),
                                 .DayofMonth = GetScheduleParameter(item.Schedule, GetPropertyName(Function() rpt.DayofMonth)),
                                 .StartDate = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.StartDate)),
                                 .EndDate = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.EndDate)),
                                 .Vehicle = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.Vehicle)),
                                 .Driver = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.Driver)),
                                 .RecipientName = GetRecipientsforApplication(item.ApplicationId, item.Recipients),
                                 .NativeID = Convert.ToString(item.Recipients),
                                 .Recipients = item.Recipients,
                                 .StartDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.StartDateSpecific)),
                    .EndDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.EndDateSpecific))
                                })
                Next
            End If
            Return objList
        End Function
        Public Shared Function GetRecipientsforApplication(appid As Guid, NativeId As Guid) As String
            Dim Result = (From x In SingletonAccess.FMSDataContextNew.usp_GetSubscribersForApplication(appid) _
                    Where (x.NativeID = NativeId)).FirstOrDefault

            If Result Is Nothing Then
                Return String.Empty
            Else
                Return Convert.ToString(Result.SubcriberType + ":" + Result.Name)
            End If  
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
            x.ReportParams = rpt.SerializeCustomValues(rpt, "", "Parm")
            x.Recipients = rpt.Recipients

            SingletonAccess.FMSDataContextContignous.ReportSchdeules.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
        Public Shared Sub update(rpt As DataObjects.ReportSchedule)
            Dim uptrptShedule As FMS.Business.ReportSchdeule = SingletonAccess.FMSDataContextContignous.ReportSchdeules.Where(Function(x) x.ReportScheduleID = rpt.ReportscheduleID).Single()

            With uptrptShedule
                .Creator = rpt.Creator
                .ReportName = rpt.ReportName
                .ReportType = rpt.ReportType
                .Schedule = rpt.SerializeCustomValues(rpt, rpt.ReportType, "Schedule")
                .ReportParams = rpt.SerializeCustomValues(rpt, "", "Parm")
                .Recipients = rpt.Recipients
                .Creator = rpt.Creator
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub delete(rpt As DataObjects.ReportSchedule)
            Dim rptShedule As FMS.Business.ReportSchdeule = SingletonAccess.FMSDataContextContignous.ReportSchdeules.Where(Function(x) x.ReportScheduleID = rpt.ReportscheduleID).Single()

            SingletonAccess.FMSDataContextContignous.ReportSchdeules.DeleteOnSubmit(rptShedule)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Function SerializeCustomValues(rptObj As DataObjects.ReportSchedule, reporttype As String, type As String) As String
            Dim result As String = String.Empty

            Dim obj As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            If rptObj Is Nothing Then
                Return result
            Else
                If type = "Schedule" Then
                    If reporttype = Utility.OneOff Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleDate), rptObj.ScheduleDate)
                    ElseIf reporttype = Utility.Daily Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                    ElseIf reporttype = Utility.Weekly Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                        obj.Add(GetPropertyName(Function() rptObj.DayofWeek), rptObj.DayofWeek)
                    ElseIf reporttype = Utility.Monthly Then
                        obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), rptObj.ScheduleTime)
                        obj.Add(GetPropertyName(Function() rptObj.DayofMonth), rptObj.DayofMonth)
                    End If
                ElseIf type = "Parm" Then
                    obj.Add(GetPropertyName(Function() ReportParam.StartDate), ReportParam.StartDate)
                    If ReportParam.StartDate.Contains("Specific") Then
                        obj.Add(GetPropertyName(Function() ReportParam.StartDateSpecific), ReportParam.StartDateSpecific.Replace("GMT+0800 (W. Australia Standard Time)", ""))
                    End If
                    obj.Add(GetPropertyName(Function() ReportParam.EndDate), ReportParam.EndDate)
                    If ReportParam.EndDate.Contains("Specific") Then
                        obj.Add(GetPropertyName(Function() ReportParam.EndDateSpecific), ReportParam.EndDateSpecific.Replace("GMT+0800 (W. Australia Standard Time)", ""))
                    End If
                    If rptObj.ReportName = ReportNameList.ReportGeoFence_byDriver Then
                        obj.Add(GetPropertyName(Function() ReportParam.Driver), ReportParam.Driver)
                    Else
                        obj.Add(GetPropertyName(Function() ReportParam.Vehicle), ReportParam.Vehicle)
                    End If
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
        Public Shared Function DeserializeCustomValues(ByVal customValuesXml As String, ByVal _type As String, ByVal _reportType As String, ByVal _reportName As String) As String
            Dim returnString As String = String.Empty

            If String.IsNullOrWhiteSpace(customValuesXml) Then
                Return returnString
            End If
            Dim serializer = New XmlSerializer(GetType(DictionarySerializer))

            Dim textReader = New StringReader(customValuesXml)
            Dim xmlReaderobj = XmlReader.Create(textReader)

            Dim ds = CType(serializer.Deserialize(xmlReaderobj), DictionarySerializer)

            If _type = "Parm" Then
                If ((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null") And (String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null")) Then
                    returnString = String.Empty
                Else
                    If (ds.Dictionary.Item("StartDate") = "Specific") And (ds.Dictionary.Item("EndDate") = "Specific") Then
                        returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "   " + String.Format("{0:d/M/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("StartDateSpecific"))) + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + String.Format("{0:d/M/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("EndDateSpecific"))) + Environment.NewLine
                    ElseIf (ds.Dictionary.Item("StartDate") = "Specific") Then
                        returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "   " + String.Format("{0:d/M/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("StartDateSpecific"))) + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + Environment.NewLine
                    ElseIf (ds.Dictionary.Item("EndDate") = "Specific") Then
                        returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "  " + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + "  " + String.Format("{0:d/M/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("EndDateSpecific"))) + Environment.NewLine
                    Else
                        returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "  " + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + Environment.NewLine
                    End If
                    If _reportName = ReportNameList.ReportGeoFence_byDriver Then
                        returnString = returnString + "Driver = " + If((String.IsNullOrEmpty(ds.Dictionary.Item("Driver")) Or Convert.ToString(ds.Dictionary.Item("Driver")) = "null"), "", ds.Dictionary.Item("Driver"))
                    Else
                        returnString = returnString + "Vehicle = " + If((String.IsNullOrEmpty(ds.Dictionary.Item("Vehicle")) Or Convert.ToString(ds.Dictionary.Item("Vehicle")) = "null"), "", ds.Dictionary.Item("Vehicle"))
                    End If
                End If
            ElseIf _type = "Schedule" Then
                If _reportType = Utility.OneOff Then
                    returnString = Convert.ToString(_reportType) + ":  " + String.Format("{0:d/M/yyyy HH:mm:ss}", If(ds.Dictionary.ContainsKey("ScheduleDate"), Convert.ToDateTime(ds.Dictionary.Item("ScheduleDate")), ""))

                ElseIf _reportType = Utility.Daily Then
                    returnString = returnString + Convert.ToString(_reportType) + ":  " + String.Format("{0:t}", If(ds.Dictionary.ContainsKey("ScheduleTime"), Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime")), ""))

                ElseIf _reportType = Utility.Weekly Then
                    returnString = returnString + Convert.ToString(_reportType) + ":  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("DayofWeek")) Or Convert.ToString(ds.Dictionary.Item("DayofWeek")) = "null"), "", ds.Dictionary.Item("DayofWeek")) + "  " + String.Format("{0:t}", If(ds.Dictionary.ContainsKey("ScheduleTime"), Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime")), ""))

                ElseIf _reportType = Utility.Monthly Then
                    returnString = returnString + Convert.ToString(_reportType) + ":  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("DayofMonth")) Or Convert.ToString(ds.Dictionary.Item("DayofMonth")) = "null"), "", ds.Dictionary.Item("DayofMonth")) + "  " + String.Format("{0:t}", If(ds.Dictionary.ContainsKey("ScheduleTime"), Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime")), ""))
                Else
                    returnString = String.Empty
                End If
            End If
            Return returnString
        End Function
        Public Shared Function GetScheduleParameter(ByVal customValuesXml As String, ByVal attributeName As String) As String
            Dim rptObj As New DataObjects.ReportSchedule
            Dim returnAttribute As String = String.Empty
            Try
                If String.IsNullOrWhiteSpace(customValuesXml) Then
                    Return returnAttribute
                End If
                Dim serializer = New XmlSerializer(GetType(DictionarySerializer))
                Dim textReader = New StringReader(customValuesXml)
                Dim xmlReaderobj = XmlReader.Create(textReader)

                Dim ds = CType(serializer.Deserialize(xmlReaderobj), DictionarySerializer)

                If ds.Dictionary.ContainsKey(attributeName) Then
                    Return Convert.ToString(ds.Dictionary.Item(attributeName))
                Else
                    If attributeName = GetPropertyName(Function() rptObj.ScheduleTime) Or attributeName = GetPropertyName(Function() rptObj.ScheduleDate) Then
                        Return ("0001/01/01 00:00:00")
                    End If
                    Return returnAttribute
                End If
            Catch ex As Exception
            End Try
            Return returnAttribute
        End Function
    End Class
    Public NotInheritable Class ReportParam
        Public Shared StartDate As String
        Public Shared StartDateSpecific As String
        Public Shared EndDate As String
        Public Shared EndDateSpecific As String
        Public Shared Vehicle As String
        Public Shared Driver As String
    End Class
End Namespace