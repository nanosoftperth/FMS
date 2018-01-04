

//function OnKeyDownCmb(s, e) {
//    if (ASPxClientUtils.GetKeyCode(e.htmlEvent) === ASPxKey.Enter){
//        ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
//        //BtnSearch.Focus();
//    }

$('html').bind('keypress', function (e) {
    if (e.keyCode == 13) {
        return false;
    }
});

function RootMasterControlsInitialised() {

    //alert('ControlsInitialised');
    ///IF THE LOGO IS A OVERLAYED ACROSS THE LOGOUT DETAILS, THEN MOVE IT TO THE LEFT
    var logoHeight = $('.headerLogo').height();
    if (logoHeight > 90) {
        $('.loginControl').css('right', '130px');
        $('.loginControl').css('top', '-17px');
    } else {
        $('.loginControl').css('right', '1px');
    }

   
}


var iframeHeightOffset = 250;

function MainLightMaster_ControlsInitialised() {

    //alert('controls initialised')


    window.onload = window.onresize = function () {

        var window_height = window.innerHeight;
        var contentHeight = window_height;

        //alert('height:' + contentHeight);

        contentRoundPanel.SetHeight(contentHeight);

        try {

            $('#frmContent').css('height', (window_height - iframeHeightOffset ) + "px");
            $('#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_CC').css('padding-bottom', '0px');

        } catch (e) { }

        try { callResize(contentHeight); } catch (e) { };
    }


    var window_height = window.innerHeight;
    var contentHeight = window_height - 180;

    //contentRoundPanel.SetHeight(contentHeight);

    try {

        $('#frmContent').css('height', (window_height - iframeHeightOffset ) + "px");
        $('#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_CC').css('padding-bottom', '0px');

    } catch (e) { }


    try { callResize(contentHeight); } catch (e) { };

}


///changes the size of the iFrame to the MAXimum possible whilst maintainin the look and feelof the page.
function callResize(contentHeight) {

    //document.getElementById("bugtrackerIframe").style.width = "50px";

    //do width first on purpose (this will resize the content div so we can get the height)
    var tWidth = window.innerWidth * 0.7; //;$('#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_CRC').width();
    tWidth = tWidth - 10;
    document.getElementById("bugtrackerIframe").style.width = tWidth + "px";

    var tHeight = contentHeight;
    tHeight = tHeight - 100;
    document.getElementById("bugtrackerIframe").style.height = tHeight + "px";

    document.getElementById("bugtrackerIframe").src = "https://bugtracker.zoho.com/portal/nanosoftfms#bugkanbanview/936511000000014017";

}


//For todays date;
Date.prototype.today = function () {
    return ((this.getDate() < 10) ? "0" : "") + this.getDate() + "/" + (((this.getMonth() + 1) < 10) ? "0" : "") + (this.getMonth() + 1) + "/" + this.getFullYear()
};
//For the time now
Date.prototype.timeNow = function () {
    return ((this.getHours() < 10) ? "0" : "") + this.getHours() + ":" + ((this.getMinutes() < 10) ? "0" : "") + this.getMinutes() + ":" + ((this.getSeconds() < 10) ? "0" : "") + this.getSeconds();
};

//Date input helper functions
var dateFormat = "dd/mm/yy";
var datePickerOptions = {};

datePickerOptions.dateFormat = dateFormat;
datePickerOptions.showOn = "button";

$(pageReady);

function pageReady() {
    ie6SearchButtonHack();
    hideLoadingPanel();
    $("#logoutButton").click(logout);
}

function logout() {
    document.location = "Logout.aspx";
}


function formatDate(d) {
    if (d == null)
        return "";

    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();

    if (day < 10)
        day = "0" + day;

    if (month < 10)
        month = "0" + month;

    return "" + day + "/" + month + "/" + year;
}

function formatDateTime(d) {
    if (d == null)
        return "";

    var hours = d.getHours();
    var minutes = d.getMinutes();
    var seconds = d.getSeconds();

    if (hours < 10)
        hours = "0" + hours;

    if (minutes < 10)
        minutes = "0" + minutes;

    if (seconds < 10)
        seconds = "0" + seconds;

    return formatDate(d) + " " + hours + ":" + minutes + ":" + seconds;
}

