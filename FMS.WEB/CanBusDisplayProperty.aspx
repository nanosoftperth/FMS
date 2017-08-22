<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusDisplayProperty.aspx.vb" Inherits="FMS.WEB.CanBusDisplayProperty" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>CAN Value Viewer</title>

    <script src="Content/javascript/jquery-3.1.0.min.js"></script>    

    <script type="text/javascript">

        var deviceid = '<%= DeviceID%>';
        var Zagro125Value = '';
        var Zagro500Value = '';

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

        var uri = '/api/vehicle/GetFormattedCanMessageSnapshot?_deviceid={0}&_standard={1}&_spn={2}'


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
                                serverResult(data, dateItm, valItm, isLastItem, id);
                            });

                        }
            )
            
                
        }

        function serverResult(data, dateItm, valItm, isLastItem, id) {
            var date = new Date(data.Time);
            var dataTime = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() +
                ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
            
            dateItm.text(dataTime);
            valItm.text((String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value);
            
            if (id == "#Zagro125|7") {
                Zagro125Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value
            }
            if (id == "#Zagro500|7") {
                Zagro500Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value
            }

            if (Zagro125Value == "") {
                $('#contentTable').find('.Zagro1257').hide();
            } else {
                $('#contentTable').find('.Zagro1257').show();
            }

            if (Zagro500Value == "") {
                $('#contentTable').find('.Zagro5007').hide();
            } else {
                $('#contentTable').find('.Zagro5007').show();
            }

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

                    <tr id="<%# Eval("Standard_SPN")%>" class="<%# Eval("Standard")%><%# Eval("spn")%>">
                        <td class="Description"  > <%# Eval("Description")%> </td>
                        <%--<td class="Description"  > <%# Eval("Standard_SPN")%> </td>--%>
                        <td class="Value"  > Loading... </td>
                        <td class="Date"  > Loading... </td>
                    </tr>

                </ItemTemplate>


                <FooterTemplate></table></FooterTemplate>

            </asp:Repeater>

            <asp:ObjectDataSource runat="server" ID="odsCanMessages" SelectMethod="GetSortedEmaxiCANMessages" TypeName="FMS.WEB.Controllers.VehicleController">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="DeviceID" Name="deviceID" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>
