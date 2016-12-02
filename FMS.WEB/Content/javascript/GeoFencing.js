var metros;
var map;
var currentPolygon;
var ALLGeoFencesArr = [];
var polygonLabelMarkers = [];
var GeoFenceIDToShowOnrefresh = null;
var GeoFenceIDToShowOnRefreshIsCircle = false;
var GeoFenceIDToShowOnRefreshMakeEditable = false;

//************//************    EDIT Screen //************//************


//cbGeoEditGeoFences,memoGeoEditDescription,colorGeoEditSelct,colorGeoEditSelct_ColorChanged
//btnEditDelete, btnEditDelete_Click, cbGeoEditGeoFences_SelectedIndexChanges(s,e);
//getGeoFenceFromID

//update the description field.

function combogeoFenceTypeSelection_SelectedIndexChanged(s, e) {

    //options are : Circle / Polygon
    var selectedValueType = combogeoFenceTypeSelection.GetValue();

    ShowEditableGeoFence();

}

function btnEditSave_Click() {

    var selectedItem = getSelectedEditItem();

    selectedItem.colour = colorGeoEditSelct.GetValue();
    selectedItem.desc = memoGeoEditDescription.GetText();

    editGeoFence(selectedItem);
}

function editGeoFence(selectedItem) {

    var param = {};

    param.colour = selectedItem.colour;
    param.desc = selectedItem.desc;
    param.id = selectedItem.id;
    param.name = selectedItem.name;
    param.latlngs = selectedItem.latlngs;


    param.latlngs = selectedItem.latlngs;
    param.latlngs = selectedItem.latlngs;
    param.circleCentre = selectedItem.circleCentre;
    param.circleRadius = selectedItem.circleRadius;
    param.isCircle = selectedItem.isCircle;

    geo_ajaxMethod("DefaultService.svc/" + 'EditGeoFence',
            param, editGeoFence_successCallback, geo_errorCallback, finallyCallback);

}

function geo_errorCallback(result) {

    var x = result;
}

function geo_finallyCallback(result) { }

function editGeoFence_successCallback(result) {

    //update the summary grid view
    dgvGeoFenceSummary.Refresh();
    //update the summary grid view
    //dgvGeoFenceSummary.Refresh();

    alert(result.d._MessageFromServer);
}

function btnEditDelete_Click() {

    var selectedItem = getSelectedEditItem();
    var message = "Are you sure?\nThis will delete {0} permanently.".format(selectedItem.name);
    var wasSure = window.confirm(message);

    if (wasSure) { deleteGeoFence(selectedItem.id); }

}

function deleteGeoFence(geoFID) {

    var param = {};
    param.ApplicationGeoFenceID = geoFID;
    //param.time = time;

    geo_ajaxMethod("DefaultService.svc/" + 'DeleteGeoFence',
            param, deleteGeoFence_successCallback, geo_errorCallback, finallyCallback);

}

function deleteGeoFence_successCallback(result) {

    alert(result.d._MessageFromServer);
    cbGeoEditGeoFences.PerformCallback();
    //update the description field.
    memoGeoEditDescription.SetText('');
    //update the colour field    
    colorGeoEditSelct.SetValue('');

    //update all the polygonson the screen
    cbViewGeoFences.SetChecked(false); cbViewGeoFences_checkChanged(null);
    cbViewGeoFences.SetChecked(false); cbViewGeoFences_checkChanged(null);
    cbViewGeoFences.SetChecked(true); cbViewGeoFences_checkChanged(null);

    //update the summary grid view
    dgvGeoFenceSummary.Refresh();

}

function colorGeoEditSelct_ColorChanged() {

    var selectedItem = getSelectedEditItem();

    //GEt Polygon related to the geoFence
    var poly = getGeoFenceFromID(selectedItem.id);

    var selectedColour = colorGeoEditSelct.GetColor();

    poly.setOptions({ strokeColor: selectedColour, fillColor: selectedColour });
}

