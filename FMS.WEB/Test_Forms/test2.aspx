<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test2.aspx.vb" Inherits="FMS.WEB.test2" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>

<%@ Register assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v17.2.Web, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web.WebDocumentViewer" tagprefix="cc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>


<body>

    <style type="text/css">
        .paddedcell {
            padding: 3px;
        }
    </style>

    <form id="form1" runat="server">
        <table>
            <tr>
                <td class="paddedcell">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Select Vehicle"></dx:ASPxLabel>
                </td>
                <td class="paddedcell">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueField="ApplicationVehileID" TextField="Name" Width="100%" DataSourceID="odsVehicles"></dx:ASPxComboBox>
                    <asp:ObjectDataSource runat="server" ID="odsVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appplicationID" DefaultValue="9B8CC16F-B045-42F8-A53E-1FAFB955232F"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="paddedcell"></td>
                <td class="paddedcell"></td>
                <td></td>
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
                <td class="paddedcell">

                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Show Report" Width="106px"></dx:ASPxButton>
                </td>

            </tr>
        </table>



        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1"
            runat="server">

            <ClientSideEvents />

        </dx:ASPxGlobalEvents>
        <dx:ASPxDocumentViewer runat="server" EnableTheming="True" Height="1100px" ReportTypeName="FMS.WEB.VehicleReport" Theme="SoftOrange" Width="100%">
        </dx:ASPxDocumentViewer>
    </form>
</body>


<script type="text/javascript">



    //$(document).ready(function () { configDateShowPopup(); })

</script>

</html>
