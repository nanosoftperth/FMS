Imports DevExpress.Web

Public Class ClientAssignments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub RunGridView_BeforePerformDataSelect(sender As Object, e As System.EventArgs)
        FMS.Business.ThisSession.RunID = CType(sender, ASPxGridView).GetMasterRowKeyValue()
    End Sub

    Private Sub odsRunMultiDocs_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsRunMultiDocs.Deleting
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Rid = FMS.Business.ThisSession.RunID
    End Sub

    Private Sub odsRunMultiDocs_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsRunMultiDocs.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Rid = FMS.Business.ThisSession.RunID
    End Sub

    Private Sub odsRunMultiDocs_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsRunMultiDocs.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Rid = FMS.Business.ThisSession.RunID
    End Sub

    Public Sub ClientGridView_BeforePerformDataSelect(sender As Object, e As System.EventArgs)
        FMS.Business.ThisSession.ClientID = CType(sender, ASPxGridView).GetMasterRowKeyValue()
    End Sub

    Private Sub odsMultiDocs_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsMultiDocs.Deleting
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Cid = FMS.Business.ThisSession.ClientID
    End Sub

    Private Sub odsMultiDocs_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsMultiDocs.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Cid = FMS.Business.ThisSession.ClientID
    End Sub

    Private Sub odsMultiDocs_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsMultiDocs.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.FleetDocument).Cid = FMS.Business.ThisSession.ClientID
    End Sub

    Protected Sub hyperLinkNew_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.AddNewRow()", container.Grid.ClientInstanceName)
    End Sub
    Protected Sub hyperLinkEdit_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.StartEditRow({1})", container.Grid.ClientInstanceName, container.VisibleIndex)
    End Sub
    Protected Sub hyperLinkDelete_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.DeleteRow({1})", container.Grid.ClientInstanceName, container.VisibleIndex)
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function GetRunValues(ByVal Rid As Integer)
        Dim objRuns = FMS.Business.DataObjects.tblRuns.GetTblRunByRId(Rid)
        Return objRuns
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function GetClientCustomerValues(ByVal Cid As Integer)
        Dim objRuns = FMS.Business.DataObjects.tblCustomers.GetACustomerByCID(Cid)
        Return objRuns
    End Function
End Class