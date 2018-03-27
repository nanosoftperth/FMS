Namespace DataObjects
    Public Class usp_GetCustomerUpdateValue
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property TotalAmount As System.Nullable(Of Double)
        Public Property Cid As System.Nullable(Of Integer)
#End Region
#Region "Get methods"
        Public Shared Function GetCustomerUpdateValue(cid As Integer) As DataObjects.usp_GetCustomerUpdateValue
            Try
                Dim objGetCustomerUpdateValue As New DataObjects.usp_GetCustomerUpdateValue
                With New LINQtoSQLClassesDataContext
                    objGetCustomerUpdateValue = (From c In .usp_GetCustomerUpdateValue(cid)
                                                 Select New DataObjects.usp_GetCustomerUpdateValue(c)).SingleOrDefault
                    .Dispose()
                End With
                Return objGetCustomerUpdateValue
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGetCustomerUpdateValue As FMS.Business.usp_GetCustomerUpdateValueResult)
            With objGetCustomerUpdateValue
                Me.Cid = .Cid
                Me.CustomerName = .CustomerName
                Me.TotalAmount = .TotalAmount
            End With
        End Sub
#End Region
    End Class
End Namespace