function getDateFromInput(input) {
    var value = input.val();

    if (isNullOrWhiteSpace(value))
        return null;

    return $.datepicker.parseDate(dateFormat, value);
}

function isDate(s) {
    var dateComponents = s.split("/");

    if (dateComponents.length != 3)
        return false;

    for (var i = 0; i < 3; i++) {
        if (!isInteger(dateComponents[i]))
            return false;

        dateComponents[i] = parseInt(dateComponents[i], 10);

        if (dateComponents[i] < 1)
            return false;
    }

    var day = dateComponents[0];
    var month = dateComponents[1];
    var year = dateComponents[2];

    if (year < 1753 || year > 9999 || month > 12)
        return false;

    switch (month) {
        case 1: case 3: case 5: case 7: case 8: case 10: case 12:
            if (day > 31)
                return false;
            break;
        case 4: case 6: case 9: case 11:
            if (day > 30)
                return false;
            break;
        case 2:
            if (day > 29 || ((year % 4) > 0 && day > 28))
                return false;
            break;
    }

    return true;
}

//Input helper functions
function isNullOrWhiteSpace(text) {
    return typeof text == "undefined" || text == null || !/\S/.test(text);
}

function getValueFromInput(input) {
    var value = input.val();

    if (isNullOrWhiteSpace(value))
        return null;

    return value;
}

function isInteger(s) {
    return (s.toString().search(/^-?[0-9]+$/) == 0);
}

//UI Helper functions
function showLoadingPanel() {
    $('#customLoadingPanel').show();
    $('#customLoadingPanelBackground').show();
}

function hideLoadingPanel() {
    $('#customLoadingPanel').hide();
    $('#customLoadingPanelBackground').hide();
}

function isLoading() {
    return $("#customLoadingPanel:visible").length > 0;
}

function disableButton(button, disabledText) {
    button.removeClass("button");
    button.addClass("disabledButton");
    button.unbind("click");

    if (disabledText != null)
        button.text(disabledText);
}

function enableButton(button, onClick, enabledText) {
    button.removeClass("disabledButton");
    button.addClass("button");
    button.unbind("click");
    button.click(onClick);

    if (enabledText != null)
        button.text(enabledText);
}

//Logged in checks
function checkRedirect(s, e) {
    if (e.message == "The session has expired.") {
        redirectToLogin();
        e.handled = true;
    }
}

function redirectToLogin() {
    __doPostBack('__Page', 'RedirectToLogin');
}

//Ajax
function ajaxMethod(url, parameters, successCallback, errorCallback, finallyCallback) {

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
            errorCallback(error);

            if (finallyCallback != null)
                finallyCallback();
        }
    });
}

function ajaxMethodOnError(error) {
    switch (typeof error) {
        case "string":
            alert(error);
            break;

        case "object":
            if (typeof error.responseText != "undefined") {
                try {
                    var errorDetail = JSON.parse(error.responseText);

                    if (errorDetail.Message != "undefined")
                        alert(errorDetail.Message + (typeof errorDetail.StackTrace == "string" ? '\n\n' + errorDetail.StackTrace : ""));
                    else
                        alert("A JSON error was returned but it did not contain a 'Message' property.");
                }
                catch (e) {
                    var errorText = "<ul>" +
                        "<li>Ready State: " + error.readyState + "</li>" +
                        "<li>Response Text: " + error.responseText + "</li>" +
                        "<li>Status: " + error.status + "</li>" +
                        "<li>Status Text: " + error.statusText +
                    "</ul>";

                    $.colorbox({
                        html: errorText,
                        width: "500px",
                        height: "400px"
                    });
                }
            }
            else {
                alert("An error was returned but it was in an unknown format.");
            }

            break;

        case "undefined":
        default:
            alert("An undefined error has occured.");
            break;
    }
}


