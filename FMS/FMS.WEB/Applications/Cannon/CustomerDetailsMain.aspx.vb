Imports DevExpress.Web

Public Class CustomerDetailsMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CustomersGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        'e.NewValues("CustomerName") = GetCustomerName()
        GetCustomersRowUpdatingRowInserting(e)
    End Sub

    Protected Sub CustomersGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        'e.NewValues("CustomerName") = GetCustomerName()
        GetCustomersRowUpdatingRowInserting(e)
    End Sub
    'Protected Function GetCustomerName() As String
    '    Dim txtCustomerName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerName"), ASPxTextBox)
    '    Return txtCustomerName.Text
    'End Function
    Protected Sub GetCustomersRowUpdatingRowInserting(e As Object)
        Dim txtCustomerID As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerID"), ASPxTextBox)
        Dim txtCustomerName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerName"), ASPxTextBox)
        Dim txtViewID As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtViewID"), ASPxTextBox)
        Dim txtAddressLine1 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine1"), ASPxTextBox)
        Dim txtAddressLine2 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine2"), ASPxTextBox)
        Dim txtSuburb As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtSuburb"), ASPxTextBox)
        Dim cbState As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbState"), ASPxComboBox) 'not yet implemented
        Dim txtPCode As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtPCode"), ASPxTextBox)
        Dim dtCustCommencementDate As ASPxDateEdit = TryCast(CustomersGridView.FindEditFormTemplateControl("dtCustCommencementDate"), ASPxDateEdit) 'not yet implemented
        Dim cbCustomerRating As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbCustomerRating"), ASPxComboBox) 'not yet implemented
        Dim cbZone As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbZone"), ASPxComboBox) 'not yet implemented
        Dim txtCustomerContactName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerContactName"), ASPxTextBox)
        Dim txtPerAnnumValue As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtPerAnnumValue"), ASPxTextBox)
        Dim txtCustomerPhone As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerPhone"), ASPxTextBox)
        Dim txtCustomerMobile As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerMobile"), ASPxTextBox)
        Dim txtCustomerFax As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerFax"), ASPxTextBox)
        Dim txtCustomerComments As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerComments"), ASPxTextBox)
        Dim cbCustomerAgentName As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbCustomerAgentName"), ASPxComboBox) 'not yet implemented
        Dim txtMYOBCustomerNumber As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtMYOBCustomerNumber"), ASPxTextBox)
        Dim chkInActiveCustomer As ASPxCheckBox = TryCast(CustomersGridView.FindEditFormTemplateControl("chkInActiveCustomer"), ASPxCheckBox)
        Dim chkExcludeFuelLevy As ASPxCheckBox = TryCast(CustomersGridView.FindEditFormTemplateControl("chkExcludeFuelLevy"), ASPxCheckBox)
        Dim cbRateIncrease As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbRateIncrease"), ASPxComboBox) 'not yet implemented

        e.NewValues("CustomerID") = txtCustomerID.Text
        e.NewValues("CustomerName") = txtCustomerName.Text
        e.NewValues("CID") = txtViewID.Text
        e.NewValues("AddressLine1") = txtAddressLine1.Text
        e.NewValues("AddressLine2") = txtAddressLine2.Text
        e.NewValues("Suburb") = txtSuburb.Text
        Dim iState As Integer = Convert.ToInt16(cbState.SelectedItem.Value)
        e.NewValues("State") = iState
        e.NewValues("PostCode") = txtPCode.Text
        'for dtCustCommencementDate
        Dim sRating As Short = Convert.ToInt16(cbCustomerRating.SelectedItem.Value)
        e.NewValues("CustomerRating") = sRating
        'for cbZone
        e.NewValues("CustomerContactName") = txtCustomerContactName.Text
        e.NewValues("CustomerValue") = txtPerAnnumValue.Text
        e.NewValues("CustomerPhone") = txtCustomerPhone.Text
        e.NewValues("CustomerMobile") = txtCustomerMobile.Text
        e.NewValues("CustomerFax") = txtCustomerFax.Text
        e.NewValues("CustomerComments") = txtCustomerComments.Text
        'for cbCustomerAgentName
        e.NewValues("MYOBCustomerNumber") = txtMYOBCustomerNumber.Text
        e.NewValues("InactiveCustomer") = chkInActiveCustomer.Checked
        e.NewValues("chkCustomerExcludeFuelLevy") = chkExcludeFuelLevy.Checked
        'for cbRateIncrease
    End Sub
End Class