function getSelectedEditItem() {

    var retObj = {};

    var selItem = cbGeoEditGeoFences.GetSelectedItem().texts;

    retObj.name = selItem[0];
    retObj.desc = selItem[1];
    retObj.id = selItem[2];
    retObj.colour = selItem[3];

    latlngs = null;
    circleCentre = null;
    circleRadius = null;    

    try {

        var selectedPolygonOrCircle = getGeoFenceFromID(retObj.id);

        retObj.isCircle = selectedPolygonOrCircle.isCircle;

        //grab different things depending on if we are dealing with a polygon or a circle
        if (retObj.isCircle) {

            circleCentre = selectedPolygonOrCircle.center.lat() + '|' + selectedPolygonOrCircle.center.lng();
            circleRadius = selectedPolygonOrCircle.radius;

        } else {

            latlngs = getPolygonArrsStr(selectedPolygonOrCircle);
        }  
        

    } catch (e) { }

    retObj.latlngs = latlngs;
    retObj.latlngs = latlngs;
    retObj.circleCentre = circleCentre;
    retObj.circleRadius = circleRadius;

    return retObj;
}

function cbGeoEditGeoFences_SelectedIndexChanges(s, e) {
    //alert(JSON.stringify(e));

    //get the item from the drop down list
    var selectedItem = getSelectedEditItem();

    var debugStr = "name:{0}\ndesc:{1}\nid:{2}\ncolour,{3}".format(selectedItem.name, selectedItem.desc, selectedItem.id, selectedItem.colour)

    //update the description field.
    memoGeoEditDescription.SetText(selectedItem.desc);
    //update the colour field    
    colorGeoEditSelct.SetValue(selectedItem.colour);
    //show the item on the map & zoom to that item on the map
    GeoFenceIDToShowOnrefresh = selectedItem.id;
    GeoFenceIDToShowOnRefreshMakeEditable = true;

    cbViewGeoFences.SetChecked(false); cbViewGeoFences_checkChanged(null);
    cbViewGeoFences.SetChecked(true); cbViewGeoFences_checkChanged(null);

    //make the item editable
}

function cbGeoEditGeoFences_Refresh() {
    cbGeoEditGeoFences.PerformCallback();
}

//***************************************************************************************************

function dgvGeoFenceSummary_CustomButtonClick(s, e) {

    if (e.buttonID == 'btnView') {
        //alert('Row index: ' + e.visibleIndex);
        var selectedGeoFenceID = dgvGeoFenceSummary.GetRowKey(e.visibleIndex);
        //var selectedPolygon;
        setBoundsToGeoFence(selectedGeoFenceID);
    }
}

function getGeoFenceFromID(geofenceid) {

    var retObj = null;

    ALLGeoFencesArr.forEach(function (item, index) {

        if (item.ApplicationGeoFenceID == geofenceid) {
            retObj = item;
        }
    });

    return retObj;
}

function setBoundsToGeoFence(geoFenceID) {

    ALLGeoFencesArr.forEach(function (item, index) {

        if (item.ApplicationGeoFenceID == geoFenceID) {

            if (GeoFenceIDToShowOnRefreshMakeEditable == true) {
                item.draggable = true;
                item.editable = true;

                item.setOptions({ draggable: true, editable: true });
            }

            GeoFenceIDToShowOnRefreshMakeEditable = false;

            if (item.isCircle) {

                map.fitBounds(item.getBounds());

            } else {

                map.fitBounds(getPolygonBounds(item));
            }
        }
    });
}

function getPolygonCenter(polygn) {

    var bounds = new google.maps.LatLngBounds();
    bounds = getPolygonBounds(polygn);
    return bounds.getCenter();
    //addLabelNoMarker(bounds.getCenter(), 'hi')     
}

function getPolygonBounds(polygn) {

    var bounds = new google.maps.LatLngBounds();
    var i;
    var retStr = '';
    var vertices = polygn.getPath();

    for (var i = 0; i < vertices.getLength() ; i++) {

        var xy = vertices.getAt(i);
        if (retStr != '') { retStr += '|'; }
        retStr += String(xy.lat()) + ',' + String(xy.lng());
        bounds.extend(xy);
    }

    return bounds;
    //addLabelNoMarker(bounds.getCenter(), 'hi')     
}

