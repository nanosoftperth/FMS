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
        Private Const ENDDATETIME_OPTIONS As String = "Now,End of Day,End of Week,End of Year,Specific"
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
        Public Shared Function GetEndDateTimeOptions() As List(Of String)
            Return ENDDATETIME_OPTIONS.Split(","c).ToList
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
        Public Property Recipients As String
        Public Property RecipientEmail As String
        Public Property NativeID As String
        Public Property RecipientName As String
        Public Property StartDateSpecific As String
        Public Property EndDateSpecific As String
        Public Property BusinessLocation As String


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

            Dim objList As New List(Of DataObjects.ReportSchedule)
            Try

                Dim objDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

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
                                     .NativeID = Convert.ToString(item.Recipients),
                                     .Recipients = item.Recipients,
                                     .StartDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.StartDateSpecific)),
                                     .EndDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.EndDateSpecific)),
                                     .BusinessLocation = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() item.BusinessLocation)),
                                     .RecipientName = GetRecipientsforApplication(item.ApplicationId, item.Recipients, GetPropertyName(Function() rpt.RecipientName)),
                                     .RecipientEmail = GetRecipientsforApplication(item.ApplicationId, item.Recipients, GetPropertyName(Function() rpt.RecipientEmail))
                                    })
                    Next
                End If
            Catch ex As Exception 
                ' ErrorLog.WriteErrorLog(ex)
            End Try
          
            Return objList
        End Function
        Public Shared Function GetScheduleForApplication() As List(Of DataObjects.ReportSchedule)

            Dim objList As New List(Of DataObjects.ReportSchedule)
            Try

                Dim objDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)

                Dim rpt As New DataObjects.ReportSchedule

                Dim objResult = (From x In SingletonAccess.FMSDataContextNew.ReportSchdeules _
                            Order By x.DateCreated
                            Select New DataObjects.ReportSchedule(x)).ToList()


                If Not objList Is Nothing Then
                    For Each item In objResult
                        objList.Add(New ReportSchedule() With
                                    {.ApplicationId = item.ApplicationId,
                                     .ReportscheduleID = item.ReportscheduleID,
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
                                     .NativeID = Convert.ToString(item.Recipients),
                                     .Recipients = item.Recipients,
                                     .StartDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.StartDateSpecific)),
                                     .EndDateSpecific = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.EndDateSpecific)),
                                     .BusinessLocation = GetScheduleParameter(item.ReportParams, GetPropertyName(Function() rpt.BusinessLocation)),
                                     .RecipientName = GetRecipientsforApplication(item.ApplicationId, item.Recipients, GetPropertyName(Function() rpt.RecipientName)),
                                     .RecipientEmail = GetRecipientsforApplication(item.ApplicationId, item.Recipients, GetPropertyName(Function() rpt.RecipientEmail))
                                    })
                    Next
                End If
            Catch ex As Exception
                ' ErrorLog.WriteErrorLog(ex)
            End Try

            Return objList
        End Function
        Public Shared Function GetRecipientsforApplication(appid As Guid, NativeIds As String, attributeName As String) As String
            Try 
                Dim RecipientNames As String = String.Empty
                Dim RecipientEmails As String = String.Empty
                Dim RecipientIds = NativeIds.Split(",")

                If RecipientIds.Count > 0 Then

                    For Each Id In RecipientIds
                        Dim Result = (From x In SingletonAccess.FMSDataContextNew.usp_GetSubscribersForApplication(appid) _
                         Where (x.NativeID = New Guid(Id))).FirstOrDefault
                        If attributeName = "RecipientName" Then
                            RecipientNames = RecipientNames + Convert.ToString(Result.SubcriberType + ":" + Result.Name) + ","
                        Else
                            RecipientEmails = RecipientEmails + Convert.ToString(Result.Email) + ","
                        End If
                    Next 
                    If attributeName = "RecipientName" Then
                        If Not String.IsNullOrEmpty(RecipientNames) Then
                            RecipientNames = RecipientNames.Remove(RecipientNames.Length - 1)
                        End If
                        Return RecipientNames
                    Else
                        If Not String.IsNullOrEmpty(RecipientEmails) Then
                            RecipientEmails = RecipientEmails.Remove(RecipientEmails.Length - 1)
                        End If
                        Return RecipientEmails
                    End If
                Else
                    Return "" 
                End If 
            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
                Return ""
            End Try
        End Function
