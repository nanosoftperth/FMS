<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropDispDashboard.aspx.vb" Inherits="FMS.WEB.CanBusPropDispDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <link href="Content/DashBoard.css" rel="stylesheet" />

    <script type="text/javascript">
        var bln_blink = false;

        function blinkbreak() {
            var imgCls = document.getElementById('ibreak');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinksteer() {
            var imgCls = document.getElementById('isteer');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkdrive() {
            var imgCls = document.getElementById('idrive');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkifm() {
            var imgCls = document.getElementById('iifm');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkcan() {
            var imgCls = document.getElementById('ican');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkalign() {
            var imgCls = document.getElementById('ialign');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkwarning() {
            var imgCls = document.getElementById('iwarning');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkstop() {
            var imgCls = document.getElementById('istop');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkspeed() {
            var imgCls = document.getElementById('ispeed');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        function blinkbattery() {
            var imgCls = document.getElementById('ibattery_100');

            if (bln_blink == false) {
                imgCls.className = "imgmax";
                bln_blink = true;
            }
            else {
                imgCls.className = "pnlimgHide";
                bln_blink = false;
            }
        }

        setTimeout('blinkbreak()', 500);
        setTimeout('blinkbreak()', 1000);
        setTimeout('blinksteer()', 1500);
        setTimeout('blinksteer()', 2000);
        setTimeout('blinkdrive()', 2500);
        setTimeout('blinkdrive()', 3000);
        setTimeout('blinkifm()', 3500);
        setTimeout('blinkifm()', 4000);
        setTimeout('blinkcan()', 4500);
        setTimeout('blinkcan()', 5000);
        setTimeout('blinkalign()', 5500);
        setTimeout('blinkalign()', 6000);
        setTimeout('blinkwarning()', 6500);
        setTimeout('blinkwarning()', 7000);
        setTimeout('blinkstop()', 7500);
        setTimeout('blinkstop()', 8000);
        setTimeout('blinkspeed()', 8500);
        setTimeout('blinkspeed()', 9000);
        setTimeout('blinkbattery()', 9500);
        setTimeout('blinkbattery()', 10000);

        //setTimeout('blinkbreak()', 1000);
        //setTimeout('blinkbreak()', 2000);
        //setTimeout('blinksteer()', 3000);
        //setTimeout('blinksteer()', 4000);
        //setTimeout('blinkdrive()', 5000);
        //setTimeout('blinkdrive()', 6000);
        //setTimeout('blinkifm()', 7000);
        //setTimeout('blinkifm()', 8000);
        //setTimeout('blinkcan()', 9000);
        //setTimeout('blinkcan()', 10000);
        //setTimeout('blinkalign()', 11000);
        //setTimeout('blinkalign()', 12000);
        //setTimeout('blinkwarning()', 13000);
        //setTimeout('blinkwarning()', 14000);
        //setTimeout('blinkstop()', 15000);
        //setTimeout('blinkstop()', 16000);
        //setTimeout('blinkspeed()', 17000);
        //setTimeout('blinkspeed()', 18000);
        //setTimeout('blinkbattery()', 19000);
        //setTimeout('blinkbattery()', 20000);

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="div_dashboard">    
        <img id="dashboard" src="Content/Images/Dashboard1.png" class="imgmax"/>
    </div>
    <div class="div_break">
        <img src="Content/Images/break.png" id="ibreak" class="pnlimgHide"/>
    </div>
    <div class="div_steer">
        <img src="Content/Images/steer.png" id="isteer" class="pnlimgHide"/>
    </div>
    <div class="div_drive">
        <img src="Content/Images/drive.png" id="idrive" class="pnlimgHide"/>
    </div>
    <div class="div_ifm">
        <img src="Content/Images/ifm.png" id="iifm" class="pnlimgHide"/>
    </div>
    <div class="div_can">
        <img src="Content/Images/can.png" id="ican" class="pnlimgHide"/>
    </div>
    <div class="div_align">
        <img src="Content/Images/align.png" id="ialign" class="pnlimgHide"/>
    </div>
    <div class="div_warning">
        <img src="Content/Images/warning.png" id="iwarning" class="pnlimgHide"/>
    </div>
    <div class="div_stop">
        <img src="Content/Images/stop.png" id="istop" class="pnlimgHide"/>
    </div>
    <div class="div_speed">
        <img src="Content/Images/speed.png" id="ispeed" class="pnlimgHide"/>
    </div>
    <div class="div_battery">
        <img src="Content/Images/battery_100.png" id="ibattery_100" class="pnlimgHide"/>
    </div>
    </form>
</body>
</html>
