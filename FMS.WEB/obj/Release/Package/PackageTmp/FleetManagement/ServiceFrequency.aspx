<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceFrequency.aspx.vb" Inherits="FMS.WEB.ServiceFrequency" %>
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
            cltServiceFrequencyGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="ServiceFrequencyGridView" KeyFieldName="FrequencyID" DataSourceID="odsServiceFrequency" 
            runat="server" Theme="SoftOrange" AutoGenerateColumns="False" ClientInstanceName="cltServiceFrequencyGridView">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Service Frequency</TitlePanel>
            </Templates>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="300px" />
            </SettingsPopup>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="FrequencyID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FrequencyDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="Factor" VisibleIndex="4" PropertiesSpinEdit-Height="20"></dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataCheckColumn FieldName="Periodical" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="6"></dx:GridViewDataTextColumn>
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
        <asp:ObjectDataSource ID="odsServiceFrequency" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServiceFrequency" DataObjectTypeName="FMS.Business.DataObjects.tblServiceFrequency" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

