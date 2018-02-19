<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClientAssignments.aspx.vb" Inherits="FMS.WEB.ClientAssignments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <script>
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
                        <dx:ASPxGridView ID="RunGridView" runat="server" DataSourceID="odsTblRuns" KeyFieldName="Rid" Width="550px" Theme="SoftOrange" AutoGenerateColumns="False"
                            OnRowValidating="RunGridView_RowValidating">
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3"/>
                            <SettingsPopup>
                                <EditForm  Modal="true" 
                                    VerticalAlign="WindowCenter" 
                                    HorizontalAlign="WindowCenter" width="700px" Height="150px"/>
                            </SettingsPopup>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="RunDocGridView" runat="server" ClientInstanceName="RunDocGridView" DataSourceID="odsRunMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="RunGridView_BeforePerformDataSelect">
                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
                                        <SettingsPopup>
                                            <EditForm  Modal="true" 
                                                VerticalAlign="WindowCenter" 
                                                HorizontalAlign="WindowCenter" width="400px" Height="250px"/>
                                        </SettingsPopup>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
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
                                <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="1" PropertiesTextEdit-ClientInstanceName="RunNumber" Visible="false" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="2" PropertiesTextEdit-ClientInstanceName="RunName" Visible="false" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RunNUmber" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="4" SortOrder="Ascending"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="RunDriver" Caption="Run Driver" VisibleIndex="2" SortIndex="0" SortOrder="Ascending">
                                    <PropertiesComboBox DataSourceID="odsDriverList" TextField="DriverName" ValueField="Did">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                                <dx:GridViewDataCheckColumn FieldName="MondayRun" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="TuesdayRun" VisibleIndex="7"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="WednesdayRun" VisibleIndex="8"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="ThursdayRun" VisibleIndex="9"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="FridayRun" VisibleIndex="10"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="SaturdayRun" VisibleIndex="11"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="SundayRun" VisibleIndex="12"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="InactiveRun" VisibleIndex="13"></dx:GridViewDataCheckColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsTblRuns" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns" DataObjectTypeName="FMS.Business.DataObjects.tblRuns" DeleteMethod="DeleteRun" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRun" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetRun" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetRun" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRunMultiDocs" runat="server" SelectMethod="GetAllByRunRID" TypeName="FMS.Business.DataObjects.FleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="RunID" Name="RID" Type="Int32"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsRunsList" runat="server" SelectMethod="GetRunList" TypeName="FMS.Business.DataObjects.FleetRun"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsDriverList" runat="server" SelectMethod="GetAllPerApplication" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="Site" Text="Site">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="ClientGridView" runat="server" DataSourceID="odsSites" KeyFieldName="Cid" Width="650px" Theme="SoftOrange" AutoGenerateColumns="False">
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="DocGridView" runat="server" ClientInstanceName="DocGridView" DataSourceID="odsMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="ClientGridView_BeforePerformDataSelect">
                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
                                        <SettingsPopup>
                                            <EditForm  Modal="true" 
                                                VerticalAlign="WindowCenter" 
                                                HorizontalAlign="WindowCenter" width="400px" Height="250px"/>
                                        </SettingsPopup>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ClientID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                                                </PropertiesBinaryImage>
                                            </dx:GridViewDataBinaryImageColumn>
                                        </Columns>
                                        <Settings ShowPreview="true" />
                                        <SettingsPager PageSize="10" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="0" PropertiesTextEdit-ClientInstanceName="CustomerContactName" Visible="false" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteID" VisibleIndex="1" PropertiesTextEdit-ClientInstanceName="CustomerPhone" Visible="false" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" PropertiesTextEdit-ClientInstanceName="CustomerMobile" Visible="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteName" VisibleIndex="3" PropertiesTextEdit-ClientInstanceName="CustomerFax" Visible="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Customer" VisibleIndex="4" PropertiesTextEdit-ClientInstanceName="CustomerComments" Visible="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerSortOrder" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddressLine2" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddressLine3" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddressLine4" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StateSortOrder" VisibleIndex="12"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="13"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="14"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FaxNo" VisibleIndex="15"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteContactName" VisibleIndex="16"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteContactPhone" VisibleIndex="17"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteContactFax" VisibleIndex="18"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteContactMobile" VisibleIndex="19"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteContactEmail" VisibleIndex="20"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostalAddressLine1" VisibleIndex="21"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostalAddressLine2" VisibleIndex="22"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostalSuburb" VisibleIndex="23"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostalState" VisibleIndex="24"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PostalPostCode" VisibleIndex="25"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="SiteStartDate" VisibleIndex="26">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="SitePeriod" VisibleIndex="27"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InitialContractPeriodSortOrder" VisibleIndex="28"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="SiteContractExpiry" VisibleIndex="29">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="SiteCeaseDate" VisibleIndex="30">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="SiteCeaseReason" VisibleIndex="31"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ContractCeaseReasonsSortOrder" VisibleIndex="32"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceFrequency" VisibleIndex="33"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoicingFrequencySortOrder" VisibleIndex="34"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="InvoiceCommencing" VisibleIndex="35">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceCommencingString" VisibleIndex="36"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IndustryGroup" VisibleIndex="37"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IndustrySortOrder" VisibleIndex="38"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PreviousSupplier" VisibleIndex="39"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PreviousSupplierSortOrder" VisibleIndex="40"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="LostBusinessTo" VisibleIndex="41"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="LostBusinessToSortOrder" VisibleIndex="42"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SalesPerson" VisibleIndex="43"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SalesPersonSortOrder" VisibleIndex="44"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InitialServiceAgreementNo" VisibleIndex="45"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth1" VisibleIndex="46"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth2" VisibleIndex="47"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth3" VisibleIndex="48"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth4" VisibleIndex="49"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="GeneralSiteServiceComments" VisibleIndex="50"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TotalUnits" VisibleIndex="51"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TotalAmount" VisibleIndex="52"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Zone" VisibleIndex="53"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ZoneSortOrder" VisibleIndex="54"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="SeparateInvoice" VisibleIndex="55"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="PurchaseOrderNumber" VisibleIndex="56"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="chkSitesExcludeFuelLevy" VisibleIndex="57"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="cmbRateIncrease" VisibleIndex="58"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="cmbRateIncreaseSortOrder" VisibleIndex="59"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="60"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerRating" VisibleIndex="61"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CustomerRatingDesc" VisibleIndex="62"></dx:GridViewDataTextColumn>
                            </Columns>
                                <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsSites" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSites"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetClient" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetClient" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsMultiDocs" runat="server" SelectMethod="GetAllByClientCID" TypeName="FMS.Business.DataObjects.fleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ClientID" Name="CID" Type="Int32"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsCustomerList" runat="server" SelectMethod="GetAllCustomer" TypeName="FMS.Business.DataObjects.FleetClient"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="RunCompletion" Text="Run Assignments">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="GetRunsForAssignmentGridView" runat="server" KeyFieldName="UniqueID" DataSourceID="odsGetRunsForAssignment" 
                            Width="750px" Theme="SoftOrange" AutoGenerateColumns="False" OnRowValidating="GetRunsForAssignmentGridView_RowValidating">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"/>
                            <SettingsPopup>
                                <EditForm  Modal="true" 
                                    VerticalAlign="WindowCenter" 
                                    HorizontalAlign="WindowCenter" width="400px" Height="120px"/>
                            </SettingsPopup>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="UniqueID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RunNUmber" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="3" ReadOnly="true" SortOrder="Ascending"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="DriverId" Caption="Driver Name" VisibleIndex="4" SortIndex="0" SortOrder="Ascending">
                                    <PropertiesComboBox DataSourceID="odsDriverList" TextField="DriverName" ValueField="Did">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>  
                                <dx:GridViewDataDateColumn FieldName="DateOfRun" VisibleIndex="5" Visible="true"></dx:GridViewDataDateColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsGetRunsForAssignment" runat="server" DataObjectTypeName="FMS.Business.DataObjects.usp_GetRunsForAssignment" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.usp_GetRunsForAssignment" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsTblDrivers" runat="server" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.usp_GetAllDrivers"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="RunClient" Text="Run Site">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="RunSiteGridView" runat="server" DataSourceID="odsRunSite" KeyFieldName="RunSiteID" Width="550px" 
                            Theme="SoftOrange" AutoGenerateColumns="False" OnRowValidating="RunSiteGridView_RowValidating">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <SettingsEditing Mode="PopupEditForm"/>
                            <SettingsPopup>
                                <EditForm  Modal="true" 
                                    VerticalAlign="WindowCenter" 
                                    HorizontalAlign="WindowCenter" width="400px" Height="100px"/>
                            </SettingsPopup>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RunSiteID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Rid" VisibleIndex="1" Caption="Run">
                                    <PropertiesComboBox DataSourceID="odsTblRuns" TextField="RunDescription" ValueField="Rid">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Cid" VisibleIndex="2" Caption="Site">
                                    <PropertiesComboBox DataSourceID="odsSites" TextField="SiteName" ValueField="Cid">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsRunSite" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblRunSite" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRunSite" UpdateMethod="Update"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</div>
    </form>
</body>
</html>
