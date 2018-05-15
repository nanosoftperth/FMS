<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreviousSupplierPopup.aspx.vb" Inherits="FMS.WEB.PreviousSupplierPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <style>
        .container {
            width: 210px;
        }

        .centerBlock {
            display: table;
            margin: auto;
        }
    </style>
    <script>
        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            clrPreviousSupplierGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="centerBlock">
                <dx:ASPxGridView ID="PreviousSupplierGridView" KeyFieldName="PreviousSupplierID" DataSourceID="odsPreviousSupplier" runat="server"
                    ClientInstanceName="clrPreviousSupplierGridView">
                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                    <Settings ShowPreview="true" />
                    <SettingsPager PageSize="10" />
                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                    <SettingsPopup>
                        <EditForm Modal="true"
                            VerticalAlign="WindowCenter"
                            HorizontalAlign="WindowCenter" Width="400px" />
                    </SettingsPopup>
                    <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="PreviousSupplierID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PreviousSupplier" VisibleIndex="3"></dx:GridViewDataTextColumn>
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
                <asp:ObjectDataSource ID="odsPreviousSupplier" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DataObjectTypeName="FMS.Business.DataObjects.tblPreviousSuppliers" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
            </div>
        </div>
    </form>
</body>
</html>
