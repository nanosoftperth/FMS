Imports DevExpress.Web

Public Class CustomerDetailsMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CustomersGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        e.NewValues("CustomerName") = GetCustomerName()
    End Sub

    Protected Sub CustomersGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        e.NewValues("CustomerName") = GetCustomerName()
    End Sub
    Protected Function GetCustomerName() As String
        Dim txtCustomerName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerName"), ASPxTextBox)
        Return txtCustomerName.Text
    End Function
    Protected Sub GetCustomersRowUpdatingRowInserting(e As Data.ASPxDataUpdatingEventArgs)
        Dim txtCustomerName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerName"), ASPxTextBox)
        Dim txtViewID As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtViewID"), ASPxTextBox)
        Dim txtAddressLine1 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine1"), ASPxTextBox)
        Dim txtAddressLine2 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine2"), ASPxTextBox)
        Dim txtSuburb As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtSuburb"), ASPxTextBox)
        Dim cbState As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbState"), ASPxComboBox) 'not yet implemented
        Dim txtPCode As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtPCode"), ASPxTextBox)
        Dim dtCustCommencementDate As ASPxDateEdit = TryCast(CustomersGridView.FindEditFormTemplateControl("dtCustCommencementDate"), ASPxDateEdit) 'not yet implemented
        e.NewValues("CustomerName") = txtCustomerName.Text
        e.NewValues("CID") = txtViewID.Text
        e.NewValues("AddressLine1") = txtAddressLine1.Text
        e.NewValues("AddressLine2") = txtAddressLine2.Text
        e.NewValues("Suburb") = txtSuburb.Text
        'for cbState
        e.NewValues("PostCode") = txtPCode.Text
        'for dtCustCommencementDate

    End Sub
End Class