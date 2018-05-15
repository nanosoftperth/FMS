<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IndustryGroupPopup.aspx.vb" Inherits="FMS.WEB.IndustryGroupPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <script>

        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            cltIndustryGroupsGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <dx:ASPxGridView ID="IndustryGroupsGridView" KeyFieldName="IndustryID" DataSourceID="odsIndustryGroups" runat="server" AutoGenerateColumns="False"
                OnRowInserting="IndustryGroupsGridView_RowInserting"
                OnRowUpdating="IndustryGroupsGridView_RowUpdating"
                OnRowDeleting="IndustryGroupsGridView_RowDeleting"
                ClientInstanceName="cltIndustryGroupsGridView">
                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                <Settings ShowPreview="true" />
                <SettingsPager PageSize="10" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                <SettingsPopup>
                    <EditForm Modal="true"
                        VerticalAlign="WindowCenter"
                        HorizontalAlign="WindowCenter" Width="450px" />
                </SettingsPopup>
                <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
                <Columns>
                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="IndustryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="IndustryDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
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
            <asp:ObjectDataSource ID="odsIndustryGroups" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblIndustryGroups" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblIndustryGroups" UpdateMethod="Update">
                <SelectParameters>
                    <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
