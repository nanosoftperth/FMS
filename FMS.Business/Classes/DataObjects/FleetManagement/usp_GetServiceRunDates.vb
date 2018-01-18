Namespace DataObjects

    Public Class usp_GetServiceRunDates

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property Did As Integer
        Public Property DriverName As String
        Public Property DRid As Integer
        Public Property DateOfRun As Date
        Public Property RunNUmber As Integer
        Public Property RunDescription As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetServiceRunDatesResult)
            With obj
                Me.ApplicationId = .ApplicationId
                Me.Did = .Did
                Me.DriverName = .DriverName
                Me.DRid = .DRid
                Me.DateOfRun = .DateOfRun
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication() As List(Of DataObjects.usp_GetServiceRunDates)
            Dim appId = ThisSession.ApplicationID
            Dim obj = (From s In SingletonAccess.FMSDataContextContignous.usp_GetServiceRunDates(appId)
                       Select New DataObjects.usp_GetServiceRunDates(s)).ToList
            Return obj
        End Function
#End Region

    End Class

End Namespace

