<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ResourceMgmtReport.aspx.vb" Inherits="FMS.WEB.ResourceMgmtReport" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>   
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0" EnableTabScrolling="True">
        <TabPages>
            <dx:TabPage Name="Tagb" Text="Reports">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table style="width: 100%;">
                            <tr> 
                                <td style="padding: 2px;"> 
                                    <dx:ASPxLabel ID="ASPxLabel4"
                                        Width="90px"
                                        runat="server"
                                        Text="Select report: ">
                                    </dx:ASPxLabel> 
                                </td> 
                                <td style="padding: 2px;"> 
                                    <dx:ASPxComboBox ID="ASPxComboBox2"
                                        runat="server"
                                        Theme="SoftOrange"
                                        DataSourceID="odsReports"
                                        EnableTheming="True"
                                        ClientInstanceName="cboSlectedReport"
                                        ValueField="DataforJavascript"
                                        TextField="VisibleReportName"> 
                                        <ClientSideEvents KeyDown="function(s,e){return false;}" SelectedIndexChanged="function(s, e) {
	cboSlectedReport_SelectedIndexChanged(s,e)
}"></ClientSideEvents>
<ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxComboBox> 
                                    <asp:ObjectDataSource runat="server" ID="odsReports"
                                        SelectMethod="GetAllReports"
                                        TypeName="FMS.ReportLogic.AvailableReport"></asp:ObjectDataSource>
                                </td> 
                                <td style="padding: 4px;">  
                                    <%--<dx:ASPxButton AutoPostBack="false"
                                        ID="ASPxButton2"
                                        runat="server"
                                        Text="load report"
                                        ClientInstanceName="btnViewReport">
                                    </dx:ASPxButton>--%>

                                </td>
                                <td style="width: 100%; padding-left: 50px;" align="left"> 
                                    <dx:ASPxLabel CssClass="descriptionlabel_nomore"
                                        ClientInstanceName="descriptionlabel"
                                        Font-Size="Larger"
                                        ID="ASPxLabel5"
                                        runat="server"
                                        Text="">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>  
                        <iframe id="frmContent" src="" style="width: 100%; bottom: 10px; border: none; overflow-y: visible;" class="row"></iframe>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            </TabPages>
            </dx:ASPxPageControl>  
    <script type="text/javascript">

        function cboSlectedReport_SelectedIndexChanged(s,e) { 

            var jScriptDataStr = cboSlectedReport.GetValue();
            var arr = jScriptDataStr.split("|");

            var selectedReport = arr[0];
            var description = arr[1];

            $('#frmContent').attr('src', 'ResourseMgmtRptContent.aspx?Report=' + selectedReport); 
        }
    </script>
</asp:Content>