function cbViewGeoFences_checkChanged(event_args) {


    cbViewGeoFences.SetEnabled(false);
    cbViewGeoFencesWithBooking.SetEnabled(false);

    var waschecked = cbViewGeoFences.GetChecked();

    if (waschecked) {
        getGeoFencesFromDBAndShow();
    } else {
        destroyAllGeoFencesAndLabels();
        cbViewGeoFences.SetEnabled(true);
        cbViewGeoFencesWithBooking.SetEnabled(true);
    }
}

function getGeoFencesFromDBAndShow_finally() {

    cbViewGeoFences.SetEnabled(true);
    cbViewGeoFencesWithBooking.SetEnabled(true);
    return false;
}

function getGeoFencesFromDBAndShow() {

    var param = {};
    //var bounds = map.getBounds()

    //param.NorthWestLat = bounds.H.j
    //param.NorthWestLong = bounds.j.j

    //param.SoutEasttLat = bounds.H.H;
    //param.SoutEasttLong = bounds.j.H;

    geo_ajaxMethod("DefaultService.svc/" + 'GetAllGeoFences',
            param, getGeoFencesFromDBAndShow_successCallback, geo_errorCallback, getGeoFencesFromDBAndShow_finally);
}

function instansiateDefaultService() {

    var param = {};


    geo_ajaxMethod("DefaultService.svc/" + 'DefalutserverInstantiate',
            param, DefalutserverInstantiate_anycallback, DefalutserverInstantiate_anycallback, null);

}

function DefalutserverInstantiate_anycallback(result) {

    clientServerroundRobin();

    roundRobinSanityCheck();
    //alert(JSON.stringify(result));
}

function getGeoFencesFromDBAndShow_successCallback(result) {

    destroyAllGeoFencesAndLabels();

    result.d._GeoFences.forEach(function (item, index) {
        if (!(cbViewGeoFencesWithBooking.GetChecked())) {
            if (item.isBooking) return true;
        }
        var colour = item.Colour;
        var name = item.Name;

        loopCoords = [];

        item.ApplicationGeoFenceSides.forEach(function (itm, indx) {

            var goolLatLng = new google.maps.LatLng(itm.Latitude, itm.Longitude);
            loopCoords.push(goolLatLng);
        });

        //no longer used? (below)
        var shouldBeEditable = (item.ApplicationGeoFenceID == GeoFenceIDToShowOnRefreshMakeEditable);

        loopPolygon = new google.maps.Polygon(
        {
            paths: loopCoords,
            strokeColor: item.Colour,
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: item.Colour,
            fillOpacity: 0.3
            //draggable: shouldBeEditable,
            //editable: shouldBeEditable
        });

        var circleCentre = new google.maps.LatLng(item.CircleCentreLat, item.CircleCentreLong);

        loopCircle = new google.maps.Circle(
        {
            strokeColor: item.Colour,
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: item.Colour,
            fillOpacity: 0.35,
            //editable: true,
            //draggable: true,
            center: circleCentre,
            radius: item.CircleRadiusMetres
        });

        loopPolygon["ApplicationGeoFenceID"] = item.ApplicationGeoFenceID;
        loopCircle["ApplicationGeoFenceID"] = item.ApplicationGeoFenceID;

        var iscircle = item.IsCircular;

        loopPolygon["isCircle"] = iscircle;
        loopCircle["isCircle"] = iscircle;

        //alert(JSON.stringify(loopPolygon));
        if (iscircle) {

            ALLGeoFencesArr.push(loopCircle);

        } else {

            ALLGeoFencesArr.push(loopPolygon);
        }

        loopPolygon.ApplicationGeoFenceID = item.ApplicationGeoFenceID;
        loopCircle.ApplicationGeoFenceID = item.ApplicationGeoFenceID;

        loopPolygon.Name = item.Name;
        loopCircle.Name = item.Name;

        loopPolygon.addListener('click', showArrays);
        loopCircle.addListener('click', showArrays);

        //get polygons center
        var polygonLocation = getPolygonCenter(loopPolygon);

        if (iscircle) { polygonLocation = circleCentre }

        var polygonName = item.Name;

        var newLabel = getLabelInvisibleMarker(polygonLocation, polygonName);

        polygonLabelMarkers.push(newLabel);
    })

    showGeoFences();
    showGeoFenceLabels();

    //update the summary grid view
    //dgvGeoFenceSummary.Refresh();
}

