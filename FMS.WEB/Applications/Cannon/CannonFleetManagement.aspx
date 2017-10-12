<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="CannonFleetManagement.aspx.vb" Inherits="FMS.WEB.CannonFleetManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxPageControl ID="CannonFleetManagementPageControl" runat="server">
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
                                <dx:GridViewDataTextColumn FieldName="RunName" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KeyNumber" VisibleIndex="3" Visible="true" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsRun" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Run" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Run" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRunMultiDocs" runat="server" SelectMethod="GetAllByRun" TypeName="FMS.Business.DataObjects.Cannon_Document" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Document" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="RunID" DbType="Guid" Name="RID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn> 
                                <%--<dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="3" Visible="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="4" Visible="true"></dx:GridViewDataTextColumn>--%>
                            </Columns>
                                <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Client" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Client" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsMultiDocs" runat="server" SelectMethod="GetAllByClient" TypeName="FMS.Business.DataObjects.Cannon_Document" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Document" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ClientID" DbType="Guid" Name="CID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsCustomerList" runat="server" SelectMethod="GetAllCustomer" TypeName="FMS.Business.DataObjects.Cannon_Client"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <%--<dx:TabPage Name="Drivers" Text="Drivers">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxHyperLink ID="lnkNewDriver" Theme="SoftOrange" ClientInstanceName="lnkNewDriver" runat="server" Text="Add New Driver" NavigateUrl="javascript:dgvDrivers.AddNewRow()">
                        </dx:ASPxHyperLink>
                        <dx:ASPxGridView SettingsBehavior-ConfirmDelete="true" SettingsText-ConfirmDelete="Are you sure you wish to delete the driver?"
                            SettingsPager-PageSize="3" ClientInstanceName="dgvDrivers" KeyFieldName="ApplicationDriverID" ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="odsDrivers" Theme="SoftOrange">
                            <Templates>
                                <DataRow>
                                    <table style="table-layout: fixed;">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="New" NavigateUrl="javascript:" OnInit="hyperLinkNew_Init">
                                                            </dx:ASPxHyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="Edit" NavigateUrl="javascript:" OnInit="hyperLinkEdit_Init">
                                                            </dx:ASPxHyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxHyperLink ID="ASPxHyperLink4" runat="server" Text="Delete" NavigateUrl="javascript:" OnInit="hyperLinkDelete_Init">
                                                            </dx:ASPxHyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="dataviewtd">
                                                <dx:ASPxBinaryImage ID="ASPxBinaryImage1" Value='<%# Eval("PhotoBinary") %>' runat="server" Height="119px" Width="119px">
                                                </dx:ASPxBinaryImage>
                                            </td>
                                            <td>
                                                <table class="dataviewtd" style="table-layout: fixed;">
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Forname:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="dataviewtd" style="word-wrap: break-word;">
                                                            <dx:ASPxLabel Width="100" ID="Label1" runat="server" Text='<%# Eval("FirstName")%>'>
                                                            </dx:ASPxLabel>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Surname:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="dataviewtd" style="word-wrap: break-word;">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%# Eval("Surname")%>'>
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Phone #:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="dataviewtd" style="word-wrap: break-word;">
                                                            <dx:ASPxLabel ID="Label2f" runat="server" Text='<%# Eval("PhoneNumber")%>'>
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Email:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="dataviewtd" style="word-wrap: break-word; max-width: 250px; width: 250px">
                                                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text='<%# Eval("EmailAddress")%>'>
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="dataviewtd">
                                                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Business Loc.:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="dataviewtd" style="word-wrap: break-word; max-width: 250px; width: 250px">
                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text='<%# Eval("ApplicationLocation")%>'>
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="4" class="dataviewtd">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Notes:">
                                                </dx:ASPxLabel>
                                                <dx:ASPxMemo ID="ASPxMemo1" ReadOnly="true" Text='<%# Eval("Notes")%>' runat="server" Height="80px" Width="300px">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                    </table>
                                </DataRow>

                                <EditForm>
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                    <div style="text-align: right">
                                        <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                            <ClientSideEvents Click="function (s, e) { 
                                dgvDrivers.UpdateEdit();
                                    sendMsg('Record Saved!');
                                }" />
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                                    </div>
                                </EditForm>
                            </Templates>
                            <SettingsPager PageSize="3"></SettingsPager>
                            <Settings ShowColumnHeaders="False"></Settings>
                            <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                            <SettingsSearchPanel Visible="True" />
                            <SettingsText ConfirmDelete="Are you sure you wish to delete the driver?"></SettingsText>
                            <EditFormLayoutProperties ColCount="2">
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="PhotoBinary" RowSpan="5">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="Surname">
                                        <CaptionSettings HorizontalAlign="Center" Location="Left" />
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="First Name" Caption="Forname:">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="Email Address" Caption="Email     ">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="Phone Number" Caption="Phone #">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="Notes" Caption="Notes     ">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="ApplicationLocationID" Caption="Application Location     ">
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                    </dx:EditModeCommandLayoutItem>
                                </Items>
                            </EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ApplicationDriverID" Visible="false" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FirstName" ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Surname" ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PhoneNumber" ShowInCustomizationForm="True" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PhotoLocation" ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="ApplicationLocationID" ShowInCustomizationForm="True" VisibleIndex="8">
                                    <PropertiesComboBox DataSourceID="odsApplicationLocation" TextField="Name" ValueField="ApplicationLocationID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataBinaryImageColumn ShowInCustomizationForm="True" VisibleIndex="2" FieldName="PhotoBinary">
                                    <PropertiesBinaryImage EnableClientSideAPI="True">
                                        <EditingSettings Enabled="True">
                                            <UploadSettings UploadMode="Advanced">
                                                <UploadValidationSettings MaxFileSize="300000"></UploadValidationSettings>
                                            </UploadSettings>
                                        </EditingSettings>
                                    </PropertiesBinaryImage>
                                </dx:GridViewDataBinaryImageColumn>
                                <dx:GridViewDataTextColumn Caption="Email Address"
                                    FieldName="EmailAddress"
                                    Name="Email"
                                    ShowInCustomizationForm="True"
                                    VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataMemoColumn FieldName="Notes" ShowInCustomizationForm="True" VisibleIndex="8">
                                    <PropertiesMemoEdit Columns="50" Height="100px">
                                    </PropertiesMemoEdit>
                                </dx:GridViewDataMemoColumn>
                            </Columns>
                        </dx:ASPxGridView>


                        <asp:ObjectDataSource ID="odsApplicationLocation" runat="server" SelectMethod="GetAllIncludingInheritFromApplication" TypeName="FMS.Business.DataObjects.ApplicationLocation">
                            <SelectParameters>
                                <asp:SessionParameter DbType="Guid" Name="ApplicationID" SessionField="ApplicationID" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsDrivers" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationDriver" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.ApplicationDriver" UpdateMethod="Update">
                            <SelectParameters>
                                <asp:SessionParameter DbType="Guid" Name="applicatoinid" SessionField="ApplicationID" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
            <dx:TabPage Name="RunCompletion" Text="Run Assignments">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="RunCompletionGridView" runat="server" DataSourceID="odsRunCompletion" KeyFieldName="RunCompletionID" Width="550px" Theme="SoftOrange">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RunCompletionID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="RunID" Caption="Run Name" VisibleIndex="2">
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
                        <asp:ObjectDataSource ID="odsRunCompletion" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_RunCompletion" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_RunCompletion" UpdateMethod="Update"></asp:ObjectDataSource>
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
                                <dx:GridViewDataComboBoxColumn FieldName="RunID" Caption="Run Name" VisibleIndex="2">
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
                        <asp:ObjectDataSource ID="odsRunClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_RunClient" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_RunClient" UpdateMethod="Update"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
