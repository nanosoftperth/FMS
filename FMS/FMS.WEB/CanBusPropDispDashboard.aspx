<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropDispDashboard.aspx.vb" Inherits="FMS.WEB.CanBusPropDispDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <link href="Content/DashBoard.css" rel="stylesheet" />
    <script src="Content/javascript/FleetMap.js"></script>
   <%-- <script src="Content/javascript/page.js"></script>--%>

    <script type="text/javascript">
        var bln_blink = false;

        function blinkbreak() {
            if (bln_blink == false) {
                $('#ibreak').removeClass('imgmax');
                $('#ibreak').removeClass('pnlimgHide')
                $('#ibreak').addClass('imgmax')

                $('#ibreak_blink').removeClass('imgmax');
                $('#ibreak_blink').removeClass('pnlimgHide')
                $('#ibreak_blink').addClass('pnlimgHide')
                bln_blink = true;
            }
            else {
                $('#ibreak').removeClass('imgmax');
                $('#ibreak').removeClass('pnlimgHide')
                $('#ibreak').addClass('pnlimgHide')

                $('#ibreak_blink').removeClass('imgmax');
                $('#ibreak_blink').removeClass('pnlimgHide')
                $('#ibreak_blink').addClass('imgmax')
                bln_blink = false;
            }
        }


        //GetDashboardData();

        //function blinkbreak() {
        //    var imgCls = $('#ibreak');
        //    //var imgCls = document.getElementById('ibreak');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinksteer() {
        //    var imgCls = document.getElementById('isteer');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkdrive() {
        //    var imgCls = document.getElementById('idrive');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkifm() {
        //    var imgCls = document.getElementById('iifm');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkcan() {
        //    var imgCls = document.getElementById('ican');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkalign() {
        //    var imgCls = document.getElementById('ialign');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkwarning() {
        //    var imgCls = document.getElementById('iwarning');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkstop() {
        //    var imgCls = document.getElementById('istop');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkspeed() {
        //    var imgCls = document.getElementById('ispeed');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        //function blinkbattery() {
        //    var imgCls = document.getElementById('ibattery_100');

        //    if (bln_blink == false) {
        //        imgCls.className = "imgmax";
        //        bln_blink = true;
        //    }
        //    else {
        //        imgCls.className = "pnlimgHide";
        //        bln_blink = false;
        //    }
        //}

        setTimeout('blinkbreak()', 200);
        setTimeout('blinkbreak()', 400);
        setTimeout('blinkbreak()', 600);
        //setTimeout('blinksteer()', 1500);
        //setTimeout('blinksteer()', 2000);
        //setTimeout('blinkdrive()', 2500);
        //setTimeout('blinkdrive()', 3000);
        //setTimeout('blinkifm()', 3500);
        //setTimeout('blinkifm()', 4000);
        //setTimeout('blinkcan()', 4500);
        //setTimeout('blinkcan()', 5000);
        //setTimeout('blinkalign()', 5500);
        //setTimeout('blinkalign()', 6000);
        //setTimeout('blinkwarning()', 6500);
        //setTimeout('blinkwarning()', 7000);
        //setTimeout('blinkstop()', 7500);
        //setTimeout('blinkstop()', 8000);
        //setTimeout('blinkspeed()', 8500);
        //setTimeout('blinkspeed()', 9000);
        //setTimeout('blinkbattery()', 9500);
        //setTimeout('blinkbattery()', 10000);

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="div_dashboard">    

    </div>
    <div class="div_break">
        <img src="Content/Images/Dashboard/break_gray.png" id="ibreak" class="imgmax"/>
    </div>
    <div class="div_break">
        <img src="Content/Images/Dashboard/Break_color.png" id="ibreak_blink" class="pnlimgHide"/>
    </div>
    <div class="div_steer">
        <img src="Content/Images/Dashboard/steering_gray.png" id="isteer" class="imgmax"/>
    </div>
    <div class="div_drive">
        <img src="Content/Images/Dashboard/drive_gray.png" id="idrive" class="imgmax"/>
    </div>
    <div class="div_ifm">
        <img src="Content/Images/Dashboard/ifm_gray.png" id="iifm" class="imgmax"/>
    </div>
    <div class="div_can">
        <img src="Content/Images/Dashboard/can_gray.png" id="ican" class="imgmax"/>
    </div>
    <div class="div_align">
        <img src="Content/Images/Dashboard/alignment_gray.png" id="ialign" class="imgmax"/>
    </div>
    <div class="div_warning">
        <img src="Content/Images/Dashboard/warning_gray.png" id="iwarning" class="imgmax"/>
    </div>
    <div class="div_stop">
        <img src="Content/Images/Dashboard/error_gray.png" id="istop" class="imgmax"/>
    </div>
    <div class="div_speed">
        <img src="Content/Images/Dashboard/speed_gray.png" id="ispeed" class="imgmax"/>
    </div>
    <div class="div_battery">
        <img src="Content/Images/Dashboard/battery_gray.png" id="ibattery_100" class="imgmax"/>
    </div>
    <div class="div_plus">
        <img src="Content/Images/Dashboard/Plus_Button_Normal.png" id="iPlus" class="imgmax"/>
    </div>
    <div class="div_minus">
        <img src="Content/Images/Dashboard/Minus_Button_Normal.png" id="iMinus" class="imgmax"/>
    </div>
    <div class="div_ex">
        <img src="Content/Images/Dashboard/Ex_Button_Normal.png" id="iEx" class="imgmax"/>
    </div>
    <div class="div_tick">
        <img src="Content/Images/Dashboard/tick_Normal.png" id="iTick" class="imgmax"/>
    </div>
    <div class="div_bottomlogo">
        <img src="Content/Images/Dashboard/NanoSoft_BW.png" id="iBottomNanosoft" class="imgmax"/>
    </div>


    <%--<div class="div_break_blink">
        <img src="Images/FixingBreak_.png" id="ibreak_blink" class="pnlimgHide"/>
    </div>--%>
    <%--
    <div class="div_LCD">
        <img src="Content/Images/Nanosoft_scroll.png" id="iLCD" class="imgmax"/>
    </div>
    <div id="idLiveData" class="div_vwlivedta" onclick="Run_infoWindowCSSForCanBus()">
        <asp:Label ID="Label1" runat="server" Text="view live data"></asp:Label>
        <asp:HyperLink ID="lnkLiveData" runat="server">view live data</asp:HyperLink>
        <asp:Label ID="hlLiveDt" runat="server" Text="view live data" ></asp:Label>
    </div>
    <div style="position:absolute; top: 150px; left: 300px">
        <asp:Label ID="lblVehicleID" runat="server" Text="Vehicle ID"></asp:Label>
    </div>--%>

    </form>


</body>
</html>
