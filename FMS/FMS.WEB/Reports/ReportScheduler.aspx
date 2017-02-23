<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportScheduler.aspx.vb" Inherits="FMS.WEB.ReportScheduler" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Src="~/Controls/NanoReportParamList.ascx" TagPrefix="uc1" TagName="NanoReportParamList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Page.ResolveClientUrl("~/Content/Jira.css")%>" rel="stylesheet" />
    <script src='<%= Page.ResolveClientUrl("~/Content/javascript/jquery-3.1.0.min.js")%>'></script>

    <script type="text/javascript">
        function sendMsg(message) {
            $('#aui-flag-container').hide();
            $('#msg').text(message);
            $('#aui-flag-container').toggle('slow'
                    , function () {
                        $(this).delay(2500).toggle('slow');
                    });
        }
        function Schedule_ValueChanged(s, e) {
            dgvReports.PerformCallback(s.GetValue());
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
        function cboSelectedIndexChanged() { alert('Hello')}


    </script>

 
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableTabScrolling="True">
        <TabPages>
            <dx:TabPage Name="Tagb" Text="Reports">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxGridView KeyFieldName="ReportscheduleID" ID="dgvReports"  ClientInstanceName="dgvReports" runat="server" AutoGenerateColumns="False" Width="100%" Theme="SoftOrange" DataSourceID="odsReports">
                                        <SettingsPager PageSize="50">
                                        </SettingsPager>
                                        <SettingsSearchPanel Visible="True" />
                                        <Templates>
                                            <EditForm> 
                                                <dx:ASPxLabel ID="rptType"  runat ="server" Text ="Report Type" ></dx:ASPxLabel>
                                                <dx:ASPxComboBox  ID="ReportType"  Value='<%# Bind("ReportName") %>'   runat ="server"  DataSourceID="odsReportList" TextField ="VisibleReportName" ValueField ="VisibleReportName"> 
                                                      <ClientSideEvents SelectedIndexChanged="function(s, e) { cboSelectedIndexChanged();}"  />  
                                                </dx:ASPxComboBox>  

                                                <uc1:NanoReportParamList  ID ="txt"  runat ="server" />


                                             <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors"></dx:ASPxGridViewTemplateReplacement>
                                                <div style="text-align: right">
                                                    <dx:ASPxHyperLink Style="text-decoration: underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvReports.UpdateEdit();
                                             sendMsg('Record Saved!'); }" />  

                                                        <%--TRY AND USE THE NANOREPORTPARAMLIST HERE IS POSSIBLE--%>
                                                        <%--POSSIBLE ISSUES: SAVING AND RETREIVING THE VALUES SELECTED (MAYBE USE THE SESSION STATE)--%>
                                                </dx:ASPxHyperLink>
                                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                                        runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                </div>

                                                <dx:ASPxCallbackPanel ID="callbackREportEdit"
                                                ClientInstanceName="callbackREportEdit"
                                                runat="server">
                                                 
                                        </dx:ASPxCallbackPanel> 
                                                 
                                              
                                            </EditForm>
                                        </Templates>
                                        <Columns>
                                        
                                            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ReportscheduleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
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
                                                <EditFormSettings Visible="false"  />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="ReportType" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Report Type" Visible ="false">
                                                <EditFormSettings VisibleIndex="5"  Visible ="false"/>
                                                <PropertiesComboBox DataSourceID="odsSchedule">
                                                    <ClientSideEvents ValueChanged="function(s,e){Schedule_ValueChanged(s,e);}" />
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn> 
                                             <dx:GridViewDataTextColumn FieldName="Schedule" ShowInCustomizationForm="True" Visible="true" VisibleIndex="6">
                                                   <EditFormSettings VisibleIndex="6"  Visible ="false"/>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Date" FieldName="ScheduleDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="7">
                                                <EditFormSettings VisibleIndex="7" />
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTimeEditColumn Caption="Time of the Day" FieldName="ScheduleTime" Visible="false" CellStyle-CssClass="test" VisibleIndex="8">                                                  
                                                <EditFormSettings VisibleIndex="8" />
                                            </dx:GridViewDataTimeEditColumn>                                        
                                            <dx:GridViewDataComboBoxColumn FieldName="DayofWeek" ShowInCustomizationForm="True" VisibleIndex="9" Caption="Day of Week" Visible ="false">
                                                   <EditFormSettings VisibleIndex="9" />
                                                <PropertiesComboBox DataSourceID="odsDaysOfWeek">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="DayofMonth" ShowInCustomizationForm="True" VisibleIndex="10" Caption="Day of Month" Visible ="false">
                                                   <EditFormSettings VisibleIndex="10" />
                                                <PropertiesComboBox DataSourceID="odsDaysOfMonth">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataDateColumn Caption="Start Date" FieldName="StartDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="11">
                                                <EditFormSettings VisibleIndex="11"  Visible ="false"/>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="End Date" FieldName="EndDate" Visible="false" CellStyle-CssClass="test" VisibleIndex="12">
                                                <EditFormSettings VisibleIndex="12" Visible ="false"  />
                                            </dx:GridViewDataDateColumn>
                                         <%--   <dx:GridViewDataComboBoxColumn FieldName="Vehicle" ShowInCustomizationForm="True" VisibleIndex="13" Caption="Vehicle"  Visible ="false" >
                                                  <EditFormSettings VisibleIndex="13" Visible ="true"  />
                                                <PropertiesComboBox DataSourceID="odsAVDTVehicles" TextField="Name" ValueField="Name">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>  --%>
                                            <dx:GridViewDataTextColumn FieldName="ReportParams" ShowInCustomizationForm="True" Visible="true" VisibleIndex="14">
                                             <EditFormSettings VisibleIndex="14" Visible ="false"  />
                                            </dx:GridViewDataTextColumn> 
                                            <dx:GridViewDataComboBoxColumn FieldName="Recipients" Caption="Recipients" VisibleIndex="15">
                                                <PropertiesComboBox DropDownStyle="DropDown" DataSourceID="odsSubscribers" TextField="NameFormatted" ValueField="NativeID">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
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
            <dx:TabPage Text="report Schedule">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        Report Schedule  Content
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>

</asp:Content>
