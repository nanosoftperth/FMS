<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataLogger7502Report.aspx.vb" Inherits="FMS.WEB.DataLogger7502Report" %>

<%@ Register assembly="DevExpress.XtraCharts.v18.1.Web, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script>
        $(document).ready(function () {
            //google.maps.event.addDomListener(window, 'load', initialize);

            //window.onload = initialize;
            //initialize();
        });
        function InitializeMap() {

            var latlng = new google.maps.LatLng(-31.9538987, 115.85823189999996);
            var myOptions =
                {
                    zoom: 8,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    disableDefaultUI: true
                };
            map = new google.maps.Map(document.getElementById("map"), myOptions);
        }
        
        window.onload = InitializeMap;
        
    </script>
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
        <div style="padding: 10px 10px 10px 10px;">
            <%--<div id="googleMap" style="width: 100%; height: 100%; background-color: gray;"></div>--%>
            <div id ="map"   style="width: 700px; height: 400px">
            </div>
        </div>
    </form>
</body>
</html>
