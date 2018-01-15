<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateIncreases.aspx.vb" Inherits="FMS.WEB.RateIncreases" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="RateIncreasesGridView" KeyFieldName="RateIncreaseID" DataSourceID="odsRateIncreases" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False"
            OnRowInserting="RateIncreasesGridView_RowInserting"
            OnRowUpdating="RateIncreasesGridView_RowUpdating"
            OnRowDeleting="RateIncreasesGridView_RowDeleting">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Rate Increases</TitlePanel>
            </Templates>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
            <SettingsPopup>
                <EditForm Modal="true"
                    VerticalAlign="WindowCenter"
                    HorizontalAlign="WindowCenter" Width="300px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="RateIncreaseID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RateIncreaseDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="AnnualIncreaseApplies" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="AlreadyDoneThisYear" VisibleIndex="3"></dx:GridViewDataCheckColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsRateIncreases" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DataObjectTypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
