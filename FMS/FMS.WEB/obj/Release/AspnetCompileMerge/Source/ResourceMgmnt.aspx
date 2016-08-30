<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ResourceMgmnt.aspx.vb" Inherits="FMS.WEB.ResourceMgmnt" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        //dateEditDay, timeEditFrom, timeEditTo, dgvApplicationVehicleDriver
        Date.prototype.yyyymmdd = function () {
            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = this.getDate().toString();
            return yyyy + (mm[1] ? mm : "0" + mm[0]) + (dd[1] ? dd : "0" + dd[0]); // padding
        };


        Date.prototype.toStringReasonable = function () {
            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
            var dd = this.getDate().toString();
            return dd + '/' + mm + '/' + yyyy
        };


        function dateEditDay_DateChanged(s, e) {

            //dateEditDay, timeEditFrom, timeEditTo, dgvApplicationVehicleDriver
            var editDate = dateEditDay.GetValue().toStringReasonable();
            var editStartTime = timeEditFrom.GetValueString();
            var editEndTime = timeEditTo.GetValueString();
            var params = editDate + '|' + editStartTime + '|' + editEndTime

            dgvApplicationVehicleDriver.PerformCallback(params);
        }

    </script>


    <style type="text/css">
        .dataviewtd {
            padding: 5px;
        }

        .floatLeft {
            float: left;
            margin-left: 5px;
        }
    </style>


    <table>
        <tr>
            <td valign="top">
                <img style="width: 200px;" src="Content/Images/mC-settings.png" />

            </td>

            <td>
                <dx:ASPxPageControl Width="700px" ID="pageControlMain" runat="server" ActiveTabIndex="0">
                    <TabPages>
                        <dx:TabPage Text="Assign Drivers to Vehicles">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div style="width: 657px;">

                                        <div>
                                            <div>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" CssClass="floatLeft" Text="Select date:"></dx:ASPxLabel>
                                                <dx:ASPxDateEdit CssClass="floatLeft" ID="dateEditDay" ClientInstanceName="dateEditDay" runat="server" EnableTheming="True" Theme="SoftOrange">
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties>
                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                        </TimeEditProperties>
                                                    </TimeSectionProperties>

                                                    <ClientSideEvents DateChanged="function(s,e){dateEditDay_DateChanged(s,e);}" />

                                                    <ClearButton Visibility="Auto"></ClearButton>

                                                </dx:ASPxDateEdit>
                                            </div>

                                            <div class="floatright">

                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" CssClass="floatLeft" Text="Start time:"></dx:ASPxLabel>
                                                <dx:ASPxTimeEdit ID="timeEditFrom" Width="90px" ClientInstanceName="timeEditFrom" CssClass="floatLeft" runat="server" Theme="SoftOrange">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </dx:ASPxTimeEdit>

                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" CssClass="floatLeft" Text="End time:"></dx:ASPxLabel>
                                                <dx:ASPxTimeEdit ID="timeEditTo" Width="90px" ClientInstanceName="timeEditTo" CssClass="floatLeft" runat="server" Theme="SoftOrange">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </dx:ASPxTimeEdit>

                                            </div>
                                        </div>


                                        <table>
                                            <tr>
                                                <td valign="bottom" halign="right">
                                                    <dx:ASPxLabel ID="ASPxLabel6"
                                                        runat="server"
                                                        CssClass="floatright"
                                                        ForeColor="Red"
                                                        Text="Click on a cell to edit its value...">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>

                                                    <div class="floatLeft" style="padding-top: 4px">

                                                        <dx:ASPxGridView ID="dgvApplicationVehicleDriver"
                                                            runat="server"
                                                            SettingsEditing-BatchEditSettings-EditMode="Row"
                                                            AutoGenerateColumns="False"
                                                            Width="650px"
                                                            KeyFieldName="ApplicationVehicleDriverTimeID"
                                                            DataSourceID="odsApplicationVehicleDriverTimeslot"
                                                            ClientInstanceName="dgvApplicationVehicleDriver"
                                                            Theme="SoftOrange">

                                                            <EditFormLayoutProperties ColCount="4">
                                                                <Items>

                                                                    <dx:GridViewColumnLayoutItem ColumnName="ApplicationVehicleDriverTimeID" Visible="false"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="VehicleID"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="StartDate"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="ApplicationDriverId" Caption="Driver"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="PassengerID" Caption="Driver"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem ColumnName="EndDate"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right"></dx:EditModeCommandLayoutItem>
                                                                </Items>
                                                            </EditFormLayoutProperties>
                                                            <ClientSideEvents />
                                                            <SettingsEditing Mode="Batch">
                                                                <BatchEditSettings EditMode="Row"></BatchEditSettings>
                                                            </SettingsEditing>
                                                            <Columns>
                                                                <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicationVehicleDriverTimeID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>

                                                                <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="G" PropertiesDateEdit-EditFormat="DateTime" FieldName="StartDate" VisibleIndex="6">

                                                                    <PropertiesDateEdit>
                                                                        <TimeSectionProperties Visible="True">
                                                                            <TimeEditProperties>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                            </TimeEditProperties>
                                                                        </TimeSectionProperties>

                                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>

                                                                <dx:GridViewDataDateColumn PropertiesDateEdit-DisplayFormatString="G" PropertiesDateEdit-EditFormat="DateTime" FieldName="EndDate" VisibleIndex="7">
                                                                    <PropertiesDateEdit>
                                                                        <TimeSectionProperties Visible="True">
                                                                            <TimeEditProperties>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                            </TimeEditProperties>
                                                                        </TimeSectionProperties>

                                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>


                                                                <dx:GridViewDataTextColumn FieldName="ApplicationVehicleDriverTimeID" VisibleIndex="10" Visible="False"></dx:GridViewDataTextColumn>

                                                                <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="11" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="5" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="VehicleName" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="3" Visible="False"></dx:GridViewDataTextColumn>

                                                                <dx:GridViewDataComboBoxColumn FieldName="VehicleID" VisibleIndex="4" Caption="Vehicle">
                                                                    <PropertiesComboBox DataSourceID="odsAVDTVehicles" TextField="Name" ValueField="ApplicationVehileID">
                                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>

                                                                <dx:GridViewDataComboBoxColumn PropertiesComboBox-NullDisplayText=" "
                                                                    FieldName="ApplicationDriverId"
                                                                    VisibleIndex="8"
                                                                    Caption="Driver">

                                                                    <PropertiesComboBox DataSourceID="odsAVDTDrivers" TextField="NameFormatted" ValueField="ApplicationDriverID">
                                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>

                                                                <dx:GridViewDataComboBoxColumn PropertiesComboBox-NullDisplayText=" "
                                                                    FieldName="PassengerID"
                                                                    VisibleIndex="9"
                                                                    Caption="Passenger">

                                                                    <PropertiesComboBox DataSourceID="odsAVDTDrivers" TextField="NameFormatted" ValueField="ApplicationDriverID">
                                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>

                                                            </Columns>
                                                        </dx:ASPxGridView>



                                                        <asp:ObjectDataSource runat="server"
                                                            ID="odsApplicationVehicleDriverTimeslot"
                                                            SelectMethod="GetApplicationVehicleDriverTimes"
                                                            TypeName="FMS.WEB.ResourceMgmnt">

                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="timeEditFrom" PropertyName="DateTime" Name="startdate" Type="DateTime"></asp:ControlParameter>
                                                                <asp:ControlParameter ControlID="timeEditTo" PropertyName="DateTime" Name="enddate" Type="DateTime"></asp:ControlParameter>
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>

                                                        <asp:ObjectDataSource runat="server" ID="odsAVDTVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                                                            <SelectParameters>
                                                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>

                                                        <asp:ObjectDataSource runat="server" ID="odsAVDTDrivers" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.ApplicationDriver">
                                                            <SelectParameters>
                                                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="applicatoinid"></asp:SessionParameter>
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>



                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Drivers" Text="Drivers">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <dx:ASPxGridView SettingsBehavior-ConfirmDelete="true" SettingsText-ConfirmDelete="Are you sure you wish to delete the driver?"
                                        SettingsPager-PageSize="3"
                                        Settings-ShowColumnHeaders="false" ClientInstanceName="dgvDrivers" KeyFieldName="ApplicationDriverID" ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="odsDrivers" Theme="SoftOrange">
                                        <Templates>
                                            <DataRow>
                                                <table>
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
                                                            <table class="dataviewtd">
                                                                <tr>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Forname:">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel Width="100" ID="Label1" runat="server" Text='<%# Eval("FirstName")%>'>
                                                                        </dx:ASPxLabel>
                                                                        <td class="dataviewtd">
                                                                            <td rowspan="3" class="dataviewtd">
                                                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Notes:">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxMemo ID="ASPxMemo1" ReadOnly="true" Text='<%# Eval("Notes")%>' runat="server" Height="80px" Width="300px">
                                                                                </dx:ASPxMemo>
                                                                            </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Surname:">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%# Eval("Surname")%>'>
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Phone Number:">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="dataviewtd">
                                                                        <dx:ASPxLabel ID="Label2f" runat="server" Text='<%# Eval("PhoneNumber")%>'>
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DataRow>
                                        </Templates>

                                        <SettingsPager PageSize="3"></SettingsPager>

                                        <Settings ShowColumnHeaders="True"></Settings>

                                        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>

                                        <SettingsSearchPanel Visible="True" />

                                        <SettingsText ConfirmDelete="Are you sure you wish to delete the driver?"></SettingsText>

                                        <EditFormLayoutProperties ColCount="2">
                                            <Items>
                                                <dx:GridViewColumnLayoutItem ColumnName="PhotoBinary">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="Notes">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="First Name">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="Phone Number">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="Surname">
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
                                            <dx:GridViewDataTextColumn FieldName="PhotoLocation" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataBinaryImageColumn ShowInCustomizationForm="True" VisibleIndex="2" FieldName="PhotoBinary">
                                                <PropertiesBinaryImage EnableClientSideAPI="True">
                                                    <EditingSettings Enabled="True">
                                                        <UploadSettings UploadMode="Advanced">
                                                            <UploadValidationSettings MaxFileSize="300000"></UploadValidationSettings>
                                                        </UploadSettings>
                                                    </EditingSettings>
                                                </PropertiesBinaryImage>
                                                
                                            </dx:GridViewDataBinaryImageColumn>
                                            <dx:GridViewDataMemoColumn FieldName="Notes" ShowInCustomizationForm="True" VisibleIndex="7">
                                                <PropertiesMemoEdit Columns="50" Height="100px">
                                                </PropertiesMemoEdit>
                                            </dx:GridViewDataMemoColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:ObjectDataSource ID="odsDrivers" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationDriver" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.ApplicationDriver" UpdateMethod="Update">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="applicatoinid" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Vehicles">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <%--VEHICLE GRIDVIEW  LOGIC HERE--%>

                                    <dx:ASPxGridView ID="dgvVehicles"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsVehicles"
                                        EnableTheming="True"
                                        KeyFieldName="ApplicationVehileID"
                                        SettingsBehavior-ConfirmDelete="true"
                                        SettingsText-ConfirmDelete="Are you sure you wish to delete the vehicle?"
                                        Theme="SoftOrange">

                                        <SettingsDetail ShowDetailRow="true" />
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText ConfirmDelete="Are you sure you wish to delete the vehicle?" />

                                        <Templates>
                                            <DetailRow>

                                                <dx:ASPxGridView ID="dgvDetailOdometerReadings"
                                                    runat="server"
                                                    AutoGenerateColumns="False"
                                                    DataSourceID="odsOdometerReadings"
                                                    EnableTheming="True"
                                                    Theme="SoftOrange"
                                                    KeyFieldName="ApplicationVehicleOdometerReadingID"
                                                    OnBeforePerformDataSelect="dgvDetailOdometerReadings_BeforePerformDataSelect">


                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowDeleteButton="True"
                                                            ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ApplicationVehicleOdometerReadingID" Visible="False" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="OdometerReading" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ApplicationVehicleID" Visible="False" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn FieldName="TimeReadingTaken" VisibleIndex="4">
                                                            <PropertiesDateEdit>
                                                                <TimeSectionProperties>
                                                                    <TimeEditProperties>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                    </TimeEditProperties>
                                                                </TimeSectionProperties>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataDateColumn FieldName="recordCreationDate" ReadOnly="True" Visible="False" VisibleIndex="5">
                                                            <PropertiesDateEdit>
                                                                <TimeSectionProperties>
                                                                    <TimeEditProperties>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                    </TimeEditProperties>
                                                                </TimeSectionProperties>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </DetailRow>
                                        </Templates>

                                        <Columns>
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationVehileID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Registration" ShowInCustomizationForm="True" VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Notes" ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="DeviceID" ShowInCustomizationForm="True" VisibleIndex="7">
                                                <PropertiesComboBox DataSourceID="odsVehiclesDevices" TextField="DeviceID" ValueField="DeviceID">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="VIN Number" FieldName="VINNumber" ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>

                                    </dx:ASPxGridView>

                                    <asp:ObjectDataSource ID="odsVehicles" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationVehicle" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle" UpdateMethod="Update">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="appplicationID" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:ObjectDataSource ID="odsOdometerReadings"
                                        runat="server"
                                        SelectMethod="GetForVehicleID"
                                        TypeName="FMS.Business.DataObjects.ApplicationVehicleOdometerReading"
                                        DataObjectTypeName="FMS.Business.DataObjects.ApplicationVehicleOdometerReading"
                                        DeleteMethod="Delete"
                                        InsertMethod="Create"
                                        UpdateMethod="Update">

                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid"
                                                Name="appVehicleID"
                                                SessionField="ApplicationVehicleID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:ObjectDataSource ID="odsVehiclesDevices" runat="server" SelectMethod="GetDevicesforApplication" TypeName="FMS.Business.DataObjects.Device">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>


                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>



            </td>
        </tr>
    </table>

</asp:Content>
