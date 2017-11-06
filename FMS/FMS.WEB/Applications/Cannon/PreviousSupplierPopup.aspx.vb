Public Class PreviousSupplierPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request("cid").Equals("") Then
            Dim PreviousSupplierSortOrder = FMS.Business.DataObjects.tblPreviousSuppliers.GetPreviousSupplierSortOrder(Request("cid"))
            If Not PreviousSupplierSortOrder Is Nothing Then
                PreviousSupplierGridView.StartEdit(PreviousSupplierSortOrder.PreviousSupplierSortOrder - 1)
            End If
        End If
    End Sub

End Class