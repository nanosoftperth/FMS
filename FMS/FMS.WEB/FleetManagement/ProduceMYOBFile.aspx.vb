Imports DevExpress
Imports DevExpress.Web
Imports DevExpress.Web.ASPxWebControl

Public Class ProduceMYOBFile
    Inherits System.Web.UI.Page
    Private priMYOBFilename As String

#Region "Page Variables"

    Dim pgeInvoiceFrequency As String
    Dim pgeNumberOfWeeks As Integer
    Dim pgeInvoiceFrequencyId As String
    Dim pgeServiceDescription As String
    Dim pgeMYOBCustomerNo As String
    Dim pgeSiteId As Integer
    Dim pgeSeparateInvoice As Boolean
    Dim pgeAnnualPriceExGST As Double
    Dim pgeAnnualPriceIncGST As Double
    Dim pgeInvoiceAmountExGST As Double
    Dim pgeInvoiceAmountIncGST As Double
    Dim pgeInvoiceGSTAmount As Double
    Dim pgeLastMYOBCustomerNo As String = "0000"
    Dim pgeExcludeCustomerFuel As Boolean

    Dim pgeTotalAnnualPriceExGST As Double = 0.0
    Dim pgeTotalAnnualPriceIncGST As Double = 0.0
    Dim pgeTotalInvoiceAmountExGST As Double = 0.0
    Dim pgeTotalInvoiceAmountIncGST As Double = 0.0
    Dim pgeTotalInvoiceGSTAmount As Double = 0.0
    Dim pgeFuelLevy As String
    Dim pgeFuelLevyGST As String
    Dim pgeLastInvoiceAmountExGST As Double = 0.0
    Dim pgeFuelPercentage As Double = 0.0

    Dim dteInvoiceDate As Date
    Dim TempMonth As Integer
    Dim pgeSvcDesc As String
    Dim pgeSvcCode As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (IsPostBack) Then

            InitComboMonth()

            InitFilenameInputs()

            '--- Get First and Last InvoiceNumber
            Dim oMI = FMS.Business.DataObjects.tblMYOBInvoicing.GetAllOrderByInvoiceNumber()
            Dim InvoiceNumberStart = oMI.FirstOrDefault.InvoiceNumber
            Dim InvoiceNumberEnd = oMI.LastOrDefault.InvoiceNumber
            txtInvStartNo.Text = InvoiceNumberEnd


        End If

        ' Bind MYOB Customer Numbers
        Dim rpt1 As New FMS.ReportLogic.MYOBCustomerNumbers()
        Me.ASPxDocumentViewer1.Report = rpt1

        ' Bind Previous Invoice Summary Report
        Dim rpt2 As New FMS.ReportLogic.PreviousInvoiceSummaryReport()
        Me.ASPxDocumentViewer2.Report = rpt2

        '' Bind MYOB Match List
        'Dim rpt3 As New FMS.ReportLogic.MYOBMatchList()
        'Me.ASPxDocumentViewer3.Report = rpt3

        ' Init Update Customer record window controls
        lblRecAdded.Visible = False




    End Sub

#Region "Init Conctrols"
    Protected Sub InitComboMonth()
        ' Populate Invoice Month Selection
        Dim objMonths As New List(Of MonthList)

        For nMonth = 1 To 12
            Dim oMonth As New MonthList

            oMonth.ID = nMonth
            Select Case nMonth
                Case 1
                    oMonth.MonthName = "January"
                Case 2
                    oMonth.MonthName = "February"
                Case 3
                    oMonth.MonthName = "March"
                Case 4
                    oMonth.MonthName = "April"
                Case 5
                    oMonth.MonthName = "May"
                Case 6
                    oMonth.MonthName = "June"
                Case 7
                    oMonth.MonthName = "July"
                Case 8
                    oMonth.MonthName = "August"
                Case 9
                    oMonth.MonthName = "September"
                Case 10
                    oMonth.MonthName = "October"
                Case 11
                    oMonth.MonthName = "November"
                Case 12
                    oMonth.MonthName = "December"
            End Select

            objMonths.Add(oMonth)

        Next

        Me.cboMonth.TextField = "MonthName"
        Me.cboMonth.ValueField = "ID"
        Me.cboMonth.DataSource = objMonths
        Me.cboMonth.DataBind()
    End Sub

    Protected Sub InitFilenameInputs()
        ' Fill Invoice File Name
        Dim oParam = FMS.Business.DataObjects.tblParameters.GetAll().ToList()
        txtInvoiceFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBFileName").FirstOrDefault().Field1

        ' Fill MYOB Customer File Name
        txtMYOBFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBCustomerFileName").FirstOrDefault().Field1

    End Sub

