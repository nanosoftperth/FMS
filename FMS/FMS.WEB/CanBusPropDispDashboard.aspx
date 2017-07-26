<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropDispDashboard.aspx.vb" Inherits="FMS.WEB.CanBusPropDispDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <link href="Content/DashBoard.css" rel="stylesheet" />

    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>--%>
    <%--<script src="Content/javascript/FleetMap.js"></script>--%>
    <script src="Content/javascript/page.js"></script>
    <script src="Content/javascript/jquery-3.1.0.min.js"></script>

    <script type="text/javascript">
        var run_blink = false;
        var bln_blink = true;
        var bln_blinkBottom = false;
        var blink_ctr = 0;
        var priDeviceID = '';
        var priVehicleName = '';
        var priClickEvent = '';

        $(document).ready(function () {
            priDeviceID = GetParameterValues('DeviceID');
            priVehicleName = GetParameterValues('VehicleName');
            priClickEvent = GetParameterValues('ClickEvent');

            initPlaceToolTips();

            if (priClickEvent == 'rightclick' || priClickEvent == 'mousedown')
            {
                goBlink();
            }

            //alert('priDeviceID: ' + priDeviceID + '; priVehicleName: ' + priVehicleName + '; priClickEvent: ' + priClickEvent);

            ///alert(document.cookie.indexOf('session_deviceid'));
            
            //if (document.cookie.indexOf('session_deviceid') == -1) {
            //    //alert('cookie session_deviceid does not exist');
            //    setCookie('session_deviceid', priDeviceID, 1);
            //    run_blink = true;
            //    goBlink();
            //}
            //else {
            //    if (priDeviceID == getCookie('session_deviceid')) {
            //        //alert('new deviceID = stored session_deviceid');
            //        var strCook = getCookie('session_deviceid');
            //        run_blink = false;
            //    }
            //    else {
            //        //alert('new deviceID not = stored session_deviceid');
            //        setCookie('session_deviceid', priDeviceID, 1);
            //        run_blink = true;
            //        goBlink();
            //    }
            //}

            //getDataFromServer();
            //timeout();

            function GetParameterValues(param) {
                var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                //alert(url);
                for (var i = 0; i < url.length; i++) {
                    var urlparam = url[i].split('=');
                    if (urlparam[0] == param) {
                        return urlparam[1];
                    }
                }
            }


        });

        function timeout() {
            setTimeout(function () {
                // Do Something Here
                // Then recall the parent function to
                // create a recursive loop.
                getDataFromServer();
                timeout();
            }, 5000);
        }

        function getDataFromServer() {
            //var uri = '../api/Vehicle/GetCanMessageValue?deviceID=' + priDeviceID;
            var uri = '../api/Vehicle/GetDashboardData?DashVehicleID=' + priDeviceID;

            $.getJSON(uri).done(function (data) { receivedData(data); });

        }

        function receivedData(dataCache) {
            var obj = dataCache;

            //alert('run blink : ' + obj);

            if (obj.length > 0) {
                var oCtr = obj.length;

                for (v = 0; v < oCtr; v++) {

                    if (obj[v].Parking_Break == 'Parking Break ON') {
                        SetToolTipPerStatus('.div_break', 'Parking Break : ' + obj[v].Parking_Break)
                        SetStatus('Parking Break', obj[v].Parking_Break);
                    }

                    if (obj[v].Steering.length > 0) {
                        SetToolTipPerStatus('.div_steer', 'Steering : ' + obj[v].Steering)
                        SetStatus('Steer', 'Err');
                    }

                    if (obj[v].Driving.length > 0) {
                        SetToolTipPerStatus('.div_drive', 'Drive : ' + obj[v].Driving)
                        SetStatus('Drive', 'Err');
                    }

                    if (obj[v].IFMControl.length > 0) {
                        SetToolTipPerStatus('.div_ifm', 'IFM : ' + obj[v].IFMControl)
                        SetStatus('IFM', 'Err');
                    }

                    if (obj[v].CANControl.length > 0) {
                        SetToolTipPerStatus('.div_can', 'CAN/CANOPEN : ' + obj[v].CANControl)
                        SetStatus('Can', 'Err');
                    }

                    if (obj[v].AlignmentControl.length > 0) {
                        SetToolTipPerStatus('.div_align', 'Alignment : ' + obj[v].AlignmentControl)
                        SetStatus('Align', 'Err');
                    }

                    if (obj[v].WarningControl.length > 0) {
                        SetToolTipPerStatus('.div_warning', 'Warning : ' + obj[v].WarningControl)
                        SetStatus('Warning', 'Err');
                    }

                    if (obj[v].SpeedControl.length > 0) {
                        SetToolTipPerStatus('.div_speed', 'Speed : ' + obj[v].SpeedControl)
                        SetStatus('Speed', 'Err');
                    }

                    if (obj[v].Battery_Voltage.length > 0) {
                        SetToolTipPerStatus('.div_battery', 'Battery Voltage : ' + obj[v].Battery_Voltage)
                        SetStatus('Battery Voltage', obj[v].Battery_Voltage);
                    }

                    if (obj[v].LCD_DataLogger.length > 0) {
                        setLCDTitle();
                    }

                    if (obj[v].LCD_Speed.length > 0) {
                        setLCDline1(obj[v].LCD_Speed);
                        setLCDline2('5:49');                        
                    }

                    if (obj[v].LCD_Driving_Mode.length > 0) {
                        setLCDline3(obj[v].LCD_Driving_Mode);                        
                    }
                    
                }

            }

        }

        function SetStatus(indicatorDescription, indicatorStatus) {
            var elementID_Blink = '';
            var elementID_NoBlink = '';
            var blnIndicatorOn = false;

            //alert('Inside SetStatus...' + indicatorDescription + ' : ' + indicatorStatus);

            switch (indicatorDescription) {
                case 'Parking Break':
                    if (indicatorStatus == 'Parking Break ON') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#ibreak_blink', '#ibreak');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#ibreak_blink', '#ibreak');
                    }

                    break;

                case 'Steer':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#isteer_blink', '#isteer');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#isteer_blink', '#isteer');
                    }

                    break;

                case 'Drive':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#idrive_blink', '#idrive');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#idrive_blink', '#idrive');
                    }

                    break;

                case 'IFM':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#iifm_blink', '#iifm');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#iifm_blink', '#iifm');
                    }

                    break;

                case 'Can':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#ican_blink', '#ican');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#ican_blink', '#ican');
                    }

                    break;

                case 'Align':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#ialign_blink', '#ialign');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#ialign_blink', '#ialign');
                    }

                    break;

                case 'Warning':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#iwarning_blink', '#iwarning');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#iwarning_blink', '#iwarning');
                    }

                    break;

                case 'Speed':
                    if (indicatorStatus == 'Err') {
                        setStopStatus(true);
                        makeIndicatorBlink(true, '#ispeed_blink', '#ispeed');
                    }
                    else {
                        setStopStatus(false);
                        makeIndicatorBlink(false, '#ispeed_blink', '#ispeed');
                    }

                    break;

                case 'Battery Voltage':
                    if (indicatorStatus >= 83.95) {
                        makeIndicatorBlink(true, '#ibattery_100_blink', '#ibattery_100');
                    }
                    if (indicatorStatus >= 83.1 && indicatorStatus <= 83.94) {
                        makeIndicatorBlink(true, '#ibattery_85', '#ibattery_100');
                    }
                    if (indicatorStatus >= 82.25 && indicatorStatus <= 83.09) {
                        makeIndicatorBlink(true, '#ibattery_71', '#ibattery_100');
                    }
                    if (indicatorStatus >= 81.4 && indicatorStatus <= 82.24) {
                        makeIndicatorBlink(true, '#ibattery_57', '#ibattery_100');
                    }
                    if (indicatorStatus >= 80.55 && indicatorStatus <= 81.39) {
                        makeIndicatorBlink(true, '#ibattery_43', '#ibattery_100');
                    }
                    if (indicatorStatus >= 79.7 && indicatorStatus <= 80.54) {
                        makeIndicatorBlink(true, '#ibattery_28', '#ibattery_100');
                    }
                    if (indicatorStatus >= 78.85 && indicatorStatus <= 79.69) {
                        makeIndicatorBlink(true, '#ibattery_14', '#ibattery_100');
                    }
                    if (indicatorStatus <= 78.84) {
                        makeIndicatorBlink(true, '#ibattery_100', '#ibattery_100_blink');
                    }

                    break;
            }

        }

        function makeIndicatorBlink(MakeBlink, elementID_Blink, elementID_NoBlink) {
            //alert('result : ' + MakeBlink + '; ' + elementID_Blink + '; ' + elementID_NoBlink)

            if (MakeBlink == true) {
                $(elementID_NoBlink).removeClass('imgmax');
                $(elementID_NoBlink).removeClass('pnlimgHide');
                $(elementID_NoBlink).addClass('pnlimgHide');

                $(elementID_Blink).removeClass('imgmax');
                $(elementID_Blink).removeClass('pnlimgHide');
                $(elementID_Blink).addClass('imgmax');

            }
            else {
                $(elementID_NoBlink).removeClass('imgmax');
                $(elementID_NoBlink).removeClass('pnlimgHide');
                $(elementID_NoBlink).addClass('imgmax');

                $(elementID_Blink).removeClass('imgmax');
                $(elementID_Blink).removeClass('pnlimgHide');
                $(elementID_Blink).addClass('pnlimgHide');
            }
        }

        function setStopStatus(StopStatus) {
            //alert('Stop Status : ' + StopStatus);

            if (StopStatus == true) {
                //alert('Stop Status : ' + StopStatus);
                $('#istop').removeClass('imgmax');
                $('#istop').removeClass('pnlimgHide')
                $('#istop').addClass('pnlimgHide')

                $('#istop_blink').removeClass('imgmax');
                $('#istop_blink').removeClass('pnlimgHide')
                $('#istop_blink').addClass('imgmax')
            }
            else {
                //alert('Stop Status : ' + StopStatus);
                $('#istop').removeClass('imgmax');
                $('#istop').removeClass('pnlimgHide')
                $('#istop').addClass('imgmax')

                $('#istop_blink').removeClass('imgmax');
                $('#istop_blink').removeClass('pnlimgHide')
                $('#istop_blink').addClass('pnlimgHide')
            }

        }

        function setLCDStartUp(strText) {
            $('#div_LCD').removeClass('LCDCont_Hide')
            $('#div_LCD').addClass('div_LCDCont')
            $('#spanLCDTitle').text(strText.replace(/%20/g, " "));

            $('#LCDLine1').removeClass('div_LCDLine');
            $('#LCDLine1').addClass('div_LCDLine1_intro');
            $('#spanLine2').text('');
            $('#spanLine3').text('');
            $('#LCDLine4').removeClass('div_LCDLine');
            $('#LCDLine4').addClass('div_LCDLine4_intro');
            //$('#spanLine1').text(' Zagro GmbH ');
            //$('#spanLine4').text('SOFTWARE 2.00.00');

        }

        function setLCDTitle() {
            var d = new Date();
            var month = d.getMonth() + 1;
            var day = d.getDate();
            
            var output = d.getFullYear() + '-' +
                (month < 10 ? '0' : '') + month + '-' +
                (day < 10 ? '0' : '') + day;

            var t = d.getHours() + ':' + d.getMinutes();


            $('#spanLCDTitle').text(output + ' ' + t);

        }

        function setLCDline1(val) {
            $('#spanLine1').text(val + ' ');
            $('#iLCD_CM').removeClass('imgLCD_NoDisplay');
            $('#iLCD_CM').addClass('imgLCD_CM_display');
            
            $('#LCDLine1').removeClass('div_LCDLine1_intro');
            $('#LCDLine1').addClass('div_LCDLine1_SpeedCM');
        }
        function setLCDline2(val) {
            $('#spanLine2').text(val + ' ');
            $('#iLCD_HR').removeClass('imgLCD_NoDisplay');
            $('#iLCD_HR').addClass('imgLCD_CM_display');

            $('#LCDLine2').removeClass('div_LCDLine');
            $('#LCDLine2').addClass('div_LCDLine2_HR');
        }
        function setLCDline3(val) {
            $('#spanLine3').text('');
            //$('#spanLine3').addClass('');
            $('#iLCD_DrvMod').removeClass('imgLCD_NoDisplay');
            $('#iLCD_DrvMod').addClass('imgLCD_DrvMod_display');

            $('#LCDLine3').removeClass('div_LCDLine');
            $('#LCDLine3').addClass('div_LCDLine3_DrvMod');

            $('#LCDLine4').removeClass('div_LCDLine');
            $('#LCDLine4').addClass('imgLCD_NoDisplay');

            var dmDiagonal = 'Content/Images/Dashboard/LCD/DiagonalMode.png';
            var dmCircular = 'Content/Images/Dashboard/LCD/CircularMode.png';
            var dmRail = 'Content/Images/Dashboard/LCD/RailMode.png';
            
            var oImgSrc = $('#iLCD_DrvMod').attr('src');

            if (val == 'Diagonal mode road')
            {
                $('img[src="' + oImgSrc + '"]').attr('src', dmDiagonal);
            }
            if (val == 'Circular mode road') {

                $('img[src="' + oImgSrc + '"]').attr('src', dmCircular);
            }
            if (val == 'Rail mode road') {

                $('img[src="' + oImgSrc + '"]').attr('src', dmRail);
            }

        }

        function initPlaceToolTips() {
            $('.div_break').prop('title', 'Flashes when fixing brake is not open ');
            $('.div_steer').prop('title', 'Flashes when steering is not enabled ');
            $('.div_drive').prop('title', 'Flashes when drive is not enabled ');
            $('.div_ifm').prop('title', 'Flashes when no communication with IFM control ');
            $('.div_can').prop('title', 'Flashes when no communication to AO412-module ');
            $('.div_align').prop('title', 'Flashes when alignment is necessary ');
            $('.div_warning').prop('title', 'Blinks when emergency operation is enabled  ');
            $('.div_stop').prop('title', 'Blinks when emergency shutdown ');
            $('.div_speed').prop('title', 'Blinks when maximum speed is limited ');
            $('.div_battery').prop('title', 'Battery life indicator ');

            $('.div_plus').prop('title', 'No Functions ');
            $('.div_minus').prop('title', 'No Functions ');
            $('.div_ex').prop('title', 'No Functions ');
            $('.div_tick').prop('title', 'No Functions ');

        }

        function SetToolTipPerStatus(elementIDOrClass, StatusDesc) {

            $(elementIDOrClass).prop('title', StatusDesc);

        }

        function blink() {
            if (bln_blink == true) {

                if (blink_ctr < 4) {
                    //--- break on gray
                    $('#ibreak').removeClass('imgmax');
                    $('#ibreak').removeClass('pnlimgHide')
                    $('#ibreak').addClass('pnlimgHide')

                    $('#ibreak_blink').removeClass('imgmax');
                    $('#ibreak_blink').removeClass('pnlimgHide')
                    $('#ibreak_blink').addClass('imgmax')

                    //--- steer on gray
                    $('#isteer').removeClass('imgmax');
                    $('#isteer').removeClass('pnlimgHide')
                    $('#isteer').addClass('pnlimgHide')

                    $('#isteer_blink').removeClass('imgmax');
                    $('#isteer_blink').removeClass('pnlimgHide')
                    $('#isteer_blink').addClass('imgmax')

                    //--- drive on gray
                    $('#idrive').removeClass('imgmax');
                    $('#idrive').removeClass('pnlimgHide')
                    $('#idrive').addClass('pnlimgHide')

                    $('#idrive_blink').removeClass('imgmax');
                    $('#idrive_blink').removeClass('pnlimgHide')
                    $('#idrive_blink').addClass('imgmax')

                    //--- ifm on gray
                    $('#iifm').removeClass('imgmax');
                    $('#iifm').removeClass('pnlimgHide')
                    $('#iifm').addClass('pnlimgHide')

                    $('#iifm_blink').removeClass('imgmax');
                    $('#iifm_blink').removeClass('pnlimgHide')
                    $('#iifm_blink').addClass('imgmax')

                    //--- can on gray
                    $('#ican').removeClass('imgmax');
                    $('#ican').removeClass('pnlimgHide')
                    $('#ican').addClass('pnlimgHide')

                    $('#ican_blink').removeClass('imgmax');
                    $('#ican_blink').removeClass('pnlimgHide')
                    $('#ican_blink').addClass('imgmax')

                    //--- alignment on gray
                    $('#ialign').removeClass('imgmax');
                    $('#ialign').removeClass('pnlimgHide')
                    $('#ialign').addClass('pnlimgHide')

                    $('#ialign_blink').removeClass('imgmax');
                    $('#ialign_blink').removeClass('pnlimgHide')
                    $('#ialign_blink').addClass('imgmax')

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
                    $('#ibattery_100').addClass('pnlimgHide')

                    $('#ibattery_100_blink').removeClass('imgmax');
                    $('#ibattery_100_blink').removeClass('pnlimgHide')
                    $('#ibattery_100_blink').addClass('imgmax')
                }

                if (blink_ctr == 4) {
                    //--- warining on color
                    $('#iwarning').removeClass('imgmax');
                    $('#iwarning').removeClass('pnlimgHide')
                    $('#iwarning').addClass('imgmax')

                    $('#iwarning_blink').removeClass('imgmax');
                    $('#iwarning_blink').removeClass('pnlimgHide')
                    $('#iwarning_blink').addClass('pnlimgHide')

                    //--- error on color
                    $('#istop').removeClass('imgmax');
                    $('#istop').removeClass('pnlimgHide')
                    $('#istop').addClass('imgmax')

                    $('#istop_blink').removeClass('imgmax');
                    $('#istop_blink').removeClass('pnlimgHide')
                    $('#istop_blink').addClass('pnlimgHide')

                    //--- speed on color
                    $('#ispeed').removeClass('imgmax');
                    $('#ispeed').removeClass('pnlimgHide')
                    $('#ispeed').addClass('imgmax')

                    $('#ispeed_blink').removeClass('imgmax');
                    $('#ispeed_blink').removeClass('pnlimgHide')
                    $('#ispeed_blink').addClass('pnlimgHide')

                }

                bln_blink = false;

                blink_ctr = blink_ctr + 1;

            }
            else {
                if (blink_ctr < 4) {
                    //--- break on color
                    $('#ibreak').removeClass('imgmax');
                    $('#ibreak').removeClass('pnlimgHide')
                    $('#ibreak').addClass('imgmax')

                    $('#ibreak_blink').removeClass('imgmax');
                    $('#ibreak_blink').removeClass('pnlimgHide')
                    $('#ibreak_blink').addClass('pnlimgHide')

                    //--- steer on color
                    $('#isteer').removeClass('imgmax');
                    $('#isteer').removeClass('pnlimgHide')
                    $('#isteer').addClass('imgmax')

                    $('#isteer_blink').removeClass('imgmax');
                    $('#isteer_blink').removeClass('pnlimgHide')
                    $('#isteer_blink').addClass('pnlimgHide')

                    //--- drive on color
                    $('#idrive').removeClass('imgmax');
                    $('#idrive').removeClass('pnlimgHide')
                    $('#idrive').addClass('imgmax')

                    $('#idrive_blink').removeClass('imgmax');
                    $('#idrive_blink').removeClass('pnlimgHide')
                    $('#idrive_blink').addClass('pnlimgHide')

                    //--- ifm on color
                    $('#iifm').removeClass('imgmax');
                    $('#iifm').removeClass('pnlimgHide')
                    $('#iifm').addClass('imgmax')

                    $('#iifm_blink').removeClass('imgmax');
                    $('#iifm_blink').removeClass('pnlimgHide')
                    $('#iifm_blink').addClass('pnlimgHide')

                    //--- can on color
                    $('#ican').removeClass('imgmax');
                    $('#ican').removeClass('pnlimgHide')
                    $('#ican').addClass('imgmax')

                    $('#ican_blink').removeClass('imgmax');
                    $('#ican_blink').removeClass('pnlimgHide')
                    $('#ican_blink').addClass('pnlimgHide')

                    //--- alignment on color
                    $('#ialign').removeClass('imgmax');
                    $('#ialign').removeClass('pnlimgHide')
                    $('#ialign').addClass('imgmax')

                    $('#ialign_blink').removeClass('imgmax');
                    $('#ialign_blink').removeClass('pnlimgHide')
                    $('#ialign_blink').addClass('pnlimgHide')

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
                    $('#ibattery_100').addClass('imgmax')

                    $('#ibattery_100_blink').removeClass('imgmax');
                    $('#ibattery_100_blink').removeClass('pnlimgHide')
                    $('#ibattery_100_blink').addClass('pnlimgHide')

                }


                bln_blink = true;

                blink_ctr = blink_ctr + 1;

            }
        }

        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toGMTString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        }

        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        function checkCookie(cookiename) {
            var valCookie = getCookie(cookiename);
            if (valCookie != "") {
                return true;
            }
            else {
                return false;
            }
        }

        function goBlink()
        {
                setTimeout('blink()', 0);
                setTimeout('blink()', 1000);
                setTimeout('blink()', 2000);
                setTimeout('blink()', 3000);
                setTimeout('blink()', 4000);
        }

        //if (run_blink == true)
        //{
        //    alert('blink');
        //    setTimeout('blink()', 0);
        //    setTimeout('blink()', 1000);
        //    setTimeout('blink()', 2000);
        //    setTimeout('blink()', 3000);
        //    setTimeout('blink()', 4000);

        //}
        
        setTimeout('getDataFromServer()', 4000);
        setTimeout(function () {
            setLCDStartUp(priVehicleName);
        }, 4500)

        //setTimeout('timeout()', 4000);
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <%--<a href="#" onClick="map.closeInfoWindow()"><img 
        src="here_you_put_your_cute_close_image.jpg" alt="close window"></a> 
        </code> --%>
        <div class="div_dashboard">
        </div>
        <%--<div id="idCloseWindow" class="div_closebutton">
            <img src="Content/Images/Dashboard/x2_button.png" id="iClose" class="imgmax" />
        </div>--%>
        <div class="div_break">
            <img src="Content/Images/Dashboard/break_gray.png" id="ibreak" class="imgmax" />
        </div>
        <div class="div_break">
            <img src="Content/Images/Dashboard/Break_color.png" id="ibreak_blink" class="pnlimgHide" />
        </div>
        <div class="div_steer">
            <img src="Content/Images/Dashboard/steering_gray.png" id="isteer" class="imgmax" />
        </div>
        <div class="div_steer">
            <img src="Content/Images/Dashboard/steering_color.png" id="isteer_blink" class="pnlimgHide" />
        </div>
        <div class="div_drive">
            <img src="Content/Images/Dashboard/drive_gray.png" id="idrive" class="imgmax" />
        </div>
        <div class="div_drive">
            <img src="Content/Images/Dashboard/drive_color.png" id="idrive_blink" class="pnlimgHide" />
        </div>
        <div class="div_ifm">
            <img src="Content/Images/Dashboard/ifm_gray.png" id="iifm" class="imgmax" />
        </div>
        <div class="div_ifm">
            <img src="Content/Images/Dashboard/ifm_color.png" id="iifm_blink" class="pnlimgHide" />
        </div>
        <div class="div_can">
            <img src="Content/Images/Dashboard/can_gray.png" id="ican" class="imgmax" />
        </div>
        <div class="div_can">
            <img src="Content/Images/Dashboard/can_color.png" id="ican_blink" class="pnlimgHide" />
        </div>
        <div class="div_align">
            <img src="Content/Images/Dashboard/alignment_gray.png" id="ialign" class="imgmax" />
        </div>
        <div class="div_align">
            <img src="Content/Images/Dashboard/alignment_color.png" id="ialign_blink" class="pnlimgHide" />
        </div>
        <div class="div_warning">
            <img src="Content/Images/Dashboard/warning_gray.png" id="iwarning" class="imgmax" />
        </div>
        <div class="div_warning">
            <img src="Content/Images/Dashboard/warning_color.png" id="iwarning_blink" class="pnlimgHide" />
        </div>
        <div class="div_stop">
            <img src="Content/Images/Dashboard/error_gray.png" id="istop" class="imgmax" />
        </div>
        <div class="div_stop">
            <img src="Content/Images/Dashboard/error_color.png" id="istop_blink" class="pnlimgHide" />
        </div>
        <div class="div_speed">
            <img src="Content/Images/Dashboard/speed_gray.png" id="ispeed" class="imgmax" />
        </div>
        <div class="div_speed">
            <img src="Content/Images/Dashboard/speed_color.png" id="ispeed_blink" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_gray.png" id="ibattery_100" class="imgmax" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_100.png" id="ibattery_100_blink" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_85.png" id="ibattery_85" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_71.png" id="ibattery_71" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_57.png" id="ibattery_57" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_43.png" id="ibattery_43" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_28.png" id="ibattery_28" class="pnlimgHide" />
        </div>
        <div class="div_battery">
            <img src="Content/Images/Dashboard/battery_14.png" id="ibattery_14" class="pnlimgHide" />
        </div>
        <div id="iPlus" class="div_plus">
        </div>
        <div id="iMinus" class="div_minus">
        </div>
        <div id="iEx" class="div_ex">
        </div>
        <div id="iTick" class="div_tick">
        </div>
        <div class="div_bottomlogo">
            <img src="Content/Images/Dashboard/NanoSoft_Colour.png" id="iBottomNanosoft" class="imgmax" />
        </div>
        <div class="div_LCD">
            <img src="Content/Images/Dashboard/NanoSoft_Colour.png" id="iLCD" class="imgLCD" />
        </div>
        <div id="div_LCD" class="LCDCont_Hide">
            <div id="LCDTitle" class="div_LCDTitle">
                <span id="spanLCDTitle"></span>                
            </div>
            <div id="LCDLine1" class="div_LCDLine">
                <span id="spanLine1"></span><img src="Content/Images/Dashboard/LCD/Num_CM-S.png" id="iLCD_CM" class="imgLCD_NoDisplay" />                
            </div>
            <div id="LCDLine2" class="div_LCDLine">
                <span id="spanLine2"></span><img src="Content/Images/Dashboard/LCD/icon.png" id="iLCD_HR" class="imgLCD_NoDisplay" />
            </div>
            <div id="LCDLine3" class="div_LCDLine">
                <span id="spanLine3"></span><img src="Content/Images/Dashboard/LCD/DiagonalMode.png" id="iLCD_DrvMod" class="imgLCD_NoDisplay" />
            </div>
            <div id="LCDLine4" class="div_LCDLine">
                <span id="spanLine4"></span>
            </div>
        </div>
        
        <%--<div id="idLiveData" class="div_vwLiveData">
        <asp:Label ID="Label1" runat="server" Text="Launch NanoSoft Display" Font-Size="11px" Width="150px" Font-Names="arial"></asp:Label>
       
    </div>--%>

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
