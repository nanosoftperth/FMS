﻿Imports System.IO
Imports DevExpress
Imports DevExpress.Web
Imports System
Imports System.Web.UI
Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.Web.ASPxGridView
Imports System.Web.Services
Imports System.Drawing

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

            If (oMI.Count > 0) Then
                Dim InvoiceNumberStart = oMI.FirstOrDefault.InvoiceNumber
                Dim InvoiceNumberEnd = oMI.LastOrDefault.InvoiceNumber
                txtInvStartNo.Text = InvoiceNumberEnd
            End If

        Else

            If (Not Session("MYOB_Cust") Is Nothing) Then
                Me.lblFileName.Text = Session("MYOB_Cust")
            End If

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

#Region "FileUploads"
    Private Const UploadDirectory As String = "~/App_Data/MYOB/"
    Protected Sub ucCust_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs)
        'Dim Path = Server.MapPath("/")
        'Dim path2 = Server.MapPath("~")

        Dim folder = Server.MapPath("~/App_Data/MYOB")

        If (Not Directory.Exists(folder)) Then
            Directory.CreateDirectory(folder)
        End If

        Dim strFilename = e.UploadedFile.FileName.ToString()
        Dim SavePath As String = System.IO.Path.Combine(folder, strFilename) 'combines the saveDirectory and the filename to get a fully qualified path.

        If System.IO.File.Exists(SavePath) Then
            System.IO.File.Delete(SavePath)
        End If

        'Dim SavePath As String = System.IO.Path.Combine(folder, "cust.txt") 'combines the saveDirectory and the filename to get a fully qualified path.

        'If System.IO.File.Exists(SavePath) Then
        '    System.IO.File.Delete(SavePath)

        'Else
        '    'the file doesn't exist
        'End If


        Dim resultExtension As String = Path.GetExtension(e.UploadedFile.FileName)
        'Dim resultFileName As String = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension)
        Dim resultFileName As String = e.UploadedFile.FileName.ToString()
        Dim resultFileUrl As String = UploadDirectory & resultFileName
        Dim resultFilePath As String = MapPath(resultFileUrl)
        e.UploadedFile.SaveAs(resultFilePath)

        If e.IsValid Then
            'Create a Cookie with a suitable Key.
            Dim nameCookie As New HttpCookie("CustName")
            'Set the Cookie value.
            nameCookie.Values("CustName") = strFilename
            'Set the Expiry date.
            nameCookie.Expires = DateTime.Now.AddDays(1)
            'Add the Cookie to Browser.
            Response.Cookies.Add(nameCookie)

            'Dim fileName As String = e.UploadedFile.FileName.ToString()
            'Dim fileName As String = Guid.NewGuid().ToString("N") + "." + fileType
            'Dim path As String = ""

            'FMS.Business.ThisSession.ImageType = fileType
            'FMS.Business.ThisSession.ImageLocByByteArray = e.UploadedFile.FileBytes

            'Session("MYOB_cust") = "Uploaded File: " + e.UploadedFile.FileName.ToString()
            e.CallbackData = e.UploadedFile.FileName.ToString()

        End If


    End Sub
