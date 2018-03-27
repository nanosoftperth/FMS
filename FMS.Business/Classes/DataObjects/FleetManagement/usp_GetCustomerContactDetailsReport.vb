Namespace DataObjects
    Public Class usp_GetCustomerContactDetailsReport
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddressLine3 As String
        Public Property StateDesc As String
        Public Property Suburb As String
        Public Property PostCode As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
        Public Property CustomerMobile As String
        Public Property CustomerFax As String
        Public Property CustomerComments As String
        Public Property CustomerAgentName As String
        Public Property CustomerRating As System.Nullable(Of Short)
        Public Property CustomerRatingDesc As String
#End Region
#Region "Get methods"
        Public Shared Function GetCustomerContactDetailsReport() As List(Of DataObjects.usp_GetCustomerContactDetailsReport)
            Try
                Dim CustomerContactDetails As New List(Of DataObjects.usp_GetCustomerContactDetailsReport)
                With New LINQtoSQLClassesDataContext
                    CustomerContactDetails = (From c In .usp_GetCustomerContactDetailsReport(ThisSession.ApplicationID)
                                              Select New DataObjects.usp_GetCustomerContactDetailsReport(c)).ToList
                    .Dispose()
                End With
                Return CustomerContactDetails
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(CustomerContactDetails As FMS.Business.usp_GetCustomerContactDetailsReportResult)
            With CustomerContactDetails
                Me.CustomerName = .CustomerName
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddressLine3 = .AddressLine3
                Me.StateDesc = .StateDesc
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
                Me.CustomerMobile = .CustomerMobile
                Me.CustomerFax = .CustomerFax
                Me.CustomerComments = .CustomerComments
                Me.CustomerAgentName = .CustomerAgentName
                Me.CustomerRating = .CustomerRating
                Me.CustomerRatingDesc = .CustomerRatingDesc
            End With
        End Sub
#End Region
    End Class
End Namespace


