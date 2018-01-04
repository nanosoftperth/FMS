<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="debugger.aspx.vb" Inherits="FMS.WEB.debugger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <script type="text/javascript">

                var i = 0;

                function stateChange() {
                    setTimeout(function () {
                        dgvData.PerformCallback('');
                        stateChange()
                        i++;
                        lblI.SetText('iteration ' + i);
                    }, 2500);
                }

                window.onload = function () { stateChange(); };

            </script>

            <style type="text/css">

                .lblI {
                    float:left;
                    padding-left:740px;
                }

            </style>

            <table>
                <tr>
                    <td colspan="3">
                        <h2>Debugger Page</h2>

                    </td>

                </tr>
                <tr>
                    <td>
                        <dx:ASPxComboBox ValueField="ApplicationID"
                            TextField="ApplicationName"
                            ID="comboApplications"
                            runat="server"
                            DataSourceID="odsApplications"
                            ValueType="System.Guid">
                        </dx:ASPxComboBox>
                    </td>
                    <td style="padding-left: 20px;">
                        <dx:ASPxButton ClientInstanceName="thisButton" ID="ASPxButton1" runat="server" Text="Refresh"></dx:ASPxButton>
                    </td>
                    <td></td>
                </tr>


            </table>


            <dx:ASPxLabel CssClass="lblI" ID="ASPxLabel1" ClientInstanceName="lblI" runat="server" Text=""></dx:ASPxLabel>

            <br />

            <asp:ObjectDataSource runat="server" ID="odsApplications" SelectMethod="GetAllApplications" TypeName="FMS.Business.DataObjects.Application"></asp:ObjectDataSource>

            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsLogs" ClientInstanceName="dgvData">
                <SettingsPager PageSize="100"></SettingsPager>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="0"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TruckName" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Message" VisibleIndex="3"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="Time" VisibleIndex="4">
                        <PropertiesDateEdit>
                            <TimeSectionProperties>
                                <TimeEditProperties>
                                    <ClearButton Visibility="Auto"></ClearButton>
                                </TimeEditProperties>
                            </TimeSectionProperties>

                            <ClearButton Visibility="Auto"></ClearButton>
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource runat="server" ID="odsLogs" SelectMethod="GetVals" TypeName="FMS.WEB.debugger"></asp:ObjectDataSource>
        </div>


                      

    </form>




</body>
</html>
