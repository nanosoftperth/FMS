<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserManagementContent.aspx.vb" Inherits="FMS.WEB.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: none !Important;">


    <form id="form1" runat="server">

        <link href="Content/Jira.css" rel="stylesheet" />

        <script src="Content/javascript/jquery-3.1.0.min.js"></script>

        <link href="Content/javascript/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
        <script src="Content/javascript/jquery-ui/jquery-ui.min.js"></script>

        <script src="Content/javascript/page.js"></script>
        <script src="Content/contextMenu/jquery.contextMenu.js"></script>
        <link href="Content/contextMenu/jquery.contextMenu.css" rel="stylesheet" />

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

        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>
        
        <script type="text/javascript">
            $(function () {
                $.contextMenu({
                    selector: '.context-menu-one',
                    callback: function (key, options) {
                        var id = (this[0].src).split('=')[1];
                        cpDeleteImage.PerformCallback(id);
                        //window.console && console.log(m) || alert(m);
                    },
                    items: {
                        "delete": { name: "Delete", icon: "delete" }
                    }
                });

                //$('.context-menu-one').on('click', function (e) {
                //    console.log('clicked', this);
                //})
            });
            //  Public Function SaveDefaultBusinessLocation(newBusinessLocation As String) As String


            function cboDefaultBusinessLocation_SelectedIndexChanged(s, e) {

                //save the value here.
                var selectedValue = cboDefaultBusinessLocation.GetValue();

                var param = {};

                param.newBusinessLocation = selectedValue;

                ajaxMethod("DefaultService.svc/" + 'SaveDefaultBusinessLocation',
                                    param, UMC_SuccessCallback, UMC_ErrorCallback, UMC_FinallyCallback);

            }

            function UMC_SuccessCallback(obj) {
                sendMsg('SUCCESS saving new default business location!');
            }

            function UMC_ErrorCallback(obj) {
                sendMsg('ERROR: ' + JSON.stringify(obj));
            }

            function UMC_FinallyCallback(obj) {
                //nop
            }


            function sendMsg(message) {

                $('#aui-flag-container').hide();

                $('#msg').text(message);

                $('#aui-flag-container').toggle('slow'
                        , function () {
                            $(this).delay(2500).toggle('slow');
                        });
            }

            function cboPossibleTimeZones_SelectedIndexChanged(s, e) {

                var selectedValue = cboPossibleTimeZones.GetValue();
                dgvTimezoneSettings.PerformCallback(selectedValue);
            }


            var initsearchlocation = function (s, e) {
                var search_input_box = s.inputElement;
                search_input_box.placeholder = "Search Box";
                var searchBox = new google.maps.places.SearchBox(search_input_box);

                searchBox.addListener('places_changed', function () {
                    newItemSelected(searchBox);

                });
            }


            function newItemSelected(searchBox) {

                var places = searchBox.getPlaces();

                if (places.length == 0) {
                    return;
                }

                var nisLat = places[0].geometry.location.lat();
                var nisLng = places[0].geometry.location.lng();

                editLat.SetText(nisLat);
                editLng.SetText(nisLng);
            }

            //For vehicle list logic
            function OnAllCheckedChangedVehicles(s, e) {
                if (s.GetChecked())
                    gridLookUp.GetGridView().SelectRows();
                else
                    gridLookUp.GetGridView().UnselectRows();
            }


            //var textSeparator = " | ";
            //function OnListBoxVehicleSelectionChanged(ddListVehicles, args) {
            //    if (args.index == 0)
            //        args.isSelected ? ddListVehicles.SelectAll() : ddListVehicles.UnselectAll();
            //    UpdateSelectAllItemState();
            //    UpdateText();
            //}

            //function UpdateSelectAllItemState() {
            //    IsAllSelected() ? clbVehicles.SelectIndices([0]) : clbVehicles.UnselectIndices([0]);
            //}

            //function IsAllSelected() {
            //    var selectedDataItemCount = clbVehicles.GetItemCount() - (clbVehicles.GetItem(0).selected ? 0 : 1);
            //    return clbVehicles.GetSelectedItems().length == selectedDataItemCount;
            //}

            //function UpdateText() {
            //    var selectedItems = clbVehicles.GetSelectedItems();
            //    ddVehicles.SetText(GetSelectedItemsText(selectedItems));
                
            //}

            //function GetSelectedItemsText(items) {
            //    var texts = [];
            //    for (var i = 0; i < items.length; i++)
            //        if (items[i].index != 0)
            //            texts.push(items[i].text);
            //    return texts.join(textSeparator);
            //}


            //End for vehicle list logic

            //Cesar: Use for Delete Dialog Box (SiteDetail)
            var visibleIndex;
            function OnCustomButtonClick(s, e, item) {
                visibleIndex = e.visibleIndex;

                if (item == 'Users') {
                    popupDelete_User.SetHeaderText("Delete Item");
                    popupDelete_User.Show();
                }
                if (item == 'Roles') {
                    popupDelete_Roles.SetHeaderText("Delete Item");
                    popupDelete_Roles.Show();
                }
                if (item == 'RolesAccessFeature') {
                    popupDelete_RolesAccessFeature.SetHeaderText("Delete Item");
                    popupDelete_RolesAccessFeature.Show();
                }
                if (item == 'BusinessLocation') {
                    popupDelete_BusinessLocation.SetHeaderText("Delete Item");
                    popupDelete_BusinessLocation.Show();
                }
                //if (item == 'SiteTab') {
                //    popupDelete_SiteTab.SetHeaderText("Delete Item");
                //    popupDelete_SiteTab.Show();
                //}
                //if (item == 'SiteDoc') {
                //    popupDelete_SiteDoc.SetHeaderText("Delete Item");
                //    popupDelete_SiteDoc.Show();
                //}
            }
            function OnClickYes(s, e, item) {
                if (item == 'Users') {
                    dgvUsers.DeleteRow(visibleIndex);
                    popupDelete_User.Hide();
                }
                if (item == 'Roles') {
                    dgvRoles.DeleteRow(visibleIndex);
                    popupDelete_Roles.Hide();
                }
                if (item == 'RolesAccessFeature') {
                    dgvRoleAccessToFeatures.DeleteRow(visibleIndex);
                    popupDelete_RolesAccessFeature.Hide();
                }
                if (item == 'BusinessLocation') {
                    cltdgvBusinessLocations.DeleteRow(visibleIndex);
                    popupDelete_BusinessLocation.Hide();
                }
                //if (item == 'SiteTab') {
                //    cltSiteDetailsGridView.DeleteRow(visibleIndex);
                //    popupDelete_SiteTab.Hide();
                //}
                //if (item == 'SiteDoc') {
                //    DocGridView.DeleteRow(visibleIndex);
                //    popupDelete_SiteDoc.Hide();
                //}
            }

        </script>

        <div id="aui-flag-container" style="display: none;">
            <div class="aui-flag" aria-hidden="false">
                <div class="aui-message aui-message-success success closeable shadowed aui-will-close">
                    <div id="msg">This is some example text</div>

                    <span class="aui-icon icon-close" role="button" tabindex="0"></span>


                </div>
            </div>
        </div>

        <div class="centreme">

            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="3"
                EnableTabScrolling="True" EnableTheming="True" Theme="SoftOrange" Width="100%">
                <TabPages>

                    <dx:TabPage Text="Users" Name="tab_Users">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView KeyFieldName="UserId" 
                                                    OnInitNewRow="dgvUsers_InitNewRow" 
                                                    OnBeforeGetCallbackResult="dgvUsers_BeforeGetCallbackResult" 
                                                    ID="dgvUsers" 
                                                    ClientInstanceName="dgvUsers" 
                                                    runat="server" 
                                                    AutoGenerateColumns="False" 
                                                    DataSourceID="odsUsers" 
                                                    EnableTheming="True" 
                                                    Theme="SoftOrange" 
                                                    Width="100%">


                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                   <%-- <SettingsBehavior ConfirmDelete="true" />--%>
                                    <Settings ShowGroupPanel="True" />
                                    <ClientSideEvents EndCallback="function(s,e){
                                            if (s.cpHasInserted) {
                                                alert('An email has been sent to ' + s.cpHasInserted + ' with an auto-generated password.');
                                                delete s.cpHasInserted;

                                            }
                                        }" />
                                    <ClientSideEvents CustomButtonClick="function(s, e)
                                        {
                                            OnCustomButtonClick(s, e, 'Users');
                                        }" />
                                    <Templates>
                                        <EditForm>
                                            <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                            <div style="text-align: right">
                                                <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                    <ClientSideEvents Click="function (s, e) { 
                                            dgvUsers.UpdateEdit();
                                             sendMsg('User Record Saved!');
                                            }" />
                                                </dx:ASPxHyperLink>
                                                <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </EditForm>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete_Users" Text="Delete" />
                                            </CustomButtons>
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
                                            VisibleIndex="7">

                                            <PropertiesComboBox DataSourceID="odsTimeZonesForUsers"
                                                TextField="Description"
                                                ValueField="ID">

                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataCheckColumn FieldName="SendEmailtoUserWithDefPass" ShowInCustomizationForm="true" VisibleIndex="9" Visible="false">
                                            <EditFormSettings Caption="Send email to user with password and login details" Visible="True" />
                                        </dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Business Location" FieldName="ApplicationLocationID" ShowInCustomizationForm="True" VisibleIndex="6">
                                            <PropertiesComboBox DataSourceID="odsApplicationLocationsWithDefault" TextField="Name" ValueField="ApplicationLocationID">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
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
                                <dx:ASPxGridView KeyFieldName="RoleID" ID="dgvRoles" ClientInstanceName="dgvRoles" runat="server" AutoGenerateColumns="False" DataSourceID="odsRolesRoles" EnableTheming="True" Theme="SoftOrange" Width="100%">
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
                                        <EditForm>
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors">
                                    </dx:ASPxGridViewTemplateReplacement>
                                    <div style="text-align:right">
                                    <dx:ASPxHyperLink style="text-decoration:underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvRoles.UpdateEdit();
                                             sendMsg('Record Saved!');
                                            }" />
                                    </dx:ASPxHyperLink>
                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                          runat="server"></dx:ASPxGridViewTemplateReplacement></div>
                                </EditForm>
                                    </Templates>
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <ClientSideEvents CustomButtonClick="function(s, e)
                                        {
                                            OnCustomButtonClick(s, e, 'Roles');
                                        }" />
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="100px" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete_Roles" Text="Delete" />
                                            </CustomButtons>
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
                                    AutoGenerateColumns="False" DataSourceID="odsFeatures" Width="100%"
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
                                <dx:ASPxGridView KeyFieldName="ApplicationFeatureRoleID" ID="dgvRoleAccessToFeatures" 
                                    ClientInstanceName="dgvRoleAccessToFeatures" runat="server" 
                                    AutoGenerateColumns="False" DataSourceID="odsApplicationFeatureRoles" 
                                    Width="100%"  Theme="SoftOrange"                                    
                                    >
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <SettingsSearchPanel Visible="True" />
                                    <ClientSideEvents CustomButtonClick="function(s, e)
                                        {
                                            OnCustomButtonClick(s, e, 'RolesAccessFeature');
                                        }" />
                                    <Templates>
                                        <EditForm>
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors">
                                    </dx:ASPxGridViewTemplateReplacement>
                                    <div style="text-align:right">
                                    <dx:ASPxHyperLink style="text-decoration:underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvRoleAccessToFeatures.UpdateEdit();
                                             sendMsg('Record Saved!');
                                            }" />
                                    </dx:ASPxHyperLink>
                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                          runat="server"></dx:ASPxGridViewTemplateReplacement></div>
                                </EditForm>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete_RolesAccessFeature" Text="Delete" />
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationFeatureRoleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="FeatureID" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Feature" >
                                            <Settings SortMode="DisplayText" />
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

                                        
                                        <%--<dx:GridViewDataComboBoxColumn FieldName="Vehicles" ShowInCustomizationForm="True" VisibleIndex="5" Visible="false">
                                            <EditFormSettings Visible="true" />
                                            <EditItemTemplate>
                                                <dx:ASPxDropDownEdit ClientInstanceName="ddVehicles" ID="cbxVehicles" Width="210px" runat="server" AnimationType="None">
                                                    <DropDownWindowStyle BackColor="#EDEDED"/>
                                                    <DropDownWindowTemplate>
                                                        <dx:ASPxListBox ID="ddListVehicles" ClientInstanceName="clbVehicles" 
                                                            SelectionMode="CheckColumn" runat="server" Width="300" Height="210px"
                                                            DataSourceID="odsVehicles" ValueField="ApplicationVehileID" TextField="Name" ValueType="System.String" 
                                                            Caption="Vehicles" >      
                                                            <CaptionSettings Position="Top"/>
                                                            <Border BorderStyle="None" />
                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="Name" Width="100%" />
                                                                
                                                            </Columns>
                                                            
                                                           
                                                            <ClientSideEvents SelectedIndexChanged="OnListBoxVehicleSelectionChanged" />
                                                        </dx:ASPxListBox>
                                                    </DropDownWindowTemplate>
                                                </dx:ASPxDropDownEdit>
                                            </EditItemTemplate>
                    
                                        </dx:GridViewDataComboBoxColumn>--%>
                                        

                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsApplicationFeatureRoles" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationFeatureRole" DeleteMethod="delete" InsertMethod="insert" SelectMethod="GetAllApplicationFeatureRoles" TypeName="FMS.Business.DataObjects.ApplicationFeatureRole" UpdateMethod="update">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsAppFeatRoleFeatures" runat="server" SelectMethod="GetAllFeatures" TypeName="FMS.Business.DataObjects.Feature">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
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

                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Default Business Location" Font-Bold="True"></dx:ASPxLabel>
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="   *The business location the map will open over" Font-Bold="False" Font-Italic="true"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <dx:ASPxComboBox
                                                            ClientInstanceName="cboDefaultBusinessLocation"
                                                            runat="server"
                                                            ID="cboDefaultBusinessLocation"
                                                            DataSourceID="odsApplicationLocations"
                                                            ValueField="ApplicationLocationID"
                                                            ValueType="System.Guid"
                                                            TextField="Name"
                                                            CssClass="cboPossibleTimeZones"
                                                            Width="260px">

                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){cboDefaultBusinessLocation_SelectedIndexChanged(s,e); }" />

