//, cbAutoIncrement, btnIncrement, comboTimeSecondsMultiplier, , btnIncrement_Click
///heatMapStartTime, cboSelectedTruck, heatMapEndTime, 
//cbShowjourney,cbShowHeatmap,cdRadiusToggle,cbOpiacyToggle,cbGradientToggle.valueChecked
//var autoUpdateHeatMap = false;
//var heatMapIsAutoUpdating = false;
//var thisHeatmap;
//checkComboBox <= the new list box 
//selectedItemString <= the ; delimited string of deviceIDs (i.e. demo1;demo2;demo3)

var textSeparator = ";";
var selectedItemString = '';


//MARKER INFOWINDOW

function showInfoWindow(event) {
    

    //var vertices = '<br><br>' + this.getPath();
    var s = '<b>' + this.Name + '</b><br><br>'

    var newURL = 'DevicePropertyDisplay.aspx?DeviceID=' + this.DeviceID;
    contentString = s + '<iframe src=\'' + newURL + '\' marginwidth=\'0\' marginheight=\'0\' frameborder=\'0\' overflow-y=\'scroll\' overflow-x=\'hidden\' style=\'height: 280px; width: 245px\'></iframe>';

    infoWindow.setContent(contentString);
    infoWindow.setPosition(event.latLng);
    infoWindow.open(map);

}


function GenericKeyDown(s, e) {

    e.returnValue = false;e.cancel = true;
}

//#####################     LIST VIEW LOGIC     #####################

function OnListBoxSelectionChanged(listBox, args) {

    if (args.index == 0)
        args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
    UpdateSelectAllItemState();
    UpdateText();
}

function UpdateSelectAllItemState() {
    IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
}

function IsAllSelected() {
    var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
    return checkListBox.GetSelectedItems().length == selectedDataItemCount;
}

function UpdateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(GetSelectedItemsText(selectedItems));
    selectedItemString = GetSelectedItemsValue(selectedItems);
}

function SynchronizeListBoxValues(dropDown, args) {
    checkListBox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts);
    checkListBox.SelectValues(values);
    UpdateSelectAllItemState();
    UpdateText(); // for remove non-existing texts
}

function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        if (items[i].index != 0)
            texts.push(items[i].text);
    return texts.join(textSeparator);
}

function GetValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = checkListBox.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}

function GetSelectedItemsValue(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        if (items[i].index != 0)
            texts.push(items[i].value);
    return texts.join(textSeparator);
}

//###################################################################

function FleetMap_ControlsInitialized() {

    configDateShowPopup();
    autoSelectVehicleByVINNumber();
}

function autoSelectVehicleByVINNumber() {

    var vINNumber = serverSetting_vehicleautoRept_VINNumber;
    var requested = 'True' == serverSetting_vehicleautoRept_Requested;
    var vehicleID = serverSetting_vehicleautoRept_VehicleID;
    var vehicleName = serverSetting_vehicleautoRept_VehicleName;
    var errorMsg = serverSetting_vehicleautoRept_ErrorMsg;
    var deviceID = serverSetting_vehicleautoRept_DeviceID;

    //leave the method if the request was not asked for
    if (!requested) { return false; }

    //if the error message has content, then show this in an alert box
    if (errorMsg != '') {
        alert(errorMsg);
        return false;
    }

    selectVehicleForTodaysActivity(deviceID, vehicleName);

    //showPopupVehicleReport(vehicleName, vehicleID);
}

function selectVehicleForTodaysActivity(deviceID, vehicleName) {

    $('#check-3').click();

    var startDate = new Date();
    var endDate = new Date();
    var testDeviceName = deviceID;

    startDate.setHours(0, 0, 0);

    heatMapStartTime.SetDate(startDate);
    heatMapEndTime.SetDate(endDate);

    checkComboBox.SetValue(deviceID);
    checkComboBox.SetText(vehicleName);

    //this is what is read by the 
    selectedItemString = deviceID;

    //cboSelectedTruck.SetValue(testDeviceName);

    btnHeatMapSearch.DoClick();

    //show the "return to uniqco" button 
    btnUniqco.SetVisible(true);

    //map.setZoom(15);
}

