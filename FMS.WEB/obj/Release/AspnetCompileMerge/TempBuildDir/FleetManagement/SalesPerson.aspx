<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalesPerson.aspx.vb" Inherits="FMS.WEB.SalesPerson" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <style>
    .dxeMemoEditAreaSys{
        border-width:1px !Important;
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
            cltSalesPersonGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="SalesPersonGridView" KeyFieldName="SalesPersonID" DataSourceID="odsSalesPerson" 
            runat="server" Theme="SoftOrange" AutoGenerateColumns="False" ClientInstanceName="cltSalesPersonGridView">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Sales Persons</TitlePanel>
            </Templates>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="400px" />
            </SettingsPopup>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" /> 
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="SalesPersonID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SalesPerson" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="SalesPersonStartDate" VisibleIndex="4"></dx:GridViewDataDateColumn>
                <dx:GridViewDataMemoColumn FieldName="SalesPersonComments" VisibleIndex="5" PropertiesMemoEdit-Height="100">
                </dx:GridViewDataMemoColumn>
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
        <asp:ObjectDataSource ID="odsSalesPerson" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSalesPersons" DataObjectTypeName="FMS.Business.DataObjects.tblSalesPersons" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>

