<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="CannonFleetManagement.aspx.vb" Inherits="FMS.WEB.CannonFleetManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server">
        <TabPages>
            <dx:TabPage Name="RunSheet" Text="Run Sheet">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                            <dx:ASPxPageControl ID="CannonFleetManagementPageControl" runat="server">
                                <TabPages>
                                    <dx:TabPage Name="Document" Text="Document">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <dx:ASPxGridView ID="DocumentGridView" runat="server" DataSourceID="odsDocuments" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange">
                                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
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
                                                <asp:ObjectDataSource ID="odsDocuments" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Document" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Document" UpdateMethod="Update"></asp:ObjectDataSource>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="Run" Text="Run">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <dx:ASPxGridView ID="RunGridView" runat="server" DataSourceID="odsRun" KeyFieldName="RunID" Width="550px" Theme="SoftOrange">
                                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="RunName" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="OtherFields" VisibleIndex="3" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="AttributeName" VisibleIndex="4" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="DocumentID" Caption="Scan Description" VisibleIndex="5">
                                                            <PropertiesComboBox DataSourceID="odsDocuments" TextField="Description" ValueField="DocumentID">
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>  
                                                        <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary" Caption="Scan Image">
                                                            <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                                <EditingSettings Enabled="false" UploadSettings-UploadValidationSettings-MaxFileSize="4194304"/> 
                                                            </PropertiesBinaryImage>
                                                        </dx:GridViewDataBinaryImageColumn>
                                                    </Columns>
                                                     <Settings ShowPreview="true" />
                                                    <SettingsPager PageSize="10" />
                                                </dx:ASPxGridView>
                                                <asp:ObjectDataSource ID="odsRun" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Run" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Run" UpdateMethod="Update"></asp:ObjectDataSource>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="Client" Text="Client">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <dx:ASPxGridView ID="ClientGridView" runat="server" DataSourceID="odsClient" KeyFieldName="ClientID" Width="550px" Theme="SoftOrange">
                                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ClientID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="3" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="OtherFields" VisibleIndex="4" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn FieldName="DocumentID" Caption="Scan Description" VisibleIndex="5">
                                                            <PropertiesComboBox DataSourceID="odsDocuments" TextField="Description" ValueField="DocumentID">
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>  
                                                        <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary" Caption="Scan Image">
                                                            <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                                <EditingSettings Enabled="false" UploadSettings-UploadValidationSettings-MaxFileSize="4194304"/> 
                                                            </PropertiesBinaryImage>
                                                        </dx:GridViewDataBinaryImageColumn>
                                                    </Columns>
                                                     <Settings ShowPreview="true" />
                                                    <SettingsPager PageSize="10" />
                                                </dx:ASPxGridView>
                                                <asp:ObjectDataSource ID="odsClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Client" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Client" UpdateMethod="Update"></asp:ObjectDataSource>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="Driver" Text="Driver">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <dx:ASPxGridView ID="DriverGridView" runat="server" DataSourceID="odsDriver" KeyFieldName="DriverID" Width="550px" Theme="SoftOrange">
                                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DriverID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="OtherFields" VisibleIndex="3" Visible="true"></dx:GridViewDataTextColumn>
                                                    </Columns>
                                                     <Settings ShowPreview="true" />
                                                    <SettingsPager PageSize="10" />
                                                </dx:ASPxGridView>
                                                <asp:ObjectDataSource ID="odsDriver" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_Driver" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_Driver" UpdateMethod="Update"></asp:ObjectDataSource>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="RunCompletion" Text="Run Completion">
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
                                                        <dx:GridViewDataComboBoxColumn FieldName="DriverID" Caption="Driver Name" VisibleIndex="2">
                                                            <PropertiesComboBox DataSourceID="odsDriver" TextField="Name" ValueField="DriverID">
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>  
                                                        <dx:GridViewDataDateColumn FieldName="RunDate" VisibleIndex="3" Visible="true"></dx:GridViewDataDateColumn>
                                                    </Columns>
                                                     <Settings ShowPreview="true" />
                                                    <SettingsPager PageSize="10" />
                                                </dx:ASPxGridView>
                                                <asp:ObjectDataSource ID="odsRunCompletion" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Cannon_RunCompletion" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.Cannon_RunCompletion" UpdateMethod="Update"></asp:ObjectDataSource>
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
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="Document" Text="Document">
                <ContentCollection>
                    <dx:ContentControl runat="server">

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>

</asp:Content>
