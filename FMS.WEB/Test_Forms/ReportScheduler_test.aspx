﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Light.master" CodeBehind="ReportScheduler_test.aspx.vb" Inherits="FMS.WEB.ReportScheduler_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script src="../Content/javascript/jquery-3.1.0.min.js"></script>

    <dx:ASPxGridView ID="ASPxGridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataSourceID="odsReportSchedules"
        Settings-ShowColumnHeaders="false"
        Theme="SoftOrange">

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

                $('.reportType').hide('slow');

                var reportType = comboReportType.GetValue();
                console.log('selected value: ' + reportType);

                $('.' + reportType).show('slow');

            }

        </script>


        <%--    dateEditReportTypeOeOffDate
                comboSelectedReport
                comboReportType
                dateEditReportTypeOeOffDate
                combodayOfWeek                          --%>


        <style type="text/css">
            #editTable td {
                padding: 3px;
                vertical-align: top;
                text-align: left;
            }

            .reportType {
                visibility: hidden;
            }
        </style>

        <table id="editTable">

            <tr>
                <td>Report</td>

                <td>Schedule</td>

                <td>Parameters</td>

                <td>Destination</td>

            </tr>

            <tr>
                <td>

                    <dx:ASPxComboBox
                        TextField="VisibleReportName"
                        ValueField="VisibleReportName"
                        ID="comboSelectedReport"
                        runat="server"
                        DataSourceID="odsReports"
                        Theme="SoftOrange">
                    </dx:ASPxComboBox>

                </td>
                <td>

                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="lblReportType" runat="server" Text="ReportType">
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
                                <dx:ASPxDateEdit ID="dateEditReportTypeOeOffDate" runat="server" Theme="SoftOrange" EditFormat="DateTime"></dx:ASPxDateEdit>
                            </td>
                        </tr>

                        <tr class="reportType Daily">
                            <td>
                                <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Date:"></dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dateEditDaily" runat="server" Theme="SoftOrange" EditFormat="DateTime"></dx:ASPxDateEdit>
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
                                <dx:ASPxLabel runat="server" ID="lblDOW" Text="Day of week:"></dx:ASPxLabel>

                            </td>
                            <td>
                                <dx:ASPxComboBox ID="combo" runat="server" DataSourceID="odsDaysOfWeek"></dx:ASPxComboBox>
                            </td>
                        </tr>



                    </table>



                </td>
            </tr>
            <tr>
            </tr>

        </table>

    </div>

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