<ClearButton Visibility="Auto"></ClearButton>

                                                        </dx:ASPxComboBox>

                                                    </td>
                                                </tr>


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
                                                    <td style="padding-left: 10px; padding-bottom: 10px; padding-top: 12px; text-align: right;">application version &quot; <%= AppVersion%> &quot;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                </table>


                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="tab_BusinessLocations" Text="Business Locations">
                        <ContentCollection>
                            <dx:ContentControl runat="server">

                                <div>
                                    <dx:ASPxGridView
                                        ID="dgvBusinessLocations"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsApplicationLocations"
                                        KeyFieldName="ApplicationLocationID"
                                        Theme="SoftOrange"
                                        ClientInstanceName="cltdgvBusinessLocations">
                                        <ClientSideEvents CustomButtonClick="function(s, e)
                                        {
                                            OnCustomButtonClick(s, e, 'BusinessLocation');
                                        }" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnDelete_BusinessLocation" Text="Delete" />
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationLocationID" Visible="False" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" Visible="False" VisibleIndex="7">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Longitude" VisibleIndex="3" EditCellStyle-CssClass="editLng" PropertiesTextEdit-ClientInstanceName="editLng">
                                                <PropertiesTextEdit ClientInstanceName="editLng"></PropertiesTextEdit>

                                                <EditCellStyle CssClass="editLng"></EditCellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Lattitude" VisibleIndex="4" EditCellStyle-CssClass="editLat" PropertiesTextEdit-ClientInstanceName="editLat">
                                                <PropertiesTextEdit ClientInstanceName="editLat"></PropertiesTextEdit>

                                                <EditCellStyle CssClass="editLat"></EditCellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Address" VisibleIndex="2" PropertiesTextEdit-ClientSideEvents-Init="initsearchlocation">
                                                <PropertiesTextEdit>
                                                    <ClientSideEvents Init="initsearchlocation"></ClientSideEvents>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataTextColumn FieldName="ApplicationImageID" Visible="False" VisibleIndex="8">
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn Caption="Image" FieldName="ApplicationImageID" VisibleIndex="5" MinWidth="150" Width="150px">
                                                <PropertiesComboBox DataSourceID="odsHomeImages" ImageUrlField="ImgURL" TextField="Name" ValueField="ApplicationImageID">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                        </Columns>
                                    </dx:ASPxGridView>
                                </div>

                                <%--========================================================================--%>
                                <%--                       BUSINESS LOCATION | OBJECT DATA SOURCES          --%>
                                <%--========================================================================--%>


                                <asp:ObjectDataSource runat="server" ID="odsApplicationLocations" DataObjectTypeName="FMS.Business.DataObjects.ApplicationLocation" DeleteMethod="Delete" InsertMethod="Update" SelectMethod="GetAllIncludingDefault" TypeName="FMS.Business.DataObjects.ApplicationLocation" UpdateMethod="Update">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="ApplicationID"></asp:SessionParameter>
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:ObjectDataSource 
                                    ID="odsHomeImages" 
                                    runat="server" 
                                    SelectMethod="GetAllApplicationImages" 
                                    TypeName="FMS.Business.DataObjects.ApplicationImage">
                                    <SelectParameters>

                                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" DefaultValue="" Name="applicationid"></asp:SessionParameter>
                                        <asp:Parameter DefaultValue="home" Name="type" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="tab_MapIcons" Text="Map Images (icons)">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <table style="width: 100%">

                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Fleet Map Markers:" Font-Bold="True"></dx:ASPxLabel>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            <dx:ASPxPanel ID="ASPxPanel1" Theme="SoftOrange" runat="server" Width="100%">
                                                <Border BorderStyle="Solid" BorderColor="#d3d3d3" BorderWidth="2px" />
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 58px; background-color: white; padding: 5px; text-align: center">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="ASPxButtonHome" Checked="true" ClientInstanceName="ASPxButtonHome" Border-BorderStyle="Dashed" Border-BorderWidth="1px" Border-BorderColor="#d3d3d3" Width="48px" Height="48px" runat="server" AutoPostBack="False" GroupName="MapMarker" ImagePosition="Top" Text="HQ">
                                                                                    <Image Width="48px" Height="48px" Url="content/FleetMapMarker.ashx?type=home">
                                                                                    </Image>

                                                                                    <Border BorderColor="LightGray" BorderStyle="Dashed" BorderWidth="1px"></Border>
                                                                                    <ClientSideEvents Click="function(s,e){
    dvGalery.PerformCallback();
    }" />
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="ASPxButtonVehicle" ClientInstanceName="ASPxButtonVehicle" Border-BorderStyle="Dashed" Border-BorderWidth="1px" Border-BorderColor="#d3d3d3" Width="48px" Height="48px" runat="server" AutoPostBack="False" GroupName="MapMarker" ImagePosition="Top" Text="Vehicle">
                                                                                    <Image Width="48px" Height="48px" Url="content/FleetMapMarker.ashx?type=vehicle">
                                                                                    </Image>

                                                                                    <Border BorderColor="LightGray" BorderStyle="Dashed" BorderWidth="1px"></Border>
                                                                                    <ClientSideEvents Click="function(s,e){
    dvGalery.PerformCallback();
    }" />
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 85%">
                                                                                <div style="padding: 10px">
                                                                                    <dx:ASPxHiddenField ID="ASPxHiddenFieldUpdateType" ClientInstanceName="ASPxHiddenFieldUpdateType" runat="server"></dx:ASPxHiddenField>
                                                                                    <dx:ASPxCallback ID="cpDeleteImage" ClientInstanceName="cpDeleteImage" OnCallback="cpDeleteImage_Callback" runat="server">
                                                                                        <ClientSideEvents EndCallback="function(s,e){
                                                                                                dvGalery.PerformCallback();
                                                                                            }" />
                                                                                    </dx:ASPxCallback>
                                                                                    <script>
                                                                                        function ImageClick(s, e) {
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
                                                                                        Layout="Flow" PageIndex="-1" Width="100%" ItemSpacing="4" Height="150px" Style="border: 1px solid black"
                                                                                        EnableTheming="false">
                                                                                        <PagerSettings ShowNumericButtons="False"></PagerSettings>
                                                                                        <ItemTemplate>
                                                                                            <a  href="javascript:void(0);">
                                                                                                <dx:ASPxImage runat="server" CssClass="context-menu-one" ImageUrl='<%#"Content/MapImages.ashx?imgId=" + Eval("Id").ToString()%>' AlternateText='<%# Eval("Name")%>' Width="20px" Height="20px" ShowLoadingImage="true">
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
                                                                                    </asp:ObjectDataSource>
                                                                                </div>


                                                                            </td>
                                                                            <td style="width: 15%; padding-right: 10px">

                                                                                <dx:ASPxBinaryImage ID="ASPxBinaryImageBrowse1" Width="100%" Height="125px" runat="server" BinaryStorageMode="Session" ShowLoadingImage="True">

                                                                                    <EditingSettings
                                                                                        UploadSettings-UploadValidationSettings-MaxFileSizeErrorText="Image size must be 30kB and below!"
                                                                                        UploadSettings-UploadValidationSettings-MaxFileSize="30000"
                                                                                        Enabled="True"
                                                                                        EmptyValueText=""
                                                                                        ButtonPanelSettings-Position="Bottom">

                                                                                        <UploadSettings>
                                                                                            <UploadValidationSettings MaxFileSize="30000" MaxFileSizeErrorText="Image size must be 30kB and below!"></UploadValidationSettings>
                                                                                        </UploadSettings>

                                                                                    </EditingSettings>

                                                                                </dx:ASPxBinaryImage>
                                                                                <dx:ASPxButton Width="100%" Height="25px" ID="ASPxButtonBrowse" OnClick="ASPxButtonBrowse_Click"
                                                                                    runat="server"
                                                                                    Text="Upload">
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px;">
                                            <div style="float: right;">
                                                <dx:ASPxButton Width="80px" CssClass="thebutton" ID="ASPxButton3" OnClick="ASPxButton3_Click"
                                                    runat="server"
                                                    Text="Save Changes">
                                                </dx:ASPxButton>
                                            </div>
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
                UpdateMethod="Update"
                DeleteMethod="Delete" OnDeleting="odsUsers_Deleting">
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
            <asp:ObjectDataSource ID="odsApplicationLocationsWithDefault" runat="server" SelectMethod="GetAllIncludingInheritFromApplication" TypeName="FMS.Business.DataObjects.ApplicationLocation">
                <SelectParameters>
                    <asp:SessionParameter DbType="Guid" Name="ApplicationID" SessionField="ApplicationID" />
                </SelectParameters>
            </asp:ObjectDataSource>


            <asp:ObjectDataSource ID="odsVehicles"
                runat="server" OnInserting="odsUsers_Inserting"
                SelectMethod="GetAllWithoutFilter"
                TypeName="FMS.Business.DataObjects.ApplicationVehicle" OnDeleting="odsUsers_Deleting">
            </asp:ObjectDataSource>


        </div>
        <dx:ASPxPopupControl ID="DeleteDialog_User" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_User" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_User" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'Users');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_User" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_User.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog_Roles" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_Roles" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_Roles" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'Roles');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_Roles" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_Roles.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog_RolesAccessFeature" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_RolesAccessFeature" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_RolesAccessFeature" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'RolesAccessFeature');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_RolesAccessFeature" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_RolesAccessFeature.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog_BusinessLocation" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_BusinessLocation" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_BusinessLocation" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'BusinessLocation');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_BusinessLocation" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_BusinessLocation.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </form>
</body>
</html>