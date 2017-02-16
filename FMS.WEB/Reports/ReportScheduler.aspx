<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportScheduler.aspx.vb" Inherits="FMS.WEB.ReportScheduler" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <link href="<%= Page.ResolveClientUrl("~/Content/Jira.css")%>"  rel="stylesheet" />
   <script src= <%= Page.ResolveClientUrl("~/Content/javascript/jquery-3.1.0.min.js")%>></script>
     
   <script type ="text/javascript"> 
        function sendMsg(message) {
            $('#aui-flag-container').hide();
            $('#msg').text(message);
            $('#aui-flag-container').toggle('slow'
                    , function () {
                        $(this).delay(2500).toggle('slow');
                    });
        }
        function Schedule_ValueChanged(s, e)
        {
         

            $(".test").show();
        }
    </script>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableTabScrolling="True">
        <TabPages>
            <dx:TabPage Name="Tagb" Text="Reports"> 
                <ContentCollection>    
                     <dx:ContentControl runat="server">  
                         <table>
                             <tr><td>  
                                 <dx:ASPxGridView KeyFieldName="ReportscheduleID" ID="dgvReports" ClientInstanceName="dgvReports" runat="server" AutoGenerateColumns="False" Width="100%" Theme="SoftOrange"  DataSourceID ="odsReports">
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <SettingsSearchPanel Visible="True" />
                                    <Templates>
                                        <EditForm>
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors">
                                    </dx:ASPxGridViewTemplateReplacement>
                                    <div style="text-align:right">
                                    <dx:ASPxHyperLink style="text-decoration:underline" ID="lnkUpdate" runat="server" Text="Update" Theme="SoftOrange" NavigateUrl="javascript:void(0);">
                                        <ClientSideEvents Click="function (s, e) { 
                                            dgvReports.UpdateEdit();
                                             sendMsg('Record Saved!');
                                            }" />
                                    </dx:ASPxHyperLink>
                                    <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementCancel" ReplacementType="EditFormCancelButton"
                                          runat="server"></dx:ASPxGridViewTemplateReplacement></div>
                                </EditForm>
                                    </Templates>                                 
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ReportscheduleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" >
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>   
                                        <dx:GridViewDataTextColumn FieldName="Creator" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>    
                                        <dx:GridViewDataTextColumn FieldName="ReportName" ShowInCustomizationForm="True" Visible="True" VisibleIndex="3">
                                          
                                        </dx:GridViewDataTextColumn>                                        
                                        <dx:GridViewDataComboBoxColumn FieldName="ReportType" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Report Type">
                                            <PropertiesComboBox DataSourceID="odsReportList" TextField="VisibleReportName" ValueField="VisibleReportName">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn> 
                                        <dx:GridViewDataComboBoxColumn FieldName="Schedule" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Schedule" >
                                            <PropertiesComboBox DataSourceID="odsSchedule"  >
                                                 <ClientSideEvents ValueChanged="function(s,e){Schedule_ValueChanged(s,e);}" />
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>   
                                       <dx:GridViewDataDateColumn  Caption ="Date" FieldName ="ScheduleDate" Visible ="true" CellStyle-CssClass="test" VisibleIndex="6" > 
                                       </dx:GridViewDataDateColumn>                                         
                                    </Columns> 
                                </dx:ASPxGridView>

                               <asp:ObjectDataSource ID="odsReports" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ReportSchedule" DeleteMethod="delete" InsertMethod="insert" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.ReportSchedule" UpdateMethod="update">
                                    <SelectParameters>
                                        <asp:SessionParameter DbType="Guid" Name="appid" SessionField="ApplicationID" /> 
                                    </SelectParameters>
                               </asp:ObjectDataSource>  
                               <asp:ObjectDataSource runat="server" ID="odsReportList" SelectMethod="GetAllReports" TypeName="FMS.WEB.AvailableReport"></asp:ObjectDataSource>    
                               <asp:ObjectDataSource runat="server" ID="odsSchedule" SelectMethod="GetReportTypes" TypeName="FMS.Business.DataObjects.ReportSchedule"></asp:ObjectDataSource>
                                  
                             </td></tr> 
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
