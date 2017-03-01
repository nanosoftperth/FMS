<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportScheduler.aspx.vb" Inherits="FMS.WEB.ReportScheduler" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Src="~/Controls/NanoReportParamList.ascx" TagPrefix="uc1" TagName="NanoReportParamList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Page.ResolveClientUrl("~/Content/Jira.css")%>" rel="stylesheet" />
    <script src='<%= Page.ResolveClientUrl("~/Content/javascript/jquery-3.1.0.min.js")%>'></script>

    <style>
        .clsEditForm {
            width: 100%;
        }

            .clsEditForm tr {
              margin-bottom: 5px;
            }
    </style>

    <script type="text/javascript">
        function sendMsg(message) {
            $('#aui-flag-container').hide();
            $('#msg').text(message);
            $('#aui-flag-container').toggle('slow'
                    , function () {
                        $(this).delay(2500).toggle('slow');
                    });
        }
        function comboDateSelected_ValueChanged(s, id) {

            var selectedVal = s.GetValue();

            $('.' + id).hide(id);

            if (selectedVal == 'Specific') {
                $('.' + id + '.specificDateEdit').show('slow');
            } else {
                //$('.specificTimeEdit').show();
            }
        }
        function cboSelectedIndexChanged() {
            var ParmList = "";
            if (comboSelectedReport.GetValue() == "ReportGeoFence_byDriver") {
                ParmList = "{StartDate:'" + StartDate.GetValue() + "',EndDate:'" + EndDate.GetValue() + "',Vehicle:'" + Drivers.GetText() + "', StartDateSpecific:'" + StartDateSpecific.GetValue() + "', EndDateSpecific:'" + EndDateSpecific.GetValue() + "' }";
            }
            else {
                ParmList = "{StartDate:'" + StartDate.GetValue() + "',EndDate:'" + EndDate.GetValue() + "',Vehicle:'" + Vehicle.GetValue() + "', StartDateSpecific:'" + StartDateSpecific.GetValue() + "', EndDateSpecific:'" + EndDateSpecific.GetValue() + "' }";
            }
            $.ajax({
                type: 'POST',
                url: 'ReportScheduler.aspx/setReportParameter',
                data: ParmList,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (r) {
                }
            });
        }
    </script>

    <!--  Report Script Section -->
    <script type="text/javascript"> 
 
        function comboDateSelected_ValueChanged(s, id) {

    var selectedVal = s.GetValue();

    $('.' + id).hide(id);

    if (selectedVal == 'Specific') {
        $('.' + id + '.specificDateEdit').show('slow');
    } else {
        //$('.specificTimeEdit').show();
    } 
}

                                    function comboReportType_ValueChanged(s, e) {

                                        $('.reportType').hide();

                                        var reportType = comboReportType.GetValue();
                                        //console.log('selected value: ' + reportType);

                                        $('.' + reportType).show('slow');

                                        //callbackREportEdit.PerformCallback(reportType);
                                    }

                                    function comboSelectedReport_ValueChanged(s, e) {

                                        var reportName = comboSelectedReport.GetValue();
                                        console.log('selected value: ' + reportName);

                                        var param = {};

                                        param.ReportName = reportName;

                                        //SetSelectedReport(ReportName
                                        ajaxMethod("../DefaultService.svc/" + 'SetSelectedReport',
                                                param, DefaultService_SuccessCallback, DefaultService_ErrorCallback, DefaultService_FinallyCallback);


                                    }

                                    function DefaultService_SuccessCallback(data) { callbackREportEdit.PerformCallback(''); }

                                    function DefaultService_ErrorCallback(data) { }

                                    function DefaultService_FinallyCallback(data) { }

                                    function StartEditRow(s,e)
                                    {
                                        alert('asdasdasdasdasd');
                                    }

                                    function OnEditClick(sender, e) {

                                        alert('Hello');
                                    }
        
                                    function OnBatchStartEdit(s, e) {

                                        alert("Eidt Start");
                                    }
                                    </script>
                                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableTabScrolling="True">
        <TabPages>
            <dx:TabPage Name="Tagb" Text="Report Schedule">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxGridView
                                        KeyFieldName="ReportscheduleID"
                                        ID="dgvReports"
                                        ClientInstanceName="dgvReports"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        Width="100%"
                                        Theme="SoftOrange"
                                        DataSourceID="odsReports"
                                        SettingsBehavior-ConfirmDelete="true"
                                        SettingsText-ConfirmDelete="Are you sure you wish to delete?">
                                        <SettingsPager PageSize="50">
                                        </SettingsPager>
                                        <SettingsSearchPanel Visible="True" />
                                        <Templates>
                                            <EditForm>
                                                <table id="editTable" class="clsEditForm">
                                                    <tr>
                                                        <td>Report</td>
                                                        <td></td>
                                                        <td>Schedule</td>
                                                        <td>Destination</td>
                                                    </tr>
                                                    <tr>
                                                        <%--REPORT--%>
                                                        <td style="padding-top: 5px;" valign="top">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxLabel Width="55px" ID="ASPxLabel2" runat="server" Text="Type"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox
                                                                            TextField="VisibleReportName"
                                                                            ValueField="ActualReportNameToDisplay"
                                                                            ID="comboSelectedReport"
                                                                            runat="server"
                                                                            DataSourceID="odsReport1"
                                                                            ClientInstanceName="comboSelectedReport"
                                                                            AutoPostBack="false" Value='<%# Bind("ReportName") %>'>
                                                                            <ClientSideEvents ValueChanged="function(s,e){comboSelectedReport_ValueChanged(s,e);}" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <dx:ASPxCallbackPanel ID="callbackREportEdit"
                                                                            ClientInstanceName="callbackREportEdit"
                                                                            runat="server">

                                                                            <%--<asp:PlaceHolder ID="mainDIV" runat="server"></asp:PlaceHolder>--%>
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    <uc1:NanoReportParamList runat="server" ID="NanoReportParamList" />
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--PARAMATERS--%>
                                                        <td></td>
                                                        <%--SCHEDULE--%>
                                                        <td valign="top" style="padding-top: 5px;">
                                                            <table id="tblSchedule" class="OptionsTable" style="width: 100%" cellspacing="10">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="lblReportType" runat="server" Text="Frequency">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="comboReportType"
                                                                            runat="server"
                                                                            DataSourceID="odsReportTypes"
                                                                            ClientInstanceName="comboReportType" Value='<%# Bind("ReportType") %>'>
                                                                            <ClientSideEvents ValueChanged="function(s,e){comboReportType_ValueChanged(s,e);}" />
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="reportType Oneoff separator" style="display: none; padding-top: 5px;" id="tdneOFF">
                                                                    <td>
                                                                        <dx:ASPxLabel runat="server" ID="lblOneOffDate" Text="Date:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit TimeSectionProperties-Visible="true"
                                                                            ID="dateEditReportTypeOeOffDate"
                                                                            runat="server" Value='<%# Bind("ScheduleDate")%>'>
                                                                        </dx:ASPxDateEdit>

                                                                        <%-- <dx:ASPxDateEdit runat="server" ID="edBirth" Value='<%# Bind("ScheduleDate") %>' Width="100%">
                                                                               </dx:ASPxDateEdit>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr class="reportType Weekly separator" style="display: none">
                                                                    <td>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Day of week:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="combodayOfWeek" runat="server" DataSourceID="odsDaysOfWeek" Value='<%# Bind("DayofWeek")%>'>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="reportType Monthly separator" style="display: none">
                                                                    <td>
                                                                        <dx:ASPxLabel runat="server" ID="lblDOW" Text="Day of month:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="combo" runat="server" DataSourceID="odsDaysOfMonth" Value='<%# Bind("DayofMonth")%>'>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="reportType Daily Monthly Weekly" style="display: none">
                                                                    <td>
                                                                        <dx:ASPxLabel runat="server" ID="lblTimeOfDay" Text="Time of day:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTimeEdit ID="teTkimeEdit" runat="server" Value='<%# Bind("ScheduleTime")%>'></dx:ASPxTimeEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--DESTINATION--%>
                                                        <td valign="top" style="padding-top: 5px;">
                                                            <dx:ASPxComboBox
                                                                ValueField="NativeID"
                                                                TextField="NameFormatted"
                                                                ID="comboSubscribers"
                                                                runat="server"
                                                                DataSourceID="odsSubscribers" Value='<%# Bind("Recipients")%>'>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                                <div style="text-align: right">
                                                    <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { cboSelectedIndexChanged();
                                            dgvReports.UpdateEdit();
                                             sendMsg('Record Saved!'); }" />

                                                        <%--TRY AND USE THE NANOREPORTPARAMLIST HERE IS POSSIBLE--%>
                                                        <%--POSSIBLE ISSUES: SAVING AND RETREIVING THE VALUES SELECTED (MAYBE USE THE SESSION STATE)--%>
                                                    </dx:ASPxHyperLink>
                                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                        runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            </EditForm>
                                        </Templates>

                                        <Columns>
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ReportscheduleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="Schedule">
                                                <EditFormSettings Visible="false" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Creator" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <%--         <dx:GridViewDataComboBoxColumn FieldName="ReportName" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Report">
                                                 <EditFormSettings VisibleIndex="3"  Visible ="false"/>
                                                <PropertiesComboBox DataSourceID="odsReportList" TextField="VisibleReportName" ValueField="VisibleReportName">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>--%>
                                            <dx:GridViewDataTextColumn FieldName="Creator" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Requestor">
                                                <EditFormSettings Visible="false" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="ReportType" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Report Type" Visible="false">
                                                <EditFormSettings VisibleIndex="5" Visible="false" />
                                                <PropertiesComboBox DataSourceID="odsSchedule">
                                                    <%--<ClientSideEvents ValueChanged="function(s,e){Schedule_ValueChanged(s,e);}" />--%>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <%--  <dx:GridViewDataTextColumn FieldName="Schedule" ShowInCustomizationForm="True" Visible="true" VisibleIndex="6">
                                                   <EditFormSettings VisibleIndex="6"  Visible ="false"/>
                                            </dx:GridViewDataTextColumn>--%>
                                            <dx:GridViewDataDateColumn Caption="Date" FieldName="ScheduleDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="7">
                                                <EditFormSettings VisibleIndex="7" />
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTimeEditColumn Caption="Time of the Day" FieldName="ScheduleTime" Visible="false" CellStyle-CssClass="test" VisibleIndex="8">
                                                <EditFormSettings VisibleIndex="8" />
                                            </dx:GridViewDataTimeEditColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="DayofWeek" ShowInCustomizationForm="True" VisibleIndex="9" Caption="Day of Week" Visible="false">
                                                <EditFormSettings VisibleIndex="9" />
                                                <PropertiesComboBox DataSourceID="odsDaysOfWeek">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="DayofMonth" ShowInCustomizationForm="True" VisibleIndex="10" Caption="Day of Month" Visible="false">
                                                <EditFormSettings VisibleIndex="10" />
                                                <PropertiesComboBox DataSourceID="odsDaysOfMonth">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn Caption="Start Date" FieldName="StartDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="11">
                                                <EditFormSettings VisibleIndex="11" Visible="false" />
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="End Date" FieldName="EndDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="12">
                                                <EditFormSettings VisibleIndex="12" Visible="false" />
                                            </dx:GridViewDataDateColumn>
                                            <%--   <dx:GridViewDataComboBoxColumn FieldName="Vehicle" ShowInCustomizationForm="True" VisibleIndex="13" Caption="Vehicle"  Visible ="false" >
                                                  <EditFormSettings VisibleIndex="13" Visible ="true"  />
                                                <PropertiesComboBox DataSourceID="odsAVDTVehicles" TextField="Name" ValueField="Name">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn> --%>
                                            <dx:GridViewDataTextColumn FieldName="ReportParams" ShowInCustomizationForm="True" Visible="true" VisibleIndex="14">
                                                <EditFormSettings VisibleIndex="14" Visible="false" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Recipients" ShowInCustomizationForm="True" Visible="True" VisibleIndex="15">
                                                <EditFormSettings VisibleIndex="15" Visible="false" />
                                            </dx:GridViewDataTextColumn>
                                            <%--     <dx:GridViewDataComboBoxColumn FieldName="Recipients" Caption="Recipients" VisibleIndex="15">
                                                <PropertiesComboBox DropDownStyle="DropDown" DataSourceID="odsSubscribers" TextField="NameFormatted" ValueField="NativeID">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>--%>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:ObjectDataSource ID="odsReports" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ReportSchedule" DeleteMethod="delete" InsertMethod="insert" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.ReportSchedule" UpdateMethod="update">
                                        <SelectParameters>
                                            <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource runat="server" ID="odsReportList" SelectMethod="GetAllReports" TypeName="FMS.WEB.AvailableReport"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource runat="server" ID="odsSchedule" SelectMethod="GetReportTypes" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="odsDaysOfWeek" runat="server" SelectMethod="GetDaysOfWeek" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="odsDaysOfMonth" runat="server" SelectMethod="GetMonthDays" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource runat="server" ID="odsAVDTVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="odsSubscribers" runat="server" SelectMethod="GetAllforApplication" TypeName="FMS.Business.DataObjects.Subscriber">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:ObjectDataSource runat="server" ID="odsReportTypes" SelectMethod="GetReportTypes" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource runat="server" ID="odsReport1" SelectMethod="GetAllReports" TypeName="FMS.WEB.AvailableReport"></asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Graphs">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        Graphs Content
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Report">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        Report Schedule  Content
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