function ajaxGetMethod(url, parameters, successCallback, errorCallback, finallyCallback) {

    if (errorCallback == null)
        errorCallback = ajaxMethodOnError;

    convertDatesToJson(parameters);

    $.ajax({
        type: "GET",
        url: url,
        //data: JSON.stringify(parameters),
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
            errorCallback(error);

            if (finallyCallback != null)
                finallyCallback();
        }
    });
}


function pageMethod(url, parameters, successCallback, errorCallback, finallyCallback) {
    if (errorCallback == null)
        errorCallback = pageMethodOnError;

    ajaxMethod(
        url,
        parameters,
        function (result) {
            convertJsonToDates(result.d);
            successCallback(result.d);
        },
        errorCallback,
        finallyCallback
    );
}

function pageMethodOnError(error) {
    var errorResponse = JSON.parse(error.responseText);

    var errorDetails =
        errorResponse.Message + "\n\n" +
        "Type: " + errorResponse.ExceptionType + "\n" +
        "Status: " + error.status + "\n" +
	    "Timeout: " + error.timeout.toString();

    alert(errorDetails);
}

function convertJsonToDates(result) {
    for (i in result) {
        if (typeof result[i] == "object")
            convertJsonToDates(result[i]);
        else if (typeof result[i] == "string" && result[i].startsWith("/Date("))
            //result[i] = new Date(parseInt(result[i].replace(/(^.*\()|([+-].*$)/g, '')));
            result[i] = convDateWithTZ(result[i]);
    }
}

function convertDatesToJson(result) {
    for (i in result) {
        if (result[i] instanceof Date)
            //result[i] = "/Date(" + result[i].getTime() + (result[i].getTimezoneOffset() * 3600) + ")/";
            result[i] = "/Date(" + result[i].getTime() + ")/";
        else if (typeof result[i] == "object")
            convertDatesToJson(result[i]);
    }
}


function convDateWithTZ(dataStr) {

    var integerWithout = parseInt(dataStr.replace(/(^.*\()|([+-].*$)/g, ''));
    var isPositive = dataStr.indexOf('+') != -1;
    var timeSection;

    if (isPositive) {
        timeSection = dataStr.split('+')[1].split(')')[0];
    } else {
        timeSection = dataStr.split('-')[1].split(')')[0];
    }

    //var timeSection = dataStr.split('+')[1].split(')')[0];

    var hours = timeSection.slice(0, 2);
    var minutes = timeSection.slice(2, 4);

    var hoursinseconds = hours * 60 * 60 * 1000;
    var minutesinseconds = minutes * 60 * 1000;

    var retnum = integerWithout + hoursinseconds + minutesinseconds;

    var retDate = new Date(parseInt(retnum));

    retnum = retnum + (retDate.getTimezoneOffset() * 60000);

    return new Date(retnum);
}

String.prototype.startsWith = function (string) {
    return (this.indexOf(string) === 0);
}

function ie6SearchButtonHack() {
    if (!isIE6OrLess())
        return;

    $(".searchButton").hover(
        function () { $(this).addClass("searchButton-hover"); },
        function () { $(this).removeClass("searchButton-hover"); }
    );
}

function isIE6OrLess() {
    var browser = navigator.appName;

    if (browser != "Microsoft Internet Explorer")
        return false;

    var ua = navigator.userAgent;
    var msieOffset = ua.indexOf("MSIE ");
    var versionNumber = parseFloat(ua.substring(msieOffset + 5, ua.indexOf(";", msieOffset)));

    return versionNumber < 7;
}

function setControlWidths(controls, minWidth) {
    var newWidth = minWidth;

    for (var i = 0; i < controls.length; i++) {
        var control = $(controls[i]);

        if (control.width() > newWidth)
            newWidth = control.width();
    }

    controls.width(newWidth);

    var dateButtonWidth = $(".ui-datepicker-trigger:first").outerWidth();
    $(".datePicker").width(newWidth - dateButtonWidth);
}

//--for dashboard
function CallInfoWindow()
{
    alert("test");
}