<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataLogger7502Report.aspx.vb" Inherits="FMS.WEB.DataLogger7502Report" %>

<%@ Register assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Test Class" OnClick="Button1_Click" />
        </div>
        <div>
            <dx:WebChartControl ID="WebChartControl2" runat="server" Height="300px"
                Width="700px" ClientInstanceName="chart"
                CrosshairEnabled="False" ToolTipEnabled="False"
                SeriesDataMember = "Description">
                <Legend AlignmentHorizontal="Center" AlignmentVertical="BottomOutside" Direction="LeftToRight" Visibility="False">
                </Legend>
                <Titles>
                    <dx:ChartTitle Text="7502 Data Logger"></dx:ChartTitle>
                </Titles>
                <SeriesTemplate ArgumentScaleType="DateTime" ArgumentDataMember="DateX" ValueDataMembersSerializable="ValueX" LabelsVisibility="True">
                    <ViewSerializable>
                        <dx:StackedLineSeriesView></dx:StackedLineSeriesView>
                    </ViewSerializable>
                </SeriesTemplate> 
                <DiagramSerializable>
                    <dx:XYDiagram>
                        <AxisX Title-Text="Years" VisibleInPanesSerializable="-1">
                            <DateTimeScaleOptions ScaleMode="Manual" MeasureUnit="Minute"/>
                            <GridLines Visible="True"></GridLines>
                        </AxisX>
                        <AxisY Interlaced="True" MinorCount="4"  Title-Text="Speed" Title-Visibility="True" VisibleInPanesSerializable="-1">
                            <WholeRange AlwaysShowZeroLevel="False"/>
                        </AxisY>
                    </dx:XYDiagram>
                </DiagramSerializable>
                <BorderOptions Visibility="False" />
            </dx:WebChartControl>
            <dx:WebChartControl ID="WebChartControl1" runat="server" Height="400px"
                Width="700px" ClientInstanceName="chart"
                CrosshairEnabled="False" ToolTipEnabled="False"
                OnCustomDrawSeriesPoint="WebChartControl1_CustomDrawSeriesPoint"
                SeriesDataMember="direction" >
                <SeriesTemplate ArgumentDataMember="description" ValueDataMembersSerializable="Value" >
                    <ViewSerializable>
                        <dx:StackedBarSeriesView></dx:StackedBarSeriesView>
                    </ViewSerializable>
                    <LabelSerializable>
                        <dx:StackedBarSeriesLabel Font="Tahoma, 8pt, style=Bold">
                        </dx:StackedBarSeriesLabel>
                    </LabelSerializable>
                </SeriesTemplate>
                <Legend Direction="BottomToTop" Visibility="False">
                </Legend>
                <BorderOptions Visibility="False" />
                <Titles>
                    <%--<dx:ChartTitle Text="7502 Data Logger"></dx:ChartTitle>--%>
                    <%--<dx:ChartTitle Dock="Bottom" Alignment="Far" Text="From www.cia.gov" Font="Tahoma, 8pt" TextColor="Gray"></dx:ChartTitle>--%>
                </Titles>
                <DiagramSerializable>
                    <dx:XYDiagram Rotated="True">
                        <AxisX VisibleInPanesSerializable="-1">
                        </AxisX>
                       <%-- <AxisY Title-Text="Millions" Title-Visibility="True" VisibleInPanesSerializable="-1">
                            <Label TextPattern="{V:0,,}"/>
                            <GridLines MinorVisible="True"></GridLines>
                        </AxisY>--%>
                        <defaultpane>
                            <%--<stackedbartotallabel textpattern="Total {TV:0,,.0}" visible="True">
                            </stackedbartotallabel>--%>
                        </defaultpane>
                    </dx:XYDiagram>
                </DiagramSerializable>
            </dx:WebChartControl>
        </div>
    </form>
</body>
</html>
