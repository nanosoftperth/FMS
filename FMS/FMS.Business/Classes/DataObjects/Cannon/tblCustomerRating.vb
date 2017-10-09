Namespace DataObjects
    Public Class tblCustomerRating
#Region "Properties / enums"
        Public Property CustomerRatingID As System.Guid
        Public Property Rid As Integer
        Public Property CustomerRating As String
        Public Property CustomerRatingDesc As String
        Public Property FromValue As System.Nullable(Of Integer)
        Public Property ToValue As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(CustomerRate As DataObjects.tblCustomerRating)
            Dim objCustomerRating As New FMS.Business.tblCustomerRating
            With objCustomerRating
                .Rid = GetLastIDUsed() + 1
                .CustomerRating = CustomerRate.CustomerRating
                .CustomerRatingDesc = CustomerRate.CustomerRatingDesc
                .FromValue = CustomerRate.FromValue
                .ToValue = CustomerRate.ToValue
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomerRatings.InsertOnSubmit(objCustomerRating)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(CustomerRate As DataObjects.tblCustomerRating)
            Dim objCustomerRating As FMS.Business.tblCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                                           Where c.Rid.Equals(CustomerRate.Rid)).SingleOrDefault
            With objCustomerRating
                .CustomerRating = CustomerRate.CustomerRating
                .CustomerRatingDesc = CustomerRate.CustomerRatingDesc
                .FromValue = CustomerRate.FromValue
                .ToValue = CustomerRate.ToValue
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(CustomerRate As DataObjects.tblCustomerRating)
            Dim objCustomerRating As FMS.Business.tblCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                                         Where c.Rid.Equals(CustomerRate.Rid)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomerRatings.DeleteOnSubmit(objCustomerRating)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                               Order By c.Rid Descending
                               Select New DataObjects.tblCustomerRating(c)).First()
            Return objCustomerRating.Rid
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerRating)
            Dim objCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                     Order By c.CustomerRatingDesc
                                          Select New DataObjects.tblCustomerRating(c)).ToList
            Return objCustomerRating
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblCustomerRating As FMS.Business.tblCustomerRating)
            With objTblCustomerRating
                Me.CustomerRatingID = .CustomerRatingID
                Me.Rid = .Rid
                Me.CustomerRating = .CustomerRating
                Me.CustomerRatingDesc = .CustomerRatingDesc
                Me.FromValue = .FromValue
                Me.ToValue = .ToValue
            End With
        End Sub
#End Region
    End Class
End Namespace

