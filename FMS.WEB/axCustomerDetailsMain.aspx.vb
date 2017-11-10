Imports DevExpress.Web

Public Class axCustomerDetailsMain
    Inherits System.Web.UI.Page

    Protected Sub CustomersGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        GetCustomersRowUpdatingRowInserting(e, False)
    End Sub
    Protected Sub CustomersGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        GetCustomersRowUpdatingRowInserting(e, True)
    End Sub
    Protected Sub GetCustomersRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        Dim txtCustomerID As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerID"), ASPxTextBox)
        Dim txtCustomerName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerName"), ASPxTextBox)
        Dim txtViewID As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtViewID"), ASPxTextBox)
        Dim txtAddressLine1 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine1"), ASPxTextBox)
        Dim txtAddressLine2 As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtAddressLine2"), ASPxTextBox)
        Dim txtSuburb As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtSuburb"), ASPxTextBox)
        Dim cbState As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbState"), ASPxComboBox)
        Dim txtPCode As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtPCode"), ASPxTextBox)
        Dim dtCustCommencementDate As ASPxDateEdit = TryCast(CustomersGridView.FindEditFormTemplateControl("dtCustCommencementDate"), ASPxDateEdit)
        Dim cbCustomerRating As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbCustomerRating"), ASPxComboBox)
        Dim cbZone As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbZone"), ASPxComboBox)
        Dim txtCustomerContactName As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerContactName"), ASPxTextBox)
        Dim txtPerAnnumValue As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtPerAnnumValue"), ASPxTextBox)
        Dim txtCustomerPhone As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerPhone"), ASPxTextBox)
        Dim txtCustomerMobile As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerMobile"), ASPxTextBox)
        Dim txtCustomerFax As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerFax"), ASPxTextBox)
        Dim txtCustomerComments As ASPxMemo = TryCast(CustomersGridView.FindEditFormTemplateControl("txtCustomerComments"), ASPxMemo)
        Dim cbCustomerAgentName As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbCustomerAgentName"), ASPxComboBox)
        Dim txtMYOBCustomerNumber As ASPxTextBox = TryCast(CustomersGridView.FindEditFormTemplateControl("txtMYOBCustomerNumber"), ASPxTextBox)
        Dim chkInActiveCustomer As ASPxCheckBox = TryCast(CustomersGridView.FindEditFormTemplateControl("chkInActiveCustomer"), ASPxCheckBox)
        Dim chkExcludeFuelLevy As ASPxCheckBox = TryCast(CustomersGridView.FindEditFormTemplateControl("chkExcludeFuelLevy"), ASPxCheckBox)
        Dim cbRateIncrease As ASPxComboBox = TryCast(CustomersGridView.FindEditFormTemplateControl("cbRateIncrease"), ASPxComboBox)

        If blnInserting = False Then
            e.NewValues("CustomerID") = txtCustomerID.Text
            e.NewValues("CID") = txtViewID.Text
        End If

        e.NewValues("CustomerName") = txtCustomerName.Text
        e.NewValues("AddressLine1") = txtAddressLine1.Text
        e.NewValues("AddressLine2") = txtAddressLine2.Text
        e.NewValues("Suburb") = txtSuburb.Text

        Dim iState As Nullable(Of Integer) = 0
        If Not cbState.SelectedItem Is Nothing Then
            iState = Convert.ToInt16(cbState.SelectedItem.Value)
            e.NewValues("State") = iState
        End If

        e.NewValues("PostCode") = txtPCode.Text
        If Not dtCustCommencementDate.Value Is Nothing Then
            e.NewValues("CustomerCommencementDate") = dtCustCommencementDate.Value.ToString()
        End If

        Dim sRating As Nullable(Of Short) = 0
        If Not cbCustomerRating.SelectedItem Is Nothing Then
            sRating = Convert.ToInt16(cbCustomerRating.SelectedItem.Value)
            e.NewValues("CustomerRating") = sRating
        End If

        Dim iZone As Nullable(Of Integer) = 0
        If Not cbZone.SelectedItem Is Nothing Then
            iZone = Convert.ToInt16(cbZone.SelectedItem.Value)
            e.NewValues("Zone") = iZone
        End If

        e.NewValues("CustomerContactName") = txtCustomerContactName.Text
        Dim dblCustomerValue As System.Nullable(Of Double) = 0
        If txtPerAnnumValue.Text.Equals("") Then
            dblCustomerValue = 0
        Else
            dblCustomerValue = Convert.ToDouble(txtPerAnnumValue.Text)
        End If
        e.NewValues("CustomerValue") = dblCustomerValue
        e.NewValues("CustomerPhone") = txtCustomerPhone.Text
        e.NewValues("CustomerMobile") = txtCustomerMobile.Text
        e.NewValues("CustomerFax") = txtCustomerFax.Text
        e.NewValues("CustomerComments") = txtCustomerComments.Text

        Dim iCustomerAgent As Nullable(Of Integer) = 0
        If Not cbCustomerAgentName.SelectedItem Is Nothing Then
            iCustomerAgent = Convert.ToInt16(cbCustomerAgentName.SelectedItem.Value)
            e.NewValues("CustomerAgent") = iCustomerAgent
        End If

        e.NewValues("MYOBCustomerNumber") = txtMYOBCustomerNumber.Text
        e.NewValues("InactiveCustomer") = chkInActiveCustomer.Checked
        e.NewValues("chkCustomerExcludeFuelLevy") = chkExcludeFuelLevy.Checked
        Dim icmbRateIncrease As Nullable(Of Short) = 0
        If Not cbRateIncrease.SelectedItem Is Nothing Then
            icmbRateIncrease = Convert.ToInt16(cbRateIncrease.SelectedItem.Value)
            e.NewValues("cmbRateIncrease") = icmbRateIncrease
        End If
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateValue(ByVal Cid As Integer)
        Dim objCustUpdateValue = FMS.Business.DataObjects.usp_GetCustomerUpdateValue.GetCustomerUpdateValue(Cid)

        Return objCustUpdateValue
    End Function
End Class