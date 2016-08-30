

//we need to move ALL the clock logic into here.


function toggleCalendar() {

    var calendarobj = $('.dxeCalendar_SoftOrange tr:first td:first');
    var isVisible = true;
    var arrStlyeOptions = calendarobj.attr('style').split(';');

    try {

        arrStlyeOptions.forEach(function (item, index) {

            var splt = item.split(':');

            var var_name = splt[0].trim();
            var var_val = splt[1].trim();

            if (var_name == 'display' && var_val == 'none') { isVisible = false; }

        })

    } catch (e) { }

    var msg = '';

    if (isVisible) {
        msg = 'show calendar';
        calendarobj.hide();
        popupDate.SetWidth(100);
    } else {
        msg = 'hide calendar';
        popupDate.SetWidth(427);
        calendarobj.show();
    }

    var htmlStr = '<a href=\'javascript:toggleCalendar()\' >' + msg + '</a>'

    $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1_ASPxDateEdit2_DDD_C_TST').html(htmlStr);

}


function registerHideShowAnchor() {

    //hide the date picker box
    $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1_ASPxDateEdit2').hide()
    $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_ASPxPanel1').css('width', '138px');
    $('#ctl00_ctl00_MainPane_Content_MainContent_popupDate_PWC-1 .dxeDETSH table').css('visibility', 'visible');
       

    toggleCalendar();

    popupDate.SetHeight(265);
}