function showPopupVehicleReport(vehicleName, vehicleID) {

    var popupHeader = 'Vehicle Report | ' + vehicleName;

    popupReport.ShowAtPos(posX, 130);
    popupReport.SetHeight(newHeight - 140);
    popupReport.SetHeaderText(popupHeader);
    popupReport.SetContentUrl('/ReportContent.aspx?Report=VehicleReport6&autoFillParams=true&vehicleid=' + vehicleID);

    var posX = $(window).width() - 850;
    var newHeight = $(window).height();

    popupReport.Show();
}

function SetPCVisible(value) {

    var isAuthorized = serverSetting_Fleet_Management_Page_Manage_geofences == 'True';

    if (isAuthorized) {
        var popupControl = GetPopupControl();
        if (value)
            popupControl.Show();
        else
            popupControl.Hide();
    } else {

        alert('Sorry, you are not authorized to manage geo-fences.')

    }
}

function GetPopupControl() {
    return popupGeoCaching;
}

function popupcloseImg_Click() {
    popupGeoCaching.Hide();
    $('#messageTag').text('');
}

function btnViewCurrentMapTime_Click() {
    popupDate.Show();
}

function hidePopupWithDate() {
    popupDate.Hide();
    _popupwithDate_Visible = false;
}

var _popupwithDate_Visible = false;
var _hasBeenShownBefore = false;
_dateEdit_QueryCloseUp__e_cancel = true;

function dateEdit_QueryCloseUp(s, e) {
    e.cancel = _dateEdit_QueryCloseUp__e_cancel;
}

var _hasfiredbefore = false;
var isFirstPageLoad = true;


//this is fired from the FleetMap_ControlsInitialized function 
function configDateShowPopup() {

    if (isFirstPageLoad == true) {

        isFirstPageLoad = false;

        dateShow.ShowDropDownArea = function (isRaiseEvent) {

            dateShow.GetClock().SetDate(this.date);
            //dateShow.GetCalendar().SetValue(this.date);
            dateShow.SetDate(this.date);

            if (!_hasfiredbefore) {

                this.CalendarShowing.FireEvent(this);
                ASPxClientDropDownEditBase.prototype.ShowDropDownArea.call(this, isRaiseEvent);
                _hasfiredbefore = true;
            }
            $("#popupDate_ASPxPanel1_ASPxDateEdit1_DDD_PW-1").css({ top: '32px' });
            $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1_ASPxDateEdit2_DDD_PW-1').css({ top: '32px', left: '5px' });
        }

        registerHideShowAnchor();
    }
}

function dateShow_ShowDropDownArea(isRaiseEvent) {
    //alert('!');

}

function showPopupWithDate(d) {

    //$(dateShow).unbind("QueryCloseUp");
    _dateEdit_QueryCloseUp__e_cancel = true;
    dateShow.SetValue(d);

    if (!_popupwithDate_Visible) {


        if (!_hasBeenShownBefore) {
            dateShow.ShowDropDown();
            $("#popupDate_ASPxPanel1_ASPxDateEdit1_DDD_PW-1").css({ top: '48px' });

            _hasBeenShownBefore = true;
            dateShow.ShowDropDown();
        }

        popupDate.Show();
        _popupwithDate_Visible = true;
    }

    dateShow.ShowDropDownArea();
    //_dateEdit_QueryCloseUp__e_cancel = false;
    //$(dateShow).bind("QueryCloseUp", function () { dateEdit_QueryCloseUp });
}

function dateEdit_QueryCloseUp(s, e) {
    e.cancel = true;
}

_popupwithDate_Visible = false;

function dateEdit_DropDown(s, e) {

    //try { $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1_ASPxDateEdit2_DDD_PW-1').css({ top: '48px', left: '5px' }); } catch (e) { }
    //try { $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1_ASPxDateEdit2_DDD_PW - 1').css({ top: '48px', left: '5px' }); } catch (e) { }
}

//#######################################   TRUCK MOVEMENTS #######################################

var _isAutoUpdating = true;

//, cbAutoIncrement, btnIncrement, comboTimeSecondsMultiplier, , btnIncrement_Click
///heatMapStartTime, cboSelectedTruck, heatMapEndTime, 
//cbShowjourney,cbShowHeatmap,cdRadiusToggle,cbOpiacyToggle,cbGradientToggle.valueChecked
//var autoUpdateHeatMap = false;
//var heatMapIsAutoUpdating = false;
//var thisHeatmap;
//checkComboBox <= the new list box