function cbViewGeoFenceLabels_checkChanged() {
    showGeoFenceLabels();
}

function showGeoFenceLabels() {

    var show = cbViewGeoFenceLabels.GetChecked();

    //avoids crashing if the object is null 
    if (polygonLabelMarkers == null) { return null; }

    polygonLabelMarkers.forEach(function (item, index) {
        item.setMap(show == true ? map : null);
    });
}

function showGeoFences() {

    ALLGeoFencesArr.forEach(function (item, index) {
        item.setMap(map);
    })

    if (GeoFenceIDToShowOnrefresh != null) {
        setBoundsToGeoFence(GeoFenceIDToShowOnrefresh);
        GeoFenceIDToShowOnrefresh = null;
    }

}

function destroyAllGeoFencesAndLabels() {

    ALLGeoFencesArr.forEach(function (item, index) {
        item.setMap(null);
    });

    polygonLabelMarkers.forEach(function (item, index) {
        item.setMap(null);
    });

    //set to nothing and then reinitialise
    ALLGeoFencesArr = null; ALLGeoFencesArr = [];
    polygonLabelMarkers = null; polygonLabelMarkers = [];
}

//take a polygon and grab all the vertices(sides). Return them in the format (Str) lat,lng|lat,lng|lat,lng
function getPolygonArrsStr(plyGon) {

    var vertices = plyGon.getPath();

    var retStr = '';

    // Iterate over the vertices.
    for (var i = 0; i < vertices.getLength() ; i++) {
        var xy = vertices.getAt(i);
        if (retStr != '') { retStr += '|'; }
        retStr += String(xy.lat()) + ',' + String(xy.lng());
    }

    return retStr;
}

function btnPlaceOnMap_Click() {

    //remove polygon if it it already on the map and has not been saved yet
    if (currentPolygon != null) { currentPolygon.setMap(null); }
    createPolygonOnClick();
}

function btnClearFromMap_Click() {
    //remove polygon if it it already on the map and has not been saved yet
    if (currentPolygon != null) { currentPolygon.setMap(null); }
    //same with the circle
    if (currentCircle != null) { currentCircle.setMap(null); }
}

function btnSave_Click() {
    //alert('btnSave_Click');

    //IF there is on polygon defined, then do nothing, we should not have to determine if there is a currentcircle object 
    //as there should not be one if there is no currentPolygon
    if (currentPolygon == null) { alert('You have not yet created a geofence to save!'); return; }

    var name = geofenceName.GetText();
    var desc = geoFenceDescription.GetText();
    var colour = goefenceColour.GetColor();

    var circleLatLngStr = currentCircle.getCenter().lat() + '|' + currentCircle.getCenter().lng();
    var circleRadius = currentCircle.getRadius();

    var circleOrPolygon = combogeoFenceTypeSelection.GetValue();

    var latlngs = getPolygonArrsStr(currentPolygon);

    saveNewPolygonToDB(latlngs, name, desc, colour, circleLatLngStr, circleRadius, circleOrPolygon)

}

//*************************************       CALLBACK TO SERVER *************************************       

function saveNewPolygonToDB(latlongs, name, description, colour,
                            circleLatLngStr, circleRadius, circleOrPolygon) {

    var param = {};
    //LatLong As String, Name As String, Description As String, Colour As String 

    param.LatLongs = latlongs;
    param.Name = name;
    param.Description = description;
    param.Colour = colour;

    param.CircleLatLngStr = circleLatLngStr;
    param.CircleRadius = circleRadius;
    param.CircleOrPolygon = circleOrPolygon;


    //param.time = time;
    //if (_isAutoUpdating == true) {
    geo_ajaxMethod("DefaultService.svc/" + 'SaveNewPolygonToDB',
                param, saveNewPolygonToDB_successCallBack, geo_errorCallback, finallyCallback);
    //}

}

