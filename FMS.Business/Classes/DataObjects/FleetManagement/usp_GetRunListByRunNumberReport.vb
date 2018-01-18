Namespace DataObjects
    Public Class usp_GetRunListByRunNumberReport
#Region "Properties / enums"
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunNum As String
        Public Property RunNo As String
        Public Property RunDescription As String
        Public Property RunDriver As System.Nullable(Of Short)
        Public Property DriverName As String
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
        Public Property Rid As Integer
        Public Property DateOfRun As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetRunListingReport() As List(Of DataObjects.usp_GetRunListByRunNumberReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objRunListing = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRunListByRunNumberReport(ThisSession.ApplicationID)
                                 Select New DataObjects.usp_GetRunListByRunNumberReport(c)).ToList
            Return objRunListing
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunListingReport As FMS.Business.usp_GetRunListByRunNumberReportResult)
            With objRunListingReport
                Me.RunNum = .RunNUmber
                Me.RunNum = .RunNum
                Me.RunNo = .RunNo
                Me.RunDescription = .RunDescription
                Me.RunDriver = .RunDriver
                Me.DriverName = .DriverName
                Me.MondayRun = .MondayRun
                Me.TuesdayRun = .TuesdayRun
                Me.WednesdayRun = .WednesdayRun
                Me.ThursdayRun = .ThursdayRun
                Me.FridayRun = .FridayRun
                Me.SaturdayRun = .SaturdayRun
                Me.SundayRun = .SundayRun
                Me.InactiveRun = .InactiveRun
                Me.Rid = .Rid
            End With
        End Sub
#End Region
    End Class
End Namespace

