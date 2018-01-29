Imports System.Globalization
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
    End Sub

    Protected Sub SiteDetailsGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Try
            GetSiteDetailsRowUpdatingRowInserting(e, True)
        Catch ex As Exception
            Dim err As String
            err = ex.Message.ToString()
        End Try

    End Sub
    Protected Sub GetSiteDetailsRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
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
            Dim txtTotalAmount As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtTotalAmount"), ASPxTextBox)
            Dim txtTotalUnits As ASPxTextBox = TryCast(SiteDetailsPageControl.FindControl("txtTotalUnits"), ASPxTextBox)
            Dim txtGeneralSiteServiceComments As ASPxMemo = TryCast(SiteDetailsPageControl.FindControl("txtGeneralSiteServiceComments"), ASPxMemo)

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
            e.NewValues("GeneralSiteServiceComments") = txtGeneralSiteServiceComments.Text
            Dim dblTotalAmount As Double = 0
            If Double.TryParse(txtTotalAmount.Text, dblTotalAmount) Then
                e.NewValues("TotalAmount") = dblTotalAmount
            End If
            Dim dblTotalUnits As Double = 0
            If Double.TryParse(txtTotalUnits.Text, dblTotalUnits) Then
                e.NewValues("TotalUnits") = dblTotalUnits
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

    Protected Sub CustomerServiceGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("CId") = hdnSiteCid.Text
        e.Cancel = True
        'GetCustomerServiceRowUpdatingRowInserting(e, False)
        UpdateInsertCustomerServiceRowUpdatingRowInserting(e, False)
    End Sub

    Protected Sub CustomerServiceGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("CId") = hdnSiteCid.Text
        e.Cancel = True
        'GetCustomerServiceRowUpdatingRowInserting(e, True)
        UpdateInsertCustomerServiceRowUpdatingRowInserting(e, True)
    End Sub
    Protected Sub CustomerServiceGridView_CancelRowEditing(sender As Object, e As Data.ASPxStartRowEditingEventArgs)
        FMS.Business.DataObjects.tblCustomerServices.GetCustomerServiceID = Guid.Empty
        e.Cancel = False
    End Sub
    Protected Sub GetCustomerServiceRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        Dim SiteDetailsPageControl As ASPxPageControl = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("SiteDetailsPageControl"), ASPxPageControl)
        Dim CustomerServiceGrid As ASPxGridView = TryCast(SiteDetailsPageControl.FindControl("CustomerServiceGridView"), ASPxGridView)
        Dim cbServices As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServices"), ASPxComboBox)
        Dim cbFrequency As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbFrequency"), ASPxComboBox)
        Dim txtServiceUnits As ASPxTextBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServiceUnits"), ASPxTextBox)
        Dim txtServicePrice As ASPxTextBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServicePrice"), ASPxTextBox)
        Dim txtPerAnnumCharge As ASPxTextBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtPerAnnumCharge"), ASPxTextBox)
        Dim cbServiceRun As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceRun"), ASPxComboBox)
        Dim chkUnitsHaveMoreThanOneRun As ASPxCheckBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("chkUnitsHaveMoreThanOneRun"), ASPxCheckBox)
        Dim cbServiceFrequency1 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency1"), ASPxComboBox)
        Dim cbServiceFrequency2 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency2"), ASPxComboBox)
        Dim cbServiceFrequency3 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency3"), ASPxComboBox)
        Dim cbServiceFrequency4 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency4"), ASPxComboBox)
        Dim cbServiceFrequency5 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency5"), ASPxComboBox)
        Dim cbServiceFrequency6 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency6"), ASPxComboBox)
        Dim cbServiceFrequency7 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency7"), ASPxComboBox)
        Dim cbServiceFrequency8 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency8"), ASPxComboBox)
        Dim txtSortCode As ASPxTextBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtSortCode"), ASPxTextBox)
        Dim txtServiceComments As ASPxMemo = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServiceComments"), ASPxMemo)

        If Not cbServices.SelectedItem Is Nothing Then
            Dim iServices As Nullable(Of Integer) = 0
            iServices = Convert.ToInt16(cbServices.SelectedItem.Value)
            e.NewValues("CSid") = iServices
        End If
        If Not cbFrequency.SelectedItem Is Nothing Then
            Dim iFrequency As Nullable(Of Short) = 0
            iFrequency = Convert.ToInt16(cbFrequency.SelectedItem.Value)
            e.NewValues("ServiceFrequencyCode") = iFrequency
        End If
        Dim ServiceUnits As Single = 0
        If Single.TryParse(txtServiceUnits.Text, ServiceUnits) Then
            e.NewValues("ServiceUnits") = ServiceUnits
        End If
        Dim ServicePrice As Single = 0
        If Single.TryParse(txtServicePrice.Text, ServicePrice) Then
            e.NewValues("ServicePrice") = ServicePrice
        End If
        Dim PerAnnumCharge As Single = 0
        If Single.TryParse(txtPerAnnumCharge.Text, PerAnnumCharge) Then
            e.NewValues("PerAnnumCharge") = PerAnnumCharge
        End If
        If Not cbServiceRun.SelectedItem Is Nothing Then
            Dim iServiceRun As Nullable(Of Short) = 0
            iServiceRun = Convert.ToInt16(cbServiceRun.SelectedItem.Value)
            e.NewValues("ServiceRun") = iServiceRun
        End If
        e.NewValues("UnitsHaveMoreThanOneRun") = chkUnitsHaveMoreThanOneRun.Checked
        If Not cbServiceFrequency1.SelectedItem Is Nothing Then
            Dim iServiceFrequency1 As Nullable(Of Short) = 0
            iServiceFrequency1 = Convert.ToInt16(cbServiceFrequency1.SelectedItem.Value)
            e.NewValues("ServiceFrequency1") = iServiceFrequency1
        End If
        If Not cbServiceFrequency2.SelectedItem Is Nothing Then
            Dim iServiceFrequency2 As Nullable(Of Short) = 0
            iServiceFrequency2 = Convert.ToInt16(cbServiceFrequency2.SelectedItem.Value)
            e.NewValues("ServiceFrequency2") = iServiceFrequency2
        End If
        If Not cbServiceFrequency3.SelectedItem Is Nothing Then
            Dim iServiceFrequency3 As Nullable(Of Short) = 0
            iServiceFrequency3 = Convert.ToInt16(cbServiceFrequency3.SelectedItem.Value)
            e.NewValues("ServiceFrequency3") = iServiceFrequency3
        End If
        If Not cbServiceFrequency4.SelectedItem Is Nothing Then
            Dim iServiceFrequency4 As Nullable(Of Short) = 0
            iServiceFrequency4 = Convert.ToInt16(cbServiceFrequency4.SelectedItem.Value)
            e.NewValues("ServiceFrequency4") = iServiceFrequency4
        End If
        If Not cbServiceFrequency5.SelectedItem Is Nothing Then
            Dim iServiceFrequency5 As Nullable(Of Short) = 0
            iServiceFrequency5 = Convert.ToInt16(cbServiceFrequency5.SelectedItem.Value)
            e.NewValues("ServiceFrequency5") = iServiceFrequency5
        End If
        If Not cbServiceFrequency6.SelectedItem Is Nothing Then
            Dim iServiceFrequency6 As Nullable(Of Short) = 0
            iServiceFrequency6 = Convert.ToInt16(cbServiceFrequency6.SelectedItem.Value)
            e.NewValues("ServiceFrequency6") = iServiceFrequency6
        End If
        If Not cbServiceFrequency7.SelectedItem Is Nothing Then
            Dim iServiceFrequency7 As Nullable(Of Short) = 0
            iServiceFrequency7 = Convert.ToInt16(cbServiceFrequency7.SelectedItem.Value)
            e.NewValues("ServiceFrequency7") = iServiceFrequency7
        End If
        If Not cbServiceFrequency8.SelectedItem Is Nothing Then
            Dim iServiceFrequency8 As Nullable(Of Short) = 0
            iServiceFrequency8 = Convert.ToInt16(cbServiceFrequency8.SelectedItem.Value)
            e.NewValues("ServiceFrequency8") = iServiceFrequency8
        End If
        e.NewValues("ServiceSortOrderCode") = txtSortCode.Text
        e.NewValues("ServiceComments") = txtServiceComments.Text
    End Sub
    Protected Sub UpdateInsertCustomerServiceRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        Dim SiteDetailsPageControl As ASPxPageControl = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("SiteDetailsPageControl"), ASPxPageControl)
        Dim CustomerServiceGrid As ASPxGridView = TryCast(SiteDetailsPageControl.FindControl("CustomerServiceGridView"), ASPxGridView)
        Dim cbServices As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServices"), ASPxComboBox)
        Dim cbFrequency As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbFrequency"), ASPxComboBox)
        Dim txtServiceUnits As ASPxSpinEdit = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServiceUnits"), ASPxSpinEdit)
        Dim txtServicePrice As ASPxSpinEdit = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServicePrice"), ASPxSpinEdit)
        Dim txtPerAnnumCharge As ASPxSpinEdit = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtPerAnnumCharge"), ASPxSpinEdit)
        Dim cbServiceRun As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceRun"), ASPxComboBox)
        Dim chkUnitsHaveMoreThanOneRun As ASPxCheckBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("chkUnitsHaveMoreThanOneRun"), ASPxCheckBox)
        Dim cbServiceFrequency1 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency1"), ASPxComboBox)
        Dim cbServiceFrequency2 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency2"), ASPxComboBox)
        Dim cbServiceFrequency3 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency3"), ASPxComboBox)
        Dim cbServiceFrequency4 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency4"), ASPxComboBox)
        Dim cbServiceFrequency5 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency5"), ASPxComboBox)
        Dim cbServiceFrequency6 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency6"), ASPxComboBox)
        Dim cbServiceFrequency7 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency7"), ASPxComboBox)
        Dim cbServiceFrequency8 As ASPxComboBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("cbServiceFrequency8"), ASPxComboBox)
        Dim txtSortCode As ASPxTextBox = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtSortCode"), ASPxTextBox)
        Dim txtServiceComments As ASPxMemo = TryCast(CustomerServiceGrid.FindEditFormTemplateControl("txtServiceComments"), ASPxMemo)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)

        Dim CustomerServices As New FMS.Business.DataObjects.tblCustomerServices

        If Not cbServices.SelectedItem Is Nothing Then
            Dim iServices As Nullable(Of Integer) = 0
            iServices = Convert.ToInt16(cbServices.SelectedItem.Value)
            CustomerServices.CSid = iServices
        End If
        If Not cbFrequency.SelectedItem Is Nothing Then
            Dim iFrequency As Nullable(Of Short) = 0
            iFrequency = Convert.ToInt16(cbFrequency.SelectedItem.Value)
            CustomerServices.ServiceFrequencyCode = iFrequency
        End If
        Dim ServiceUnits As Single = 0
        If Single.TryParse(txtServiceUnits.Text, ServiceUnits) Then
            CustomerServices.ServiceUnits = ServiceUnits
        End If
        Dim ServicePrice As Single = 0
        If Single.TryParse(txtServicePrice.Text, ServicePrice) Then
            CustomerServices.ServicePrice = ServicePrice
        End If
        Dim PerAnnumCharge As Single = 0
        If Single.TryParse(txtPerAnnumCharge.Text, PerAnnumCharge) Then
            CustomerServices.PerAnnumCharge = PerAnnumCharge
        End If
        If Not cbServiceRun.SelectedItem Is Nothing Then
            Dim iServiceRun As Nullable(Of Short) = 0
            iServiceRun = Convert.ToInt16(cbServiceRun.SelectedItem.Value)
            CustomerServices.ServiceRun = iServiceRun
        End If
        CustomerServices.UnitsHaveMoreThanOneRun = chkUnitsHaveMoreThanOneRun.Checked
        If Not cbServiceFrequency1.SelectedItem Is Nothing Then
            Dim iServiceFrequency1 As Nullable(Of Short) = 0
            iServiceFrequency1 = Convert.ToInt16(cbServiceFrequency1.SelectedItem.Value)
            CustomerServices.ServiceFrequency1 = iServiceFrequency1
        End If
        If Not cbServiceFrequency2.SelectedItem Is Nothing Then
            Dim iServiceFrequency2 As Nullable(Of Short) = 0
            iServiceFrequency2 = Convert.ToInt16(cbServiceFrequency2.SelectedItem.Value)
            CustomerServices.ServiceFrequency2 = iServiceFrequency2
        End If
        If Not cbServiceFrequency3.SelectedItem Is Nothing Then
            Dim iServiceFrequency3 As Nullable(Of Short) = 0
            iServiceFrequency3 = Convert.ToInt16(cbServiceFrequency3.SelectedItem.Value)
            CustomerServices.ServiceFrequency3 = iServiceFrequency3
        End If
        If Not cbServiceFrequency4.SelectedItem Is Nothing Then
            Dim iServiceFrequency4 As Nullable(Of Short) = 0
            iServiceFrequency4 = Convert.ToInt16(cbServiceFrequency4.SelectedItem.Value)
            CustomerServices.ServiceFrequency4 = iServiceFrequency4
        End If
        If Not cbServiceFrequency5.SelectedItem Is Nothing Then
            Dim iServiceFrequency5 As Nullable(Of Short) = 0
            iServiceFrequency5 = Convert.ToInt16(cbServiceFrequency5.SelectedItem.Value)
            CustomerServices.ServiceFrequency5 = iServiceFrequency5
        End If
        If Not cbServiceFrequency6.SelectedItem Is Nothing Then
            Dim iServiceFrequency6 As Nullable(Of Short) = 0
            iServiceFrequency6 = Convert.ToInt16(cbServiceFrequency6.SelectedItem.Value)
            CustomerServices.ServiceFrequency6 = iServiceFrequency6
        End If
        If Not cbServiceFrequency7.SelectedItem Is Nothing Then
            Dim iServiceFrequency7 As Nullable(Of Short) = 0
            iServiceFrequency7 = Convert.ToInt16(cbServiceFrequency7.SelectedItem.Value)
            CustomerServices.ServiceFrequency7 = iServiceFrequency7
        End If
        If Not cbServiceFrequency8.SelectedItem Is Nothing Then
            Dim iServiceFrequency8 As Nullable(Of Short) = 0
            iServiceFrequency8 = Convert.ToInt16(cbServiceFrequency8.SelectedItem.Value)
            CustomerServices.ServiceFrequency8 = iServiceFrequency8
        End If
        CustomerServices.ServiceSortOrderCode = txtSortCode.Text
        CustomerServices.ServiceComments = txtServiceComments.Text

        If blnInserting Then
            If FMS.Business.DataObjects.tblCustomerServices.GetCustomerServiceID.Equals(Guid.Empty) Then
                CustomerServices.CId = hdnSiteCid.Text
                FMS.Business.DataObjects.tblCustomerServices.Create(CustomerServices)

            Else
                CustomerServices.CustomerServiceID = FMS.Business.DataObjects.tblCustomerServices.GetCustomerServiceID
                CustomerServices.CId = hdnSiteCid.Text
                FMS.Business.DataObjects.tblCustomerServices.Update(CustomerServices)
            End If
        Else
            CustomerServices.CustomerServiceID = Guid.Parse(e.Keys.Item(0).ToString())
            CustomerServices.CId = hdnSiteCid.Text
            FMS.Business.DataObjects.tblCustomerServices.Update(CustomerServices)
        End If
    End Sub

    Protected Sub CIRHistoryGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("Cid") = hdnSiteCid.Text
        CIRHistoryGridViewRowUpdatingRowInserting(e, False)
    End Sub

    Protected Sub CIRHistoryGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("Cid") = hdnSiteCid.Text
        CIRHistoryGridViewRowUpdatingRowInserting(e, True)
    End Sub

    Protected Sub CIRHistoryGridViewRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Dim SiteDetailsPageControl As ASPxPageControl = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("SiteDetailsPageControl"), ASPxPageControl)
        Dim CIRHistoryGridView As ASPxGridView = TryCast(SiteDetailsPageControl.FindControl("CIRHistoryGridView"), ASPxGridView)
        Dim dtNCRDate As ASPxDateEdit = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("dtNCRDate"), ASPxDateEdit)
        Dim txtNCRNumber As ASPxSpinEdit = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("txtNCRNumber"), ASPxSpinEdit)
        Dim cbReason As ASPxComboBox = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("cbReason"), ASPxComboBox)
        Dim cbDrivers As ASPxComboBox = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("cbDrivers"), ASPxComboBox)
        Dim txtNCRRecordedBY As ASPxTextBox = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("txtNCRRecordedBY"), ASPxTextBox)
        Dim txtNCRClosedBy As ASPxTextBox = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("txtNCRClosedBy"), ASPxTextBox)
        Dim txtNCRDescription As ASPxMemo = TryCast(CIRHistoryGridView.FindEditFormTemplateControl("txtNCRDescription"), ASPxMemo)
        If Not dtNCRDate.Value Is Nothing Then
            e.NewValues("NCRDate") = dtNCRDate.Value.ToString()
        End If
        Dim NCRNumber As Integer = 0
        If Single.TryParse(txtNCRNumber.Text, NCRNumber) Then
            e.NewValues("NCRNumber") = NCRNumber
        End If
        If Not cbReason.SelectedItem Is Nothing Then
            Dim sNCRReason As Nullable(Of Short) = 0
            sNCRReason = Convert.ToInt16(cbReason.SelectedItem.Value)
            e.NewValues("NCRReason") = sNCRReason
        End If
        If Not cbDrivers.SelectedItem Is Nothing Then
            Dim sDriver As Nullable(Of Short) = 0
            sDriver = Convert.ToInt16(cbDrivers.SelectedItem.Value)
            e.NewValues("Driver") = sDriver
        End If
        e.NewValues("NCRRecordedBY") = txtNCRRecordedBY.Text
        e.NewValues("NCRClosedBy") = txtNCRClosedBy.Text
        e.NewValues("NCRDescription") = txtNCRDescription.Text
    End Sub
    Protected Sub SiteCommentsGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("Cid") = hdnSiteCid.Text
        SiteCommentsGridViewRowUpdatingRowInserting(e, False)
    End Sub

    Protected Sub SiteCommentsGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("Cid") = hdnSiteCid.Text
        SiteCommentsGridViewRowUpdatingRowInserting(e, True)
    End Sub

    Protected Sub SiteCommentsGridViewRowUpdatingRowInserting(e As Object, blnInserting As Boolean)
        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Dim SiteDetailsPageControl As ASPxPageControl = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("SiteDetailsPageControl"), ASPxPageControl)
        Dim SiteCommentsGridView As ASPxGridView = TryCast(SiteDetailsPageControl.FindControl("SiteCommentsGridView"), ASPxGridView)
        Dim dtCommentDate As ASPxDateEdit = TryCast(SiteCommentsGridView.FindEditFormTemplateControl("dtCommentDate"), ASPxDateEdit)
        Dim txtSiteComments As ASPxMemo = TryCast(SiteCommentsGridView.FindEditFormTemplateControl("txtSiteComments"), ASPxMemo)
        If Not dtCommentDate.Value Is Nothing Then
            e.NewValues("CommentDate") = dtCommentDate.Value.ToString()
        End If
        e.NewValues("Comments") = txtSiteComments.Text
    End Sub

#Region "WebMethods"
    <System.Web.Services.WebMethod()>
    Public Shared Function GetSiteInvoicingDetails(ByVal Cid As Integer)
        Dim objSites = FMS.Business.DataObjects.tblSites.GetAllBySiteID(Cid)
        objSites.InvoiceCommencingString = objSites.InvoiceCommencing.ToString()
        Return objSites
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function GetRecalculatedServices(ByVal siteId As Integer)
        Dim objSites = FMS.Business.DataObjects.tblCustomerServices.GetRecalculatedServices(siteId)
        Return objSites
    End Function


#End Region


End Class