#End Region

    Public Class MonthList
        Public Property ID As Integer
        Public Property MonthName As String

    End Class
    Public Class FuelLevyList
        Public Property FuelCode As String
        Public Property FuelPercentage As Double
        Public Property FuelDescription As String


    End Class

    Public Class InvoicingFrequencyList
        Public Property Frequency As String
        Public Property NoOfWeeks As Integer
        Public Property InvoiceId As String


    End Class

    Protected Sub btnChkMYOBCustNum_Click(sender As Object, e As EventArgs) Handles btnChkMYOBCustNum.Click
        pupChkMYOB.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Above
        pupChkMYOB.ShowOnPageLoad = True
    End Sub

    Protected Sub btnMatchMYOBNames_Click(sender As Object, e As EventArgs) Handles btnMatchMYOBNames.Click
        pupMatchMYOBNames.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Middle
        pupMatchMYOBNames.ShowOnPageLoad = True

    End Sub

    Protected Sub btnCloseChkMYOB_Click(sender As Object, e As EventArgs) Handles btnCloseChkMYOB.Click
        pupChkMYOB.ShowOnPageLoad = False
    End Sub

    Protected Sub btnCloseMatchMYOBNames_Click(sender As Object, e As EventArgs) Handles btnCloseMatchMYOBNames.Click
        pupMatchMYOBNames.ShowOnPageLoad = False
    End Sub

    Protected Sub cboMonth_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim SelectedMonth = Me.cboMonth.SelectedItem.Value

        Dim dteStart = CDate(SelectedMonth + "/1/" + Now.Year.ToString())
        Dim dteEnd = CDate(dteStart).Date.AddDays(-(CDate(dteStart).Day - 1)).AddMonths(1).AddDays(-1)

        Me.dteStart.Value = dteStart
        Me.dteEnd.Value = dteEnd

    End Sub

    Protected Sub btnClosePrevInvSumRep_Click(sender As Object, e As EventArgs) Handles btnClosePrevInvSumRep.Click
        pupPrevInvSumRep.ShowOnPageLoad = False
    End Sub

    Protected Sub btnPrevInvSumRep_Click(sender As Object, e As EventArgs) Handles btnPrevInvSumRep.Click
        pupPrevInvSumRep.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Above
        pupPrevInvSumRep.ShowOnPageLoad = True
    End Sub

    Private Function ValidEntries() As Boolean

        '--- Validate Entries
        If txtInvoiceFilename.Text.Length <= 0 Then
            lblBoxText.Text = "Please enter an output file name starting invoice number"
            btnYes.Text = "OK"
            btnNo.Visible = False
            puDialogBox.ShowOnPageLoad = True
            Return False
        End If

        Dim invoiceValue As Integer
        If Not Int32.TryParse(txtInvStartNo.Text, invoiceValue) Then
            lblBoxText.Text = "Please enter a starting and valid invoice number"
            btnYes.Text = "OK"
            btnNo.Visible = False
            puDialogBox.ShowOnPageLoad = True
            Return False
        End If

        Return True

    End Function

    '--- Create or Update paramater
    Protected Sub CheckParameterPerID(parID As String, field1 As String)
        '--- Check Paramater per PARID (create or update)
        Dim oParam = FMS.Business.DataObjects.tblParameters.GetAll().Where(Function(p) p.ParId = parID).ToList()
        Dim rParam As New FMS.Business.DataObjects.tblParameters

        If (oParam.Count > 0) Then
            rParam.ParId = oParam.FirstOrDefault.ParId
            rParam.Field1 = field1
            FMS.Business.DataObjects.tblParameters.Update(rParam)
        Else
            rParam.ParId = parID
            rParam.Field1 = field1
            FMS.Business.DataObjects.tblParameters.Create(rParam)
        End If
    End Sub

    '--- Get Fuel Levy details
    Protected Function GetFuelLevy(FuelCode As String) As List(Of FuelLevyList)
        Dim oFuel As New List(Of FuelLevyList)

        Dim fl = FMS.Business.DataObjects.tblFuelLevy.GetAll().Where(Function(f) f.Code = FuelCode).ToList()

        If (fl.Count > 0) Then
            Dim rFuel As New FuelLevyList

            rFuel.FuelCode = fl.FirstOrDefault.MYOBInvoiceCode
            rFuel.FuelPercentage = fl.FirstOrDefault.Percentage
            rFuel.FuelDescription = fl.FirstOrDefault.Description

            oFuel.Add(rFuel)

        End If

        Return oFuel

    End Function

    '--- Get Invoicing Frequency
    Protected Function GetInvoicingFrequency(ifID As Integer) As List(Of InvoicingFrequencyList)

        Dim obj As New List(Of InvoicingFrequencyList)
        Dim row As New InvoicingFrequencyList

        '------ Get Invoicing Frequency
        Dim objInvoicingFrequency = FMS.Business.DataObjects.tblInvoicingFrequency.GetAll().Where(Function(i) i.IId = ifID).ToList()
        'Dim objInvoicingFrequency = FMS.Business.DataObjects.tblInvoicingFrequency.GetAll().Where(Function(i) i.IId = rMYOB.InvoiceFrequency).ToList()

        If (objInvoicingFrequency.Count > 0) Then

            row.Frequency = objInvoicingFrequency.FirstOrDefault.Frequency
            row.NoOfWeeks = objInvoicingFrequency.FirstOrDefault.NoOfWeeks
            row.InvoiceId = objInvoicingFrequency.FirstOrDefault.InvoiceId

        Else
            row.Frequency = "Unknown"
            row.NoOfWeeks = 12
            row.InvoiceId = "M"

        End If

        obj.Add(row)

        Return obj


    End Function

    '--- Get Invoice Date:
    Protected Function GetInvoiceDate(invCommencing As Date, dateStart As Date, InvoiceFrequencyId As String, _
                                      InvoiceMonth As Integer, InvoiceMonth1 As Integer, InvoiceMonth2 As Integer, _
                                      InvoiceMonth3 As Integer, InvoiceMonth4 As Integer) As Date
        'dteInvoiceDate = rMYOB.InvoiceCommencing
        Dim temp1 = Day(invCommencing)
        Dim temp2 = Month(invCommencing)
        Dim temp3 = Year(dateStart)
        Dim temp5 As Integer
        Dim temp6 As Integer
        Dim tempdate As Date
        Dim InvoiceDate As Date

        Select Case InvoiceFrequencyId
            Case "A"
                'Nothing to do as date is set to current year
            Case "M"
                'Change month to current month and leave the rest the same
                temp2 = InvoiceMonth
            Case "B"
                'Check 4 specified dates
                temp1 = Day(invCommencing)
                ' temp2 = InvoiceMonth
                temp3 = Year(dateStart)

                If (InvoiceMonth1 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth2 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth3 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth4 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                End If

            Case "Q"
                'Check 4 specified dates
                temp1 = Day(invCommencing)
                ' temp2 = InvoiceMonth
                temp3 = Year(dateStart)

                If (InvoiceMonth1 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth2 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth3 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                ElseIf (InvoiceMonth4 = InvoiceMonth) Then
                    temp2 = InvoiceMonth
                End If
        End Select

        If temp1 > 28 And InvoiceFrequencyId <> "A" Then
            temp5 = 1
            temp6 = CInt(temp2)

            If temp6 > 11 Then
                temp6 = 1
            Else
                temp6 = temp6 + 1
            End If

            tempdate = Date.FromOADate(temp5 + "/" + temp6 + "/" + temp3)
            tempdate = tempdate.AddDays(-1)
            temp1 = Day(tempdate)

            'tempdate = temp5 & "/" & temp6 & "/" & temp3
            'tempdate = tempdate - 1
            'temp1 = Day(tempdate)

        End If

        'InvoiceDate = temp1 & "/" & CStr(temp2) & "/" & CStr(temp3)     '---> AUS Format
        InvoiceDate = CStr(temp2) & "/" & temp1 & "/" & CStr(temp3)     '---> PHL Format

        Return InvoiceDate

    End Function

    '----- Get Service Description
    Protected Sub GetServiceDescription(CSID As Integer)
        Dim strSvcCode As String = ""
        Dim strSvcDesc As String = ""

        If (CSID > 0) Then
            Dim oSvc = FMS.Business.DataObjects.tblServices.GetAll().Where(Function(s) s.Sid = CSID).ToList()
            pgeSvcCode = oSvc.FirstOrDefault.ServiceCode
            pgeSvcDesc = oSvc.FirstOrDefault.ServiceDescription
        Else
            pgeSvcCode = ""
            pgeSvcDesc = "Unknown"
        End If

    End Sub

    '--- Calculate Invoice Amounts
    Protected Sub CalcInvoiceAmounts(PerAnnumCharge As Double, NumberOfWeeks As Integer)
        pgeAnnualPriceExGST = Format(PerAnnumCharge, "0.00")
        pgeAnnualPriceIncGST = Format((PerAnnumCharge * 1.1), "0.00")
        pgeInvoiceAmountExGST = Format((PerAnnumCharge / Convert.ToDouble(NumberOfWeeks)), "0.00")
        pgeInvoiceAmountIncGST = Format((pgeInvoiceAmountExGST * 1.1), "0.00")
        pgeInvoiceGSTAmount = Format(pgeInvoiceAmountIncGST - pgeInvoiceAmountExGST, "0.00")
    End Sub

    '--- Create Final Invoice Line
    Protected Sub CreateFinalInvoiceLine(ExcludeCustomerFuel As Boolean)

        If Not (ExcludeCustomerFuel) Then
            'Calculate fuel levy
            pgeFuelLevy = Format((pgeLastInvoiceAmountExGST * (pgeFuelPercentage / 100)), "0.00")
            pgeTotalInvoiceAmountIncGST = Format((pgeFuelLevy * 1.1), "0.00")
            pgeTotalInvoiceGSTAmount = Format(pgeTotalInvoiceAmountIncGST - pgeFuelLevy, "0.00")

        End If

    End Sub

    '--- Create Fuel record
    Protected Sub CreateFuelRecord()
        'rsOut.AddNew()
        'rsOut!CustomerNumber = LastMYOBCustomerNo
        'rsOut!CustomerName = Replace(LastCustomerName, Sep, "")
        'rsOut!invoicenumber = NextInvoiceNumber
        'rsOut!InvoiceDate = InvoiceDate
        'rsOut!customerpurchaseordernumber = ""
        'rsOut!quantity = 1
        'rsOut!ProductCode = FuelCode
        'rsOut!ProductDescription = FuelDescription & " @ " & LastSiteName
        'rsOut!annualpriceexgst = sglTotalInvoiceAmountExGST
        'rsOut!annualpriceincgst = sglTotalInvoiceAmountIncGST
        'rsOut!invoiceamountexgst = sglTotalInvoiceAmountExGST
        'rsOut!invoiceamountincgst = sglTotalInvoiceAmountIncGST
        'rsOut!discount = 0
        'rsOut!journalmemo = "Sale " & Replace(LastCustomerName, Sep, "")
        'rsOut!SiteName = LastSiteName

        'rsOut!job = "01"
        'rsOut!taxcode = "GST"
        'rsOut!gstamount = sglTotalInvoiceGSTAmount
        'rsOut!Category = txtInvoicingFrequency
        'rsOut.Update()
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs)
        puDialogBox.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Middle

        If ValidEntries() = False Then
            Exit Sub
        End If

        '--- Create or update paramaters
        CheckParameterPerID("MYOBFileName", txtInvoiceFilename.Text)
        CheckParameterPerID("MYOBCustomerFileName", txtMYOBFilename.Text)
        Dim objecFuelLevey = GetFuelLevy("FUELL")

        '--- Main Processing
        Dim objMYOBFileList = FMS.Business.DataObjects.usp_GetMYOBFileList.GetAll().Where(Function(m) _
                                m.UnitsHaveMoreThanOneRun = False)

        If (objMYOBFileList.Count > 0) Then

            Dim intInvNum As Integer = Convert.ToInt64(txtInvStartNo.Text)

            '--- Delete tblMYOBInvoicing records (Commented for now)
            'FMS.Business.DataObjects.tblMYOBInvoicing.DeleteAll()

            Dim objMI As New List(Of FMS.Business.DataObjects.tblMYOBInvoicing)

            For Each rMYOB In objMYOBFileList
                Dim rowMI As New FMS.Business.DataObjects.tblMYOBInvoicing
                Dim ExcludeCustomerFuel = rMYOB.chkCustomerExcludeFuelLevy

                'SiteName	nvarchar(50)	Checked

                rowMI.CustomerNumber = rMYOB.MYOBCustomerNumber
                rowMI.CustomerName = rMYOB.CustomerName
                rowMI.InvoiceNumber = intInvNum
                intInvNum = intInvNum + 1

                '--- Get Invoicing Frequency
                Dim objIF = GetInvoicingFrequency(rMYOB.InvoiceFrequency)

                '------ Get Invoice Date
                Dim InvoiceDate = GetInvoiceDate(rMYOB.InvoiceCommencing, dteStart.Value, objIF.FirstOrDefault.InvoiceId,
                                                    cboMonth.Value, rMYOB.InvoiceMonth1, rMYOB.InvoiceMonth2, rMYOB.InvoiceMonth3,
                                                    rMYOB.InvoiceMonth4)
                rowMI.InvoiceDate = InvoiceDate
                rowMI.CustomerPurchaseOrderNumber = rMYOB.PurchaseOrderNumber
                rowMI.Quantity = IIf(ExcludeCustomerFuel = False, 1, rMYOB.ServiceUnits)

                If (IsDBNull(rMYOB.CSid) = False) Then
                    GetServiceDescription(rMYOB.CSid)
                Else
                    GetServiceDescription(0)
                End If

                rowMI.ProductCode = pgeSvcCode

                If (rMYOB.PurchaseOrderNumber = Nothing) Then
                    rowMI.ProductDescription = pgeSvcDesc + "@" + rMYOB.SiteName
                Else
                    rowMI.ProductDescription = "P/O: " + rMYOB.PurchaseOrderNumber + " " + pgeSvcDesc + "@" + rMYOB.SiteName
                End If

                CalcInvoiceAmounts(rMYOB.PerAnnumCharge, objIF.FirstOrDefault.NoOfWeeks)

                rowMI.AnnualPriceExGST = pgeAnnualPriceExGST
                rowMI.AnnualPriceIncGST = pgeAnnualPriceIncGST
                rowMI.Discount = 0
                rowMI.InvoiceAmountExGST = pgeInvoiceAmountExGST
                rowMI.InvoiceAmountIncGST = pgeInvoiceAmountIncGST
                rowMI.Job = "01"
                rowMI.JournalMemo = "Sale " + rMYOB.CustomerName
                rowMI.TaxCode = "GST"
                rowMI.GSTAmount = pgeInvoiceGSTAmount
                rowMI.Category = objIF.FirstOrDefault.Frequency
                rowMI.SiteName = rMYOB.SiteName

            Next

        Else

        End If


    End Sub

    Protected Sub btnNo_Click(sender As Object, e As EventArgs)
        puDialogBox.ShowOnPageLoad = False

    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        Dim strDialog = lblBoxText.Text

        Select Case strDialog
            Case "Please enter an output file name starting invoice number"
                txtInvoiceFilename.Focus()

            Case "Please enter a starting invoice number"
                txtInvStartNo.Focus()

        End Select

        puDialogBox.ShowOnPageLoad = False

    End Sub
End Class