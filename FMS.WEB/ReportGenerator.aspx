<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="ReportGenerator.aspx.vb" Inherits="FMS.WEB.ReportGenerator" %>

<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script src="Content/javascript/jquery-1.10.2.min.js"></script>



    <style type="text/css">
        .paddedcell {
            padding: 3px;
        }
        .dxtc-content{ height:500px;}
    </style>
       

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
                                     
                                    <dx:ASPxHyperLink ID="ASPxHyperLink1"
                                        runat="server"
                                        Text="load report"
                                        ClientInstanceName="btnViewReport" 
                                        Cursor="pointer" 
                                        Width="100px">
                                        <ClientSideEvents Click="function(s, e) {
	btnViewReport_Click(s,e);
}"></ClientSideEvents>
                                    </dx:ASPxHyperLink>

                                    <%--<dx:ASPxButton AutoPostBack="false"
                                        ID="ASPxButton2"
                                        runat="server"
                                        Text="load report"
                                        ClientInstanceName="btnViewReport">
                                    </dx:ASPxButton>--%>

                                <%--</td>--%>
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
            <dx:TabPage Text="Grahps" Visible="False">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td class="paddedcell">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Select Vehicle"></dx:ASPxLabel>
                                            </td>
                                            <td class="paddedcell">
                                                <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueField="ApplicationVehileID" TextField="Name" Width="100%" DataSourceID="odsVehicles">
                                                    <ClearButton Visibility="Auto"></ClearButton>
                                                </dx:ASPxComboBox>
                                                <asp:ObjectDataSource runat="server" ID="odsVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                                                    <SelectParameters>
                                                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>  
                                        </tr>
                                        <tr> 
                                            <td class="paddedcell">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Time From:"></dx:ASPxLabel>
                                            </td>
                                            <td class="paddedcell">
                                                <dx:ASPxDateEdit ID="deTimeFrom"
                                                    runat="server"
                                                    Date="01/20/2016 00:06:00"
                                                    EditFormat="DateTime"
                                                    EnableTheming="True"
                                                    Height="22px"
                                                    Paddings-PaddingLeft="10px"
                                                    padding-right="10px"
                                                    Theme="MetropolisBlue"
                                                    Width="198px">

                                                    <TimeSectionProperties Visible="True">
                                                        <TimeEditProperties>
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </TimeEditProperties>
                                                    </TimeSectionProperties>

                                                    <Buttons>
                                                        <dx:EditButton Visible="false" Text="View">
                                                        </dx:EditButton>
                                                    </Buttons>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>

                                                    <Paddings PaddingLeft="10px"></Paddings>
                                                </dx:ASPxDateEdit>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="paddedcell">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Time To:"></dx:ASPxLabel>
                                            </td>
                                            <td class="paddedcell">
                                                <dx:ASPxDateEdit ID="deTimeTo"
                                                    runat="server"
                                                    Date="01/20/2016 00:06:00"
                                                    EditFormat="DateTime"
                                                    EnableTheming="True"
                                                    Height="22px"
                                                    Paddings-PaddingLeft="10px"
                                                    padding-right="10px"
                                                    Theme="MetropolisBlue"
                                                    Width="198px">

                                                    <TimeSectionProperties Visible="True">
                                                        <TimeEditProperties>
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </TimeEditProperties>
                                                    </TimeSectionProperties>

                                                    <Buttons>
                                                        <dx:EditButton Visible="false" Text="View">
                                                        </dx:EditButton>
                                                    </Buttons>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>

                                                    <Paddings PaddingLeft="10px"></Paddings>
                                                </dx:ASPxDateEdit>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td class="paddedcell"></td>
                                            <td class="paddedcell">

                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Show Report" Width="106px"></dx:ASPxButton>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <dxchartsui:WebChartControl ID="chartSpeed" runat="server" CrosshairEnabled="True" Height="460px" Width="699px">
                                        <DiagramSerializable>
                                            <cc1:XYDiagram>
                                                <axisx visibleinpanesserializable="-1" title-visibility="Default">
                    </axisx>
                                                <axisy visibleinpanesserializable="-1">
                    </axisy>
                                                <secondaryaxesy>
                    <cc1:SecondaryAxisY AxisID="0" Name="Secondary AxisY 1" VisibleInPanesSerializable="-1">
                    </cc1:SecondaryAxisY>
                </secondaryaxesy>
                                            </cc1:XYDiagram>
                                        </DiagramSerializable>
                                        <SeriesSerializable>
                                            <cc1:Series LabelsVisibility="False" Name="Distance">
                                                <viewserializable>
                    <cc1:AreaSeriesView AxisYName="Secondary AxisY 1" Color="250, 192, 143" MarkerVisibility="False" Transparency="90">
                    </cc1:AreaSeriesView>
                </viewserializable>
                                            </cc1:Series>
                                            <cc1:Series Name="Time">
                                                <viewserializable>
                        <cc1:SplineSeriesView MarkerVisibility="False" Color="49, 133, 155">
                        </cc1:SplineSeriesView>
                    </viewserializable>
                                            </cc1:Series>

                                        </SeriesSerializable>
                                    </dxchartsui:WebChartControl>

                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Graphs">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="report schedule">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <iframe id="frmContent" src="Reports/ReportScheduler.aspx" style="width: 100%; height:100%; bottom: 10px; border: none; overflow-y: visible; margin-left:0px;" class="row"></iframe>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>


    <script type="text/javascript">


        function ReportGenerator_ControlsInitialised() {

            $('.dxtc-sbSpacer').css('height', '24px');
            $('.dxtc-rightIndent').css('height', '24px');
            $('.dxtc-sbIndent').css('height', '24px');
            $('.dxtc-leftIndent').css('height', '24px');
        }

        function btnViewReport_Click() {


            var jScriptDataStr = cboSlectedReport.GetValue();
            var arr = jScriptDataStr.split("|");

            var selectedReport = arr[0];
            var description = arr[1];

            $('#frmContent').attr('src', 'ReportContent.aspx?Report=' + selectedReport);
            //descriptionlabel
        }

        function cboSlectedReport_SelectedIndexChanged(s, e) {

            var jScriptDataStr = cboSlectedReport.GetValue();
            var arr = jScriptDataStr.split("|");
            var description = arr[1];

            description = '"' + description + '"';

            descriptionlabel.SetText(description);
        }

    </script>

    <style type="text/css">
        .dxtc-sbSpacer, .dxtc-rightIndent, .dxtc-sbIndent, .dxtc-leftIndent {
            height: 24px !important;
        }

        .descriptionlabel {
            float: right;
        }
    </style>


    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents2" runat="server">
        <ClientSideEvents ControlsInitialized="ReportGenerator_ControlsInitialised" />
    </dx:ASPxGlobalEvents>

</asp:Content>