var _viewMachineAtTime = false;
var _GetLatLongObjs = false;

function clientServerroundRobin() {

    var activityViewer_SelectedVehicle = selectedItemString;//cboSelectedTruck.GetValueString();
    var activityViewerStartTime = String(heatMapStartTime.GetValue());
    var activityViewerEndTime = String(heatMapEndTime.GetValue());
    var activityViewerAutoUpdateToNow = cbHeatmapAutoUpdate.GetChecked();

    var activityViewer_autoincrement_selected = cbAutoIncrement.GetChecked();
    var activityViewer_autoIncrement_seconds = spinTimeSeconds.GetValue();
    var activityViewer_autoIncrement_secondsMultiplier = comboTimeSecondsMultiplier.GetValue();
    //"calculated"
    var activityViewer_autoIncrement_secondsTotal = activityViewer_autoIncrement_seconds * activityViewer_autoIncrement_secondsMultiplier;

    var autoUpdateCurrentActivity = autoUpdate.GetChecked();

    var viewMachineAtTime = _viewMachineAtTime;

    var getAllTrucksAtSpecificTime = _GetLatLongObjs;
    var getAllTrucksAtSpecificTimeDate = String(dateViewThisDateTime.GetValue());

    var param = {};
    param.activityViewer_SelectedVehicle = activityViewer_SelectedVehicle;
    param.activityViewerStartTime = activityViewerStartTime;
    param.activityViewerEndTime = activityViewerEndTime;
    param.activityViewerAutoUpdateToNow = activityViewerAutoUpdateToNow;
    param.activityViewer_autoincrement_selected = activityViewer_autoincrement_selected;
    param.activityViewer_autoIncrement_seconds = activityViewer_autoIncrement_seconds;
    param.activityViewer_autoIncrement_secondsMultiplier = activityViewer_autoIncrement_secondsMultiplier;
    param.activityViewer_autoIncrement_secondsTotal = activityViewer_autoIncrement_secondsTotal;
    param.autoUpdateCurrentActivity = autoUpdateCurrentActivity;
    param.viewMachineAtTime = viewMachineAtTime;
    param.getAllTrucksAtSpecificTime = getAllTrucksAtSpecificTime;
    param.getAllTrucksAtSpecificTimeDate = getAllTrucksAtSpecificTimeDate;

    ajaxMethod("RoundRobinService.svc/" + 'ClientServerRoundRobin',
          param, clientServerroundRobin_SuccessCallback, clientServerroundRobin_ErrorCallback, clientServerroundRobin_FinallyCallback);

    msgcount += 1;

}

var lastRoundRobinReceived = new Date();
var msgcount = 0;


function clientServerroundRobin_FinallyCallback() {

    lastRoundRobinReceived = new Date();
    setTimeout(function () { clientServerroundRobin(); }, 3000);
}

function roundRobinSanityCheck() {

    var currentDate = new Date();

    var difference = (currentDate - lastRoundRobinReceived) / 1000;

    if (difference > 20) {
        //clientServerroundRobin();
        lastRoundRobinReceived = new Date();
    }

    //setTimeout(function () { roundRobinSanityCheck(); }, 1000);
}


var TrucksReturnedFromServer = {};

function clientServerroundRobin_SuccessCallback(result) {


    //activityViewerAutoUpdateToNow
    //activityViewer_autoincrement_selected
    //autoUpdateCurrentActivity 
    //_viewMachineAtTime

    try {

        var updatetime = false;

        if (result.d._autoUpdateCurrentActivity == true) {
            upsertMapTrucks(result);
            updatetime = true;
        }

        if (result.d._getAllTrucksAtSpecificTime == true) {
            upsertMapTrucks(result);
            updatetime = true;
        }

        if (result.d._activityViewer_autoincrement_selected == true) {
            heatMap_successCallBack(result);
            UpdateHeatMapEndTime(false);
            updatetime = true;
        }

        if (result.d._activityViewerAutoUpdateToNow == true) {
            heatMap_successCallBack(result);
            UpdateHeatMapEndTime(true);
            updatetime = true;
        }

        if (result.d._viewMachineAtTime == true) {
            //the button is pressed
            heatMap_successCallBack(result);
            _viewMachineAtTime = false;
            updatetime = true;

            //show the journey and the heatmap options as default
            cbShowHeatmap.SetChecked(true);
            cbShowjourney.SetChecked(true);
            //normally, then below would trigger if the user clicks the 
            //checkbox, however changing the "checked" value, does not cause this event to fire.
            showJourneyTogle();
            toggleHeatmap();
        }

       
        //if we have not had to do anything, do not update the clock
        if (updatetime == true) {
            showPopupWithDate(result.d._queryDate);
        }

    } catch (e) {

        //alert(e);
    }
    //clientServerRoundRobin is called again from the finally callback
}


