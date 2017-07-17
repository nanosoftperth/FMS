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
        var bln_blink = true;
        var bln_blinkBottom = false;
        var blink_ctr = 0;
        var priDeviceID = '';
        var priVehicleName = '';

        $(document).ready(function () {
            priDeviceID = GetParameterValues('DeviceID');
            priVehicleName = GetParameterValues('VehicleName');

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
            var uri = '../api/Vehicle/GetCanMessageValue?deviceID=' + priDeviceID;

            $.getJSON(uri).done(function (data) { receivedData(data); });

        }

        function receivedData(dataCache) {
            var obj = dataCache;

            if (obj.length > 0) {
                var oCtr = obj.length;
                var cvCtr = 0;
                var strDesc = '';
                var strCV_Value = '';

                if (obj[0].CanValues.length == 0)
                {
                    //alert('No data to fetch for this vehicle!');
                    return;                   
                }

                for (i = 0; i < oCtr; i++) {

                    //strDesc = obj[i].MessageDefinition.Description;
                    //strCV_Value = obj[i].CanValues[0].Value;

                    // for tesing fault codes only. Will delete when deployed to demo/live
                    strDesc = 'Fault Codes';
                    strCV_Value = 'S 1,MS 34,MS 41,MS 42,M1 1,M1 4,M2 4,M3 2,M4 4';

                    switch (strDesc) {
                        case 'Parking Break':
                            SetStatus(strDesc, strCV_Value);
                            //break;

                        case 'Fault Codes':
                            var fltCtr = (strCV_Value.match(/,/g) || []).length + 1;
                            var arr = strCV_Value.split(',');
                            var oMS = '';
                            var oMNum = '';

                            for (var i = 0; i < fltCtr; i++) {

                                if (arr[i].substring(0, 2).trim() == 'MS') {
                                    
                                    if (oMS.length == 0)
                                    {
                                        SetStatus('Steer', 'Err');
                                        oMS = arr[i];
                                    }
                                    
                                }

                                if (arr[i].substring(0, 2).trim() == 'M1' || arr[i].substring(0, 2).trim() == 'M2' || arr[i].substring(0, 2).trim() == 'M3' || arr[i].substring(0, 2).trim() == 'M4') {

                                    if (oMNum.length == 0) {
                                        SetStatus('Drive', 'Err');
                                        oMNum = arr[i];
                                    }

                                }
                            }

                            break;
                        
                    }

                    // for tesing fault codes only. Will delete when deployed to demo/live
                    if (strDesc == 'Fault Codes')
                    {
                        break;
                    }

                }

                //str = JSON.parse(str);
                //$.each(obj.Object.MessageDefinition, function (i, item) {
                //    if (item.Description == 'Parking Break')
                //    {
                //        alert('test');
                //    }
                //    //$(".result1").append(item.username);
                //    //$(".result2").append(item.uid);
                //});

                //alert(obj.CanValueMessageDefinition.MessageDefinition)
                //document.getElementById("demo").innerHTML = obj.name + ", " + obj.age;

            }

        }

        function SetStatus(indicatorDescription, indicatorStatus)
        {
            var elementID_Blink = '';
            var elementID_NoBlink = '';
            var blnIndicatorOn = false;

            //alert('Inside SetStatus...');

            switch (indicatorDescription) {
                case 'Parking Break':
                    //alert('Inside parking break...');

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
            }

        }

        function makeIndicatorBlink(MakeBlink, elementID_Blink, elementID_NoBlink)
        {
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

        function setStopStatus(StopStatus)
        {
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
            else
            {
                //alert('Stop Status : ' + StopStatus);
                $('#istop').removeClass('imgmax');
                $('#istop').removeClass('pnlimgHide')
                $('#istop').addClass('imgmax')

                $('#istop_blink').removeClass('imgmax');
                $('#istop_blink').removeClass('pnlimgHide')
                $('#istop_blink').addClass('pnlimgHide')
            }
            
        }

        function setLCDTitle(strText)
        {
            $('#spanLCDTitle').text(strText);
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

        setTimeout('blink()', 0);
        setTimeout('blink()', 1000);
        setTimeout('blink()', 2000);
        setTimeout('blink()', 3000);
        setTimeout('blink()', 4000);
        //setTimeout(function () {
        //    setLCDTitle(priVehicleName);
        //}, 4000)
        setTimeout('getDataFromServer()', 4000);
        //setTimeout('timeout()', 4000);

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="div_dashboard">
        </div>
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
        <%--<div id="LCDTitle" class="div_LCDTitle">
            <span id="spanLCDTitle"></span>
        </div>--%>
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
