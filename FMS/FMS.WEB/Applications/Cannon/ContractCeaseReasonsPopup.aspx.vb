Public Class ContractCeaseReasonsPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ContractCeaseReason = FMS.Business.DataObjects.tblContractCeaseReasons.GetContractCeaseReasonSortOrder(Request("aid"))
        ContractCeaseReasonsGridView.StartEdit(ContractCeaseReason.CeaseReasonSortOrder - 1)
    End Sub

End Class