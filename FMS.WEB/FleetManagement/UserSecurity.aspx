<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserSecurity.aspx.vb" Inherits="FMS.WEB.UserSecurity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    
</head>
<body>
    <%--<form id="form1" runat="server">--%>
        <div>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="User Security" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>            
        </div>
        <br />
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="false" 
            KeyFieldName="usersecID" DataSourceID="odsUsers" Width="100%">
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" 
                    ShowNewButtonInHeader="True" ShowDeleteButton="True"
                    VisibleIndex="0" ></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="usersecID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="User">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="txtUserName" VisibleIndex="2" Visible="true" Caption="User Name"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="Administrator" VisibleIndex="3" Visible="true" Caption="Administrator"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="UserPassword" VisibleIndex="4" Visible="true" Caption="Password">
                            <PropertiesTextEdit Password="True" ClientInstanceName="psweditor"></PropertiesTextEdit>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtPassword" runat="server" AutoGenerateColumns="false" 
                                    OnInit="txtPassword_Init">                                 
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UserGroup" VisibleIndex="4" Visible="true" Caption="User Group">
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="cboUserGroup" runat="server" AutoGenerateColumns="false" 
                                    DropDownStyle="DropDownList" DataSourceID="odsUserGroup" ValueField="UserGroup"
                                    ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                                    CallbackPageSize="30" OnInit="cboUserGroup_Init">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="UserGroup" Width="100px" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>

                    </Columns>
                </dx:GridViewBandColumn>
                
            </Columns>
            <Templates>
                <DetailRow>
                    Main Menu Items
                    <dx:ASPxGridView ID="gvMainMenu" runat="server" AutoGenerateColumns="false" 
                        KeyFieldName="usersecID" DataSourceID="odsUsers" Width="100%">

                    </dx:ASPxGridView>
                </DetailRow>
                <DetailRow>
                    Maintenance Menu Items
                </DetailRow>
            </Templates>
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="GetAll" 
            TypeName="FMS.Business.DataObjects.tblUserSecurity" 
            DataObjectTypeName="FMS.Business.DataObjects.tblUserSecurity" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUserGroup" runat="server" SelectMethod="GetAllPerApplication" 
            TypeName="FMS.Business.DataObjects.tblUserGroups">
            <SelectParameters>
                <asp:SessionParameter DbType="Guid" Name="AppID" SessionField="ApplicationID" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
