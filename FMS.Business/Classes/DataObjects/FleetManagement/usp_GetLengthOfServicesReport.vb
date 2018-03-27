Namespace DataObjects
    Public Class usp_GetLengthOfServicesReport
#Region "Properties / enums"
        Public Property Years As System.Nullable(Of Integer)
        Public Property sitestartdate As System.Nullable(Of Date)
        Public Property SiteName As String
#End Region
#Region "Get methods"
        Public Shared Function GetLengthOfService(GTYears As Integer) As List(Of DataObjects.usp_GetLengthOfServicesReport)
            Try
                Dim objLengthOfService As New List(Of DataObjects.usp_GetLengthOfServicesReport)
                With New LINQtoSQLClassesDataContext
                    objLengthOfService = (From c In .usp_GetLengthOfServicesReport(GTYears, ThisSession.ApplicationID)
                                          Select New DataObjects.usp_GetLengthOfServicesReport(c)).ToList
                    .Dispose()
                End With
                Return objLengthOfService
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objLengthOfService As FMS.Business.usp_GetLengthOfServicesReportResult)
            With objLengthOfService
                Me.Years = .Years
                Me.sitestartdate = .sitestartdate
                Me.SiteName = .SiteName
            End With
        End Sub
#End Region
    End Class
End Namespace