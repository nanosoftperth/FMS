
window.onload = window.onresize = function () {
    

    var left = document.getElementById('frmContent');
    var window_height = window.innerHeight;
    //console.log(left.offsetHeight);
    //console.log(window_height);
    if (left.offsetHeight < window_height) {
        left.style.height = window_height - 360 + "px";

    } else { }

}
