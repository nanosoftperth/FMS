﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserManagementContent.aspx.vb" Inherits="FMS.WEB.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>


    <form id="form1" runat="server">

        <style type="text/css">
            .thebutton {
                margin-left: 10px;
            }

            .btnSaveTimeZone {
                float: right;
            }

            .auto-style1 {
                width: 400px;
            }

            .cboPossibleTimeZones {
                float: left;
            }
        </style>

        <script type="text/javascript">

            function cboPossibleTimeZones_SelectedIndexChanged(s, e) {

                var selectedValue = cboPossibleTimeZones.GetValue();
                dgvTimezoneSettings.PerformCallback(selectedValue);
            }

        </script>



        <div class="centreme">

            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="4"
                EnableTabScrolling="True" EnableTheming="True" Theme="SoftOrange" Width="100%">
                <TabPages>

                    <dx:TabPage Text="Users" Name="tab_Users">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView KeyFieldName="UserId" OnInitNewRow="dgvUsers_InitNewRow" OnBeforeGetCallbackResult="dgvUsers_BeforeGetCallbackResult" ID="dgvUsers" runat="server" AutoGenerateColumns="False" DataSourceID="odsUsers" EnableTheming="True" Theme="SoftOrange" Width="100%">
                                    <SettingsPager PageSize="50" >
                                    </SettingsPager>
                                    <Settings ShowGroupPanel="True" />
                                    <ClientSideEvents EndCallback ="function(s,e){
                                            if (s.cpHasInserted) {
                                                alert('A mail has been sent to ' + s.cpHasInserted + ' with the default password.');
                                                delete s.cpHasInserted;

                                            }
                                        }" />
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UserId" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="UserName" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Email" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                         
                                        <dx:GridViewDataComboBoxColumn FieldName="RoleID" ShowInCustomizationForm="True" VisibleIndex="8" Caption="Role">
                                            <PropertiesComboBox DataSourceID="odsRoles" TextField="Name" ValueField="RoleID">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>

                                        <dx:GridViewDataTextColumn FieldName="Mobile"
                                            ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataComboBoxColumn Caption="Time Zone"
                                            FieldName="TimeZoneID"
                                            ShowInCustomizationForm="True"
                                            VisibleIndex="6">

                                            <PropertiesComboBox DataSourceID="odsTimeZonesForUsers"
                                                TextField="Description"
                                                ValueField="ID">

                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataCheckColumn FieldName="SendEmailtoUserWithDefPass" ShowInCustomizationForm="true" VisibleIndex="8"  Visible="false" >
                                             <EditFormSettings Caption="Send email to user with password and login details" Visible="True" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsRoles"
                                    runat="server"
                                    SelectMethod="GetAllRolesforApplication"
                                    TypeName="FMS.Business.DataObjects.Role">

                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid"
                                            Name="appID"
                                            SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Roles" Name="tab_Roles">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView KeyFieldName="RoleID" ID="dgvRoles" runat="server" AutoGenerateColumns="False" DataSourceID="odsRolesRoles" EnableTheming="True" Theme="SoftOrange" Width="100%">
                                    <SettingsDetail ShowDetailRow="True" />
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="dgvRolesUsers" KeyFieldName="UserID" runat="server" AutoGenerateColumns="False" DataSourceID="odsRolesUsers" OnBeforePerformDataSelect="dgvRolesUsers_BeforePerformDataSelect">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="0" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="UserId" VisibleIndex="1" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn FieldName="LastLoggedInDate" VisibleIndex="4">
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
                                                    <dx:GridViewDataComboBoxColumn FieldName="RoleID" Caption="Role" VisibleIndex="5">
                                                        <PropertiesComboBox DataSourceID="odsRolesRoles" TextField="Name" ValueField="RoleID">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesComboBox>
                                                    </dx:GridViewDataComboBoxColumn>
                                                </Columns>
                                            </dx:ASPxGridView>

                                        </DetailRow>
                                    </Templates>
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="100px" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RoleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsRolesUsers" runat="server" SelectMethod="GetUsersForRole" TypeName="FMS.Business.DataObjects.Role">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="rlID" SessionField="CurrentExpandedRow" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsRolesRoles" runat="server" SelectMethod="GetAllRolesforApplication" TypeName="FMS.Business.DataObjects.Role" DataObjectTypeName="FMS.Business.DataObjects.Role" DeleteMethod="delete" InsertMethod="insert" UpdateMethod="Update">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appID" SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Feature List" Name="tab_FeatureList">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <asp:ObjectDataSource ID="odsFeatures" runat="server"
                                    SelectMethod="GetAllFeatures"
                                    TypeName="FMS.Business.DataObjects.Feature"></asp:ObjectDataSource>
                                <dx:ASPxGridView ID="ASPxGridView2" runat="server"
                                    AutoGenerateColumns="False" DataSourceID="odsFeatures"  Width="100%"
                                    Theme="SoftOrange">
                                    <SettingsPager PageSize="50"></SettingsPager>
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FeatureID" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Roles Access to Features" Name="tab_RolesAccessToFeatures">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView KeyFieldName="ApplicationFeatureRoleID" ID="dgvRoleAccessToFeatures" runat="server" AutoGenerateColumns="False" DataSourceID="odsApplicationFeatureRoles"  Width="100%" Theme="SoftOrange">
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <SettingsSearchPanel Visible="True" />
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationFeatureRoleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="FeatureID" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Feature">
                                            <PropertiesComboBox DataSourceID="odsAppFeatRoleFeatures" TextField="Name" ValueField="FeatureID">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="RoleID" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Role">
                                            <PropertiesComboBox DataSourceID="odsAppFeatRoleRoles" TextField="Name" ValueField="RoleID">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsApplicationFeatureRoles" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationFeatureRole" DeleteMethod="delete" InsertMethod="insert" SelectMethod="GetAllApplicationFeatureRoles" TypeName="FMS.Business.DataObjects.ApplicationFeatureRole" UpdateMethod="update">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsAppFeatRoleFeatures" runat="server" SelectMethod="GetAllFeatures" TypeName="FMS.Business.DataObjects.Feature"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsAppFeatRoleRoles" runat="server" SelectMethod="GetAllRolesforApplication" TypeName="FMS.Business.DataObjects.Role">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appID" SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Name="tab_ApplicationSettings" Text="Application Settings">
                        <ContentCollection>
                            <dx:ContentControl runat="server">


                                <table>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table>
                                                <tr>
                                                    <td style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 0px; vertical-align: top;">
                                                        <table style="width: 100px;">
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Company Logo:" Font-Bold="True"></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right bottom"></td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                    <td rowspan="2">
                                                        <dx:ASPxBinaryImage ID="imgCompanylogo" runat="server" BinaryStorageMode="Session">
                                                            <EditingSettings Enabled="True"></EditingSettings>
                                                        </dx:ASPxBinaryImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; padding: 5px;">
                                                        <dx:ASPxButton Width="80px" CssClass="thebutton" ID="ASPxButton1"
                                                            runat="server"
                                                            Text="save logo">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 0px; vertical-align: top;">
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="General Settings:" Font-Bold="True"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="vertical-align: top;">
                                                        <dx:ASPxGridView ID="dgvSettings" KeyFieldName="SettingID" runat="server" AutoGenerateColumns="False" DataSourceID="odsSettings">
                                                            <SettingsPager Visible="False"></SettingsPager>
                                                            <SettingsDataSecurity AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="3" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="SettingID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Value" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicatiopnSettingValueID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="6" Visible="False"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Visible="True"></dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; padding-left: 50px;">
                                            <table>
                                                                                             

                                                <tr>
                                                    <td style="padding-left: 3px; padding-right: 10px; padding-bottom: 10px; padding-top: 12px; vertical-align: top;">

                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Timezone Information:" Font-Bold="True"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%--TIME ZONE SETTINGS--%>

                                                        <table>
                                                            <tr>
                                                                <td class="auto-style1">

                                                                    <dx:ASPxComboBox ID="cboPossibleTimeZones"
                                                                        ClientInstanceName="cboPossibleTimeZones"
                                                                        runat="server"
                                                                        DataSourceID="odsAllTimeZoneOptions"
                                                                        ValueField="ID"
                                                                        TextField="Description"
                                                                        CssClass="cboPossibleTimeZones"
                                                                        Width="260px">

                                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){cboPossibleTimeZones_SelectedIndexChanged(s,e); }" />

                                                                        <ClearButton Visibility="Auto"></ClearButton>

                                                                    </dx:ASPxComboBox>


                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 10px;" class="auto-style1">

                                                                    <dx:ASPxGridView ID="dgvTimezoneSettings"
                                                                        ClientInstanceName="dgvTimezoneSettings"
                                                                        runat="server"
                                                                        DataSourceID="odsTimeZoneData" Width="100%"
                                                                        AutoGenerateColumns="False">


                                                                        <SettingsPager Visible="False"></SettingsPager>

                                                                        <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="0" Visible="False"></dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Visible="True"></dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Value" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="ApplicatiopnSettingValueID" VisibleIndex="4" Visible="False"></dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="5" Visible="False"></dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                 <tr>
                                                    <td style="padding-left: 10px; padding-bottom: 10px; padding-top: 12px; text-align:right;">
                                                        application version &quot;&quot;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                                            <br />
                                <table style="width:100%">
                                    <tr>
                                        <td style="padding:10px;">
                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Fleet Map Markers:" Font-Bold="True"></dx:ASPxLabel>
                                        
                                          <dx:ASPxButton Width="80px" CssClass="thebutton" ID="ASPxButton3" OnClick="ASPxButton3_Click"
                                                            runat="server"
                                                            Text="Save Changes">
                                                        </dx:ASPxButton></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxPanel ID="ASPxPanel1" Theme="SoftOrange" runat="server" Width="100%">
                                                <Border BorderStyle="Solid" BorderColor="#d3d3d3" BorderWidth="2px" />
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:58px;background-color:white;padding:5px;text-align:center">
                                                                    <table style="width:100%">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="ASPxButtonHome" Checked="true" ClientInstanceName="ASPxButtonHome" Border-BorderStyle="Dashed" Border-BorderWidth="1px" Border-BorderColor="#d3d3d3" Width="48px" Height="48px"  runat="server" AutoPostBack="False" GroupName="MapMarker" ImagePosition="Top" Text="HQ">
                                                                                        <Image Width="48px" Height="48px" Url="content/FleetMapMarker.ashx?type=home">
                                                                                        </Image>

