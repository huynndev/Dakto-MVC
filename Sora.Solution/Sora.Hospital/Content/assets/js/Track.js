// if not login
// 1 get and save product id on cookie
// 2 read cookie check
// 3 change html tracked or not track
// 4 site out notification login or register
// esle login
// 5 save database
// 6 detele track
// 7 list product
var ListTrack=null;
//-------------1
function SaveProductToCookie(id) {
    //let id = $(event).parents(".details-product").attr("data-id");
    let idproduct = getCookie("track");
    var d = new Date();
    d.setTime(d.getTime() + (4 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    if (idproduct != "" && idproduct.length > 0) {
        if (idproduct.indexOf(id) >= 0)
            return;
        else {
            idproduct = idproduct + "," + id + ";" + expires + ";path=/";
            setCookie("track", idproduct);
        }
    } else {
        idproduct = id + ";" + expires + ";path=/";
        setCookie("track", idproduct);
    }
}
function FristGetAndSetTrack(userID) {
    if (userID != null && userID != "") {
        ListTrack = getCookie("track");
        if (ListTrack != null && ListTrack != "") {
            let param = {
                ProductREID: ListTrack,
                UserID: userID
            }
            UpdateTrack(param);
        }
        GetTrackService(userID);
    }
}
//-------------End 1
//-------------2
//-------------End 2
//-------------3
function GetListTrack(userID) {
    if (userID != null && userID != "") {
        GetTrackService(userID);
    } else {
        ListTrack = getCookie("track");
        if (ListTrack != null && ListTrack != "") ChangeTrack(ListTrack);
    }
}
function ChangeTrack(data) {
    let id = $(".details-product").attr("data-id");
    let $parent = $(".details-product .btn-view-details");
    if (data != null && data.indexOf(id) >= 0) {
        $parent.find(".btn-track").hide();
        $parent.find(".btn-track-cancel").show();
    } else {
        $parent.find(".btn-track").show();
        $parent.find(".btn-track-cancel").hide();
    }
}
function GetTrackService(id) {
    $.ajax({
        url: '/Track/GetTrack?UserID='+id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            ListTrack = result;
            if (ListTrack != null && ListTrack != "") {
                //setCookie("track", ListTrack);
                removeCookie("track");
                ChangeTrack(ListTrack);
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//-------------End 3
//-------------4
//-------------End 4
//-------------5
function SetTrack(userID,productID) {
    if (userID != null && userID != "") {
        let param = {
            ProductREID: productID,
            UserID: userID
        }
        UpdateTrack(param);
            
    } else {
        SaveProductToCookie(productID);
    }
    GetListTrack(userID);
}
function UpdateTrack(param) {
    $.ajax({
        url: '/Track/UpdateTrack',
        type: 'POST',
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: true,
        success: function (result) {
            GetTrackService(param.UserID);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//-------------End 5
//-------------6
function DeleteTrack(param) {
    $.ajax({
        url: '/Track/DeleteTrack',
        type: 'POST',
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: true,
        success: function (result) {
            GetTrackService(param.UserID);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//-------------End 6