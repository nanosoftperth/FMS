<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClientAssignments.aspx.vb" Inherits="FMS.WEB.ClientAssignments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <script>
        function RunClicked(s, e) {
            $.ajax({
                type: "POST",
                url: "ClientAssignments.aspx/GetRunValues",
                dataType: "json",
                data: JSON.stringify({ Rid: s.GetValue() }),
                contentType: "application/json",
                crossDomain: true,
                success: function (data) {
                    if (data.d != null) {
                        RunNumber.SetValue(data.d.RunNUmber);
                        RunDriver.SetValue(data.d.RunDriver)
                        RunName.SetValue(data.d.RunDescription)
                    }
                },
                error: function () {
                }
            });
        }
        function ClientCustomerClicked(s, e) {
            $.ajax({
                type: "POST",
                url: "ClientAssignments.aspx/GetClientCustomerValues",
                dataType: "json",
                data: JSON.stringify({ Cid: s.GetValue() }),
                contentType: "application/json",
                crossDomain: true,
                success: function (data) {
                    if (data.d != null) {
                        Name.SetValue(data.d.CustomerName);
                        Address.SetValue(data.d.AddressLine1)
                        AddressLine2.SetValue(data.d.AddressLine2)
                        Suburb.SetValue(data.d.Suburb)
                        PostCode.SetValue(data.d.PostCode)
                        CustomerContactName.SetValue(data.d.CustomerContactName)
                        CustomerPhone.SetValue(data.d.CustomerPhone)
                        CustomerMobile.SetValue(data.d.CustomerMobile)
                        CustomerFax.SetValue(data.d.CustomerFax)
                        CustomerComments.SetValue(data.d.CustomerComments)
                    }
                },
                error: function () {
                }
            });
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <dx:ASPxPageControl ID="FleetManagementPageControl" runat="server">
        <TabPages>
            <dx:TabPage Name="Run" Text="Run">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="RunGridView" runat="server" DataSourceID="odsRun" KeyFieldName="RunID" Width="550px" Theme="SoftOrange">
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="RunDocGridView" runat="server" ClientInstanceName="RunDocGridView" DataSourceID="odsRunMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange" 
                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="RunGridView_BeforePerformDataSelect">
                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304"/> 
                                                </PropertiesBinaryImage>
                                            </dx:GridViewDataBinaryImageColumn>
                                        </Columns>
                                        <Settings ShowPreview="true" />
                                        <SettingsPager PageSize="10" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Rid" Caption="Run Description" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="odsRunsList" TextField="RunDescription" ValueField="Rid">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){RunClicked(s,e);}" />
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn> 
                                <dx:GridViewDataComboBoxColumn FieldName="RunDriver" Caption="Run Driver" VisibleIndex="3" PropertiesComboBox-ClientInstanceName="RunDriver" ReadOnly="true">
                                    <PropertiesComboBox DataSourceID="odsDriverList" TextField="DriverName" ValueField="Did">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn> 
                                <dx:GridViewDataTextColumn FieldName="RunNumber" VisibleIndex="4" PropertiesTextEdit-ClientInstanceName="RunNumber" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RunName" VisibleIndex="5" PropertiesTextEdit-ClientInstanceName="RunName" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsRun" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetRun" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetRun" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRunMultiDocs" runat="server" SelectMethod="GetAllByRun" TypeName="FMS.Business.DataObjects.FleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="RunID" DbType="Guid" Name="RID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRunsList" runat="server" SelectMethod="GetRunList" TypeName="FMS.Business.DataObjects.FleetRun"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsDriverList" runat="server" SelectMethod="GetAllPerApplication" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="Client" Text="Client">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="ClientGridView" runat="server" DataSourceID="odsClient" KeyFieldName="ClientID" Width="650px" Theme="SoftOrange">
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="DocGridView" runat="server" ClientInstanceName="DocGridView" DataSourceID="odsMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange" 
                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="ClientGridView_BeforePerformDataSelect">
                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ClientID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304"/> 
                                                </PropertiesBinaryImage>
                                            </dx:GridViewDataBinaryImageColumn>
                                        </Columns>
                                        <Settings ShowPreview="true" />
                                        <SettingsPager PageSize="10" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ClientID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="CustomerID" Caption="Customer Name" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="odsCustomerList" TextField="Name" ValueField="CustomerID">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ClientCustomerClicked(s,e);}" />
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn> 
                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" PropertiesTextEdit-ClientInstanceName="Name" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="4" PropertiesTextEdit-ClientInstanceName="Address" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="200"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddressLine2" VisibleIndex="5" PropertiesTextEdit-ClientInstanceName="AddressLine2" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="200"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="6" PropertiesTextEdit-ClientInstanceName="Suburb" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="7" PropertiesTextEdit-ClientInstanceName="PostCode" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="4"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerContactName" VisibleIndex="8" PropertiesTextEdit-ClientInstanceName="CustomerContactName" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerPhone" VisibleIndex="9" PropertiesTextEdit-ClientInstanceName="CustomerPhone" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerMobile" VisibleIndex="10" PropertiesTextEdit-ClientInstanceName="CustomerMobile" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerFax" VisibleIndex="11" PropertiesTextEdit-ClientInstanceName="CustomerFax" Visible="true" ReadOnly="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerComments" VisibleIndex="12" PropertiesTextEdit-ClientInstanceName="CustomerComments" Visible="true" ReadOnly="true"></dx:GridViewDataTextColumn>
                            </Columns>
                                <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetClient" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetClient" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsMultiDocs" runat="server" SelectMethod="GetAllByClient" TypeName="FMS.Business.DataObjects.fleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ClientID" DbType="Guid" Name="CID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsCustomerList" runat="server" SelectMethod="GetAllCustomer" TypeName="FMS.Business.DataObjects.FleetClient"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="RunCompletion" Text="Run Assignments">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="RunCompletionGridView" runat="server" DataSourceID="odsRunCompletion" KeyFieldName="RunCompletionID" Width="550px" Theme="SoftOrange">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RunCompletionID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="RunID" Caption="Run Name" VisibleIndex="2" SortIndex="0" SortOrder="Ascending">
                                    <PropertiesComboBox DataSourceID="odsRun" TextField="RunName" ValueField="RunID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                                <dx:GridViewDataComboBoxColumn FieldName="DriverID" Caption="Driver Name" VisibleIndex="3">
                                    <PropertiesComboBox DataSourceID="odsTblDrivers" TextField="DriverName" ValueField="DriverID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                                <dx:GridViewDataDateColumn FieldName="RunDate" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                            </Columns>
                                <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsRunCompletion" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetRunCompletion" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetRunCompletion" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsTblDrivers" runat="server" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.usp_GetAllDrivers"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="RunClient" Text="Run Client">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="RunClientGridView" runat="server" DataSourceID="odsRunClient" KeyFieldName="RunClientID" Width="550px" Theme="SoftOrange">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RunClientID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="RunID" Caption="Run Name" VisibleIndex="2" SortIndex="0" SortOrder="Ascending">
                                    <PropertiesComboBox DataSourceID="odsRun" TextField="RunName" ValueField="RunID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                                <dx:GridViewDataComboBoxColumn FieldName="ClientID" Caption="Client" VisibleIndex="3">
                                    <PropertiesComboBox DataSourceID="odsClient" TextField="Name" ValueField="ClientID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                            </Columns>
                                <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsRunClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetRunClient" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetRunClient" UpdateMethod="Update"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</div>
    </form>
</body>
</html>
