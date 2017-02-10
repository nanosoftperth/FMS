<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Light.master" CodeBehind="ReportScheduler_test.aspx.vb" Inherits="FMS.WEB.ReportScheduler_test" %>

<%@ Register Src="~/Controls/NanoReportParamList.ascx" TagPrefix="uc1" TagName="NanoReportParamList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script src="../Content/javascript/jquery-3.1.0.min.js"></script>
    <script src="../Content/javascript/page.js"></script>


    <dx:ASPxGridView ID="ASPxGridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataSourceID="odsReportSchedules"
        Settings-ShowColumnHeaders="false">

        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>

        <Columns>

            <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowEditButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ReportscheduleID" VisibleIndex="1"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="2"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ReportName" VisibleIndex="3"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ReportType" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ReportTypeSpecific" VisibleIndex="5"></dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="ReportTime" VisibleIndex="6">
                <PropertiesDateEdit>
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto"></ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>

                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataCheckColumn FieldName="Enabled" VisibleIndex="7"></dx:GridViewDataCheckColumn>
            <dx:GridViewDataDateColumn FieldName="DateCreated" VisibleIndex="8">
                <PropertiesDateEdit>
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto"></ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>

                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Creator" VisibleIndex="9"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ReportParams" VisibleIndex="10"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SunbscriberID" VisibleIndex="11"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <div id="reportShcedulerEditDiv">

        <script type="text/javascript">

            function comboReportType_ValueChanged(s, e) {

                $('.reportType').hide();

                var reportType = comboReportType.GetValue();
                console.log('selected value: ' + reportType);

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

        </script>


        <%--    dateEditReportTypeOeOffDate
                comboSelectedReport
                comboReportType
                dateEditReportTypeOeOffDate
                combodayOfWeek            
                dateEditDaily    
                teTkimeEdit
        --%>


        <style type="text/css">
            #editTable td {
                padding: 3px;
                vertical-align: top;
                text-align: left;
            }

            .reportType {
                display: none;
            }
        </style>

        <table id="editTable">

            <tr>
                <td>Report</td>
                <td></td>
                <td>Schedule</td>
                <td>Destination</td>
            </tr>

            <tr>

                <%--REPORT--%>
                <td>

                    <table>
                        <tr>
                            <td ">
                                <dx:ASPxLabel Width="55px" ID="ASPxLabel2" runat="server" Text="Type"></dx:ASPxLabel>
                            </td>
                            <td style="padding-left:4px;">
                                <dx:ASPxComboBox
                                    TextField="VisibleReportName"
                                    ValueField="ActualReportNameToDisplay"
                                    ID="comboSelectedReport"
                                    runat="server"
                                    DataSourceID="odsReports"
                                    ClientInstanceName="comboSelectedReport"
                                    AutoPostBack="false">

                                    <ClientSideEvents ValueChanged="function(s,e){comboSelectedReport_ValueChanged(s,e);}" />

                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <dx:ASPxCallbackPanel ID="callbackREportEdit"
                                    ClientInstanceName="callbackREportEdit"
                                    runat="server">

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
                <td>

                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="lblReportType" runat="server" Text="Frequency">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="comboReportType"
                                    runat="server"
                                    DataSourceID="odsReportTypes"
                                    ClientInstanceName="comboReportType">
                                    <ClientSideEvents ValueChanged="function(s,e){comboReportType_ValueChanged(s,e);}" />

                                </dx:ASPxComboBox>

                            </td>
                        </tr>
                        <tr class="reportType One-off">
                            <td>
                                <dx:ASPxLabel runat="server" ID="lblOneOffDate" Text="Date:"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit TimeSectionProperties-Visible="true" DisplayFormatString="G" ID="dateEditReportTypeOeOffDate" runat="server" EditFormat="DateTime"></dx:ASPxDateEdit>
                            </td>
                        </tr>

                        <tr class="reportType Weekly">
                            <td>
                                <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Day of week:"></dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxComboBox ID="combodayOfWeek" runat="server" DataSourceID="odsDaysOfWeek"></dx:ASPxComboBox>
                            </td>
                        </tr>

                        <tr class="reportType Monthly">
                            <td>
                                <dx:ASPxLabel runat="server" ID="lblDOW" Text="Day of month:"></dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxComboBox ID="combo" runat="server" DataSourceID="odsDaysOfMonth"></dx:ASPxComboBox>
                            </td>
                        </tr>


                        <tr class="reportType Daily Monthly Weekly">
                            <td>
                                <dx:ASPxLabel runat="server" ID="lblTimeOfDay" Text="Time of day:"></dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTimeEdit ID="teTkimeEdit" runat="server"></dx:ASPxTimeEdit>
                            </td>
                        </tr>
                    </table>

                </td>
                <%--DESTINATION--%>

                <td>
                    <dx:ASPxComboBox
                        ValueField="NativeID"
                        TextField="NameFormatted"
                        ID="comboSubscribers"
                        runat="server"
                        DataSourceID="odsSubscribers">
                    </dx:ASPxComboBox>
                </td>
            </tr>


        </table>

    </div>




    <asp:ObjectDataSource runat="server" ID="odsSubscribers" SelectMethod="GetAllforApplication" TypeName="FMS.Business.DataObjects.Subscriber">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="odsReportTypes" SelectMethod="GetReportTypes" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDaysOfMonth" runat="server" SelectMethod="GetMonthDays" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsDaysOfWeek" runat="server" SelectMethod="GetDaysOfWeek" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="odsReports" SelectMethod="GetAllReports" TypeName="FMS.WEB.AvailableReport"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="odsReportSchedules" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.ReportSchedule">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
