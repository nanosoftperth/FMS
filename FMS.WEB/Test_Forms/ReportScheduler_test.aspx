<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Light.master" CodeBehind="ReportScheduler_test.aspx.vb" Inherits="FMS.WEB.ReportScheduler_test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsReportSchedules" Theme="SoftOrange">
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





    <asp:ObjectDataSource runat="server" ID="odsReportSchedules" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.ReportSchedule">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
