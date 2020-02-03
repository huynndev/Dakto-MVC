var Setting = null;
function getSettingPage() {
    $.ajax({
        url: '/Setting/GetSetting',
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            Setting = result;
            ZoomOption();
            let mapArea = result.AreaMap.split(";");
            moveToLocation(mapArea[1], mapArea[2]);
            SettingDownload(Setting);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function setCookie(cname, cvalue) {
    //var d = new Date();
    //d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    //var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue;
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
function removeCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}
function SettingDownload(setting) {
    if (setting.StatusDownload.toString().toLowerCase().indexOf("true") >= 0) {
        $(".navbar-inverse .dowload-map").css("display", "block");
        $(".navbar-inverse .dowload-map").attr("href", (setting.LinkDownload.length > 0)?setting.LinkDownload.length:"#");
    } else {
        $(".navbar-inverse .dowload-map").css("display", "none");
    }
}

