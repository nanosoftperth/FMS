<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerAgents.aspx.vb" Inherits="FMS.WEB.CustomerAgents" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <script>
        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            cltCustomerAgentsGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="CustomerAgentsGridView" KeyFieldName="CustomerAgentID" DataSourceID="odsCustomerAgents" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False"
            OnRowInserting="CustomerAgentsGridView_RowInserting"
            OnRowUpdating="CustomerAgentsGridView_RowUpdating"
            OnRowDeleting="CustomerAgentsGridView_RowDeleting"
            ClientInstanceName="cltCustomerAgentsGridView">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Customer Agents</TitlePanel>
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
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" /> 
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" >
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerAgentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerAgentName" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <dx:ASPxPopupControl ID="DeleteDialog" runat="server" Text="Are you sure you want to delete this?" 
            ClientInstanceName="popupDelete" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickYes" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickNo" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <asp:ObjectDataSource ID="odsCustomerAgents" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblCustomerAgent" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerAgent" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
