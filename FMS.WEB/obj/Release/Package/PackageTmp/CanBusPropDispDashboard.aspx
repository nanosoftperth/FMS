﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusPropDispDashboard.aspx.vb" Inherits="FMS.WEB.CanBusPropDispDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/javascript/jquery-1.10.2.min.js"></script>
    <script src="Content/javascript/page.js"></script>
    <link href="Content/DashBoard.css" rel="stylesheet" />
    <link href="Content/font_style.css" rel="stylesheet" />

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
        var reconnectctr = 0;
        var priXL = '';
        var priL = '';
        
        $(document).ready(function () {
            priDeviceID = GetParameterValues('DeviceID');
            //priVehicleName = GetParameterValues('VehicleName');
            priVehicleName = GetParameterValues('VehicleName').replace(/%20/g, " ");
            priXL = priVehicleName.indexOf(" XL");
            priL = priVehicleName.indexOf(" L");

            priClickEvent = GetParameterValues('ClickEvent');
            //replace(/%20/g, " ")
            initPlaceToolTips();

            var ThisDevID = getCookie('devID');
            var coockieexist = checkCookie('devID');

            if (coockieexist == false) {
                setCookie('devID', priDeviceID, 1);
                //getDataFromServer();
                goBlink();

                setTimeout(function () {
                    setLCDStartUp(priVehicleName);
                }, 4500);
                //timeout();
                setTimeout('timeout()', 2500);

            }
            else
            {
                if (priDeviceID != ThisDevID)
                {
                    setCookie('devID', priDeviceID, 1);
                    //getDataFromServer();
                    goBlink();
                    //setLCDStartUp(priVehicleName);
                    setTimeout(function () {
                        setLCDStartUp(priVehicleName);
                    }, 4500);
                    //timeout();
                    setTimeout('timeout()', 2500);
                    
                }
                else
                {
                    //getDataFromServer();
                    $('#iLCD').removeClass('imgLCD');
                    $('#iLCD').addClass('pnlimgHide');
                    getDataFromServer();
                    timeout();
                    //setTimeout('timeout()', 2500);
                }
            }

            
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

        function setArrDev(val)
        {
            priListDed.push(val);
        }

        function timeout() {
            setTimeout(function () {
                // Do Something Here
                // Then recall the parent function to
                // create a recursive loop.
                getDataFromServer();
                timeout();
            }, 2500);
        }

        (function() {
            /**
             * Decimal adjustment of a number.
             *
             * @param {String}  type  The type of adjustment.
             * @param {Number}  value The number.
             * @param {Integer} exp   The exponent (the 10 logarithm of the adjustment base).
             * @returns {Number} The adjusted value.
             */
            function decimalAdjust(type, value, exp) {
                // If the exp is undefined or zero...
                if (typeof exp === 'undefined' || +exp === 0) {
                    return Math[type](value);
                }
                value = +value;
                exp = +exp;
                // If the value is not a number or the exp is not an integer...
                if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
                    return NaN;
                }
                // If the value is negative...
                if (value < 0) {
                    return -decimalAdjust(type, -value, exp);
                }
                // Shift
                value = value.toString().split('e');
                value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
                // Shift back
                value = value.toString().split('e');
                return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
            }

            // Decimal round
            if (!Math.round10) {
                Math.round10 = function(value, exp) {
                    return decimalAdjust('round', value, exp);
                };
            }
            // Decimal floor
            if (!Math.floor10) {
                Math.floor10 = function(value, exp) {
                    return decimalAdjust('floor', value, exp);
                };
            }
            // Decimal ceil
            if (!Math.ceil10) {
                Math.ceil10 = function(value, exp) {
                    return decimalAdjust('ceil', value, exp);
                };
            }
        })();

        function getDataFromServer() {
            //var uri = '../api/Vehicle/GetCanMessageValue?deviceID=' + priDeviceID;
            var uri = '../api/Vehicle/GetDashboardData?DashDeviceID=' + priDeviceID;
            //var uri = '../api/Vehicle/GetDashboardDataSim';

            $.getJSON(uri).done(function (data) { receivedData(data); });
            
        }

        function receivedData(dataCache) {
            //var obj = dataCache;

            initLEDs();

            if (oDashList == null) {
                var obj = dataCache;
            }
            else
            {
                var obj = oDashList
                var objErrCat = oDashList[0].LCD_ErrorCategory
                
            }
            
            var ThisDevID = getCookie('devID');

            if (priDeviceID == ThisDevID)
            {
                $('#div_LCD').removeClass('LCDCont_Hide')
                $('#div_LCD').addClass('div_LCDCont')
                $('#spanLCDTitle').text(priVehicleName.replace(/%20/g, " "));
            }

            if (obj.length > 0) {
                var oCtr = obj.length;

                //alert('Inside OBJ... Len: ' + oCtr);

                for (v = 0; v < oCtr; v++) {

                    if (obj[v].Parking_Break == 'Parking Brake ON') {
                        SetToolTipPerStatus('.div_break', 'Parking Brake : ' + obj[v].Parking_Break);
                        SetStatus('Parking Brake', obj[v].Parking_Break);
                    }

                    if (obj[v].Steering.length > 0) {
                        //alert('Inside steering');
                        SetToolTipPerStatus('.div_steer', 'Check Steering');
                        SetStatus('Steer', 'Err');
                        //setLCDErrCat(obj[v].Steering, 'Steering');
                    }

                    if (obj[v].Driving.length > 0) {
                       // alert('Inside OBJ');
                        //SetToolTipPerStatus('.div_drive', 'Drive : ' + obj[v].Driving);
                        SetToolTipPerStatus('.div_drive', 'Check Driving');
                        SetStatus('Drive', 'Err');
                    }

                    if (obj[v].IFMControl.length > 0) {
                        SetToolTipPerStatus('.div_ifm', 'IFM : ' + obj[v].IFMControl);
                        SetStatus('IFM', 'Err');
                    }

                    if (obj[v].CANControl.length > 0) {
                        SetToolTipPerStatus('.div_can', 'CAN/CANOPEN : ' + obj[v].CANControl);
                        SetStatus('Can', 'Err');
                        //alert('receivedData: ' + obj[v].CANControl);
                        //setLCDErrCat(obj[v].CANControl, 'CAN');
                    }

                    if (obj[v].CANOPENControl.length > 0) {
                        SetToolTipPerStatus('.div_can', 'CAN/CANOPEN : ' + obj[v].CANOPENControl);
                        SetStatus('Can', 'Err');
                        //alert('receivedData: ' + obj[v].CANControl);
                        //setLCDErrCat(obj[v].CANOPENControl, 'CANOPEN');
                    }

                    if (obj[v].AlignmentControl.length > 0) {
                        //SetToolTipPerStatus('.div_align', 'Alignment : ' + obj[v].AlignmentControl);
                        SetToolTipPerStatus('.div_align', 'Check Alignment');
                        SetStatus('Align', 'Err');
                    }

                    if (obj[v].WarningControl.length > 0) {
                        SetToolTipPerStatus('.div_warning', 'Warning : ' + obj[v].WarningControl);
                        SetStatus('Warning', 'Err');
                    }

                    if (obj[v].StopControl.length > 0) {
                        SetToolTipPerStatus('.div_stop', 'Stop');
                        SetStatus('Stop', obj[v].StopControl);
                    }

                    if (obj[v].SpeedControl.length > 0) {
                        //SetToolTipPerStatus('.div_speed', 'Speed : ' + obj[v].SpeedControl);
                        SetToolTipPerStatus('.div_speed', 'Check Speed');
                        SetStatus('Speed', 'Err');

                    }

                    if (obj[v].Battery_Voltage.length > 0) {
                        //alert('Battery_Voltage > 0');
                        SetToolTipPerStatus('.div_battery', 'Battery Voltage : ' + obj[v].Battery_Voltage);
                        SetStatus('Battery Voltage', obj[v].Battery_Voltage);
                        setLCDTitle_percentage(obj[v].Battery_Voltage);
                    }

                    //if (obj[v].LCD_WithFaultCode.length > 0) {
                    //    setLCDErrCat(obj[v].LCD_WithFaultCode, 'NOFAULTCODE');
                    //}

                    //if (obj[v].LCD_DataLogger.length > 0) {
                    //    setLCDTitle();
                    //}

                    if (obj[v].LCD_Speed.length > 0) {
                        setLCDline1(obj[v].LCD_Speed);
                        //setLCDline2('5:49');                        
                    }

                    if (obj[v].LCD_Hour.length > 0) {
                        setLCDline2(obj[v].LCD_Hour);
                    }
                    else //will delete if there is already a return value from API
                    {
                        if (priL >= 0) {
                            setLCDline2('00:00');
                            
                        }

                        if (priXL >= 0) {

                            setLCDline2('00:00');
                        }
                        
                    }


                    if (obj[v].LCD_Driving_Mode.length > 0) {
                        setLCDline3(obj[v].LCD_Driving_Mode);                        
                    }

                    if (obj[v].LCD_ErrorCategory != null)
                    {
                        if (obj[v].LCD_ErrorCategory.length > 0) {
                            var lenErrCat = obj[v].LCD_ErrorCategory.length;

                            for (e = 0; e < lenErrCat; e++) {
                                if (obj[v].LCD_ErrorCategory[e].Err_Category.length > 0) {
                                    var strCat = obj[v].LCD_ErrorCategory[e].Err_Category;
                                    var strCode = obj[v].LCD_ErrorCategory[e].Err_Value;

                                    if (strCat == 'Safety') {
                                        setLCDErrCat(strCode, 'SAFE');
                                    }

                                    if (strCat == 'Steering') {
                                        setLCDErrCat(strCode, 'Steering');
                                    }

                                    if (strCat == 'DriveM1') {
                                        setLCDErrCat(strCode, 'DriveM1');
                                    }

                                    if (strCat == 'DriveM2') {
                                        setLCDErrCat(strCode, 'DriveM2');
                                    }

                                    if (strCat == 'DriveM2') {
                                        setLCDErrCat(strCode, 'DriveM2');
                                    }

                                    if (strCat == 'DriveM2') {
                                        setLCDErrCat(strCode, 'DriveM2');
                                    }

                                    if (strCat == 'DriveM4') {
                                        setLCDErrCat(strCode, 'DriveM4');
                                    }

                                    if (strCat == 'CAN') {
                                        setLCDErrCat(strCode, 'CAN');
                                    }

                                    if (strCat == 'CANOPEN') {
                                        setLCDErrCat(strCode, 'CANOPEN');
                                    }

                                    if (strCat == 'InOut') {
                                        setLCDErrCat(strCode, 'InOut');
                                    }


                                }
                            }
                        }
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
                case 'Parking Brake':
                    if (indicatorStatus == 'Parking Brake ON') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#ibreak_blink', '#ibreak');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#ibreak_blink', '#ibreak');
                    }

                    break;

                case 'Steer':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#isteer_blink', '#isteer');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#isteer_blink', '#isteer');
                    }

                    break;

                case 'Drive':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#idrive_blink', '#idrive');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#idrive_blink', '#idrive');
                    }

                    break;

                case 'IFM':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#iifm_blink', '#iifm');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#iifm_blink', '#iifm');
                    }

                    break;

                case 'Can':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#ican_blink', '#ican');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#ican_blink', '#ican');
                    }

                    break;

                case 'Align':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#ialign_blink', '#ialign');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#ialign_blink', '#ialign');
                    }

                    break;

                case 'Warning':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#iwarning_blink', '#iwarning');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#iwarning_blink', '#iwarning');
                    }

                    break;

                case 'Stop':
                    if (indicatorStatus == 'On') {
                        setStopStatus(true);
                        
                    }
                    else {
                        setStopStatus(false);                        
                    }

                    break;

                case 'Speed':
                    if (indicatorStatus == 'Err') {
                        //setStopStatus(true);
                        makeIndicatorBlink(true, '#ispeed_blink', '#ispeed');
                    }
                    else {
                        //setStopStatus(false);
                        makeIndicatorBlink(false, '#ispeed_blink', '#ispeed');
                    }

                    break;

                case 'Battery Voltage':
                    //alert('Setting battery indicator: ' + indicatorDescription + '; ' +indicatorStatus);
                    makeIndicatorBlink(true, '#ibattery_100', '#ibattery_100_blink');

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
            $('#spanLine1').text(' Zagro GmbH ');
            $('#spanLine4').text('SOFTWARE 4.05.05');

        }

        function setLCDNoStartUp(strText) {
            $('#div_LCD').removeClass('LCDCont_Hide')
            $('#div_LCD').addClass('div_LCDCont')
            $('#spanLCDTitle').text(strText.replace(/%20/g, " "));

            //$('#LCDLine1').removeClass('div_LCDLine');
            //$('#LCDLine1').addClass('div_LCDLine1_intro');
            //$('#spanLine2').text('');
            //$('#spanLine3').text('');
            //$('#LCDLine4').removeClass('div_LCDLine');
            //$('#LCDLine4').addClass('div_LCDLine4_intro');
            //$('#spanLine1').text(' Zagro GmbH ');
            //$('#spanLine4').text('SOFTWARE 4.05.05');

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

        function setLCDTitle_percentage(val) {

            $('#iBatteryPerc').removeClass('imgLCD_NoDisplay');
            $('#iBatteryPerc').addClass('span_batterypercentage');

            $('#iBatteryPerc').text(val + '%');
           
        }

        function setLCDline1(val) {
            
            var num = Math.round10(val, -1);

            if (num == 0) {
                varnum = "0.0";
            }
            else
            {
                varnum = num;
            }

            var speedImgKMH = 'Content/Images/Dashboard/LCD/Num_KM-H.png';
            var speedImgCMS = 'Content/Images/Dashboard/LCD/Num_CM-S.png';

            var oImgSrc = $('#iLCD_CM').attr('src');

            if (priL >= 0) {

                if (oImgSrc != 'Content/Images/Dashboard/LCD/Num_CM-S.png')
                {
                    $('img[src="' + oImgSrc + '"]').attr('src', speedImgCMS);
                }                
            }

            if (priXL >= 0) {

                if (oImgSrc != 'Content/Images/Dashboard/LCD/Num_KM-H.png') {
                    $('img[src="' + oImgSrc + '"]').attr('src', speedImgKMH);
                }
            }

            $('#spanLine1').text(varnum + ' ');
            //$('#iLCD_CM_NumSign').removeClass('imgLCD_NoDisplay');
            //$('#iLCD_CM_1stNum').removeClass('imgLCD_NoDisplay');
            //$('#iLCD_CM_2ndNum').removeClass('imgLCD_NoDisplay');
            //$('#iLCD_CM_NumDel').removeClass('imgLCD_NoDisplay');
            //$('#iLCD_CM_3rdNum').removeClass('imgLCD_NoDisplay');
            //$('#iLCD_CM_4thNum').removeClass('imgLCD_NoDisplay');
            $('#iLCD_CM').removeClass('imgLCD_NoDisplay');

            //$('#iLCD_CM_NumSign').addClass('imgLCD_CM_display');
            //$('#iLCD_CM_1stNum').addClass('imgLCD_CM_display');
            //$('#iLCD_CM_2ndNum').addClass('imgLCD_CM_display');
            //$('#iLCD_CM_NumDel').addClass('imgLCD_CM_display');
            //$('#iLCD_CM_3rdNum').addClass('imgLCD_CM_display');
            //$('#iLCD_CM_4thNum').addClass('imgLCD_CM_display');
            $('#iLCD_CM').addClass('imgLCD_CM_display');
            
            $('#LCDLine1').removeClass('div_LCDLine1_intro');
            $('#LCDLine1').addClass('div_LCDLine1_SpeedCM');

            
            //Content/Images/Dashboard/LCD/Num_KM-H.png
            //Content/Images/Dashboard/LCD/Num_CM-S.png
            //priXL = priVehicleName.indexOf(" XL");
            //priL = priVehicleName.indexOf(" L");
            //var dmDiagonal = 'Content/Images/Dashboard/LCD/DiagonalMode.png';
            //$('img[src="' + oImgSrc + '"]').attr('src', dmDiagonal);
        }
        function setLCDline2(val) {
            var strVal = val.replace(':', ' : ')

            $('#spanLine2').text(strVal + ' ');
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
            var dmCircular = 'Content/Images/Dashboard/LCD/CircularMode_rot90.png';
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

            if (val == 'Undefined') {

                $('img[src="' + oImgSrc + '"]').attr('src', dmCircular);
            }

        }

        function setLCDErrCat(val, category) {
            var strVal = val;
            var nStart = strVal.indexOf(' ');
            var nEnd = strVal.length;
            var NumCode = strVal.substring(nStart, nEnd);

            //alert(val + ' : ' + NumCode);
            var errCAN = 'Content/Images/Dashboard/LCD/can.png';
            var errCANOPEN = 'Content/Images/Dashboard/LCD/CanOpen.png';
            var errStop = 'Content/Images/Dashboard/LCD/Stop.png';
            var errDriveM1 = 'Content/Images/Dashboard/LCD/FaultyM1.png';
            var errDriveM2 = 'Content/Images/Dashboard/LCD/FaultyM2.png';
            var errDriveM3 = 'Content/Images/Dashboard/LCD/FaultyM3.png';
            var errDriveM4 = 'Content/Images/Dashboard/LCD/FaultyM4.png';
            var errSteering = 'Content/Images/Dashboard/LCD/SteeringFault.png';
            var errIO = 'Content/Images/Dashboard/LCD/InOut.png';

            $('#LCDLine4').removeClass('div_LCDLine');
            $('#LCDLine4').addClass('imgLCD_NoDisplay');

            var oImgSrc = $('#iErrCat').attr('src');
            
            //$('#ErrCat').removeClass('imgLCD_NoDisplay');
            //$('#ErrCat').addClass('imgLCD_ErrCat_display');

            $('#iErrCat').removeClass('imgLCD_NoDisplay');
            $('#iErrCat').addClass('imgLCD_ErrCat_display');

            if (category == 'CAN')
            {
                $('img[src="' + oImgSrc + '"]').attr('src', errCAN);
            }
            if (category == 'CANOPEN') {
                $('img[src="' + oImgSrc + '"]').attr('src', errCANOPEN);
            }
            if (category == 'SAFE') {
                $('img[src="' + oImgSrc + '"]').attr('src', errStop);
            }
            if (category == 'DriveM1') {
                $('img[src="' + oImgSrc + '"]').attr('src', errDriveM1);
            }
            if (category == 'DriveM2') {
                $('img[src="' + oImgSrc + '"]').attr('src', errDriveM2);
            }
            if (category == 'DriveM3') {
                $('img[src="' + oImgSrc + '"]').attr('src', errDriveM3);
            }
            if (category == 'DriveM4') {
                $('img[src="' + oImgSrc + '"]').attr('src', errDriveM4);
            }
            if (category == 'Steering') {
                $('img[src="' + oImgSrc + '"]').attr('src', errSteering);
            }
            if (category == 'InOut') {
                $('img[src="' + oImgSrc + '"]').attr('src', errIO);
            }

            if (category == 'NOFAULTCODE') {
                $('#spanNoFaultCode').text('No Fault Code');
                
            }

            $('#spanErrNum').text(NumCode);
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

        function initLEDs()
        {
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
                <span id="iBatteryPerc" class="span_batterypercentage"></span>            
            </div>
            <div id="LCDLine1" class="div_LCDLine">
                <span id="spanLine1"></span>
               <%-- <img src="Content/Images/Dashboard/LCD/negative.png" id="iLCD_CM_NumSign" class="imgLCD_NoDisplay" 
                    /><img src="Content/Images/Dashboard/LCD/Num_0.png" id="iLCD_CM_1stNum" class="imgLCD_NoDisplay" 
                    /><img src="Content/Images/Dashboard/LCD/Num_0.png" id="iLCD_CM_2ndNum" class="imgLCD_NoDisplay" 
                    /><img src="Content/Images/Dashboard/LCD/Num_0.png" id="iLCD_CM_3rdNum" class="imgLCD_NoDisplay" 
                    /><img src="Content/Images/Dashboard/LCD/DecPoint.png" id="iLCD_CM_DecPt" class="imgLCD_NoDisplay" 
                    /><img src="Content/Images/Dashboard/LCD/Num_0.png" id="iLCD_CM_4thNum" class="imgLCD_NoDisplay"
                    /><img src="Content/Images/Dashboard/LCD/Num_0.png" id="iLCD_CM_5thNum" class="imgLCD_NoDisplay"/>--%>
                    <img src="Content/Images/Dashboard/LCD/Num_KM-H.png" id="iLCD_CM" class="imgLCD_NoDisplay" />
            </div>
            <div id="LCDLine2" class="div_LCDLine">
                <span id="spanLine2"></span><img src="Content/Images/Dashboard/LCD/Num_hrs.png" id="iLCD_HR" class="imgLCD_NoDisplay" />
            </div>
            <div id="LCDLine3" class="div_LCDLine">
                <span id="spanLine3"></span><img src="Content/Images/Dashboard/LCD/DiagonalMode.png" id="iLCD_DrvMod" class="imgLCD_NoDisplay" />
            </div>
            <div id="LCDLine4" class="div_LCDLine">
                <span id="spanLine4"></span>
            </div>
            <div id="ErrCat" class="div_LCD_ErrCat">
                <span id="spanNoFaultCode" class="span_errnum"></span><img src="Content/Images/Dashboard/LCD/FaultyM1.png" id="iErrCat" class="imgLCD_NoDisplay" />
            </div>
            <div id="ErrNum" class="div_LCD_ErrNum">
                <span id="spanErrNum" class="span_errnum"></span>
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