function saveNewPolygonToDB_successCallBack(result) {


    if (result.d._wasSuccess == true) {

        geofenceName.SetText('');
        geoFenceDescription.SetText('');
        btnClearFromMap_Click();//HACK: remove the EDITABLE polygon from the screen

        GeoFenceIDToShowOnrefresh = result.d._NewGeoFenceID;
        GeoFenceIDToShowOnRefreshIsCircle = result.d._IsCircular;

        var waschecked = cbViewGeoFences.GetChecked();

        cbViewGeoFences.SetChecked(false);
        //for some reason, the above does not fire the checkChanged event
        cbViewGeoFences_checkChanged(null);

        cbViewGeoFences.SetChecked(true);
        cbViewGeoFences_checkChanged(null);


        dgvGeoFenceSummary.Refresh();
        //dgvGeoFenceSummary.PerformCallback();
    }

    alert(result.d._MessageFromServer);
}

//****************************************************************************************************

function btnUpdate_Click() {
    alert('btnUpdate_Click event fired, you should not see this message.');
}

function createPolygonOnClick() {

    google.maps.event.addListener(map, 'click', function (event) { func_mapClicked(event); });
    $('#messageTag').text('Click on the map to start creating a new geo-fence');

}

function showPolygonLatLongs() {

    showArrays(currentPolygon);

}

function showArrays(plyGon) {

    var vertices = plyGon.getPath();

    // Iterate over the vertices.
    for (var i = 0; i < vertices.getLength() ; i++) {
        var xy = vertices.getAt(i);
        alert(xy.lng());
    }

}

function getDifrnce(num1, num2) {
    return (num1 > num2) ? num1 - num2 : num2 - num1
}

function goefenceColour_ColorChanged() {

    if (currentPolygon != null) {

        currentPolygon.fillColor = goefenceColour.GetColor();
        currentPolygon.strokeColor = goefenceColour.GetColor();

        if (currentPolygon.getMap() != null) {
            currentPolygon.setMap(null);
            currentPolygon.setMap(map);
        }
    }

    if (currentCircle != null) {

        currentCircle.fillColor = goefenceColour.GetColor();
        currentCircle.strokeColor = goefenceColour.GetColor();

        if (currentCircle.getMap() != null) {
            currentCircle.setMap(null);
            currentCircle.setMap(map);
        }
    }

}

//If we are closing the popup, then remove the click handler from the map object
//also, remove the current polygon from the screen (if it is there).
function popupGeoCaching_Closing() {
    google.maps.event.clearListeners(map, 'click');
    if (currentPolygon != null) { currentPolygon.setMap(null); currentPolygon = null; }
}


//this function shows the info box related to a geo-fence
function showArrays(event) {


    //var vertices = '<br><br>' + this.getPath();
    var s = '<b>' + this.Name + '</b><br><br>'

    var newURL = 'GeoFencePropertyDisplay.aspx?ApplicationGeoFenceID=' + this.ApplicationGeoFenceID;
    contentString = s + '<iframe src=\'' + newURL + '\' marginwidth=\'0\' marginheight=\'0\' frameborder=\'0\' overflow-y=\'scroll\' overflow-x=\'hidden\' style=\'height: 200px; width: 400px\'></iframe>';

    //infoWindow = new google.maps.InfoWindow();

    infoWindow.setContent(contentString);
    infoWindow.setPosition(event.latLng);
    infoWindow.open(map);

}