<Border BorderColor="LightGray" BorderStyle="Dashed" BorderWidth="1px"></Border><ClientSideEvents Click="function(s,e){
    dvGalery.PerformCallback();
    }" />
                                                                                        </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="ASPxButtonVehicle" ClientInstanceName="ASPxButtonVehicle" Border-BorderStyle="Dashed" Border-BorderWidth="1px" Border-BorderColor="#d3d3d3" Width="48px" Height="48px"  runat="server" AutoPostBack="False" GroupName="MapMarker" ImagePosition="Top" Text="Vehicle">
                                                                                        <Image Width="48px" Height="48px" Url="content/FleetMapMarker.ashx?type=vehicle">
                                                                                        </Image>

<Border BorderColor="LightGray" BorderStyle="Dashed" BorderWidth="1px"></Border><ClientSideEvents Click="function(s,e){
    dvGalery.PerformCallback();
    }" />
                                                                                        </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td >  
                                                                    <table style="width:100%">
                                                                        <tr>
                                                                            <td  style="width:85%">
                                                                    <div style="padding:10px">
                                                                        <dx:ASPxHiddenField ID="ASPxHiddenFieldUpdateType" ClientInstanceName="ASPxHiddenFieldUpdateType" runat="server"></dx:ASPxHiddenField>
                                                                       <script>
                                                                           function ImageClick(s, e) {
                                                                               debugger;
                                                                               var id = s.GetImageUrl();
                                                                               id = id.split('=')[1];
                                                                               if (ASPxButtonHome.GetChecked()) {
                                                                                   ASPxButtonHome.SetImageUrl(s.GetImageUrl());
                                                                                   ASPxHiddenFieldUpdateType.Set("UTHome", id);
                                                                               }

                                                                               else if (ASPxButtonVehicle.GetChecked()) {
                                                                                   ASPxButtonVehicle.SetImageUrl(s.GetImageUrl());
                                                                                   ASPxHiddenFieldUpdateType.Set("UTVehicle", id);
                                                                               }
                                                                           }
                                                                       </script>
                                                                        <dx:ASPxDataView ID="dvGalery" OnCustomCallback="dvGalery_CustomCallback" ClientInstanceName="dvGalery" runat="server" DataSourceID="odsMapMarker" AllowPaging="False"
        Layout="Flow" PageIndex="-1" Width="100%" ItemSpacing="4" Height="150px"  style="border:1px solid black"
        EnableTheming="false">
