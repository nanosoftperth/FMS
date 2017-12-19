Public Class ProduceMYOBFile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        ' Fill Invoice File Name
        Dim oParam = FMS.Business.DataObjects.tblParameters.GetAll().ToList()
        txtInvoiceFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBFileName").FirstOrDefault().Field1

        ' Fill MYOB Customer File Name
        txtMYOBFilename.Text = oParam.Where(Function(m) m.ParId = "MYOBCustomerFileName").FirstOrDefault().Field1

        
    End Sub

    Public Class MonthList
        Public Property ID As Integer
        Public Property MonthName As String

    End Class

    Protected Sub btnChkMYOBCustNum_Click(sender As Object, e As EventArgs) Handles btnChkMYOBCustNum.Click
        'Response.Redirect("MYOBCustomers.aspx")
        Dim rpt As New FMS.ReportLogic.MYOBCustomerNumbers()
        Me.ASPxDocumentViewer1.Report = rpt

        'nmw.Left = CInt(ob(1))
        'nmw.Top = CInt(ob(2))
        'nmw.PopupControl.PopupVerticalAlign = DevExpress.Web.ASPxClasses.PopupVerticalAlign.NotSet
        'nmw.PopupControl.PopupHorizontalAlign = DevExpress.Web.ASPxClasses.PopupHorizontalAlign.NotSet
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
End Class