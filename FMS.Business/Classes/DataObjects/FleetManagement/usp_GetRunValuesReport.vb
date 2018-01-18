Namespace DataObjects
    Public Class usp_GetRunValuesReport
#Region "Properties / enums"
        Public Property SiteName As String
        Public Property Add As String
        Public Property AddLastLine As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Single)
        Public Property PerAnnumCharge As System.Nullable(Of Single)
        Public Property ServiceRun As System.Nullable(Of Short)
        Public Property RunDescription As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetRunValuesReport(ServiceRun As String) As List(Of DataObjects.usp_GetRunValuesReport)
            Dim objRunValues = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRunValuesReport(ServiceRun, ThisSession.ApplicationID)
                                Select New DataObjects.usp_GetRunValuesReport(c)).ToList
            Return objRunValues
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunValues As FMS.Business.usp_GetRunValuesReportResult)
            With objRunValues
                Me.SiteName = .SiteName
                Me.Add = .Add
                Me.AddLastLine = .AddLastLine
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.ServiceRun = .ServiceRun
                Me.RunDescription = .RunDescription
                Me.SiteCeaseDate = .SiteCeaseDate
            End With
        End Sub
#End Region
    End Class
End Namespace


