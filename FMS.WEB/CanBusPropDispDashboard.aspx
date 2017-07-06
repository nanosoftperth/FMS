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
        var bln_blink = true;
        var bln_blinkBottom = false;

        function blinkTop()
        {
            if (bln_blink == false) {

                //--- break on gray
                $('#ibreak').removeClass('imgmax');
                $('#ibreak').removeClass('pnlimgHide')
                $('#ibreak').addClass('imgmax')

                $('#ibreak_blink').removeClass('imgmax');
                $('#ibreak_blink').removeClass('pnlimgHide')
                $('#ibreak_blink').addClass('pnlimgHide')

                //--- steer on gray
                $('#isteer').removeClass('imgmax');
                $('#isteer').removeClass('pnlimgHide')
                $('#isteer').addClass('imgmax')

                $('#isteer_blink').removeClass('imgmax');
                $('#isteer_blink').removeClass('pnlimgHide')
                $('#isteer_blink').addClass('pnlimgHide')

                //--- drive on gray
                $('#idrive').removeClass('imgmax');
                $('#idrive').removeClass('pnlimgHide')
                $('#idrive').addClass('imgmax')

                $('#idrive_blink').removeClass('imgmax');
                $('#idrive_blink').removeClass('pnlimgHide')
                $('#idrive_blink').addClass('pnlimgHide')

                //--- ifm on gray
                $('#iifm').removeClass('imgmax');
                $('#iifm').removeClass('pnlimgHide')
                $('#iifm').addClass('imgmax')

                $('#iifm_blink').removeClass('imgmax');
                $('#iifm_blink').removeClass('pnlimgHide')
                $('#iifm_blink').addClass('pnlimgHide')

                //--- can on gray
                $('#ican').removeClass('imgmax');
                $('#ican').removeClass('pnlimgHide')
                $('#ican').addClass('imgmax')

                $('#ican_blink').removeClass('imgmax');
                $('#ican_blink').removeClass('pnlimgHide')
                $('#ican_blink').addClass('pnlimgHide')

                //--- alignment on gray
                $('#ialign').removeClass('imgmax');
                $('#ialign').removeClass('pnlimgHide')
                $('#ialign').addClass('imgmax')

                $('#ialign_blink').removeClass('imgmax');
                $('#ialign_blink').removeClass('pnlimgHide')
                $('#ialign_blink').addClass('pnlimgHide')

                //--- warning on gray
                $('#iwarning').removeClass('imgmax');
                $('#iwarning').removeClass('pnlimgHide')
                $('#iwarning').addClass('imgmax')

                $('#iwarning_blink').removeClass('imgmax');
                $('#iwarning_blink').removeClass('pnlimgHide')
                $('#iwarning_blink').addClass('pnlimgHide')

                //--- error on gray
                $('#istop').removeClass('imgmax');
                $('#istop').removeClass('pnlimgHide')
                $('#istop').addClass('imgmax')

                $('#istop_blink').removeClass('imgmax');
                $('#istop_blink').removeClass('pnlimgHide')
                $('#istop_blink').addClass('pnlimgHide')

                //--- speed on gray
                $('#ispeed').removeClass('imgmax');
                $('#ispeed').removeClass('pnlimgHide')
                $('#ispeed').addClass('imgmax')

                $('#ispeed_blink').removeClass('imgmax');
                $('#ispeed_blink').removeClass('pnlimgHide')
                $('#ispeed_blink').addClass('pnlimgHide')

                //--- battery 100% on gray
                $('#ibattery_100').removeClass('imgmax');
                $('#ibattery_100').removeClass('pnlimgHide')
                $('#ibattery_100').addClass('imgmax')

                $('#ibattery_100_blink').removeClass('imgmax');
                $('#ibattery_100_blink').removeClass('pnlimgHide')
                $('#ibattery_100_blink').addClass('pnlimgHide')

                bln_blink = true;

            }
            else
            {
                //--- break on color
                $('#ibreak').removeClass('imgmax');
                $('#ibreak').removeClass('pnlimgHide')
                $('#ibreak').addClass('pnlimgHide')

                $('#ibreak_blink').removeClass('imgmax');
                $('#ibreak_blink').removeClass('pnlimgHide')
                $('#ibreak_blink').addClass('imgmax')

                //--- steer on color
                $('#isteer').removeClass('imgmax');
                $('#isteer').removeClass('pnlimgHide')
                $('#isteer').addClass('pnlimgHide')

                $('#isteer_blink').removeClass('imgmax');
                $('#isteer_blink').removeClass('pnlimgHide')
                $('#isteer_blink').addClass('imgmax')

                //--- drive on color
                $('#idrive').removeClass('imgmax');
                $('#idrive').removeClass('pnlimgHide')
                $('#idrive').addClass('pnlimgHide')

                $('#idrive_blink').removeClass('imgmax');
                $('#idrive_blink').removeClass('pnlimgHide')
                $('#idrive_blink').addClass('imgmax')

                //--- ifm on color
                $('#iifm').removeClass('imgmax');
                $('#iifm').removeClass('pnlimgHide')
                $('#iifm').addClass('pnlimgHide')

                $('#iifm_blink').removeClass('imgmax');
                $('#iifm_blink').removeClass('pnlimgHide')
                $('#iifm_blink').addClass('imgmax')

                //--- can on color
                $('#ican').removeClass('imgmax');
                $('#ican').removeClass('pnlimgHide')
                $('#ican').addClass('pnlimgHide')

                $('#ican_blink').removeClass('imgmax');
                $('#ican_blink').removeClass('pnlimgHide')
                $('#ican_blink').addClass('imgmax')

                //--- alignment on color
                $('#ialign').removeClass('imgmax');
                $('#ialign').removeClass('pnlimgHide')
                $('#ialign').addClass('pnlimgHide')

                $('#ialign_blink').removeClass('imgmax');
                $('#ialign_blink').removeClass('pnlimgHide')
                $('#ialign_blink').addClass('imgmax')

                //--- warining on color
                $('#iwarning').removeClass('imgmax');
                $('#iwarning').removeClass('pnlimgHide')
                $('#iwarning').addClass('pnlimgHide')

                $('#iwarning_blink').removeClass('imgmax');
                $('#iwarning_blink').removeClass('pnlimgHide')
                $('#iwarning_blink').addClass('imgmax')

                //--- error on color
                $('#istop').removeClass('imgmax');
                $('#istop').removeClass('pnlimgHide')
                $('#istop').addClass('pnlimgHide')

                $('#istop_blink').removeClass('imgmax');
                $('#istop_blink').removeClass('pnlimgHide')
                $('#istop_blink').addClass('imgmax')

                //--- speed on color
                $('#ispeed').removeClass('imgmax');
                $('#ispeed').removeClass('pnlimgHide')
                $('#ispeed').addClass('pnlimgHide')

                $('#ispeed_blink').removeClass('imgmax');
                $('#ispeed_blink').removeClass('pnlimgHide')
                $('#ispeed_blink').addClass('imgmax')

                //--- battery on color
                $('#ibattery_100').removeClass('imgmax');
                $('#ibattery_100').removeClass('pnlimgHide')
                $('#ibattery_100').addClass('pnlimgHide')

                $('#ibattery_100_blink').removeClass('imgmax');
                $('#ibattery_100_blink').removeClass('pnlimgHide')
                $('#ibattery_100_blink').addClass('imgmax')

                bln_blink = false;

            }
        }

        function blinkBottom()
        {
            if (bln_blinkBottom == false)
            {
                //--- plus sign on gray
                $('#iPlus').removeClass('div_plus_blink');
                $('#iPlus').removeClass('div_plus');
                $('#iPlus').addClass('div_plus');
                
                //--- minus sign on gray
                $('#iMinus').removeClass('div_minus_blink');
                $('#iMinus').removeClass('div_minus')
                $('#iMinus').addClass('div_minus')

                //--- Ex sign on gray
                $('#iEx').removeClass('div_ex_blink');
                $('#iEx').removeClass('div_ex')
                $('#iEx').addClass('div_ex')

                //--- tick sign on gray
                $('#iTick').removeClass('div_tick_blink');
                $('#iTick').removeClass('div_tick')
                $('#iTick').addClass('div_tick')

                bln_blinkBottom = true;
            }
            else
            {
                //--- battery on hover
                $('#iPlus').removeClass('div_plus');
                $('#iPlus').removeClass('div_plus_blink');
                $('#iPlus').addClass('div_plus_blink');

                //--- minus on hover
                $('#iMinus').removeClass('div_minus');
                $('#iMinus').removeClass('div_minus_blink')
                $('#iMinus').addClass('div_minus_blink')

                //--- Ex on hover
                $('#iEx').removeClass('div_ex');
                $('#iEx').removeClass('div_ex_blink')
                $('#iEx').addClass('div_ex_blink')

                //--- tick on hover
                $('#iTick').removeClass('div_tick');
                $('#iTick').removeClass('div_tick_blink')
                $('#iTick').addClass('div_tick_blink')

                bln_blinkBottom = false;
            }
        }
        //setTimeout('blinkBottom()', 0);
        //setTimeout('blinkTop()', 0);
        //setTimeout('blinkTop()', 500);
        //setTimeout('blinkBottom()', 500);
        //setTimeout('blinkTop()', 1000);
        //setTimeout('blinkBottom()', 1000);
        //setTimeout('blinkTop()', 1500);
        //setTimeout('blinkBottom()', 1500);
        //setTimeout('blinkBottom()', 2000);

        setTimeout('blinkBottom()', 0);
        setTimeout('blinkTop()', 0);
        setTimeout('blinkTop()', 1000);
        setTimeout('blinkBottom()', 1000);
        setTimeout('blinkTop()', 2000);
        setTimeout('blinkBottom()', 2000);
        setTimeout('blinkTop()', 3000);
        setTimeout('blinkBottom()', 3000);
        setTimeout('blinkBottom()', 4000);

        function iPlusMouseOver()
        {
            $("#iPlus").attr("src", "Content/Images/Dashboard/Plus_Button_hover.png");
        }

        function iPlusMouseOut() {
            $("#iPlus").attr("src", "Content/Images/Dashboard/Plus_Button_normal.png");
        }
        
        function iPlusMouseDown() {
            $("#iPlus").attr("src", "Content/Images/Dashboard/Plus_Button_press.png");
        }

        function iMinusMouseOver() {
            $("#iMinus").attr("src", "Content/Images/Dashboard/Minus_Button_Hover.png");
        }

        function iMinusMouseOut() {
            $("#iMinus").attr("src", "Content/Images/Dashboard/Minus_Button_Normal.png");
        }

        function iMinusMouseDown() {
            $("#iMinus").attr("src", "Content/Images/Dashboard/Minus_Button_Press.png");
        }

        function iExMouseOver() {
            $("#iEx").attr("src", "Content/Images/Dashboard/Ex_Button_Hover.png");
        }

        function iMinusMouseOut() {
            $("#iEx").attr("src", "Content/Images/Dashboard/Ex_Button_normal.png");
        }

        function iExMouseDown() {
            $("#iEx").attr("src", "Content/Images/Dashboard/Ex_Button_press.png");
        }

        //$(function () {
        //    $("#iPlus")
        //        .mouseover(function () {
        //            //var src = $(this).attr("src").match(/[^\.]+/) + "over.gif";
        //            $(this).attr("src", "Content/Images/Dashboard/Plus_Button_Hover.png");
        //        })
        //        .mouseout(function () {
        //            //var src = $(this).attr("src").replace("over.gif", ".gif");
        //            $(this).attr("src", "Content/Images/Dashboard/Plus_Button_Normal.png");
        //        });
        //});



        
        

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
    <div class="div_steer">
        <img src="Content/Images/Dashboard/steering_color.png" id="isteer_blink" class="pnlimgHide"/>
    </div>
    <div class="div_drive">
        <img src="Content/Images/Dashboard/drive_gray.png" id="idrive" class="imgmax"/>
    </div>
    <div class="div_drive">
        <img src="Content/Images/Dashboard/drive_color.png" id="idrive_blink" class="pnlimgHide"/>
    </div>
    <div class="div_ifm">
        <img src="Content/Images/Dashboard/ifm_gray.png" id="iifm" class="imgmax"/>
    </div>
    <div class="div_ifm">
        <img src="Content/Images/Dashboard/ifm_color.png" id="iifm_blink" class="pnlimgHide"/>
    </div>
    <div class="div_can">
        <img src="Content/Images/Dashboard/can_gray.png" id="ican" class="imgmax"/>
    </div>
    <div class="div_can">
        <img src="Content/Images/Dashboard/can_color.png" id="ican_blink" class="pnlimgHide"/>
    </div>
    <div class="div_align">
        <img src="Content/Images/Dashboard/alignment_gray.png" id="ialign" class="imgmax"/>
    </div>
    <div class="div_align">
        <img src="Content/Images/Dashboard/alignment_color.png" id="ialign_blink" class="pnlimgHide"/>
    </div>
    <div class="div_warning">
        <img src="Content/Images/Dashboard/warning_gray.png" id="iwarning" class="imgmax"/>
    </div>
    <div class="div_warning">
        <img src="Content/Images/Dashboard/warning_color.png" id="iwarning_blink" class="pnlimgHide"/>
    </div>
    <div class="div_stop">
        <img src="Content/Images/Dashboard/error_gray.png" id="istop" class="imgmax"/>
    </div>
    <div class="div_stop">
        <img src="Content/Images/Dashboard/error_color.png" id="istop_blink" class="pnlimgHide"/>
    </div>
    <div class="div_speed">
        <img src="Content/Images/Dashboard/speed_gray.png" id="ispeed" class="imgmax"/>
    </div>
    <div class="div_speed">
        <img src="Content/Images/Dashboard/speed_color.png" id="ispeed_blink" class="pnlimgHide"/>
    </div>
    <div class="div_battery">
        <img src="Content/Images/Dashboard/battery_gray.png" id="ibattery_100" class="imgmax"/>
    </div>
    <div class="div_battery">
        <img src="Content/Images/Dashboard/battery_100.png" id="ibattery_100_blink" class="pnlimgHide"/>
    </div>
   <%-- <div class="div_plus">
        <img src="Content/Images/Dashboard/Plus_Button_Normal.png" id="iPlus" class="imgmax"/>
    </div>
    <div class="div_plus">
        <img src="Content/Images/Dashboard/Plus_Button_hover.png" id="iPlus_hover" class="pnlimgHide"/>
    </div>--%>

    <div id="iPlus">
    </div>
    <div id="iMinus">
    </div>
    <div id="iEx">
    </div>
    <div id="iTick">
    </div>
    <%--<div class="div_tick">
        <img src="Content/Images/Dashboard/tick_hover.png" id="iTick_hover" class="pnlimgHide"/>
    </div>--%>
    <div class="div_bottomlogo">
        <img src="Content/Images/Dashboard/NanoSoft_Colour.png" id="iBottomNanosoft" class="imgmax"/>
    </div>
    <div class="div_LCD">
        <img src="Content/Images/Dashboard/NanoSoft_Colour.png" id="iLCD" class="imgmax"/>
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
