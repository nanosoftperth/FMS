Public Class axContractCeaseReasonsPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request("aid") Is Nothing AndAlso Not Request("aid").Equals("") Then
            Dim ContractCeaseReason = FMS.Business.DataObjects.tblContractCeaseReasons.GetContractCeaseReasonSortOrder(Request("aid"))
            ContractCeaseReasonsGridView.StartEdit(ContractCeaseReason.CeaseReasonSortOrder - 1)
        End If
    End Sub
End Class