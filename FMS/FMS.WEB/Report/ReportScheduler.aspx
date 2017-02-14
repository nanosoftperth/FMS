<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportScheduler.aspx.vb" Inherits="FMS.WEB.ReportScheduler" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type ="text/javascript">


    </script>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableTabScrolling="True">
        <TabPages>
            <dx:TabPage Name="Tagb" Text="Reports"> 
                <ContentCollection>    
                     <dx:ContentControl runat="server">  
                         <table>
                             <tr><td>  
                                 <dx:ASPxGridView KeyFieldName="ReportscheduleID" ID="dgvReports" ClientInstanceName="dgvReports" runat="server" AutoGenerateColumns="False" Width="100%" Theme="SoftOrange">
                                    <SettingsPager PageSize="50">
                                    </SettingsPager>
                                    <SettingsSearchPanel Visible="True" />
                                    <Templates>
                                        <EditForm>
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ID="tr" ReplacementType="EditFormEditors">
                                    </dx:ASPxGridViewTemplateReplacement>
                                    <div style="text-align:right">
                                        update 
                                   </div>
                                </EditForm>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ReportscheduleID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>      
                                        
                                          <dx:GridViewDataComboBoxColumn FieldName="ReportscheduleID" ShowInCustomizationForm="True" VisibleIndex="3" Caption="Report Name">
                                            <PropertiesComboBox DataSourceID="odsAppFeatRoleFeatures" TextField="Name" ValueField="ReportscheduleID">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>                                 
                                    </Columns>

                                         
                                </dx:ASPxGridView>
                                   
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
