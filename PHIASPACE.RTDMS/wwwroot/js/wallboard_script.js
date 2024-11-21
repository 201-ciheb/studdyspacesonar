//var base_api = "https://baisvlisting.umbphia.org/api/";
var base_api = "https://localhost:5001/api/";
function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function startTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    // add a zero in front of numbers<10
    m = checkTime(m);
    s = checkTime(s);
    $('#time').text(h + ":" + m + ":" + s);
    t = setTimeout(function () {
        startTime()
    }, 500);
    $('#date').text(today.getFullYear() + "/" + (today.getMonth() + 1) + "/" + today.getDate());
    if(undefined === surveyStartDate) var surveyStartDate = new Date();
    var start_date = new Date(surveyStartDate);//'2021/3/12'
    $('#days_done').text(datediff(new Date(today.getFullYear(), today.getMonth(), today.getDate()), start_date) + 1);
}
startTime();

function datediff(first, second) {
    return Math.round((first-second)/(1000*60*60*24));
}

function alternatDivs(divIds) {
    var ids = divIds;
    ids.forEach(e => {
        $(e).hide();
    });
    var index = 0;
    $(ids[index]).show(300);
    setInterval(() => {
        if (index >= ids.length - 1)
            index = 0;
        else
            index = index + 1;
        ids.forEach(e => {
            $(e).hide();
        });
        $(ids[index]).show(300);
    }, 1000 * 10 * 5);
}

function requestFullScreen(element) {
    // Supports most browsers and their versions.
    var requestMethod = element.requestFullScreen || element.webkitRequestFullScreen || element.mozRequestFullScreen || element.msRequestFullScreen;

    if (requestMethod) { // Native full screen.
        requestMethod.call(element);
    } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
        var wscript = new ActiveXObject("WScript.Shell");
        if (wscript !== null) {
            wscript.SendKeys("{F11}");
        }
    }
}
