
Namespace DataObjects


    Public Class ReportSchedule

        Private Const REPORT_TYPES As String = "One-off,Daily,Weekly,Monthly"
        Private Const DAYS_OF_WEEK As String = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday"

#Region "Misc"

        Public Shared Function GetReportTypes() As List(Of String)
            Return REPORT_TYPES.Split(","c).ToList()
        End Function

        Public Shared Function GetDaysOfWeek() As List(Of String)
            Return DAYS_OF_WEEK.Split(","c).ToList()
        End Function

        Public Shared Function GetMonthDays() As List(Of Integer)

            Dim retobj As New List(Of Integer)

            For i As Integer = 0 To 1
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
        Public Property SunbscriberID As Guid

#End Region

#Region "constructors"

        Public Sub New(x As Business.ReportSchdeule)

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
            Me.SunbscriberID = x.SubscriberID

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
      

    End Class


End Namespace