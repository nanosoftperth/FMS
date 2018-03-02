<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DriverDetails.aspx.vb" Inherits="FMS.WEB.DriverDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Details</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <script>
        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            cltgvDriver.DeleteRow(visibleIndex);
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
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Driver Details" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>      
        </div>
        <dx:ASPxGridView ID="gvDriver" runat="server" AutoGenerateColumns="false" 
            KeyFieldName="DriverID" DataSourceID="odsDriver" Width="100%"
            OnHtmlRowPrepared="gvDriver_HtmlRowPrepared" 
            OnStartRowEditing="gvDriver_StartRowEditing" OnInitNewRow="gvDriver_InitNewRow"
            Theme="SoftOrange" ClientInstanceName="cltgvDriver">
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="400px" 
                    />           
            </SettingsPopup>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" /> 
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" 
                    ShowNewButtonInHeader="True"
                    VisibleIndex="0" >
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="DriverID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Did" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="4" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DriversLicenseNo" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="DriversLicenseExpiryDate" VisibleIndex="6" Visible="true"></dx:GridViewDataDateColumn>
                <dx:GridViewDataCheckColumn FieldName="Technician" VisibleIndex="7" Visible="true"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="Inactive" VisibleIndex="8" Visible="true"></dx:GridViewDataCheckColumn>
                
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxLabel runat="server" text="Comments" Font-Bold="true" />
                    <br />
                    <dx:ASPxGridView ID="gvComments" runat="server" AutoGenerateColumns="false" 
                        KeyFieldName="Aid" DataSourceID="odsDriverComments" Width="100%"
                        OnStartRowEditing="gvComments_StartRowEditing">
                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
                        <SettingsPopup>
                            <EditForm Modal="true" 
                                VerticalAlign="WindowCenter" 
                                HorizontalAlign="WindowCenter" width="400px" 
                                />           
                        </SettingsPopup>
                        <Columns>
                            <dx:GridViewCommandColumn ShowEditButton="True" 
                                ShowNewButtonInHeader="True" ShowDeleteButton="True"
                                VisibleIndex="0" ></dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Did" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="CommentDate" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="CommentReason" VisibleIndex="5" Visible="true">
                                <PropertiesComboBox TextField="CommentDescription" IncrementalFilteringMode="Contains" ValueField="Aid" DataSourceID="odsDriverCommentsReason"></PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            
                        </Columns>
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
            <SettingsDetail ShowDetailRow="true" />
            <SettingsPager EnableAdaptivity="true" />
        </dx:ASPxGridView>
        <br />
        <table style="width: 200px">
            <tr>
                <td style="width: 50px">
                    <dx:ASPxButton ID="btnComment" runat="server" Text="Driver Comment Reason" Width="100%" OnClick="btnComment_Click">
                    </dx:ASPxButton>
                </td>
                <td style="width: 50px">
                    <dx:ASPxButton ID="btnAllocate" runat="server" Text="Allocate Runs To Different Driver" Width="100%" OnClick="btnAllocate_Click">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxPopupControl ID="pupComment" runat="server" ClientInstanceName="pupComment" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="500px" 
                        AllowDragging="True" ShowHeader="False"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" >
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="lblComment" runat="server" Text="Driver Comment Reason"></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gvDriverCommentReason" runat="server" AutoGenerateColumns="false" 
                                    KeyFieldName="Aid" DataSourceID="odsDriverCommentsReason" Width="100%">
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" 
                                            ShowNewButtonInHeader="True" ShowDeleteButton="True"
                                            VisibleIndex="0" ></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CommentDescription" VisibleIndex="3" Visible="true" Width="500px">
                                            <PropertiesTextEdit Width="200px"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        
                                    </Columns>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        
                    </table>
                    <br />
                    <dx:ASPxButton ID="btnCloseComments" runat="server" Text="Close" OnClick="btnCloseComments_Click"></dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="pupAllocate" runat="server" ClientInstanceName="pupAllocate" 
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="500px" 
                        AllowDragging="True" ShowHeader="False"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" >
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Allocate Runs To Different Driver"></dx:ASPxLabel>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Driver to remove runs from:"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="cboDriverFrom" runat="server" DropDownStyle="DropDownList" DataSourceID="odsAllocateDriverFrom"
                                    ValueField="Did" ValueType="System.String" TextFormatString="{0}" Width="100%">
                                    <Columns>
                                        <%--<dx:ListBoxColumn FieldName="DriverID" Width="100px"/>--%>
                                        <dx:ListBoxColumn FieldName="DriverName" Width="300px" />
                                    </Columns>
                                </dx:ASPxComboBox>
                                <%--<dx:ASPxComboBox ID="cboDriverFrom" runat="server" AutoGenerateColumns="false" 
                                    DropDownStyle="DropDownList" DataSourceID="odsAllocateDriverFrom" 
                                    ValueField="DriverID" TextField="DriverName" 
                                    TextFormatString="{1}" EnableCallbackMode="true" 
                                    IncrementalFilteringMode="StartsWith" CallbackPageSize="30">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="DriverID" Width="30px" Visible="false"/>
                                        <dx:ListBoxColumn FieldName="DriverName" Width="100px" />
                                    </Columns>
                                </dx:ASPxComboBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Driver to allocate runs to:"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="cboDriverTo" runat="server" DropDownStyle="DropDownList" DataSourceID="odsAllocateDriverTo"
                                    ValueField="Did" ValueType="System.String" TextFormatString="{0}" Width="100%">
                                    <Columns>
                                        <%--<dx:ListBoxColumn FieldName="DriverID" Width="100px"/>--%>
                                        <dx:ListBoxColumn FieldName="DriverName" Width="300px" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <dx:ASPxButton ID="btnChangeRun" runat="server" Text="ChangeRun" OnClick="btnChangeRun_Click"></dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCloseAllocate" runat="server" Text="Close" OnClick="btnCloseAllocate_Click"></dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <br />
                    
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
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
        <asp:ObjectDataSource ID="odsDriver" runat="server" SelectMethod="GetAllPerApplication" 
            TypeName="FMS.Business.DataObjects.tblDrivers" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDrivers" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsDriverComments" runat="server" SelectMethod="GetAllPerApplication" 
            TypeName="FMS.Business.DataObjects.tblDriverComments" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDriverComments" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsDriverCommentsReason" runat="server" SelectMethod="GetAllPerApplication" 
            TypeName="FMS.Business.DataObjects.tblDriverCommentsReason" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDriverCommentsReason" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsAllocateDriverFrom" runat="server" SelectMethod="GetAllPerApplicationMinusInActive" 
            TypeName="FMS.Business.DataObjects.tblDrivers" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDrivers" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsAllocateDriverTo" runat="server" SelectMethod="GetAllPerApplicationMinusInActive" 
            TypeName="FMS.Business.DataObjects.tblDrivers" 
            DataObjectTypeName="FMS.Business.DataObjects.tblDrivers" 
            DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
        
    </form>
</body>
</html>