#End Region

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
        'txtInvoiceFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBFileName").FirstOrDefault().Field1

        ' Fill MYOB Customer File Name
        'txtMYOBFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBCustomerFileName").FirstOrDefault().Field1

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
        'If txtInvoiceFilename.Text.Length <= 0 Then
        '    lblBoxText.Text = "Please enter an output file name starting invoice number"
        '    btnYes.Text = "OK"
        '    btnNo.Visible = False
        '    puDialogBox.ShowOnPageLoad = True
        '    Return False
        'End If

        Dim invoiceValue As Integer
        If Not Int32.TryParse(txtInvStartNo.Text, invoiceValue) Then
            lblBoxText.Text = "Please enter a starting and valid invoice number"
            btnYes.Text = "OK"
            btnNo.Visible = False
            puDialogBox.ShowOnPageLoad = True
            Return False
        End If

        If cboMonth.Value Is Nothing Then
            lblBoxText.Text = "Please select a invoice month."
            btnYes.Text = "OK"
            btnNo.Visible = False
            puDialogBox.ShowOnPageLoad = True
            Return False
        End If

        If dteStart.Value Is Nothing Or dteEnd.Value Is Nothing Then
            lblBoxText.Text = "Please invoice start and end date."
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
        Dim objInvoicingFrequency = FMS.Business.DataObjects.tblInvoicingFrequency.GetAllPerApplication().Where(Function(i) i.IId = ifID).ToList()
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

            tempdate = Convert.ToDateTime(temp5.ToString() + "/" + temp6.ToString() + "/" + temp3.ToString())
            tempdate = tempdate.AddDays(-1)
            temp1 = Day(tempdate)

            'tempdate = temp5 & "/" & temp6 & "/" & temp3
            'tempdate = tempdate - 1
            'temp1 = Day(tempdate)

        End If

        InvoiceDate = temp1 & "/" & CStr(temp2) & "/" & CStr(temp3)     '---> AUS Format
        'InvoiceDate = CStr(temp2) & "/" & temp1 & "/" & CStr(temp3)     '---> PHL Format

        Return InvoiceDate

    End Function

    '----- Get Service Description
    Protected Sub GetServiceDescription(CSID As Integer)
        Dim strSvcCode As String = ""
        Dim strSvcDesc As String = ""

        If (CSID > 0) Then
            Dim appId = FMS.Business.ThisSession.ApplicationID
            Dim oSvc = FMS.Business.DataObjects.tblServices.GetAll().Where(Function(s) s.Sid = CSID _
                            And s.ApplicationID = appId).ToList()

            If (oSvc.Count > 0) Then
                pgeSvcCode = oSvc.FirstOrDefault.ServiceCode
                pgeSvcDesc = oSvc.FirstOrDefault.ServiceDescription
            Else
                pgeSvcCode = ""
                pgeSvcDesc = "Unknown"
            End If

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

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs)
        'puDialogBox.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Middle

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
            FMS.Business.DataObjects.tblMYOBInvoicing.DeleteAll()

            Dim objMI As New List(Of FMS.Business.DataObjects.tblMYOBInvoicing)

            For Each rMYOB In objMYOBFileList
                Dim rowMI As New FMS.Business.DataObjects.tblMYOBInvoicing
                Dim ExcludeCustomerFuel = rMYOB.chkCustomerExcludeFuelLevy

                rowMI.CustomerNumber = rMYOB.MYOBCustomerNumber
                rowMI.CustomerName = rMYOB.CustomerName
                rowMI.InvoiceNumber = intInvNum
                intInvNum = intInvNum + 1

                '--- Get Invoicing Frequency
                Dim objIF = GetInvoicingFrequency(rMYOB.InvoiceFrequency)

                '------ Get Invoice Date
                Dim InvoiceDate = GetInvoiceDate(rMYOB.InvoiceCommencing, dteStart.Value, objIF.FirstOrDefault.InvoiceId,
                                                    cboMonth.Value, IIf(IsDBNull(rMYOB.InvoiceMonth1), 0, rMYOB.InvoiceMonth1), IIf(IsDBNull(rMYOB.InvoiceMonth2), 0, rMYOB.InvoiceMonth2), IIf(IsDBNull(rMYOB.InvoiceMonth3), 0, rMYOB.InvoiceMonth3),
                                                    IIf(IsDBNull(rMYOB.InvoiceMonth4), 0, rMYOB.InvoiceMonth4))
                rowMI.InvoiceDate = InvoiceDate
                rowMI.CustomerPurchaseOrderNumber = rMYOB.PurchaseOrderNumber
                rowMI.Quantity = Convert.ToDouble(IIf(ExcludeCustomerFuel = False, 1, rMYOB.ServiceUnits))

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

                objMI.Add(rowMI)

            Next

            FMS.Business.DataObjects.tblMYOBInvoicing.CreateAll(objMI)

            Dim objMYOB = FMS.Business.DataObjects.tblMYOBInvoicing.GetAll()

            Response.Cookies("showLP").Value = "NO"

            If (objMYOB.Count > 0) Then

                '--- check folder exist
                Dim folder = Server.MapPath("~/App_Data/MYOB")

                If (Not Directory.Exists(folder)) Then
                    Directory.CreateDirectory(folder)
                End If

                '--- create text file
                Dim fileLoc = System.IO.Path.Combine(folder, "myob.txt")
                Dim fs As FileStream = Nothing
                If (Not File.Exists(fileLoc)) Then
                    fs = File.Create(fileLoc)
                    fs.Flush()
                    fs.Close()
                Else
                    File.Delete(fileLoc)
                    fs = File.Create(fileLoc)
                    fs.Flush()
                    fs.Close()
                End If

                If File.Exists(fileLoc) Then
                    Dim ThisInv As String
                    Dim LastInv As String
                    Dim PrintVar As String
                    Dim Sep As String = vbTab

                    '--- construct text file header
                    PrintVar = ""
                    PrintVar = PrintVar & "Co./Last Name" & Sep
                    PrintVar = PrintVar & "First Name" & Sep
                    PrintVar = PrintVar & "Addr 1" & Sep
                    PrintVar = PrintVar & "Addr 2" & Sep
                    PrintVar = PrintVar & "Addr 3" & Sep
                    PrintVar = PrintVar & "Addr 4" & Sep
                    PrintVar = PrintVar & "Inclusive" & Sep
                    PrintVar = PrintVar & "Invoice #" & Sep
                    PrintVar = PrintVar & "Date" & Sep
                    PrintVar = PrintVar & "Customer PO" & Sep
                    PrintVar = PrintVar & "Ship Via" & Sep
                    PrintVar = PrintVar & "Delivery Status" & Sep
                    PrintVar = PrintVar & "Item Number" & Sep
                    PrintVar = PrintVar & "Quantity" & Sep
                    PrintVar = PrintVar & "Description" & Sep
                    PrintVar = PrintVar & "Price" & Sep
                    PrintVar = PrintVar & "Inc-Tax Price" & Sep
                    PrintVar = PrintVar & "Discount" & Sep
                    PrintVar = PrintVar & "Total" & Sep
                    PrintVar = PrintVar & "Inc-Tax Total" & Sep
                    PrintVar = PrintVar & "Job" & Sep
                    PrintVar = PrintVar & "Comment" & Sep
                    PrintVar = PrintVar & "Journal Memo" & Sep
                    PrintVar = PrintVar & "Salesperson Last Name" & Sep
                    PrintVar = PrintVar & "Salesperson First Name" & Sep
                    PrintVar = PrintVar & "Shipping Date" & Sep
                    PrintVar = PrintVar & "Referral Source" & Sep
                    PrintVar = PrintVar & "Tax Code" & Sep
                    PrintVar = PrintVar & "Non-GST Amount" & Sep
                    PrintVar = PrintVar & "GST Amount" & Sep
                    PrintVar = PrintVar & "LCT Amount" & Sep
                    PrintVar = PrintVar & "Freight Amount" & Sep
                    PrintVar = PrintVar & "Inc-Tax Freight Amount" & Sep
                    PrintVar = PrintVar & "Freight Tax Code" & Sep
                    PrintVar = PrintVar & "Freight Non-GST Amount" & Sep
                    PrintVar = PrintVar & "Freight GST Amount" & Sep
                    PrintVar = PrintVar & "Freight LCT Amount" & Sep
                    PrintVar = PrintVar & "Sale Status" & Sep
                    PrintVar = PrintVar & "Currency Code" & Sep
                    PrintVar = PrintVar & "Exchange Rate" & Sep
                    PrintVar = PrintVar & "Terms - Payment is Due" & Sep
                    PrintVar = PrintVar & "Terms - Discount Days" & Sep
                    PrintVar = PrintVar & "Terms - Balance Due Days" & Sep
                    PrintVar = PrintVar & "Terms - % Discount" & Sep
                    PrintVar = PrintVar & "Terms - % Monthly Charge" & Sep
                    PrintVar = PrintVar & "Amount Paid" & Sep
                    PrintVar = PrintVar & "Payment Method" & Sep
                    PrintVar = PrintVar & "Payment Notes" & Sep
                    PrintVar = PrintVar & "Name on card" & Sep
                    PrintVar = PrintVar & "Card Number" & Sep
                    PrintVar = PrintVar & "Expiry Date" & Sep
                    PrintVar = PrintVar & "Authorisation Code" & Sep
                    PrintVar = PrintVar & "BSB" & Sep
                    PrintVar = PrintVar & "Account Number" & Sep
                    PrintVar = PrintVar & "Drawer/Account Name" & Sep
                    PrintVar = PrintVar & "Cheque Number" & Sep
                    PrintVar = PrintVar & "Category" & Sep
                    PrintVar = PrintVar & "LocationID" & Sep
                    PrintVar = PrintVar & "CardID" & Sep
                    PrintVar = PrintVar & "RecordID"

                    Using sw As StreamWriter = New StreamWriter(fileLoc)
                        '--- print header file
                        sw.Write(PrintVar)

                        '--- print body file
                        LastInv = "999999"
                        For Each row In objMYOB
                            ThisInv = row.InvoiceNumber

                            If LastInv <> "999999" Or ThisInv <> LastInv Then
                                'PrintVar = vbCrLf
                                PrintVar = vbCr
                                sw.Write(PrintVar)
                            End If

                            PrintVar = ""
                            PrintVar = PrintVar & UCase(row.CustomerName) & Sep            'Co./Last Name
                            PrintVar = PrintVar & Sep                                       'First Name
                            PrintVar = PrintVar & Sep                                       'Addr 1
                            PrintVar = PrintVar & Sep                                       'Addr 2
                            PrintVar = PrintVar & Sep                                       'Addr 3
                            PrintVar = PrintVar & Sep                                       'Addr 4
                            PrintVar = PrintVar & Sep                                       'Inclusive
                            PrintVar = PrintVar & row.InvoiceNumber & Sep                   'Invoice #
                            PrintVar = PrintVar & row.InvoiceDate & Sep                     'Date
                            PrintVar = PrintVar & row.CustomerPurchaseOrderNumber & Sep     'Customer PO
                            PrintVar = PrintVar & Sep                                       'Ship Via
                            PrintVar = PrintVar & Sep                                       'Delivery Status
                            PrintVar = PrintVar & row.ProductCode & Sep                     'Item Number
                            PrintVar = PrintVar & row.Quantity & Sep                        'Quantity
                            PrintVar = PrintVar & row.ProductDescription & Sep              'Description
                            PrintVar = PrintVar & row.annualpriceexgst & Sep                'Price
                            PrintVar = PrintVar & row.annualpriceincgst & Sep               'Inc-Tax Price
                            PrintVar = PrintVar & row.Discount & Sep                        'Discount
                            PrintVar = PrintVar & row.invoiceamountexgst & Sep              'Total
                            PrintVar = PrintVar & row.invoiceamountincgst & Sep             'Inc-Tax Total
                            PrintVar = PrintVar & row.Job & Sep                             'Job
                            PrintVar = PrintVar & Sep                                       'Comment
                            PrintVar = PrintVar & row.JournalMemo & Sep                     'Journal Memo
                            PrintVar = PrintVar & Sep                                       'Salesperson Last Name
                            PrintVar = PrintVar & Sep                                       'Salesperson First Name
                            PrintVar = PrintVar & Sep                                       'Shipping Date
                            PrintVar = PrintVar & Sep                                       'Referral Source
                            PrintVar = PrintVar & row.TaxCode & Sep                         'Tax Code
                            PrintVar = PrintVar & Sep                                       'NON GST Amount
                            PrintVar = PrintVar & row.GSTAmount & Sep                       'GST Amount
                            PrintVar = PrintVar & Sep                                       'LCT Amount
                            PrintVar = PrintVar & Sep                                       'Freight Amount
                            PrintVar = PrintVar & Sep                                       'Inc-Tax Freight Amount
                            PrintVar = PrintVar & Sep                                       'Freight Tax Code
                            PrintVar = PrintVar & Sep                                       'Freight Non-GST Amount
                            PrintVar = PrintVar & Sep                                       'Freight GST Amount
                            PrintVar = PrintVar & Sep                                       'Freight LCT Amount
                            PrintVar = PrintVar & Sep                                       'Sale Status
                            PrintVar = PrintVar & Sep                                       'Currency Code
                            PrintVar = PrintVar & Sep                                       'Exchange Rate
                            PrintVar = PrintVar & Sep                                       'Terms - Payment is Due
                            PrintVar = PrintVar & Sep                                       'Terms - Discount Days
                            PrintVar = PrintVar & Sep                                       'Terms - Balance Due Days
                            PrintVar = PrintVar & Sep                                       'Terms - % Discount
                            PrintVar = PrintVar & Sep                                       'Terms - % Monthly Charge
                            PrintVar = PrintVar & Sep                                       'Amount Paid
                            PrintVar = PrintVar & Sep                                       'Payment Method
                            PrintVar = PrintVar & Sep                                       'Payment Notes
                            PrintVar = PrintVar & Sep                                       'Name on Card
                            PrintVar = PrintVar & Sep                                       'Card Number
                            PrintVar = PrintVar & Sep                                       'Expiry Date
                            PrintVar = PrintVar & Sep                                       'Authorisation Code
                            PrintVar = PrintVar & Sep                                       'BSB
                            PrintVar = PrintVar & Sep                                       'Account Number
                            PrintVar = PrintVar & Sep                                       'Drawer/Account Name
                            PrintVar = PrintVar & Sep                                       'Cheque Number
                            PrintVar = PrintVar & row.Category & Sep                        'Category
                            PrintVar = PrintVar & Sep                                       'LocationID
                            PrintVar = PrintVar & row.CustomerNumber & Sep                  'CardID
                            PrintVar = PrintVar                                             'RecordID

                            sw.Write(PrintVar)
                        Next

                        sw.Flush()
                        sw.Close()

                        '---download file to browser
                        'Dim path As String = Server.MapPath(strRequest)
                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileLoc)
                        If file.Exists Then
                            Response.Clear()
                            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                            Response.AddHeader("Content-Length", file.Length.ToString())
                            Response.ContentType = "application/octet-stream"
                            Response.WriteFile(file.FullName)
                            Response.End()

                        Else
                            Response.Write("This file does not exist.")
                        End If

                        Me.lpProcess.Visible = False

                    End Using
                End If
            End If

        End If

        Me.lpProcess.Visible = False


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

    Protected Sub cbProcess_Callback(source As Object, e As CallbackEventArgs)
        System.Threading.Thread.Sleep(3000)
    End Sub

#Region "WebMethods"
    '<WebMethod>
    'Public Shared Function Sample(RunDate As Date, DriverID As Integer, RunNumber As Integer) As Boolean
    '    CheckParameterPerID("MYOBFileName", this.txtInvoiceFilename.Text)
    '    CheckParameterPerID("MYOBCustomerFileName", txtMYOBFilename.Text)
    '    Dim objecFuelLevey = GetFuelLevy("FUELL")

    'End Function

#End Region

End Class