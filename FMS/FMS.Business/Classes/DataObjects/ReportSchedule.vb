Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml
Namespace DataObjects
    Public Class ReportSchedule

        Private Const REPORT_TYPES As String = "One-off,Daily,Weekly,Monthly"
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
        End Sub

#End Region

#Region "GETS & SETS"


        Public Shared Function GetAllForApplication(appID As Guid) As List(Of DataObjects.ReportSchedule)

            Return (From x In SingletonAccess.FMSDataContextNew.ReportSchdeules _
                     Where x.ApplicationID = appID _
                        Order By x.DateCreated
                        Select New DataObjects.ReportSchedule(x)).ToList()

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
            'x.Schedule = rpt.Schedule 


            SingletonAccess.FMSDataContextContignous.ReportSchdeules.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
        Public Shared Function SerializeCustomValues(rptObj As DataObjects.ReportSchedule) As String
            Dim result As String = String.Empty

            If rptObj Is Nothing Then
                Dim obj As Dictionary(Of Object, Object) = New Dictionary(Of Object, Object)

                obj.Add("Report", "Daily")

 



            End If
 

            'Dim ds = New DictionarySerializer(request)
            'Dim xs = New XmlSerializer(GetType(DictionarySerializer))
            'Dim textWriter = New StringWriter
            'Dim xmlWriter = XmlWriter.Create(textWriter)
            'xs.Serialize(xmlWriter, ds)
            'Dim result = textWriter.ToString
            'Return result
            Return result
        End Function

    End Class
  
End Namespace