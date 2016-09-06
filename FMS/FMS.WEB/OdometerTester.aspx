﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OdometerTester.aspx.vb" Inherits="FMS.WEB.OdometerTester" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <style type="text/css">
                .floatright {
                    float: right;
                }

                #leftTable td {
                    padding: 2px;
                }
                #leftTable{
                    float:left;
                    padding-left:20px;
                }


            </style>

            <table id="leftTable">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="DeviceID"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="comboDEviceID" TextField="DeviceID"  ValueField="DeviceID" runat="server" DataSourceID="odsDevices" Theme="SoftOrange"></dx:ASPxComboBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="StartTime"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dateEditStartTime" runat="server" EditFormat="DateTime" EnableTheming="True" Theme="SoftOrange">
                            <TimeSectionProperties>
                                <TimeEditProperties>
                                    <ClearButton Visibility="Auto"></ClearButton>
                                </TimeEditProperties>
                            </TimeSectionProperties>

                            <ClearButton Visibility="Auto" DisplayMode="OnHover"></ClearButton>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>

                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="EndTime"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dateEditEndTime" runat="server" EditFormat="DateTime" Theme="SoftOrange"></dx:ASPxDateEdit>
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxButton ID="btnSearch" CssClas="floatright" runat="server" Text="Fetch!" Height="17px" Width="94px" Theme="SoftOrange" CssClass="floatright" EnableTheming="True"></dx:ASPxButton>
                        <asp:ObjectDataSource ID="odsDevices" runat="server" SelectMethod="GetAllDevices" TypeName="FMS.Business.DataObjects.Device"></asp:ObjectDataSource>
                    </td>
                </tr>

            </table>

            <div id="dataDiv">

                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsDeviceEvent">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                    <SettingsSearchPanel Visible="True" />
                    <Columns>
                        <dx:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowClearFilterButton="True" ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="StartTime" VisibleIndex="2" Width="150px">
                            <PropertiesDateEdit DisplayFormatString="G">
                                <TimeSectionProperties>
                                    <TimeEditProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </TimeEditProperties>
                                </TimeSectionProperties>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="Speed" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="Endtime" VisibleIndex="3" Width="150px">
                            <PropertiesDateEdit DisplayFormatString="G">
                                <TimeSectionProperties>
                                    <TimeEditProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </TimeEditProperties>
                                </TimeSectionProperties>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="Distance" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Cumulativedistance" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>                

                <asp:ObjectDataSource ID="odsDeviceEvent" runat="server" SelectMethod="GetDistanceVals" TypeName="FMS.Business.DebugObjects.DeviceEvent"></asp:ObjectDataSource>

            </div>

        </div>
    </form>
   
</body>
</html>
