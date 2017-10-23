Imports DevExpress.Web
Public Class SiteDetailsMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub SiteDetailsGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Try
            GetSiteDetailsRowUpdatingRowInserting(e, False)
        Catch ex As Exception
            Dim err As String
            err = ex.Message.ToString()
        End Try

        'GetCustomersRowUpdatingRowInserting(e, False)

    End Sub

    Protected Sub SiteDetailsGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        'GetCustomersRowUpdatingRowInserting(e, True)
        Try
            GetSiteDetailsRowUpdatingRowInserting(e, True)
        Catch ex As Exception
            Dim err As String
            err = ex.Message.ToString()
        End Try

    End Sub
    Protected Sub GetSiteDetailsRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        Try
            Dim SiteDetailsPageControl As ASPxPageControl = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("SiteDetailsPageControl"), ASPxPageControl)
            Dim txtSiteName As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSiteName"), ASPxTextBox)
            Dim txtAddressLine1 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtAddressLine1"), ASPxTextBox)
            Dim txtAddressLine2 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtAddressLine2"), ASPxTextBox)
            Dim txtAddressLine3 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtAddressLine3"), ASPxTextBox)
            Dim txtAddressLine4 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtAddressLine4"), ASPxTextBox)
            Dim txtSuburb As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSuburb"), ASPxTextBox)
            Dim cbState As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbState"), ASPxComboBox)
            Dim txtPCode As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPCode"), ASPxTextBox)
            Dim cbZone As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbZone"), ASPxComboBox)
            Dim cbCustomer As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbCustomer"), ASPxComboBox)
            Dim cbIndustryGroup As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbIndustryGroup"), ASPxComboBox)
            Dim txtContactName As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtContactName"), ASPxTextBox)
            Dim txtSitePhone As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSitePhone"), ASPxTextBox)
            Dim txtSiteFax As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSiteFax"), ASPxTextBox)
            Dim txtSiteContactMobile As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSiteContactMobile"), ASPxTextBox)
            Dim txtSiteEmail As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtSiteEmail"), ASPxTextBox)
            Dim cbPreviousSupplier As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbPreviousSupplier"), ASPxComboBox)
            Dim dtContractStartDate As ASPxDateEdit = TryCast(SiteDetailsPageControl.FindControl("dtContractStartDate"), ASPxDateEdit)
            Dim dtContractExpiryDate As ASPxDateEdit = TryCast(SiteDetailsPageControl.FindControl("dtContractExpiryDate"), ASPxDateEdit)
            Dim cbSalesPerson As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbSalesPerson"), ASPxComboBox)
            Dim cbInitialContractPeriod As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInitialContractPeriod"), ASPxComboBox)
            Dim txtInitialServiceAgreementNo As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtInitialServiceAgreementNo"), ASPxTextBox)
            Dim dtContractCeaseDate As ASPxDateEdit = TryCast(SiteDetailsPageControl.FindControl("dtContractCeaseDate"), ASPxDateEdit)
            Dim cbSiteCeaseReason As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbSiteCeaseReason"), ASPxComboBox)
            Dim cbLostBusinessTo As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbLostBusinessTo"), ASPxComboBox)
            Dim txtPostalAddressLine1 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPostalAddressLine1"), ASPxTextBox)
            Dim txtPostalAddressLine2 As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPostalAddressLine2"), ASPxTextBox)
            Dim txtPostalSuburb As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPostalSuburb"), ASPxTextBox)
            Dim cbPostalState As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbPostalState"), ASPxComboBox)
            Dim txtPostalPostCode As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPostalPostCode"), ASPxTextBox)
            Dim cbInvoiceFrequency As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInvoiceFrequency"), ASPxComboBox)
            Dim dtInvoiceCommencing As ASPxDateEdit = TryCast(SiteDetailsPageControl.FindControl("dtInvoiceCommencing"), ASPxDateEdit)
            Dim chkSeparateInvoice As ASPxCheckBox = TryCast(SiteDetailsPageControl.FindControl("chkSeparateInvoice"), ASPxCheckBox)
            Dim txtPurchaseOrderNumber As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtPurchaseOrderNumber"), ASPxTextBox)
            Dim chkExcludeFuelLevy As ASPxCheckBox = TryCast(SiteDetailsPageControl.FindControl("chkExcludeFuelLevy"), ASPxCheckBox)
            Dim cbRateIncrease As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbRateIncrease"), ASPxComboBox)
            Dim cbInvoiceMonth1 As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInvoiceMonth1"), ASPxComboBox)
            Dim cbInvoiceMonth2 As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInvoiceMonth2"), ASPxComboBox)
            Dim cbInvoiceMonth3 As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInvoiceMonth3"), ASPxComboBox)
            Dim cbInvoiceMonth4 As ASPxComboBox = TryCast(SiteDetailsPageControl.FindControl("cbInvoiceMonth4"), ASPxComboBox)

            e.NewValues("SiteName") = txtSiteName.Text
            e.NewValues("AddressLine1") = txtAddressLine1.Text
            e.NewValues("AddressLine2") = txtAddressLine2.Text
            e.NewValues("AddressLine3") = txtAddressLine3.Text
            e.NewValues("AddressLine4") = txtAddressLine4.Text
            e.NewValues("Suburb") = txtSuburb.Text
            If Not cbState.SelectedItem Is Nothing Then
                e.NewValues("State") = cbState.SelectedItem.Value
            End If
            Dim sPCode As Short
            If Short.TryParse(txtPCode.Text, sPCode) Then
                e.NewValues("PostCode") = sPCode
            End If
            If Not cbZone.SelectedItem Is Nothing Then
                Dim iZone As Nullable(Of Integer) = 0
                iZone = Convert.ToInt16(cbZone.SelectedItem.Value)
                e.NewValues("Zone") = iZone
            End If
            If Not cbCustomer.SelectedItem Is Nothing Then
                Dim sCustomer As Nullable(Of Short) = 0
                sCustomer = Convert.ToInt16(cbCustomer.SelectedItem.Value)
                e.NewValues("Customer") = sCustomer
            End If
            If Not cbIndustryGroup.SelectedItem Is Nothing Then
                Dim sIndustryGroup As Nullable(Of Short) = 0
                sIndustryGroup = Convert.ToInt16(cbIndustryGroup.SelectedItem.Value)
                e.NewValues("IndustryGroup") = sIndustryGroup
            End If
            e.NewValues("SiteContactName") = txtContactName.Text
            e.NewValues("SiteContactPhone") = txtSitePhone.Text
            e.NewValues("SiteContactFax") = txtSiteFax.Text
            e.NewValues("SiteContactMobile") = txtSiteContactMobile.Text
            e.NewValues("SiteContactEmail") = txtSiteEmail.Text
            If Not cbPreviousSupplier.SelectedItem Is Nothing Then
                Dim sPreviousSupplier As Nullable(Of Short) = 0
                sPreviousSupplier = Convert.ToInt16(cbPreviousSupplier.SelectedItem.Value)
                e.NewValues("PreviousSupplier") = sPreviousSupplier
            End If
            If Not dtContractStartDate.Value Is Nothing Then
                e.NewValues("SiteStartDate") = dtContractStartDate.Value.ToString()
            End If
            If Not dtContractExpiryDate.Value Is Nothing Then
                e.NewValues("SiteContractExpiry") = dtContractExpiryDate.Value.ToString()
            End If
            If Not cbSalesPerson.SelectedItem Is Nothing Then
                Dim sSalesPerson As Nullable(Of Short) = 0
                sSalesPerson = Convert.ToInt16(cbSalesPerson.SelectedItem.Value)
                e.NewValues("SalesPerson") = sSalesPerson
            End If
            If Not cbInitialContractPeriod.SelectedItem Is Nothing Then
                Dim iSitePeriod As Nullable(Of Integer) = 0
                iSitePeriod = Convert.ToInt16(cbInitialContractPeriod.SelectedItem.Value)
                e.NewValues("SitePeriod") = iSitePeriod
            End If
            e.NewValues("InitialServiceAgreementNo") = txtInitialServiceAgreementNo.Text
            If Not dtContractCeaseDate.Value Is Nothing Then
                e.NewValues("SiteCeaseDate") = dtContractCeaseDate.Value.ToString()
            End If
            If Not cbSiteCeaseReason.SelectedItem Is Nothing Then
                Dim iSiteCeaseReason As Nullable(Of Integer) = 0
                iSiteCeaseReason = Convert.ToInt16(cbSiteCeaseReason.SelectedItem.Value)
                e.NewValues("SiteCeaseReason") = iSiteCeaseReason
            End If
            If Not cbLostBusinessTo.SelectedItem Is Nothing Then
                Dim sLostBusinessTo As Nullable(Of Short) = 0
                sLostBusinessTo = Convert.ToInt16(cbLostBusinessTo.SelectedItem.Value)
                e.NewValues("LostBusinessTo") = sLostBusinessTo
            End If
            e.NewValues("PostalAddressLine1") = txtPostalAddressLine1.Text
            e.NewValues("PostalAddressLine2") = txtPostalAddressLine2.Text
            e.NewValues("PostalSuburb") = txtPostalSuburb.Text
            If Not cbPostalState.SelectedItem Is Nothing Then
                e.NewValues("PostalState") = cbPostalState.SelectedItem.Value
            End If
            Dim sPostalCode As Short
            If Short.TryParse(txtPostalPostCode.Text, sPostalCode) Then
                e.NewValues("PostalPostCode") = sPostalCode
            End If
            If Not cbInvoiceFrequency.SelectedItem Is Nothing Then
                Dim iInvoiceFrequency As Nullable(Of Integer) = 0
                iInvoiceFrequency = Convert.ToInt16(cbInvoiceFrequency.SelectedItem.Value)
                e.NewValues("InvoiceFrequency") = iInvoiceFrequency
            End If
            If Not dtInvoiceCommencing.Value Is Nothing Then
                e.NewValues("InvoiceCommencing") = dtInvoiceCommencing.Value.ToString()
            End If
            e.NewValues("SeparateInvoice") = chkSeparateInvoice.Checked
            e.NewValues("PurchaseOrderNumber") = txtPurchaseOrderNumber.Text
            e.NewValues("chkSitesExcludeFuelLevy") = chkExcludeFuelLevy.Checked
            If Not cbRateIncrease.SelectedItem Is Nothing Then
                Dim scmbRateIncrease As Nullable(Of Short) = 0
                scmbRateIncrease = Convert.ToInt16(cbRateIncrease.SelectedItem.Value)
                e.NewValues("cmbRateIncrease") = scmbRateIncrease
            End If
            If Not cbInvoiceMonth1.SelectedItem Is Nothing Then
                Dim iInvoiceMonth1 As Nullable(Of Integer) = 0
                iInvoiceMonth1 = Convert.ToInt16(cbInvoiceMonth1.SelectedItem.Value)
                e.NewValues("InvoiceMonth1") = iInvoiceMonth1
            End If
            If Not cbInvoiceMonth2.SelectedItem Is Nothing Then
                Dim iInvoiceMonth2 As Nullable(Of Integer) = 0
                iInvoiceMonth2 = Convert.ToInt16(cbInvoiceMonth2.SelectedItem.Value)
                e.NewValues("InvoiceMonth2") = iInvoiceMonth2
            End If
            If Not cbInvoiceMonth3.SelectedItem Is Nothing Then
                Dim iInvoiceMonth3 As Nullable(Of Integer) = 0
                iInvoiceMonth3 = Convert.ToInt16(cbInvoiceMonth3.SelectedItem.Value)
                e.NewValues("InvoiceMonth3") = iInvoiceMonth3
            End If
            If Not cbInvoiceMonth4.SelectedItem Is Nothing Then
                Dim iInvoiceMonth4 As Nullable(Of Integer) = 0
                iInvoiceMonth4 = Convert.ToInt16(cbInvoiceMonth4.SelectedItem.Value)
                e.NewValues("InvoiceMonth4") = iInvoiceMonth4
            End If
        Catch ex As Exception
            Dim err As String
            err = ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ResignHistoryGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("SiteCId") = hdnSiteCid.Text
    End Sub

    Protected Sub ResignHistoryGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("SiteCId") = hdnSiteCid.Text
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function GetSiteInvoicingDetails(ByVal Cid As Integer)
        Dim objSites = FMS.Business.DataObjects.tblSites.GetAllBySiteID(Cid)
        objSites.InvoiceCommencingString = objSites.InvoiceCommencing.ToString()
        Return objSites
    End Function

    Protected Sub CustomerServiceGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("CId") = hdnSiteCid.Text
    End Sub

    Protected Sub CustomerServiceGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("CId") = hdnSiteCid.Text
    End Sub
End Class