function dateedit_Click(s, e) {


    cbHeatmapAutoUpdate.SetChecked(false);
    cbAutoIncrement.SetChecked(false);
    _viewMachineAtTime = false;
    autoUpdate.SetChecked(false);

    _GetLatLongObjs = true;
}

//function GetLatLongObjs(time) {

//    var param = {};
//    param.time = time;

//    ajaxMethod("DefaultService.svc/" + 'GetListOfItemsWithPositions', param, successCallBack, errorCallback, finallyCallback);

//}

function clientServerroundRobin_ErrorCallback(result) {

    //alert('exception caused' + JSON.stringify(result));
    //clientServerRoundRobin is called again from the finally callback
}

function autoUpdate_CheckedChanged(s, e) {

    if (autoUpdate.GetChecked() == true) {

        cbHeatmapAutoUpdate.SetChecked(false);
        cbAutoIncrement.SetChecked(false);
        _viewMachineAtTime = false;
        _GetLatLongObjs = false;

        cbShowHeatmap.SetChecked(false);
        cbShowjourney.SetChecked(false);

        showJourneyTogle();
        toggleHeatmap();

    }

    //hidePopupWithDate();
}

function cbAutoIncrement_CheckedChanged(s, e) {

    if (cbAutoIncrement.GetChecked() == true) {

        autoUpdate.SetChecked(false);
        cbHeatmapAutoUpdate.SetChecked(false);
        _viewMachineAtTime = false;
        _GetLatLongObjs = false;
    }
    //viewMachineAtTime();
}


function cbHeatmapAutoUpdate_CheckChanged(s, e) {

    if (cbHeatmapAutoUpdate.GetChecked() == true) {
        autoUpdate.SetChecked(false);
        //cbHeatmapAutoUpdate.SetChecked(false);
        cbAutoIncrement.SetChecked(false);
        _viewMachineAtTime = false;
        _GetLatLongObjs = false;
    }
}

function cbSnapToRoad_CheckChanged(s, e) {

    //Snap to road ( as in do we use the google "road hug" option).
    drawPathTestFromGlobalObject();
}


function btnHeatMapSearch_Click(s, e) {

    autoUpdate.SetChecked(false);
    cbHeatmapAutoUpdate.SetChecked(false);
    cbAutoIncrement.SetChecked(false);
    _viewMachineAtTime = true;
    _GetLatLongObjs = false;
}


function btnGo_Click() {

    //marker.setPosition(latlng);
    // GetLatLongObjs(); 

    var d = new Date('20 jan 2016 06:00:01');

    function iteration() {

        d = new Date(d.getTime() + 60000);

        GetLatLongObjs(d.toString());
        setTimeout(iteration, 1000)
    }

    iteration();
}

function contentdiv_Click() {

    alert($('#contentdiv').text());

}

function logthis(latlng) {

    var content = latlng.lat() + '|' + latlng.lng() + '\n';
    $('#contentdiv').html($('#contentdiv').html() + content);

}



function successCallBack(result) {
    //alert(JSON.stringify(result));
    upsertMapTrucks(result);
}

function errorCallback(result) {
    alert('ERROR\n' + JSON.stringify(result));
}

function finallyCallback(result) {
    //alert('finally');
}

var iconBase = 'content/images/markers/';

var icon_truck = iconBase + 'truck_48.png';
var icon_lorry = iconBase + 'lorry_48.png';
var icon_home = iconBase + 'home-icon.png';
var icon_nothing = iconBase + 'nothing.png';

var markers = [];


// Adds a marker to the map and push to the array.
function getLabelInvisibleMarker(location, text) {

    var marker = new MarkerWithLabel({
        position: location,
        icon: icon_nothing,
        labelContent: text,
        labelAnchor: new google.maps.Point(22, 0),
        labelClass: "labelsPloygon", // the CSS class for the label
        labelStyle: { opacity: 0.75 },
        map: map
    });

    return marker;//
}


