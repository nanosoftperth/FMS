<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserSecurity.aspx.vb" Inherits="FMS.WEB.UserSecurity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            cltGrid.DeleteRow(visibleIndex);
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
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="User Security" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>            
        </div>
        <br />
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="false" 
            KeyFieldName="usersecID" DataSourceID="odsUsers" Width="100%" Theme="SoftOrange"
            OnRowInserting="Grid_RowInserting"
            OnRowUpdating="Grid_RowUpdating"
            OnStartRowEditing="Grid_StartRowEditing" 
            OnInitNewRow="Grid_InitNewRow"
            ClientInstanceName="cltGrid">
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="700px" />
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
                <dx:GridViewDataTextColumn FieldName="usersecID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="User">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="txtUserName" VisibleIndex="2" Visible="true" Caption="User Name"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="Administrator" VisibleIndex="3" Visible="true" Caption="Administrator"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="UserPassword" VisibleIndex="4" Visible="true" 
                            Caption="Password" PropertiesTextEdit-Password="true">
                            <%--<PropertiesTextEdit Password="True" ClientInstanceName="psweditor"></PropertiesTextEdit>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtPassword" runat="server" AutoGenerateColumns="false"
                                    OnInit="txtPassword_Init">                                 
                                </dx:ASPxTextBox>
                            </EditItemTemplate>--%>
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
                <dx:GridViewBandColumn Caption="Main Menu Items" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                       
                        <dx:GridViewDataCheckColumn FieldName="lblCustomerDetails" VisibleIndex="5" Visible="true" Caption="Customer Details"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="lblSites" VisibleIndex="6" Visible="true" Caption="Sites"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="lblMaintenance" VisibleIndex="7" Visible="true" Caption="Maintenance"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="lblReports" VisibleIndex="8" Visible="true" Caption="Reports"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="lblOtherProcesses" VisibleIndex="9" Visible="true" Caption="Other Processes"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Maintenance Menu Items" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <dx:GridViewDataCheckColumn FieldName="cmdServices" VisibleIndex="10" Visible="true" Caption="Services"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Toggle41" VisibleIndex="11" Visible="true" Caption="Driver Details"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Toggle42" VisibleIndex="12" Visible="true" Caption="Runs"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdIndustryGroups" VisibleIndex="13" Visible="true" Caption="Industry Groups"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdInvoicingFrequency" VisibleIndex="14" Visible="true" Caption="Invoicing Frequency"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdPubHolReg" VisibleIndex="14" Visible="true" Caption="Public Holiday Register"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdSalesPersons" VisibleIndex="14" Visible="true" Caption="Sales Persons"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdCeaseReasons" VisibleIndex="14" Visible="true" Caption="Contract Cease reasons"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdCycles" VisibleIndex="14" Visible="true" Caption="Run F/N Cycles"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdTurnOffAuditing" VisibleIndex="14" Visible="true" Caption="Turn On/Off Auditing"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdAuditChangeReason" VisibleIndex="14" Visible="true" Caption="Audit Change Reasons"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdAreas" VisibleIndex="14" Visible="true" Caption="Zones"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Command55" VisibleIndex="14" Visible="true" Caption="Service Frequency"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Reports Menu" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <dx:GridViewDataCheckColumn FieldName="CmdContractRenewalReport" VisibleIndex="15" Visible="true" Caption="Contract Renewals"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdQuickViewBySuburb" VisibleIndex="16" Visible="true" Caption="Quick View By Suburb"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdAuditReport" VisibleIndex="17" Visible="true" Caption="Audit Report G & L"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdProductsReport" VisibleIndex="18" Visible="true" Caption="Service List"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdRunReport" VisibleIndex="19" Visible="true" Caption="Run Listing"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdRunValue2" VisibleIndex="20" Visible="true" Caption="Run Values"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdRunValue" VisibleIndex="21" Visible="true" Caption="Run Value Summary"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Command13" VisibleIndex="22" Visible="true" Caption="Annual Analysis"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdCustomerSummary" VisibleIndex="23" Visible="true" Caption="Customer Contract Details"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdSiteReport" VisibleIndex="24" Visible="true" Caption="Site List"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Command9" VisibleIndex="25" Visible="true" Caption="Industry List"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Command10" VisibleIndex="26" Visible="true" Caption="Revenue Report By Zone"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdSalesSummaryDislocations" VisibleIndex="27" Visible="true" Caption="Gains & Losses"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="CmdDrivesLicenseExpiry" VisibleIndex="27" Visible="true" Caption="Drivers License Expiry"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="Command14" VisibleIndex="27" Visible="true" Caption="Per Annum Value"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdServiceSummary" VisibleIndex="27" Visible="true" Caption="Service Summary"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdInvoicing" VisibleIndex="27" Visible="true" Caption="Invoicing"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdLengthOfService" VisibleIndex="27" Visible="true" Caption="Length Of Service"></dx:GridViewDataCheckColumn>
                        <dx:GridViewDataCheckColumn FieldName="cmdSitesWithNoContract" VisibleIndex="27" Visible="true" Caption="Sites With No Contract"></dx:GridViewDataCheckColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                
            </Columns>
            <EditFormLayoutProperties>
                <Items>
                    <dx:GridViewLayoutGroup GroupBoxDecoration="Box" Caption="User" ColCount="2" Width="600px">
                        <Items>
                            <dx:GridViewColumnLayoutItem ColumnName="txtUserName" />
                            <dx:GridViewColumnLayoutItem ColumnName="Administrator" />
                            <dx:GridViewColumnLayoutItem ColumnName="UserPassword" />
                            <dx:GridViewColumnLayoutItem ColumnName="UserGroup" />
                            
                        </Items>
                    </dx:GridViewLayoutGroup>
                    <dx:GridViewLayoutGroup GroupBoxDecoration="Box" Caption="Main Menu Items" ColCount="2" Width="600px">
                        <Items>
                            <dx:GridViewColumnLayoutItem ColumnName="lblCustomerDetails" />
                            <dx:GridViewColumnLayoutItem ColumnName="lblSites" />
                            <dx:GridViewColumnLayoutItem ColumnName="lblMaintenance" />
                            <dx:GridViewColumnLayoutItem ColumnName="lblReports" />
                            <dx:GridViewColumnLayoutItem ColumnName="lblOtherProcesses" />                            
                        </Items>
                    </dx:GridViewLayoutGroup>
                    <dx:GridViewLayoutGroup GroupBoxDecoration="Box" Caption="Maintenance Menu Items" ColCount="3" Width="600px">
                        <Items>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdServices" />
                            <dx:GridViewColumnLayoutItem ColumnName="Toggle41" />
                            <dx:GridViewColumnLayoutItem ColumnName="Toggle42" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdIndustryGroups" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdInvoicingFrequency" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdPubHolReg" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdSalesPersons" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdCeaseReasons" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdCycles" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdTurnOffAuditing" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdAuditChangeReason" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdAreas" />
                            <dx:GridViewColumnLayoutItem ColumnName="Command55" />

                        </Items>
                    </dx:GridViewLayoutGroup>
                    <dx:GridViewLayoutGroup GroupBoxDecoration="Box" Caption="Reports Menu" ColCount="4" Width="600px">
                        <Items>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdContractRenewalReport" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdQuickViewBySuburb" />
                            <dx:GridViewColumnLayoutItem ColumnName="CmdAuditReport"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdProductsReport"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdRunReport"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdRunValue2"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdRunValue"/>
                            <dx:GridViewColumnLayoutItem ColumnName="Command13"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdCustomerSummary"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdSiteReport"/>
                            <dx:GridViewColumnLayoutItem ColumnName="Command9"/>
                            <dx:GridViewColumnLayoutItem ColumnName="Command10"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdSalesSummaryDislocations"/>
                            <dx:GridViewColumnLayoutItem ColumnName="CmdDrivesLicenseExpiry"/>
                            <dx:GridViewColumnLayoutItem ColumnName="Command14"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdServiceSummary"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdInvoicing"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdLengthOfService"/>
                            <dx:GridViewColumnLayoutItem ColumnName="cmdSitesWithNoContract"/>
                        </Items>
                    </dx:GridViewLayoutGroup>
                    <dx:EditModeCommandLayoutItem ShowUpdateButton="true" ShowCancelButton="true">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            

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