#End Region
        Public Shared Sub insert(rpt As DataObjects.ReportSchedule)

            Try
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
                x.Recipients = ReportParam.NativeID


                If Not Convert.ToString(rpt.ReportType) Is Nothing And Not Convert.ToString(rpt.ReportName) Is Nothing And Not Convert.ToString(ReportParam.StartDate) Is Nothing And Not Convert.ToString(ReportParam.EndDate) Is Nothing Then
                    SingletonAccess.FMSDataContextContignous.ReportSchdeules.InsertOnSubmit(x)
                    SingletonAccess.FMSDataContextContignous.SubmitChanges()
                End If

            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
            End Try 
        End Sub
        Public Shared Sub update(rpt As DataObjects.ReportSchedule)
            Dim uptrptShedule As FMS.Business.ReportSchdeule = SingletonAccess.FMSDataContextContignous.ReportSchdeules.Where(Function(x) x.ReportScheduleID = rpt.ReportscheduleID).Single()
            Try

                With uptrptShedule
                    .Creator = rpt.Creator
                    .ReportName = rpt.ReportName
                    .ReportType = rpt.ReportType
                    .Schedule = rpt.SerializeCustomValues(rpt, rpt.ReportType, "Schedule")
                    .ReportParams = rpt.SerializeCustomValues(rpt, "", "Parm")
                    .Recipients = ReportParam.NativeID
                    .Creator = rpt.Creator
                End With
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
            End Try
        End Sub
        Public Shared Sub delete(rpt As DataObjects.ReportSchedule)
            Try
                Dim rptShedule As FMS.Business.ReportSchdeule = SingletonAccess.FMSDataContextContignous.ReportSchdeules.Where(Function(x) x.ReportScheduleID = rpt.ReportscheduleID).Single()

                SingletonAccess.FMSDataContextContignous.ReportSchdeules.DeleteOnSubmit(rptShedule)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
            End Try 
        End Sub
        Public Function SerializeCustomValues(rptObj As DataObjects.ReportSchedule, reporttype As String, type As String) As String
            Dim result As String = String.Empty
            Try

                Dim obj As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
                If rptObj Is Nothing Then
                    Return result
                Else
                    If type = "Schedule" Then
                        If reporttype = Utility.OneOff Then
                            obj.Add(GetPropertyName(Function() rptObj.ScheduleDate), If(String.IsNullOrEmpty(rptObj.ScheduleDate), "", rptObj.ScheduleDate))
                        ElseIf reporttype = Utility.Daily Then
                            obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), If(String.IsNullOrEmpty(rptObj.ScheduleTime), "", rptObj.ScheduleTime))
                        ElseIf reporttype = Utility.Weekly Then
                            obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), If(String.IsNullOrEmpty(rptObj.ScheduleTime), "", rptObj.ScheduleTime))
                            obj.Add(GetPropertyName(Function() rptObj.DayofWeek), If(String.IsNullOrEmpty(rptObj.DayofWeek), "", rptObj.DayofWeek))
                        ElseIf reporttype = Utility.Monthly Then
                            obj.Add(GetPropertyName(Function() rptObj.ScheduleTime), If(String.IsNullOrEmpty(rptObj.ScheduleTime), "", rptObj.ScheduleTime))
                            obj.Add(GetPropertyName(Function() rptObj.DayofMonth), If(String.IsNullOrEmpty(rptObj.DayofMonth), "", rptObj.DayofMonth))
                        End If
                    ElseIf type = "Parm" Then
                        obj.Add(GetPropertyName(Function() ReportParam.StartDate), If(String.IsNullOrEmpty(ReportParam.StartDate), "", ReportParam.StartDate))
                        If ReportParam.StartDate.Contains("Specific") Then
                            Dim index As Int32 = ReportParam.StartDateSpecific.IndexOf("GMT")
                            If (index > 0) Then
                                ReportParam.StartDateSpecific = ReportParam.StartDateSpecific.Substring(0, index)
                            End If
                            obj.Add(GetPropertyName(Function() ReportParam.StartDateSpecific), If(String.IsNullOrEmpty(ReportParam.StartDateSpecific), "", ReportParam.StartDateSpecific))
                        End If
                        obj.Add(GetPropertyName(Function() ReportParam.EndDate), If(String.IsNullOrEmpty(ReportParam.EndDate), "", ReportParam.EndDate))
                        If ReportParam.EndDate.Contains("Specific") Then
                            Dim index As Int32 = ReportParam.EndDateSpecific.IndexOf("GMT")
                            If (index > 0) Then
                                ReportParam.EndDateSpecific = ReportParam.EndDateSpecific.Substring(0, index)
                            End If
                            obj.Add(GetPropertyName(Function() ReportParam.EndDateSpecific), If(String.IsNullOrEmpty(ReportParam.EndDateSpecific), "", ReportParam.EndDateSpecific))
                        End If
                        If rptObj.ReportName = ReportNameList.ReportGeoFence_byDriver Then
                            obj.Add(GetPropertyName(Function() ReportParam.Driver), If(String.IsNullOrEmpty(ReportParam.Driver), "", ReportParam.Driver))
                        Else
                            obj.Add(GetPropertyName(Function() ReportParam.Vehicle), If(String.IsNullOrEmpty(ReportParam.Vehicle), "", ReportParam.Vehicle))
                        End If
                        obj.Add(GetPropertyName(Function() ReportParam.BusinessLocation), If(String.IsNullOrEmpty(ReportParam.BusinessLocation), "", ReportParam.BusinessLocation))
                    End If

                    Dim ds = New DictionarySerializer(obj)
                    Dim xs = New XmlSerializer(GetType(DictionarySerializer))
                    Dim textWriter = New StringWriter
                    Dim xmlWriter1 = XmlWriter.Create(textWriter)
                    xs.Serialize(xmlWriter1, ds)
                    result = textWriter.ToString
                End If

            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex) 
            End Try 
            Return result
        End Function
        'Public Shared Function GetPropertyName(propertyName As String) As String
        '    Dim MyType As Type = Type.GetType("ReportSchedule")
        '    Dim Mypropertyinfo As PropertyInfo = MyType.GetProperty(GetType ())

        '    Return Mypropertyinfo.Name
        'End Function
        Public Shared Function GetPropertyName(Of T)(ByVal expression As Expressions.Expression(Of Func(Of T))) As String
            Try
                Dim memberExpression As Expressions.MemberExpression = DirectCast(expression.Body, Expressions.MemberExpression)
                Return memberExpression.Member.Name
            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
                Return "" 
            End Try 
        End Function
        Public Shared Function DeserializeCustomValues(ByVal customValuesXml As String, ByVal _type As String, ByVal _reportType As String, ByVal _reportName As String) As String
            Dim returnString As String = String.Empty
            Try
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
                            returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "   " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDateSpecific")) Or Convert.ToString(ds.Dictionary.Item("StartDateSpecific")) = "null"), "", String.Format("{0:MM/dd/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("StartDateSpecific")))) + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDateSpecific")) Or Convert.ToString(ds.Dictionary.Item("EndDateSpecific")) = "null"), "", String.Format("{0:MM/dd/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("EndDateSpecific")))) + Environment.NewLine
                        ElseIf (ds.Dictionary.Item("StartDate") = "Specific") Then
                            If String.IsNullOrEmpty(Convert.ToString(ds.Dictionary.Item("StartDateSpecific"))) Or Convert.ToString(ds.Dictionary.Item("StartDateSpecific")) = "null" Then
                                returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "   " + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + Environment.NewLine
                            Else
                                returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "   " + String.Format("{0:MM/dd/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("StartDateSpecific"))) + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + Environment.NewLine
                            End If
                        ElseIf (ds.Dictionary.Item("EndDate") = "Specific") Then
                               returnString = returnString + "Time Period =" + If((String.IsNullOrEmpty(ds.Dictionary.Item("StartDate")) Or Convert.ToString(ds.Dictionary.Item("StartDate")) = "null"), "", ds.Dictionary.Item("StartDate")) + "  " + "to" + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDate")) Or Convert.ToString(ds.Dictionary.Item("EndDate")) = "null"), "", ds.Dictionary.Item("EndDate")) + "  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("EndDateSpecific")) Or Convert.ToString(ds.Dictionary.Item("EndDateSpecific")) = "null"), "", String.Format("{0:MM/dd/yyyy HH:mm:ss}", Convert.ToDateTime(ds.Dictionary.Item("EndDateSpecific")))) + Environment.NewLine
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
                        If Not Convert.ToString(ds.Dictionary.Item("ScheduleDate")) = "" And Not Convert.ToString(ds.Dictionary.Item("ScheduleDate")) = "01/01/0001 00:00:00" Then
                            returnString = Convert.ToString(_reportType) + ":  " + String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(ds.Dictionary.Item("ScheduleDate"), System.Globalization.CultureInfo.InvariantCulture))
                        Else
                            returnString = Convert.ToString(_reportType)
                        End If
                    ElseIf _reportType = Utility.Daily Then
                        If (Convert.ToString(ds.Dictionary.ContainsKey("ScheduleTime"))) Then
                            If Not Convert.ToString(ds.Dictionary.Item("ScheduleTime")) = "" Then
                                returnString = returnString + Convert.ToString(_reportType) + ":  " + String.Format("{0:t}", Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime"), System.Globalization.CultureInfo.InvariantCulture))
                            Else
                                returnString = returnString + Convert.ToString(_reportType)
                            End If
                        Else
                            returnString = returnString + Convert.ToString(_reportType)
                        End If
                    ElseIf _reportType = Utility.Weekly Then
                        returnString = returnString + Convert.ToString(_reportType) + ":  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("DayofWeek")) Or Convert.ToString(ds.Dictionary.Item("DayofWeek")) = "null"), "", ds.Dictionary.Item("DayofWeek")) + "  " + If(String.IsNullOrEmpty(Convert.ToString(ds.Dictionary.ContainsKey("ScheduleTime")) And Not Convert.ToString(ds.Dictionary.Item("ScheduleTime")) = ""), "", String.Format("{0:t}", Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime"), System.Globalization.CultureInfo.InvariantCulture)))

                    ElseIf _reportType = Utility.Monthly Then
                        returnString = returnString + Convert.ToString(_reportType) + ":  " + If((String.IsNullOrEmpty(ds.Dictionary.Item("DayofMonth")) Or Convert.ToString(ds.Dictionary.Item("DayofMonth")) = "null"), "", ds.Dictionary.Item("DayofMonth")) + "  " + If(String.IsNullOrEmpty(Convert.ToString(ds.Dictionary.ContainsKey("ScheduleTime")) And Not Convert.ToString(ds.Dictionary.Item("ScheduleTime")) = ""), "", String.Format("{0:t}", Convert.ToDateTime(ds.Dictionary.Item("ScheduleTime"), System.Globalization.CultureInfo.InvariantCulture)))
                    Else
                        returnString = String.Empty
                    End If
                End If

            Catch ex As Exception
                'ErrorLog.WriteErrorLog(ex)
            End Try 
             
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
                'ErrorLog.WriteErrorLog(ex)
            End Try  
            Return returnAttribute
        End Function
        Public Shared Function GetData() As List(Of DataObjects.ReportSchedule)

            Return (From x In SingletonAccess.FMSDataContextNew.ReportSchdeules _
                        Order By x.DateCreated
                        Select New DataObjects.ReportSchedule(x)).ToList()

        End Function
    End Class
    Public NotInheritable Class ReportParam
        Public Shared StartDate As String
        Public Shared StartDateSpecific As String
        Public Shared EndDate As String
        Public Shared EndDateSpecific As String
        Public Shared Vehicle As String
        Public Shared Driver As String
        Public Shared BusinessLocation As String
        Public Shared NativeID As String
    End Class
End Namespace