function func_mapClicked(event) {

    //marker = new google.maps.Marker({position: event.latLng, map: map});
    google.maps.event.clearListeners(map, 'click');
    var bnds = map.getBounds();
    //alert(bnds('south'));
    //alert(JSON.stringify(bnds));

    var top = bnds.H.j;
    var bottom = bnds.H.H;
    var left = bnds.j.j;
    var right = bnds.j.H;

    var event_lat = event.latLng.lat();
    var event_lng = event.latLng.lng();

    var clickLocation = new google.maps.LatLng(event_lat, event_lng);

    //alert("left: {0},\nright: {1},\ntop: {2},\nbottom: {3} | \nevent_lat:{4}, \nevent_lng:{5} ".format(left,right,top,bottom,event_lat,event_lng));

    //top left
    var new_topleftlat = event_lat + (getDifrnce(top, bottom) / 10);
    var new_topleftlng = event_lng - (getDifrnce(left, right) / 10);
    //bottom left
    var new_bottomleftlat = event_lat - (getDifrnce(top, bottom) / 10);
    var new_bottomleftlng = event_lng - (getDifrnce(left, right) / 10);
    //bottom right
    var new_bottomrightlat = event_lat - (getDifrnce(top, bottom) / 10);
    var new_bottomrightlng = event_lng + (getDifrnce(left, right) / 10);
    //top right
    var new_toprightlat = event_lat + (getDifrnce(top, bottom) / 10);
    var new_toprightlng = event_lng + (getDifrnce(left, right) / 10);

    //calculate the circle radius (in metres). 
    var circleRadius = getDistance(new_topleftlat, new_topleftlng, new_toprightlat, new_toprightlng) / 4;

    var coords =
   [
       //new google.maps.LatLng(top,left), 
       //event.latLng,
       new google.maps.LatLng(new_topleftlat, new_topleftlng),
       new google.maps.LatLng(new_bottomleftlat, new_bottomleftlng),
       new google.maps.LatLng(new_bottomrightlat, new_bottomrightlng),
       new google.maps.LatLng(new_toprightlat, new_toprightlng),
   ];

    var selectedColour = goefenceColour.GetColor();


    //Do we want a circle or a polygon

    currentPolygon = new google.maps.Polygon(
    {
        paths: coords,
        strokeColor: selectedColour,
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: selectedColour,
        fillOpacity: 0.26,
        draggable: true,
        editable: true
    });


    currentCircle = new google.maps.Circle(
        {
            strokeColor: selectedColour,
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: selectedColour,
            fillOpacity: 0.35,
            editable: true,
            draggable: true,
            center: clickLocation,
            radius: circleRadius
        });

    ShowEditableGeoFence();

    $('#messageTag').text('');

    //console.log(map.getZoom());
}

currentCircle = {};

function ShowEditableGeoFence() {


    var selectedValueType = combogeoFenceTypeSelection.GetValue();

    try {

        currentPolygon.setMap((selectedValueType == "Circle") ? null : map);
        currentCircle.setMap((selectedValueType == "Polygon") ? null : map);

    } catch (e) { }



}

function getDistance(lat1, lon1, lat2, lon2) {  // generally used geo measurement function
    var R = 6378.137; // Radius of earth in KM
    var dLat = (lat2 - lat1) * Math.PI / 180;
    var dLon = (lon2 - lon1) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
    Math.cos(lat1 * Math.PI / 180) * Math.cos(lat2 * Math.PI / 180) *
    Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    return d * 1000; // meters
}

function initialize() {
    map = new google.maps.Map(document.getElementById("map"),
    {
        zoom: 4,
        center: new google.maps.LatLng(22.7964, 79.8456),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    var coords =
    [
        new google.maps.LatLng(18.979026, 72.949219), //Mumbai
        new google.maps.LatLng(28.613459, 77.255859), //Delhi
        new google.maps.LatLng(22.512557, 88.417969), //Kolkata
        new google.maps.LatLng(12.940322, 77.607422), //Bengaluru
    ];

    metros = new google.maps.Polygon(
    {
        paths: coords,
        strokeColor: "#0000FF",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#0000FF",
        fillOpacity: 0.26,
        draggable: true,
        editable: true
    });

    metros.setMap(map);
}



//$(document).ready(function () { instansiateDefaultService(); })
//$(document).ready(function () { initialize(); })initialize();



function geo_ajaxMethod(url, parameters, successCallback, errorCallback, finallyCallback) {

    if (errorCallback == null)
        errorCallback = ajaxMethodOnError;

    convertDatesToJson(parameters);

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(parameters),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,

        success: function (result) {
            convertJsonToDates(result);
            successCallback(result);

            if (finallyCallback != null)
                finallyCallback();
        },

        error: function (error) {
            //convertJsonToDates(result); // do we need this?
            errorCallback(error);

            if (finallyCallback != null)
                finallyCallback();
        }
    });
    return false;
}