<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CANBusPropertyDisplay2.aspx.vb" Inherits="FMS.WEB.CANBusPropertyDisplay2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>CAN Value Viewer</title>

    <script src="Content/javascript/jquery-3.1.0.min.js"></script>    

    <script type="text/javascript">

        var deviceid = '<%= DeviceID%>';


        // First, checks if it isn't implemented yet.
        if (!String.prototype.format) {
            String.prototype.format = function () {
                var args = arguments;
                return this.replace(/{(\d+)}/g, function (match, number) {
                    return typeof args[number] != 'undefined'
                      ? args[number]
                      : match
                    ;
                });
            };
        }

        var uri = '/api/vehicle/?deviceid={0}&standard={1}&spn={2}'


        function loopGetVals() {

            var countOfCANVals = $('#contentTable tr').length;

            $('#contentTable tr').each(

                        function (index, item) {

                            var id = '#' + item.id;

                            var dateItm = $(item).find('.Date');
                            var valItm = $(item).find('.Value');

                            var standard = item.id.split('|')[0];
                            var spn = item.id.split('|')[1];

                            var this_url = uri.format(deviceid, standard, spn);

                            var isLastItem = ((countOfCANVals - 1) == index);

                            $.getJSON(this_url, function (data) {
                                serverResult(data, dateItm, valItm, isLastItem);
                            });

                        }
            )

        }

        function serverResult(data, dateItm, valItm, isLastItem) {

            dateItm.text(data.Time);
            valItm.text(data.Value);

            if (isLastItem) setTimeout(loopGetVals, 1000);
        }


        $(document).ready(function () { loopGetVals(); });
        

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Repeater ID="repeater_main" runat="server" DataSourceID="odsCanMessages">

                <HeaderTemplate>
                    <table id="contentTable">
                </HeaderTemplate>


                <ItemTemplate>

                    <tr id="<%# Eval("Standard_SPN")%>">
                        <td class="Description"  > <%# Eval("Description")%> </td>
                        <%--<td class="Description"  > <%# Eval("Standard_SPN")%> </td>--%>
                        <td class="Value"  > Loading... </td>
                        <td class="Date"  > Loading... </td>
                    </tr>

                </ItemTemplate>


                <FooterTemplate></table></FooterTemplate>

            </asp:Repeater>

            <asp:ObjectDataSource runat="server" ID="odsCanMessages" SelectMethod="GetEmaxiCANMessages" TypeName="FMS.WEB.Controllers.VehicleController">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="DeviceID" Name="deviceID" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>

