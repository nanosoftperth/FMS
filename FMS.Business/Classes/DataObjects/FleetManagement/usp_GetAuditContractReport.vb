Namespace DataObjects
    Public Class usp_GetAuditContractReport
#Region "Properties / enums"
        Public Property FieldType As String
        Public Property ChangeDate As System.Nullable(Of Date)
        Public Property Customer As String
        Public Property OldContractCeasedate As System.Nullable(Of Date)
        Public Property NewContractCeasedate As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetAuditContractReport(sDate As Date, eDate As Date) As List(Of DataObjects.usp_GetAuditContractReport)
            Try
                Dim objAuditContract As New List(Of DataObjects.usp_GetAuditContractReport)
                With New LINQtoSQLClassesDataContext
                    objAuditContract = (From c In .usp_GetAuditContractReport(sDate, eDate, ThisSession.ApplicationID)
                                        Select New DataObjects.usp_GetAuditContractReport(c)).ToList
                    .Dispose()
                End With

                Return objAuditContract
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objAuditContract As FMS.Business.usp_GetAuditContractReportResult)
            With objAuditContract
                Me.FieldType = .FieldType
                Me.ChangeDate = .ChangeDate
                Me.Customer = .Customer
                Me.OldContractCeasedate = .OldContractCeasedate
                Me.NewContractCeasedate = .NewContractCeasedate
            End With
        End Sub
#End Region
    End Class
End Namespace