// Adds a marker to the map and push to the array.
function addMarker(location, lblContent, markerID) {

    var marker = new MarkerWithLabel({
        position: location,
        icon: icon_truck,
        labelContent: lblContent,
        labelAnchor: new google.maps.Point(22, 0),
        labelClass: "labels", // the CSS class for the label
        labelStyle: { opacity: 0.75 },
        map: map
    });

    marker.DeviceID = markerID;
    marker.Name = lblContent.replace('\n', ' ');

    marker.addListener('click', showInfoWindow);//DeviceID

    marker.ID = markerID;
    markers.push(marker);
}


var alreadyRan = false;

function upsertMapTrucks(result) {

    var index;
    var trucks = result.d._Trucks;
    var truckArr = [];

    //console.log(result);

    for (index = 0; index < trucks.length; ++index) {

        //console.log(trucks[index].Driver);

        var truckName = trucks[index].TruckName;
        var truckDriver = trucks[index].Driver;
        var itemExistsInArr = false;
        var i;
        var trucksMarker = null;
        var labelContent = "{0}</br>({1})".format(truckDriver, truckName);

        //Find the truck if it exists in the markers array
        markers.forEach(function (entry) {
            if (entry.ID == trucks[index].ID) {
                trucksMarker = entry;
            }
        });

        //alter the lat and long for the truck
        var markerPosn = new google.maps.LatLng(trucks[index].lat, trucks[index].lng);

        if (trucksMarker != null) {
            trucksMarker.labelContent = labelContent;
            trucksMarker.setPosition(markerPosn);
            trucksMarker.label.setStyles();
            trucksMarker.label.draw();
            trucksMarker.setShape();

            //update the marker label here to reflect the new driver


        } else {
            addMarker(markerPosn, labelContent, trucks[index].ID);
        }
    }
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}


// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}


var map;


window.mobileAndTabletcheck = function () {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino|android|ipad|playbook|silk/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
}

window.onload = window.onresize = function () {

    //var left = document.getElementById('googleMap');

    $('#googleMap').height($('#ctl00_ctl00_MainPane_Content_ContentLeft_LeftPane').height() + 14);

    if (window.mobileAndTabletcheck()) { $('#viewInRealTimeCheckbox').css({ top: '70px' }); }

    //var window_height = window.innerHeight;
    ////console.log(left.offsetHeight);
    ////console.log(window_height);
    //if (left.offsetHeight < window_height) {
    //    left.style.height = window_height - 70 + "px";

    //} else { }


}





function addPPJSMarker() {

    //serverSetting_

    var lat = parseFloat(serverSetting_Business_Lattitude);
    var lng = parseFloat(serverSetting_Business_Longitude);
    var markerPosn = new google.maps.LatLng(lat, lng);

    var companyName = serverSetting_CompanyName;

    var marker = new MarkerWithLabel({
        position: markerPosn,
        icon: icon_home,
        labelContent: companyName,
        labelAnchor: new google.maps.Point(22, 0),
        labelClass: "labels", // the CSS class for the label
        labelStyle: { opacity: 0.75 },
        map: map
    });

    marker.setMap(map);
}



