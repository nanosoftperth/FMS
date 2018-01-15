
Imports System.Threading

Public Class PerAnnumValueReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        'Thread.Sleep(5000)

        FMS.Business.DataObjects.usp_UpdateCustomeRating.UpdateCustomerRating()

        Me.lpUpdCustRating.Visible = False

        Me.pupReport.HeaderText = "Per Annum Value Report"
        Me.pupReport.ShowOnPageLoad = True


    End Sub


End Class