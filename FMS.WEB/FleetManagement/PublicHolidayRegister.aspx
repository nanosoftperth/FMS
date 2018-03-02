<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PublicHolidayRegister.aspx.vb" Inherits="FMS.WEB.PublicHolidayRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Public Holiday Register</title>
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
            gvPublicHolidayRegister.DeleteRow(visibleIndex);
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
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Public Holiday Register" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>            
        </div>
        <br />
        <dx:ASPxGridView ID="gvPublicHolidayRegister" ClientInstanceName="gvPublicHolidayRegister" 
            KeyFieldName="Aid" DataSourceID="odsHolidays" runat="server" 
            EnableTheming="True"             
            Theme="SoftOrange" AutoGenerateColumns="False">
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" /> 
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" 
                    ShowNewButtonInHeader="True"
                    VisibleIndex="0" >
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="PublicHolidayDate" VisibleIndex="2" Visible="true"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
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
    <%--<dx:ASPxGridView ID="gvPublicHolidayRegister" KeyFieldName="AID" DataSourceID="odsHolidays" runat="server" 
            Theme="SoftOrange" AutoGenerateColumns="False">
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="300px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" 
                    ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDate" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PublicHolidayDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>--%>
        <asp:ObjectDataSource ID="odsHolidays" runat="server" SelectMethod="GetAll" 
            TypeName="FMS.Business.DataObjects.tblPublicHolidayRegister" 
            DataObjectTypeName="FMS.Business.DataObjects.tblPublicHolidayRegister" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
    </form>
</body>
</html>
