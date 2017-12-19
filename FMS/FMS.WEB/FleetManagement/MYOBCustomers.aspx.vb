Public Class MYOBCustomers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rpt As New FMS.ReportLogic.MYOBCustomerNumbers()
        Me.ASPxDocumentViewer1.Report = rpt

    End Sub

End Class