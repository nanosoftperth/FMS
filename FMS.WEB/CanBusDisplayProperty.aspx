<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusDisplayProperty.aspx.vb" Inherits="FMS.WEB.CanBusDisplayProperty" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>CAN Value Viewer</title>
    <style>
        table {
            border-collapse: collapse;
        }

        table, td, th {
            border: 1px solid #C1C5C8;
        }
    </style>
    <script src="Content/javascript/jquery-3.1.0.min.js"></script>    

    <script type="text/javascript">

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
                            var this_url = uri.format($("#deviceId").val(), standard, spn);

                            var isLastItem = ((countOfCANVals - 1) == index);

                            $.getJSON(this_url, function (data) {
                                serverResult(data, dateItm, valItm, isLastItem, id);
                            });

                        }
            )
            
                
        }

        function serverResult(data, dateItm, valItm, isLastItem, id) {
            var dataVal = "";
            var date = new Date(data.Time);
            var dataTime = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() +
                ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
            
            switch (id) {
                case "#Zagro500|8":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro500|9":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro500|10":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro500|11":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro125|1":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro125|8":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro125|9":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    valItm.text(dataVal);
                    break;
                case "#Zagro125|10":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    if (data.DeviceName.indexOf("XL")==-1) {
                        valItm.text("").append("<a href='javascript:void(0)' onclick='OnPressureValueClick()' id='faultCodes'>Not implemented</a>");
                    } else {
                        valItm.text(dataVal);
                    }
                    break;
                case "#Zagro125|11":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value.toFixed(1);
                    if (data.DeviceName.indexOf("XL") == -1) {
                        valItm.text("").append("<a href='javascript:void(0)' onclick='OnPressureValueClick()' id='faultCodes'>Not implemented</a>");
                    } else {
                        valItm.text(dataVal);
                    }
                    break;
                case "#Zagro500|12":
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value;
                    $("#faultDesc").val(data.ValueString);
                    valItm.text("").append("<a href='javascript:void(0)' onclick='OnFaultCodesClick()' id='faultCodes'>" + dataVal + "</a>");
                    break;
                default:
                    dataVal = (String(data.Value).indexOf("ERROR:") == 0) ? "" : data.Value;
                    valItm.text(dataVal);
            }
            dateItm.text(dataTime);
            
            if (id == "#Zagro125|7") {
                Zagro125Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : dataVal;
            }
            if (id == "#Zagro500|7") {
                Zagro500Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : dataVal;
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

        function OnPressureValueClick() {
            var contentUrl = $("#faultDesc").val();

            textVal = "Not implemented in the E-Maxis S, M & L series";
            document.getElementById("pMessage").innerHTML = textVal;
            myPopup.SetHeaderText('Pressure Values Information');
            ShowMyPopupWindow();

        }

        function OnFaultCodesClick() {
            var contentUrl = $("#faultDesc").val();
            var textVal = "";
            var content = contentUrl.split(',');
            for (i = 0; i < content.length; i++) {
                textVal += content[i] + "<br>";
            }
            
            document.getElementById("pMessage").innerHTML = textVal;
            myPopup.SetHeaderText('Fault Code Information');
            ShowMyPopupWindow();

        }
        function ShowMyPopupWindow() {
            myPopup.ShowAtPos(10, 10);
            
            myPopup.Show();
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
        <div>
            <dx:ASPxPopupControl ID="myPopup" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="myPopup"
                HeaderText="Fault Code Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="370px" >        
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">             
                                    <p id="pMessage"></p>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>                
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>
        </div>
        <input type="hidden" id="deviceId"  value="<%= DeviceID%>"/>
        <input type="hidden" id="faultDesc"  value=""/>
    </form>
</body>
</html>
