
window.onload = window.onresize = function () {
    

    var iframeobj = document.getElementById('frmContent');
    var window_height = window.innerHeight;
    //console.log(left.offsetHeight);
    //console.log(window_height);
    if (iframeobj.offsetHeight < window_height) {
        iframeobj.style.height = window_height - 250 + "px";

    } else { }

}