function initialize() {


    var lat = parseFloat(serverSetting_Business_Lattitude);
    var lng = parseFloat(serverSetting_Business_Longitude);


    //center: new google.maps.LatLng(-32.052925, 115.930010),

    var mapProp = {
        center: new google.maps.LatLng(lat, lng),
        zoom: 11,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    addPPJSMarker();


    google.maps.event.addListener(map, 'click', function (event) {
        logthis(event.latLng);
    });


    initAutocomplete();

    //this will "keep alive" the connection to the server if the browser is left open.
    //also performs the "auto-update" logic.
    //autoUpdateAjaxStart();    

    instansiateDefaultService();


}





function getInfo() {

    //alert($('#contentdiv').text());
    GetLatLongObjs();
}

var retobj;

function updateMarkers(result) {

    retobj = result;

    //var marker = new MarkerWithLabel({
    //    position: markerPosn,
    //    icon: icon_home,
    //    labelContent: "PPJS",
    //    labelAnchor: new google.maps.Point(22, 0),
    //    labelClass: "labels", // the CSS class for the label
    //    labelStyle: { opacity: 0.75 },
    //    map: map
    //});
}

// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.

function initAutocomplete() {

    //var map = new google.maps.Map(document.getElementById('map'), {
    //    center: { lat: -33.8688, lng: 151.2195 },
    //    zoom: 13,
    //    mapTypeId: google.maps.MapTypeId.ROADMAP
    //});

    // Create the search box and link it to the UI element.
    var search_input_box = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(search_input_box);
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();

        places.forEach(function (place) {
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
        map.setZoom(15);
    });

    var pac_input = document.getElementById('pac-input');

    (function pacSelectFirst(input) {
        // store the original event binding function
        var _addEventListener = (input.addEventListener) ? input.addEventListener : input.attachEvent;

        function addEventListenerWrapper(type, listener) {
            // Simulate a 'down arrow' keypress on hitting 'return' when no pac suggestion is selected,
            // and then trigger the original listener.
            if (type == "keydown") {
                var orig_listener = listener;
                listener = function (event) {
                    var suggestion_selected = $(".pac-item-selected").length > 0;
                    if (event.which == 13 && !suggestion_selected) {
                        var simulated_downarrow = $.Event("keydown", {
                            keyCode: 40,
                            which: 40
                        });

                        orig_listener.apply(input, [simulated_downarrow]);
                    }

                    if (event.which == 13) {
                        event.returnValue = false;
                        event.cancel = true;
                        //return false;
                    }

                    orig_listener.apply(input, [event]);
                };
            }

            _addEventListener.apply(input, [type, listener]);
        }

        input.addEventListener = addEventListenerWrapper;
        input.attachEvent = addEventListenerWrapper;

        var autocomplete = new google.maps.places.Autocomplete(input);

    })(pac_input);


}

//########################################################################################################################
//########################                                                              ##################################
//########################                                                              ##################################
//########################                 HEAT MAP SECTION                             ##################################
//########################                                                              ##################################
//########################                                                              ##################################
//########################################################################################################################

var autoUpdateHeatMap = false;
var heatMapIsAutoUpdating = false;
var thisHeatmap;

// cbAutoIncrement, btnIncrement, comboTimeSecondsMultiplier,  btnIncrement_Click
///heatMapStartTime, cboSelectedTruck, heatMapEndTime, 
//cbShowjourney,cbShowHeatmap,cdRadiusToggle,cbOpiacyToggle,cbGradientToggle.valueChecked
//var autoUpdateHeatMap = false;
//var heatMapIsAutoUpdating = false;
//var thisHeatmap;





thisHeatmap = new google.maps.visualization.HeatmapLayer({
    data: getPoints(),
    map: map
});

function UpdateHeatMapEndTime(setToNow) {

    var secondCount = spinTimeSeconds.GetValue();
    var secondMultiplier = parseInt(comboTimeSecondsMultiplier.GetValue());
    var secondsToAdd = secondCount * secondMultiplier;
    var newEndDate = new Date(heatMapEndTime.GetValue().setSeconds(secondsToAdd));

    if (setToNow) { newEndDate = new Date(); }

    heatMapEndTime.SetValue(newEndDate);
    showPopupWithDate(newEndDate);

}


function btnHeatMapSearch_Click_old() {

    var selectedTruck = cboSelectedTruck.GetValueString();
    var startTime = String(heatMapStartTime.date);
    var endTime = String(heatMapEndTime.date);
    //alert(selectedTruck + ', ' + startTime + ', ' + endTime)

    if (!heatMapIsAutoUpdating) {
        HeatMap(selectedTruck, startTime, endTime, true);
    } else {

        alert('You cannot view an activity whilst auto-update is ticked!')
    }

    //toggleHeatmap();
    //heatMapStartTime, cboSelectedTruck, heatMapEndTime, 
}





//function HeatMap(selectedTruck, startTime, endTime, forceUpdate) {

//    if (heatMapIsAutoUpdating || forceUpdate) {

//        var param = {};
//        param.SelectedTruck = selectedTruck;
//        param.StartTime = startTime;
//        param.EndTime = endTime;

//        ajaxMethod("DefaultService.svc/" + 'GetHeatMapCoords',
//                    param, heatMap_successCallBack, heatMapErrorCallback, heatMapfinallyCallback);

//    } else {
//    }

//}


var pointLLs = [];

var HeatMapTrucksReturnedFromServer = {};

function heatMap_successCallBack(result) {

    //alert('Success!: \n' + JSON.stringify(result));

    if (result.d._WasError == true) {
        alert(result.d.ErrorMessage);
        return null;
    }

    HeatMapTrucksReturnedFromServer = result.d._Trucks;

    var ll = result.d._LatLongs;
    var googlelatlngs = [];

    if (thisHeatmap != null) { thisHeatmap.setMap(null) };
    thisHeatmap = null;

    pointLLs = [];

    //heatmap.data.length = 0;    

    var googlelatlngs = [];

    HeatMapTrucksReturnedFromServer.forEach(function (item, index) {

        if (item.ShowJourneyOnMap) {

            item.JourneyLatLngs.forEach(function (entry, index) {

                googlelatlngs.push(new google.maps.LatLng(entry.Lat, entry.Lng));
                // console.debug(entry.Lat);
            })
        }
    });


    thisHeatmap = new google.maps.visualization.HeatmapLayer({
        data: googlelatlngs,
        map: map
    });

    applyActionViewerOptions();

    //from fleetmap.js
    upsertMapTrucks(result);

    //centre on the truck were concentrating on 
    var centreOnVan = cbFollowTruck.GetChecked();
    //if (centreOnVan) centreInOnTruck(result.d._DeviceID);
}

function centreInOnTruck(ID) {

    var foundMarker = {};

    //Find the truck if it exists in the markers array
    markers.forEach(function (entry) {
        if (entry.ID == ID) {
            foundMarker = entry;
        }
    })

    try {
        var latlng = foundMarker.getPosition();
        map.setCenter(latlng);
    }
    catch (e) {
        //TODO: swallows exception here, should bubble up or show user
    }
}

function heatMapErrorCallback(result) {
    alert('ERROR\n' + JSON.stringify(result));
}

function heatMapfinallyCallback(result) {

    //alert('finally');
}

function applyActionViewerOptions() {

    toggleHeatmap();
    drawPathTestFromGlobalObject();
    changeRadius();
    changeOpacity();

}

//cbShowjourney
//cbShowHeatmap
//cdRadiusToggle
//cbOpiacyToggle
//cbGradientToggle.valueChecked

function toggleHeatmap() {

    if (thisHeatmap == null) return null;

    if (cbShowHeatmap.GetChecked() == true) {
        thisHeatmap.setMap(map);
    } else {
        thisHeatmap.setMap(null);
    }

    //thisHeatmap.setMap(cbShowHeatmap.valueChecked == false ? null : map);
}

function changeGradient() {

    if (thisHeatmap == null) return null;

    var gradient = [
     'rgba(0, 255, 255, 0)',
     'rgba(0, 255, 255, 1)',
     'rgba(0, 191, 255, 1)',
     'rgba(0, 127, 255, 1)',
     'rgba(0, 63, 255, 1)',
     'rgba(0, 0, 255, 1)',
     'rgba(0, 0, 223, 1)',
     'rgba(0, 0, 191, 1)',
     'rgba(0, 0, 159, 1)',
     'rgba(0, 0, 127, 1)',
     'rgba(63, 0, 91, 1)',
     'rgba(127, 0, 63, 1)',
     'rgba(191, 0, 31, 1)',
     'rgba(255, 0, 0, 1)'
    ]
    thisHeatmap.set('gradient', !cbGradientToggle.GetChecked() ? null : gradient);
}



function changeRadius() {

    if (thisHeatmap == null) return null;

    thisHeatmap.set('radius', !cdRadiusToggle.GetChecked() ? null : 20);
}

function changeOpacity() {

    if (thisHeatmap == null) return null;

    thisHeatmap.set('opacity', !cbOpiacyToggle.GetChecked() ? null : 1);
}



// Heatmap data (test): 500 Points
function getPoints() {
    return [
      new google.maps.LatLng(37.782551, -122.445368),
      new google.maps.LatLng(37.782745, -122.444586),
      new google.maps.LatLng(37.782842, -122.443688),
      new google.maps.LatLng(37.782919, -122.442815)
    ];
}


//########################################################################################################################
//########################                                                              ##################################
//########################                                                              ##################################
//########################                 initialization                               ##################################
//########################                                                              ##################################
//########################                                                              ##################################
//########################################################################################################################


google.maps.event.addDomListener(window, 'load', initialize);


//$(document).ready(function () { initialize(); })

