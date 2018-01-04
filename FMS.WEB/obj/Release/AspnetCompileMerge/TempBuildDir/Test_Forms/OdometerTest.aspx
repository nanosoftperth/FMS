<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="OdometerTest.aspx.vb" Inherits="FMS.WEB.OdometerTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


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
            <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Registration" ShowInCustomizationForm="True" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Notes" ShowInCustomizationForm="True" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="DeviceID" ShowInCustomizationForm="True" VisibleIndex="5">
                <PropertiesComboBox DataSourceID="odsVehiclesDevices" TextField="DeviceID" ValueField="DeviceID">
                    <ClearButton Visibility="Auto">
                    </ClearButton>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
        </Columns>

    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="odsVehicles" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationVehicle" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle" UpdateMethod="Update">
        <SelectParameters>
            <asp:SessionParameter DbType="Guid" Name="appplicationID" SessionField="ApplicationID" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource   ID="odsOdometerReadings"
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

</asp:Content>
