<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="AlarmsAndAlerts.aspx.vb" Inherits="FMS.WEB.AlarmsAndAlerts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">


        function dgvGroups_FocusedRowChanged(s, e) {

            var focusedRowIndex = dgvGroups.GetFocusedRowIndex();
            var focusedKey = dgvGroups.GetRowKey(focusedRowIndex);
            dgvMembers.PerformCallback('get|' + focusedKey);
        }

        function btnRemove_Click(s, e) {


            var selectedVals = dgvMembers.GetSelectedKeysOnPage().toString();
            dgvMembers.PerformCallback('remove|' + selectedVals);
            e.cancel = true;
        }

        function btnAdd_Click(s, e) {

            var selectedVals = dgvPotentialGroupMembers.GetSelectedKeysOnPage().toString();
            dgvMembers.PerformCallback('add|' + selectedVals);
            e.cancel = true;
        }

        function btnSaveChanges_Click(s, e) {

            alert('save button pressed');
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
        .btnSaveChanges {
            float: right;
        }
    </style>
     <link href="Content/Jira.css" rel="stylesheet" />
    <div id="aui-flag-container" style="display: none;">
            <div class="aui-flag" aria-hidden="false">
                <div class="aui-message aui-message-success success closeable shadowed aui-will-close">
                    <div id="msg">This is some example text</div>

                    <span class="aui-icon icon-close" role="button" tabindex="0"></span>


                </div>
            </div>
        </div>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Theme="SoftOrange" ActiveTabIndex="0" EnableTheming="True">
        <TabPages>
            <dx:TabPage Text="Geo-Fence Alert Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server">

                        <dx:ASPxGridView ID="dgvApplicationAlerts" 
                                            ClientInstanceName="dgvApplicationAlerts"
                                            KeyFieldName="ApplicationAlertTypeID"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataSourceID="odsAlerts"
                                            EnableTheming="True"
                                            Theme="SoftOrange">

                            <Settings ShowGroupPanel="True" />
                            <Templates>
                                 <EditForm>
                                            <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                            <div style="text-align: right">
                                                <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                    <ClientSideEvents Click="function (s, e) { 
                                            dgvApplicationAlerts.UpdateEdit();
                                             sendMsg('Record Saved!');
                                            }" />
                                                </dx:ASPxHyperLink>
                                                <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </EditForm>
                            </Templates>
                            <Columns>

                                <%--command column definition--%>

                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>

                                <%--field names etc--%>
                                
                                <dx:GridViewDataComboBoxColumn FieldName="DriverID" Caption="Driver" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="odsDrivers" TextField="NameFormatted" ValueField="ApplicationDriverID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                                <dx:GridViewDataComboBoxColumn FieldName="GeoFenceId" Caption="Geo-Fence" VisibleIndex="3">
                                    <PropertiesComboBox DataSourceID="odsGeoFences" TextField="Name" ValueField="ApplicationGeoFenceID">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>


                                <dx:GridViewDataTextColumn FieldName="ApplicationAlertTypeID" VisibleIndex="10" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="11" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DeliveryGroup" VisibleIndex="9" Visible="False">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataComboBoxColumn FieldName="Action" Caption="Action" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="odsEnumVals" TextField="EnumString" ValueField="EnumValue">
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                                <dx:GridViewDataSpinEditColumn FieldName="Time_Period_mins" Caption="For period (mins)" VisibleIndex="4">
                                    <PropertiesSpinEdit DisplayFormatString="g">
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>

                                <dx:GridViewDataComboBoxColumn FieldName="SubscriberNativeID" Caption="Message Destination" VisibleIndex="5">
                                    <PropertiesComboBox DropDownStyle="DropDown" DataSourceID="odsSubscribers" TextField="NameFormatted" ValueField="NativeID">
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                                <dx:GridViewDataCheckColumn FieldName="SendEmail" Caption="Email" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="SendText" Caption="Text" VisibleIndex="7"></dx:GridViewDataCheckColumn>
                            </Columns>
                        </dx:ASPxGridView>

                        <asp:ObjectDataSource ID="odsAlerts" runat="server" DataObjectTypeName="FMS.Business.DataObjects.AlertType" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetAllForApplicationWithoutBookings" TypeName="FMS.Business.DataObjects.AlertType" UpdateMethod="Update">
                            <SelectParameters>
                                <asp:SessionParameter DbType="Guid" Name="appID" SessionField="ApplicationID" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsDrivers" runat="server" SelectMethod="GetAllDriversIncludingEveryone" TypeName="FMS.Business.DataObjects.ApplicationDriver">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="applicatoinid"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="odsGeoFences" runat="server" SelectMethod="GetAllApplicationGeoFences" TypeName="FMS.Business.DataObjects.ApplicationGeoFence">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="odsSubscribers" runat="server" SelectMethod="GetAllforApplication" TypeName="FMS.Business.DataObjects.Subscriber">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="odsEnumVals" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ActionEnumOption"></asp:ObjectDataSource>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>

            <dx:TabPage Text="Geo-Fence Explorer">
                <ContentCollection>
                    <dx:ContentControl runat="server">

                        <table>
                            <tr>
                                <td style="padding: 5px;">
                                    <dx:ASPxLabel ID="ASPxLabel1" Width="80px" runat="server" Text="Start Time: "></dx:ASPxLabel>
                                </td>
                                <td style="padding: 5px;">
                                    <dx:ASPxDateEdit ID="dateEditStartTime" runat="server">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="padding: 5px;">
                                    <dx:ASPxLabel ID="ASPxLabel2" Width="80px" runat="server" Text="End time: "></dx:ASPxLabel>
                                </td>
                                <td style="padding: 5px;">
                                    <dx:ASPxDateEdit ID="dateEditEndtime" runat="server">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="padding: 5px;">
                                    <dx:ASPxButton ID="ASPxButton1"
                                        runat="server"
                                        Text="Refresh Report"
                                        Theme="SoftOrange">
                                    </dx:ASPxButton>
                                </td>
                                <td style="width: 100%">

                                    <%--<dx:ASPxMenu ID="ASPxMenu1" ShowAsToolbar="true" runat="server" Theme="SoftOrange" EnableTheming="True"></dx:ASPxMenu>--%>
                                    <%--<uc1:ToolbarExport runat="server" ID="ToolbarExport1" ExportItemTypes="Pdf,Xls,Xlsx,Rtf,Csv" OnItemClick="ToolbarExport_ItemClick" />--%>
                                    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="dgvGeoFences"></dx:ASPxGridViewExporter>
                                </td>
                            </tr>
                        </table>

                        <dx:ASPxGridView ID="dgvGeoFences" KeyFieldName="PK" runat="server" AutoGenerateColumns="False" DataSourceID="odsGeoFenceReport" Theme="SoftOrange" EnableTheming="True">
                          
                              <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowFooter="True"></Settings>
                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                            <SettingsPager PageSize="500"></SettingsPager>
                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>

                            <Columns>
                                <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>

                                <dx:GridViewDataDateColumn FieldName="StartTime" VisibleIndex="6">

                                    <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy HH:mm:ss">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>

                                <dx:GridViewDataDateColumn FieldName="EndTime" VisibleIndex="7">
                                    <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy HH:mm:ss">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>

                                <dx:GridViewDataTextColumn FieldName="GeoFence_Description" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Vehicle_Name" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="GeoFence_Name" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Driver_Name" VisibleIndex="5"></dx:GridViewDataTextColumn>

                                <%--the primary key (cheated, row_number() over (order by x)--%>
                                <dx:GridViewDataTextColumn FieldName="PK" VisibleIndex="5" Visible="false"></dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="TimeTakes" VisibleIndex="8"></dx:GridViewDataTextColumn>

                                <%----%>
                            </Columns>
                        </dx:ASPxGridView>

                        <asp:ObjectDataSource runat="server"
                                            ID="odsGeoFenceReport"
                                            SelectMethod="GetReport"
                                            TypeName="FMS.Business.ReportGeneration.GeoFenceReport_Simple">

                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                                <asp:ControlParameter ControlID="dateEditStartTime" PropertyName="Value" Name="startdate" Type="DateTime"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="dateEditEndtime" PropertyName="Value" Name="enddate" Type="DateTime"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>


                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Alert Group Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server">

                        <%--THE LOGIC FOR THE ADDING/EDITING AND DELETING OF GROUP CONTENTS IS DONE HERE--%>

                        <table>

                            <tr>

                                <td style="padding-left: 20px;">
                                    <dx:ASPxLabel ID="labelGroups" runat="server" Text="Groups:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
                                </td>

                                <td style="padding-left: 20px;">
                                    <dx:ASPxLabel ID="labelMembers" runat="server" Text="Potential Members:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
                                </td>

                                <td style="padding: 7px;" valign="center"></td>
                                <td style="padding-left: 20px;">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Group Members:" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>
                                </td>

                            </tr>

                            <tr>

                                <td style="padding-left: 20px;" valign="top">
                                    <dx:ASPxGridView ID="dgvGroups" ClientInstanceName="dgvGroups" KeyFieldName="GroupID" SettingsBehavior-AllowFocusedRow="true" runat="server" AutoGenerateColumns="False" DataSourceID="odsGroups">
                                        <ClientSideEvents FocusedRowChanged="function(s,e){dgvGroups_FocusedRowChanged(s,e);}" />

                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="GroupID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="GroupName" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="2" Visible="False"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                    <asp:ObjectDataSource runat="server" ID="odsGroups" DataObjectTypeName="FMS.Business.DataObjects.Group" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetForApplication" TypeName="FMS.Business.DataObjects.Group" UpdateMethod="Update">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>

                                <td style="padding-left: 20px;" valign="top">

                                    <dx:ASPxGridView ID="dgvPotentialGroupMembers"
                                        KeyFieldName="NativeID"
                                        ClientInstanceName="dgvPotentialGroupMembers"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsSubscriber">

                                        <Settings ShowGroupPanel="True"></Settings>

                                        <ClientSideEvents />
                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="SubscriberType_Str" VisibleIndex="2" Caption="Subscriber Type"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="NativeID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Mobile" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>


                                    <asp:ObjectDataSource runat="server" ID="odsSubscriber" SelectMethod="GetAllforApplication" TypeName="FMS.Business.DataObjects.Subscriber" DataObjectTypeName="FMS.Business.DataObjects.Subscriber" UpdateMethod="update">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>

                                <td style="padding: 7px;" valign="center">

                                    <dx:ASPxButton ID="btnAdd" runat="server" AutoPostBack="false" Text="add &gt;&gt;" ClientInstanceName="btnAdd" EnableClientSideAPI="True" Width="80px">
                                        <ClientSideEvents Click="function(s,e){btnAdd_Click(s,e);}" />
                                    </dx:ASPxButton>
                                    <br />
                                    <br />
                                    <br />
                                    <dx:ASPxButton ID="btnRemove" AutoPostBack="false" runat="server" Text="&lt;&lt; remove" ClientInstanceName="btnRemove" EnableClientSideAPI="True" Width="80px">
                                        <ClientSideEvents Click="function(s,e){btnRemove_Click(s,e);}" />

                                    </dx:ASPxButton>
                                </td>

                                <td style="padding-left: 20px;" valign="top">

                                    <dx:ASPxGridView ID="dgvGroupMembers"
                                        KeyFieldName="NativeID"
                                        ClientInstanceName="dgvMembers"
                                        runat="server"
                                        SettingsEditing-BatchEditSettings-EditMode="Row"
                                        AutoGenerateColumns="False"
                                        DataSourceID="odsSubscriber">

                                        <Settings ShowGroupPanel="True"></Settings>

                                        <ClientSideEvents />

                                        <SettingsEditing Mode="Batch">
                                            <BatchEditSettings EditMode="Cell"></BatchEditSettings>
                                        </SettingsEditing>

                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowInsert="False"></SettingsDataSecurity>

                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="SubscriberType_Str" VisibleIndex="2" Caption="Subscriber Type" ReadOnly="True"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="NativeID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="3" ReadOnly="True"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Mobile" VisibleIndex="4" ReadOnly="True"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn FieldName="SendEmail" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn FieldName="SendText" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Alerts History (sent)">

                <%--THIS PAGE SHOWS THE "AUDIT HISTORY" SECTION--%>

                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="dgvAlertTypeOccurences"
                            runat="server"
                            KeyFieldName="AlertTypeOccuranceID"
                            AutoGenerateColumns="False"
                            DataSourceID="odsATOAlarmTypeOccurance"
                            Theme="SoftOrange">

                            <SettingsPager Visible="False"></SettingsPager>

                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>

                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="AlertTypeOccuranceID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="2" Caption="Driver Name"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SubscriberTypeStr" VisibleIndex="5" Caption="Subscriber Type"></dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="GeoFenceCollisionID" VisibleIndex="2"></dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="SubscriberTypeName" VisibleIndex="6" Caption="Subscriber Name(s)"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Emails" VisibleIndex="7" Caption="Email Addresses"></dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="SubscriberNativeID" VisibleIndex="5"></dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="Texts" VisibleIndex="8" Caption="Mobile Numbers"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DateSent" VisibleIndex="9" Caption="Date/Time Alert Sent"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceName" VisibleIndex="4" Caption="Geo-Fence Name"></dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceID" VisibleIndex="9"></dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="MessageContent" VisibleIndex="11" Caption="Message Content"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="AlertTypeID" Caption="Alert Type" VisibleIndex="3">
                                    <PropertiesComboBox DataSourceID="odsATOAlertType" TextField="Action" ValueField="ApplicationAlertTypeID">
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                            </Columns>
                        </dx:ASPxGridView>

                        <asp:ObjectDataSource runat="server" ID="odsATOAlarmTypeOccurance" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.AlertTypeOccurance">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="applicationID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource runat="server" ID="odsATOAlertType" SelectMethod="GetALLForApplication" TypeName="FMS.Business.DataObjects.AlertType">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