<PagerSettings ShowNumericButtons="False"></PagerSettings>
        <ItemTemplate>
            <a href="javascript:void(0);">
                <dx:ASPxImage runat="server" ImageUrl= '<%#"Content/MapImages.ashx?imgId=" + Eval("Id").ToString()%>' AlternateText='<%# Eval("Name")%>' Width="20px" Height="20px" ShowLoadingImage="true">
                    <ClientSideEvents Click="ImageClick" />
                </dx:ASPxImage>
            </a>
        </ItemTemplate>
        <Paddings Padding="0px" />
        <ItemStyle Height="20px" Width="20px" BackColor="Transparent" Border-BorderStyle="None">
            <Paddings Padding="0px" />
            <Border BorderWidth="1px" />
        </ItemStyle>
    </dx:ASPxDataView>
          <asp:ObjectDataSource ID="odsMapMarker" runat="server" OnSelecting="odsMapMarker_Selecting" SelectMethod="GetAllImages" TypeName="FMS.Business.DataObjects.ApplicationImage">
        <SelectParameters>
            <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
            <asp:Parameter Name="type" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource></div>
                                                                    
                                                                                
                                                                            </td>
                                                                            <td style="width:15%;padding-right:10px">
                                                                                
                                                        <dx:ASPxBinaryImage ID="ASPxBinaryImageBrowse" Width="100%" Height="125px" runat="server" BinaryStorageMode="Session">
                                                            <EditingSettings UploadSettings-UploadValidationSettings-MaxFileSizeErrorText="Image size must be 10kb and below!" UploadSettings-UploadValidationSettings-MaxFileSize="10000" Enabled="True" EmptyValueText="" ButtonPanelSettings-Position="Bottom"></EditingSettings>
                                                        
                                                        </dx:ASPxBinaryImage>
                                                                                <dx:ASPxButton Width="100%" Height="25px" ID="ASPxButtonBrowse" OnClick="ASPxButtonBrowse_Click"
                                                            runat="server"
                                                            Text="Upload">
                                                        </dx:ASPxButton></td></tr></table></td></tr></table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>



            <%--TIME ZONE SETTINGS--%>

            <asp:ObjectDataSource runat="server"
                ID="odsAllTimeZoneOptions"
                SelectMethod="GetMSftTimeZonesAndCurrentIfGoogle"
                TypeName="FMS.Business.DataObjects.TimeZone"></asp:ObjectDataSource>

            <asp:ObjectDataSource runat="server"
                ID="odsTimeZoneData"
                SelectMethod="GetTimeZoneValues"
                TypeName="FMS.WEB.ThisSession"></asp:ObjectDataSource>


            <asp:ObjectDataSource ID="odsUsers"
                runat="server"
                DataObjectTypeName="FMS.Business.DataObjects.User"
                InsertMethod="Insert" OnInserting="odsUsers_Inserting"
                SelectMethod="GetAllUsersForApplication"
                TypeName="FMS.Business.DataObjects.User"
                UpdateMethod="Update"> 
                <SelectParameters>
                    <asp:SessionParameter DbType="Guid"
                        Name="applicationid"
                        SessionField="ApplicationID" />
                </SelectParameters>
            </asp:ObjectDataSource>


            <asp:ObjectDataSource runat="server"
                ID="odsSettings"
                SelectMethod="GetSettingsForApplication_withoutImages"
                TypeName="FMS.Business.DataObjects.Setting"
                DataObjectTypeName="FMS.Business.DataObjects.Setting"
                UpdateMethod="Update">

                <SelectParameters>
                    <asp:SessionParameter SessionField="ApplicationID" Name="applicationid" DbType="Guid"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsTimeZonesForUsers"
                runat="server"
                SelectMethod="GetMSftTimeZonesAutoInheritOption"
                TypeName="FMS.Business.DataObjects.TimeZone"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>