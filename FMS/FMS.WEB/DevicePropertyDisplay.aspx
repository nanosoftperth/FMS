<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DevicePropertyDisplay.aspx.vb" Inherits="FMS.WEB.DriverPropertyDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Device viewer</title>

    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <%-- <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">


        <script type="text/javascript">

            function btnSendMessageToDriver_Click(s, e) {

                //cbSendText, cbSendEmail, btnSendMessageToDriver,memMessageToSend

                var sendEmail = cbSendEmail.GetChecked();
                var sendText = cbSendText.GetChecked();
                var msgToSend = memMessageToSend.GetText();

                var param = {};

                param.SendEmail = sendEmail;
                param.SendText = sendText;
                param.MsgToSend = msgToSend;

                param.DriverID = driverID;

                if (msgToSend == '') {
                    alert('please enter a message to send');
                    return false;
                }

                if (sendEmail == false && sendText == false) {
                    alert('You must either send a text, an email, or both.')
                    return false;
                }

                btnSendMessageToDriver.SetEnabled(false);

                ajaxMethod("DefaultService.svc/" + 'SendDriverMessage',
                                param, sendMessage_SuccessCallback, sendMessage_ErrorCallback, sendMessage_FinallyCallback);

            }

            //GetLatestMessage

            $(document).ready(function () {
                getLastMessage();
                //logLabel.SetText('sd');

            })

            var deviceID = '<%=DeviceID%>';
            var driverID = '<%=DriverID%>';
            var driverName = '<%=DriverName%>';
            var vehicleName = '<%=VehicleName%>';


            function getLastMessage() {

                var param = {};

                param.DeviceID = deviceID;
                
                ajaxMethod("DefaultService.svc/" + 'GetLatest2MessageWithLatLong',
                                param, getLog_SuccessCallback, sendMessage_ErrorCallback, sendMessage_FinallyCallback);

            }


            function toRads(dec) { return dec * (Math.PI / 180); }
            function toDegs(rad) { return rad / (Math.PI / 180); }

            function getHeading(startLat, startLng, endLat, endLng) {

                var r_startLat = toRads(startLat);
                var r_startLng = toRads(startLng);
                var r_endLat = toRads(endLat);
                var r_endLng = toRads(endLng);

                var deltaL = endLng - startLng;
                var r_deltaL = toRads(deltaL);

                var X = Math.cos(r_endLat) * Math.sin(r_deltaL);

                var Y = Math.cos(r_startLat) * Math.sin(r_endLat)
                        - Math.sin(r_startLat) * Math.cos(r_endLat) * Math.cos(r_deltaL);

                var heading = Math.atan2((X), (Y));

                return toDegs(heading);

            }

            var i = 0;

            prevLat = 0;
            prevLng = 0;

            function getLog_SuccessCallback(result) {

                var s = result.d._ReturnString.replace(',', '\n').replace(',', '\n').replace(',', '\n').replace(',', '\n').replace(',', '\n');

                try {
                    var x = result.d._ReturnString.split(",");
                    var lat = result.d._Lat;
                    var lon = result.d._Lng;

                    var heading = getHeading(prevLat, prevLng, lat, lon);

                    prevLat = lat;
                    prevLng = lon;

                    $('#streetView_image').show();
                    $('#streetView_image').attr("src", "https://maps.googleapis.com/maps/api/streetview?size=150x100&location=" + lat + "," + lon + "&heading=" + heading + "&key=AIzaSyCeMLNUixkGRU-PwkiVbHTrMr5Foz07KWQ");
                }
                catch (err) {
                    //  $('#streetView_image').hide();
                }

                logLabel.SetText(s);
                setTimeout(function () {
                    getLastMessage();
                }, 2500);

            }

            function sendMessage_SuccessCallback(result) {
                alert(result.d._ReturnString);

                btnSendMessageToDriver.SetEnabled(true);
            }

            function sendMessage_ErrorCallback(result) {

            }

            function sendMessage_FinallyCallback() {

            }



        </script>

        <style type="text/css">
            #container {
                float: left;
            }

            .checkboxCSS {
                float: left;
                padding-top: 5px;
            }

            .boldme {
                font-weight: bold;
            }
        </style>

        <div style="width: 243px;">

            <table>
                <tr>
                    <td>


                        <dx:ASPxBinaryImage ID="binaryImageDriver" runat="server" Width="72px">
                        </dx:ASPxBinaryImage>


                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="padding: 3px;margin-left:10px;"><b>
                                    <dx:ASPxLabel ID="ASPxLabel2" CssClass="boldme" runat="server" Text="Vehicle"></dx:ASPxLabel>
                                </b>
                                </td>
                                <td style="padding: 3px;">
                                    <dx:ASPxLabel ID="lblVehicle" runat="server" Text="Vehicle"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                 <td style="padding: 3px;margin-left:10px;"><b>
                                    <dx:ASPxLabel ID="ASPxLabel4" CssClass="boldme" runat="server" Text="Driver"></dx:ASPxLabel>
                                </b>
                                </td>
                                <td style="padding: 3px;">
                                    <dx:ASPxLabel ID="lblDriver" runat="server" Text="Vehicle"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>



            <hr />
            <%--<div id="container" style="width: 200px; height: 200px; margin: 0 auto"></div>--%>
            <dx:ASPxLabel ID="ASPxLabel3" CssClass="boldme" runat="server" Text="Last message from vehicle:"></dx:ASPxLabel>
            <br />
            <dx:ASPxLabel ClientInstanceName="logLabel" ID="logLabel" runat="server" Text="loading..."></dx:ASPxLabel>
            <img src="#" id="streetView_image" style ="width:100%;height:100px"/>
            <hr />
            <table>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Write message to driver"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxMemo ClientInstanceName="memMessageToSend" ID="memMessageToSend" runat="server" Height="71px" Width="234px"></dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <td>
                                <dx:ASPxCheckBox ClientInstanceName="cbSendText" CssClass="checkboxCSS" ID="cbSendText" runat="server" Text="text" Checked="true"></dx:ASPxCheckBox>
                                <dx:ASPxCheckBox ClientInstanceName="cbSendEmail" CssClass="checkboxCSS" ID="cbSendEmail" runat="server" Text="email" Checked="false">
                                </dx:ASPxCheckBox>
                            </td>
                            <td>
                                <div style="float: right; padding-top: 5px;">
                                    <dx:ASPxButton ClientInstanceName="btnSendMessageToDriver"
                                        ID="btnSendMessageToDriver"
                                        AutoPostBack="false"
                                        runat="server"
                                        Text="Send Message">

                                        <ClientSideEvents Click="function(s,e) {btnSendMessageToDriver_Click(s,e);}" />
                                    </dx:ASPxButton>
                                </div>
                            </td>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
