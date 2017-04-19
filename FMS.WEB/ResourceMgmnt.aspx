<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ResourceMgmnt.aspx.vb" Inherits="FMS.WEB.ResourceMgmnt" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/Jira.css" rel="stylesheet" />
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

        function CloseGeofenceLeaveLookup() {
            GeofenceLeaveLookup.ConfirmCurrentSelection();
            GeofenceLeaveLookup.HideDropDown();
            GeofenceLeaveLookup.Focus();
        }
        function CloseGeofenceDestinationLookup() {
            GeofenceDestinationLookup.ConfirmCurrentSelection();
            GeofenceDestinationLookup.HideDropDown();
            GeofenceDestinationLookup.Focus();
        }
        function dateEditDay_DateChanged(s, e) {

            //dateEditDay, timeEditFrom, timeEditTo, dgvApplicationVehicleDriver
            var editDate = dateEditDay.GetValue().toStringReasonable();
            var editStartTime = timeEditFrom.GetValueString();
            var editEndTime = timeEditTo.GetValueString();
            var params = editDate + '|' + editStartTime + '|' + editEndTime

            dgvApplicationVehicleDriver.PerformCallback(params);
        }
        var initsearchlocation = function (s, e) {
            var search_input_box = s.inputElement;
            search_input_box.placeholder = "Search Box";
            var searchBox = new google.maps.places.SearchBox(search_input_box);

        }

        function sendMsg(message) {
            $('#aui-flag-container').hide();

            $('#msg').text(message);

            $('#aui-flag-container').toggle('slow'
                    , function () {
                        $(this).delay(2500).toggle('slow');
                    });
        }
    </script>
    <style type="text/css">
        .small-nonbold label {
            font-weight: 400;
        }

        .dataviewtd {
            padding: 5px;
        }

        .floatLeft {
            float: left;
            margin-left: 5px;
        }
    </style>


    <div id="aui-flag-container" style="display: none;">
        <div class="aui-flag" aria-hidden="false">
            <div class="aui-message aui-message-success success closeable shadowed aui-will-close">
                <div id="msg">This is some example text</div>

                <span class="aui-icon icon-close" role="button" tabindex="0"></span>


            </div>
        </div>
    </div>
    <table>
        <tr>
            <td valign="top">
                <img style="width: 200px;" src="Content/Images/mC-settings.png" />
            </td>
            <td>
                <dx:ASPxPageControl ID="pageControlMain" runat="server" ActiveTabIndex="2">
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
                                                        <script>
                                                            var AssignisUpdated = false;
                                                            function AssignBeginCallback(s, e) {
                                                                if (e.command == "UPDATEEDIT")
                                                                    AssignisUpdated = true;
                                                            }
                                                            function AssignEndCallback(s, e) {
                                                                if (AssignisUpdated) {
                                                                    sendMsg('Record Saved!');
                                                                    AssignisUpdated = false;
                                                                }
                                                            }
                                                        </script>
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
                                                            <ClientSideEvents EndCallback="AssignEndCallback" BeginCallback="AssignBeginCallback" />
                                                            <SettingsEditing Mode="Batch">
                                                                <BatchEditSettings EditMode="Row"></BatchEditSettings>
                                                            </SettingsEditing>
                                                            <SettingsPager PageSize="20"></SettingsPager>

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
                        </dx:TabPage>


                        <dx:TabPage Text="Vehicles">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <%--VEHICLE GRIDVIEW  LOGIC HERE--%>
                                    <dx:ASPxGridView ID="dgvVehicles"
                                        ClientInstanceName="dgvVehicles"
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
                                        <SettingsPager PageSize="20"></SettingsPager>
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
                                                        <dx:GridViewDataSpinEditColumn FieldName="OdometerReading" VisibleIndex="2">
                                                        </dx:GridViewDataSpinEditColumn>
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

                                            <EditForm>
                                                <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                                <div style="text-align: right">
                                                    <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvVehicles.UpdateEdit();
                                             sendMsg('Record Saved!');
                                            }" />
                                                    </dx:ASPxHyperLink>
                                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                        runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationVehileID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" VisibleIndex="3" Caption="Icon" FieldName="ApplicationImageID">
                                                <PropertiesComboBox ImageUrlField="ImgUrl" DataSourceID="odsMapMarker" TextField="Name" ValueField="ApplicationImageID">
                                                    <ItemImage Height="24px" Width="23px" />
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="VIN Number" FieldName="VINNumber" ShowInCustomizationForm="True" VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Registration" ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Notes" ShowInCustomizationForm="True" VisibleIndex="7">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="DeviceID" ShowInCustomizationForm="True" VisibleIndex="8">
                                                <PropertiesComboBox DataSourceID="odsVehiclesDevices" TextField="DeviceID" ValueField="DeviceID">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="CANbus standard" ShowInCustomizationForm="True" VisibleIndex="9" FieldName="CAN_Protocol_Type">
                                                <PropertiesComboBox DataSourceID="odsCanStandards" TextField="ID" ValueField="Name">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:ObjectDataSource ID="odsMapMarker" runat="server" SelectMethod="GetAllApplicationImages" TypeName="FMS.Business.DataObjects.ApplicationImage">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
                                            <asp:Parameter Name="type" Type="String" DefaultValue="vehicle" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:ObjectDataSource ID="odsCanStandards"
                                        runat="server"
                                        SelectMethod="GetAllCANPRotocols"
                                        TypeName="FMS.Business.DataObjects.ApplicationVehicle+CanStandard"></asp:ObjectDataSource>

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
                        <dx:TabPage Text="Bookings">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="dgvDetailBookings" ClientInstanceName="dgvDetailBookings"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EnableTheming="True"
                                        Theme="SoftOrange"
                                        DataSourceID="odsBooking"
                                        KeyFieldName="ApplicationBookingId" Settings-HorizontalScrollBarMode="Auto" Width="900px">
                                        <Settings HorizontalScrollBarMode="Auto"></Settings>
                                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="800px" AllowResize="true" ResizingMode="Live" />
                                        </SettingsPopup>
                                        <Templates>
                                            <EditForm>
                                                <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                                <div style="text-align: right">
                                                    <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvDetailBookings.UpdateEdit();
                                             sendMsg('Booking Saved!');
                                            }" />
                                                    </dx:ASPxHyperLink>
                                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                        runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                        <EditFormLayoutProperties ColCount="2">
                                            <Items>
                                                <dx:GridViewColumnLayoutItem ColumnName="ApplicationDriverID">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="ContactID">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="ArrivalTime">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="CustomerPhone">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="GeofenceLeave" CssClass="search-address">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="CustomerEmail">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem VerticalAlign="Top" ColumnName="GeofenceDestination" CssClass="search-address" RowSpan="2">
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="IsAlert5min" Caption="Send 'within 5 minutes away' message" CaptionCellStyle-CssClass="small-nonbold" HorizontalAlign="Right">
                                                    <CaptionCellStyle CssClass="small-nonbold"></CaptionCellStyle>
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:GridViewColumnLayoutItem ColumnName="IsAlertLeaveForPickup" Caption="Send 'left to pick you up' message" CaptionCellStyle-CssClass="small-nonbold" HorizontalAlign="Right">
                                                    <CaptionCellStyle CssClass="small-nonbold"></CaptionCellStyle>
                                                </dx:GridViewColumnLayoutItem>
                                                <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                                </dx:EditModeCommandLayoutItem>
                                            </Items>
                                        </EditFormLayoutProperties>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" ShowNewButtonInHeader="True" ShowEditButton="True" ShowDeleteButton="True">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationBookingId" Visible="false" ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationId" Visible="false" ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Width="100px" PropertiesDateEdit-DisplayFormatString="G" PropertiesDateEdit-EditFormat="DateTime" FieldName="ArrivalTime" VisibleIndex="3">
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


                                            <dx:GridViewDataComboBoxColumn Width="200px" FieldName="ApplicationDriverID" Caption="Driver" ShowInCustomizationForm="True" VisibleIndex="5">
                                                <PropertiesComboBox DataSourceID="odsBookingDriver" ImageUrlField="ImgUrl" TextField="NameFormatted" ValueField="ApplicationDriverID">
                                                    <ItemImage Height="44px" Width="43px" />

                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </PropertiesComboBox>

                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Width="200px" FieldName="GeofenceLeave" Caption="Geo-fence Leave" ShowInCustomizationForm="True" VisibleIndex="6" PropertiesTextEdit-ClientSideEvents-Init="initsearchlocation">

                                                <PropertiesTextEdit>
                                                    <ClientSideEvents Init="initsearchlocation"></ClientSideEvents>
                                                </PropertiesTextEdit>

                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Width="200px" FieldName="GeofenceDestination" Caption="Geo-fence Destination" ShowInCustomizationForm="True" VisibleIndex="7" PropertiesTextEdit-ClientSideEvents-Init="initsearchlocation">

                                                <PropertiesTextEdit>
                                                    <ClientSideEvents Init="initsearchlocation"></ClientSideEvents>
                                                </PropertiesTextEdit>

                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn Width="150px" FieldName="ContactID" Caption="Customer" ShowInCustomizationForm="True" VisibleIndex="8">
                                                <PropertiesComboBox DataSourceID="odsBookingContact" TextField="NameFormatted" ValueField="ContactID" ValueType="System.Guid" IncrementalFilteringMode="Contains">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </PropertiesComboBox>
                                                <EditItemTemplate>
                                                    <dx:ASPxGridLookup ID="ASPxGridLookup1" ClientInstanceName="lkContact" Width="100%" runat="server"
                                                        AutoGenerateColumns="False" Theme="SoftOrange" DataSourceID="odsBookingContact"
                                                        KeyFieldName="ContactID" TextFormatString="{1}"
                                                        Value='<%# Bind("ContactID")%>' IncrementalFilteringMode="Contains">
                                                        <GridViewClientSideEvents FocusedRowChanged="function(s, e) {
                                               var g = lkContact.GetGridView()
                                               var val = g.GetRowValues(s.focusedRowIndex,'MobileNumber;EmailAddress',function (values) {
                                                    var mn = values[0];
                                                    var ea = values[1];
                                                    var cp =  dgvDetailBookings.GetEditor('CustomerPhone')
                                                    var ce =  dgvDetailBookings.GetEditor('CustomerEmail')
                                                    cp.SetText(mn);
                                                    ce.SetText(ea);
                                                });
                                            }" />
                                                        <GridViewProperties>
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                        </GridViewProperties>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="True" ShowEditButton="False" ShowDeleteButton="False">
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="ContactID" Visible="false" VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="NameFormatted" Visible="false" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Forname" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Surname" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CompanyName" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="MobileNumber" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridLookup>
                                                </EditItemTemplate>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Width="150px" FieldName="CustomerPhone" ShowInCustomizationForm="True" VisibleIndex="9">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Width="200px" FieldName="CustomerEmail" ShowInCustomizationForm="True" VisibleIndex="10">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn Width="150px" FieldName="IsAlert5min" Caption="Alert if 5 min away" ShowInCustomizationForm="True" VisibleIndex="11">
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Width="200px" FieldName="IsAlertLeaveForPickup" Caption="Alert if has left for pickup" ShowInCustomizationForm="True" VisibleIndex="12">
                                            </dx:GridViewDataCheckColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:ObjectDataSource ID="odsBookingContact" OnInserting="odsBookingContact_Inserting" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Contact" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.Contact" UpdateMethod="Update">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="appidd" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="odsBookingDriver" runat="server" SelectMethod="GetAllDriversWithImageUrl" TypeName="FMS.Business.DataObjects.ApplicationDriver">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="applicatoinid" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="odsBooking" OnInserting="odsBooking_Inserting" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationBooking" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllBookingsForApplication" TypeName="FMS.Business.DataObjects.ApplicationBooking" UpdateMethod="Update">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
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
