var Zagro125Value = '';
var Zagro500Value = '';
var FC_Codes = '';
var AC_Codes = '';

// First, checks if it isn't implemented yet.
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

var uri = '/api/vehicle/GetFormattedCanMessageSnapshot?_deviceid={0}&_standard={1}&_spn={2}'

function loopGetVals() {

    var countOfCANVals = $('#contentTable tr').length;

    $('#contentTable tr').each(

        function (index, item) {

            var id = '#' + item.id;

            var dateItm = $(item).find('.Date');
            var valItm = $(item).find('.Value');

            var standard = item.id.split('|')[0];
            var spn = item.id.split('|')[1];
            var this_url = uri.format($("#deviceId").val(), standard, spn);

            var isLastItem = ((countOfCANVals - 1) == index);

            $.getJSON(this_url, function (data) {
                serverResult(data, dateItm, valItm, isLastItem, id);
            });
        }



    )
}
function GetDataInput(data, num) {
    if (data != null) {
        if (String(data.Value).indexOf("ERROR:") == 0) {
            return "";
        } else {
            if (num == true) {
                return data.Value.toFixed(1);
            } else {
                return data.Value;
            }
        }
    } else {
        return "";
    }
    //return (String(data.Value).indexOf("ERROR:") == 0) ? "" : (num == true) ? data.Value.toFixed(1) : data.Value;
}
function serverResult(data, dateItm, valItm, isLastItem, id) {
    var dataVal = "";
    
    switch (id) {
        case "#Zagro500|8":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro500|9":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro500|10":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro500|11":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro125|1":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro125|8":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro125|9":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            valItm.text(dataVal);
            break;
        case "#Zagro125|10":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            if (data.DeviceName != null && data.DeviceName.indexOf("XL") == -1) {
                valItm.text("").append("<a href='javascript:void(0)' onclick='OnPressureValueClick()' id='faultCodes'>Not implemented</a>");
            } else {
                valItm.text(dataVal);
            }
            break;
        case "#Zagro125|11":
            dataVal = GetDataInput(data,true) + " " + data.Units;
            if (data.DeviceName != null && data.DeviceName.indexOf("XL") == -1) {
                valItm.text("").append("<a href='javascript:void(0)' onclick='OnPressureValueClick()' id='faultCodes'>Not implemented</a>");
            } else {
                valItm.text(dataVal);
            }
            break;
        case "#Zagro500|12":
            dataVal = GetDataInput(data, false) + " " + data.Units;
            $("#faultDesc").val(data.ValueString);
            valItm.text("").append("<a href='javascript:void(0)' onclick='OnFaultCodesClick()' id='faultCodes'>" + dataVal + "</a>");
            FC_Codes = data.ValueString;

            break;
        case "#Zagro500|16":
            dataVal = GetDataInput(data, false) + " " + data.Units;
            //$("#faultDesc").val(data.ValueString);
            valItm.text("").append("<a href='javascript:void(0)' onclick='OnFaultCodesClick()' id='faultCodes'>" + dataVal + "</a>");
            AC_Codes = data.ValueString;
            //dataVal = GetDataInput(data, true) + " " + data.Units;
            //valItm.text(dataVal);
            break;
        default:
            dataVal = GetDataInput(data, false) + " " + data.Units;
            valItm.text(dataVal);
    }
    dateItm.text(dateFormat(data.Time, "mm/dd/yyyy h:MM:ss"));

    if (id == "#Zagro125|7") {
        Zagro125Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : dataVal;
    }
    if (id == "#Zagro500|7") {
        Zagro500Value = (String(data.Value).indexOf("ERROR:") == 0) ? "" : dataVal;
    }

    if (Zagro125Value == "") {
        $('#contentTable').find('.Zagro1257').hide();
    } else {
        $('#contentTable').find('.Zagro1257').show();
    }

    if (Zagro500Value == "") {
        $('#contentTable').find('.Zagro5007').hide();
        //var tmp = $('#contentTable').find('.Zagro50016').find('Value tdFont').find('.faultCodes').text();
        var tmp = $('#contentTable').children();
        //alert(tmp);
        //$('#contentTable').find('.Zagro50016').hide();
    } else {
        $('#contentTable').find('.Zagro5007').show();
        //$('#contentTable').find('.Zagro50016').show();
    }

    UpdateFaulCodesRow('', '');

    if (isLastItem) setTimeout(loopGetVals, 1000);
}

function UpdateFaulCodesRow(strFC_Codes, strAC_Codes)
{
    //$("table tr td:last-child").each(function (k, v) {
    //    alert($(v).html());
    //});

    var countOfCANVals = $('#contentTable tr').length;

    $('#contentTable tr').each(

        function (index, item) {

            var id = '#' + item.id;

            if (id == '#Zagro125|1')
            {

            }

            //var dateItm = $(item).find('.Date');
            //var valItm = $(item).find('.Value');

            //var standard = item.id.split('|')[0];
            //var spn = item.id.split('|')[1];
            //var this_url = uri.format($("#deviceId").val(), standard, spn);

            //var isLastItem = ((countOfCANVals - 1) == index);

        }



    )
}

function OnPressureValueClick() {
    var contentUrl = $("#faultDesc").val();

    textVal = "Not implemented in the E-Maxis S, M & L series";
    document.getElementById("pMessage").innerHTML = textVal;
    myPopup.SetHeaderText('Pressure Values Information');
    ShowMyPopupWindow();
}

function OnFaultCodesClick() {
    var contentUrl = $("#faultDesc").val();
    var textVal = "";
    var content = contentUrl.split(',');
    for (i = 0; i < content.length; i++) {
        textVal += content[i] + "<br>";
    }

    document.getElementById("pMessage").innerHTML = textVal;
    myPopup.SetHeaderText('Fault Code Information');
    ShowMyPopupWindow();

}
function ShowMyPopupWindow() {
    myPopup.ShowAtPos(10, 10);
    myPopup.Show();
}

$(document).ready(function () { loopGetVals(); });

