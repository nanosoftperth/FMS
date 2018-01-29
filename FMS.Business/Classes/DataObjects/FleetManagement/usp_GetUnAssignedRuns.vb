Namespace DataObjects

    Public Class usp_GetUnAssignedRuns

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property Rid As Integer
        Public Property DateOfRun As Date
        Public Property RunNUmber As Integer
        Public Property RunDescription As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetUnAssignedRunsResult)
            With obj
                Me.ApplicationId = .ApplicationID
                Me.Rid = .Rid
                Me.DateOfRun = .DateOfRun
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetUnAssignedRuns)
            Dim appId = ThisSession.ApplicationID
            Dim obj = (From r In SingletonAccess.FMSDataContextContignous.usp_GetUnAssignedRuns(appId, StartDate, EndDate)
                       Order By r.RunDescription
                       Select New DataObjects.usp_GetUnAssignedRuns(r)).ToList
            Return obj
        End Function
#End Region

    End Class

End Namespace

