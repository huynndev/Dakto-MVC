var productItem = null;
var lazyScroll = false;
var paramProductSearch = {
    AreaID: '',
    ProjectID: '',
    DistanceMin: -2,
    DistanceMax: -2,
    Type: '',
    Deriction: '',
    Limit: 10,
    Start: 1,
    UserID: '',
    Name:''
}
//clear param product search
function clearParamProductSearch(){
    paramProductSearch.AreaID='';
    paramProductSearch.ProjectID = '';
    paramProductSearch.DistanceMin =-2;
    paramProductSearch.DistanceMax = -2;
    paramProductSearch.Type = '';
    paramProductSearch.Deriction = '';
    paramProductSearch.Limit = 10;
    paramProductSearch.Start = 1;
    paramProductSearch.UserID = '';
    paramProductSearch.Name = '';
}
//add html modal
function addModal() {
    let str=`<div class="modal-dialog modal-lg bs-modal-product" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-3 left-select">
                        <div>
                            <span class ="title-left-select"> Loại hình sản phẩm </span>
                            <i class ="fa fa-caret-down icon-detail"></i>
                            <div class="left-select-content type-product">
                                <div class="be-checkbox inline"><input id="check6" type="checkbox"><label for="check6">Option 1</label></div>
                            </div>
                            <div class="left-select-detail">....</div>
                        </div>
                        <div>
                            <span class ="title-left-select">Hướng </span>
                            <i class ="fa fa-caret-down icon-detail"></i>
                            <div class="left-select-content direction-product">
                                <div class="be-checkbox inline"><input id="check6" type="checkbox"><label for="check6">Option 1</label></div>
                            </div>
                            <div class="left-select-detail">....</div>
                        </div>
                        <div>
                            <span class ="title-left-select">Khoảng đầu tư </span>
                            <i class ="fa fa-caret-down icon-detail"></i>
                            <div class="left-select-content distance-product">
                                <div class="be-checkbox inline"><input id="check6" type="checkbox"><label for="check6">Option 1</label></div>
                            </div>
                            <div class="left-select-detail">....</div>
                        </div>
                    </div>
                    <div class="col-sm-9 right-content">
                        <div class="right-content-header">
                            <span class="title-right-content">Sản phầm</span>
                            <span>|</span>
                            <span class="total-right-content">35 kết quả</span>
                        </div>
                        <div class ="right-content-content">
                            <div class ="right-content-content-item">
                                <div class="right-content-item">
                                    <div class="right-content-item-img"><img src="/Content/image/default.jpg" /></div>
                                    <div class="right-content-item-content">
                                        <div class="item-title"><a href="javascript:;">Bán đất 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, hướng Nam</a></div>
                                        <div class="item-content-short"><span>DT: 65m2, hướng Nam, 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, Đà Nẵng. Gần chợ Cẩm Lệ, khu dân cư đông đúc, thuận tiện đi lại. , sổ hồng chính chủ. Giá: thỏa thuận LH chính chủ: 0965529468...</span></div>
                                        <div class="item-option">
                                            <div class="item-option-time"><i class="fa fa-clock-o"></i><span>15:00 22/02/2018</span> </div>
                                            <span class="item-option-view">150 người quan tâm</span>
                                            <button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="right-content-item">
                                    <div class="right-content-item-img"><img src="/Content/image/default.jpg" /></div>
                                    <div class="right-content-item-content">
                                        <div class="item-title"><a href="javascript:;">Bán đất 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, hướng Nam</a></div>
                                        <div class="item-content-short"><span>DT: 65m2, hướng Nam, 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, Đà Nẵng. Gần chợ Cẩm Lệ, khu dân cư đông đúc, thuận tiện đi lại. , sổ hồng chính chủ. Giá: thỏa thuận LH chính chủ: 0965529468...</span></div>
                                        <div class="item-option">
                                            <div class="item-option-time"><i class="fa fa-clock-o"></i><span>15:00 22/02/2018</span> </div>
                                            <span class="item-option-view">150 người quan tâm</span>
                                            <button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="right-content-item">
                                    <div class="right-content-item-img"><img src="/Content/image/default.jpg" /></div>
                                    <div class="right-content-item-content">
                                        <div class="item-title"><a href="javascript:;">Bán đất 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, hướng Nam</a></div>
                                        <div class="item-content-short"><span>DT: 65m2, hướng Nam, 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, Đà Nẵng. Gần chợ Cẩm Lệ, khu dân cư đông đúc, thuận tiện đi lại. , sổ hồng chính chủ. Giá: thỏa thuận LH chính chủ: 0965529468...</span></div>
                                        <div class="item-option">
                                            <div class="item-option-time"><i class="fa fa-clock-o"></i><span>15:00 22/02/2018</span> </div>
                                            <span class="item-option-view">150 người quan tâm</span>
                                            <button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="right-content-item">
                                    <div class="right-content-item-img"><img src="/Content/image/default.jpg" /></div>
                                    <div class="right-content-item-content">
                                        <div class="item-title"><a href="javascript:;">Bán đất 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, hướng Nam</a></div>
                                        <div class="item-content-short"><span>DT: 65m2, hướng Nam, 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, Đà Nẵng. Gần chợ Cẩm Lệ, khu dân cư đông đúc, thuận tiện đi lại. , sổ hồng chính chủ. Giá: thỏa thuận LH chính chủ: 0965529468...</span></div>
                                        <div class="item-option">
                                            <div class="item-option-time"><i class="fa fa-clock-o"></i><span>15:00 22/02/2018</span> </div>
                                            <span class="item-option-view">150 người quan tâm</span>
                                            <button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="right-content-item">
                                    <div class="right-content-item-img"><img src="/Content/image/default.jpg" /></div>
                                    <div class="right-content-item-content">
                                        <div class="item-title"><a href="javascript:;">Bán đất 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, hướng Nam</a></div>
                                        <div class="item-content-short"><span>DT: 65m2, hướng Nam, 2 mặt tiền đường Nguyễn Xuân Hữu, Cẩm Lệ, Đà Nẵng. Gần chợ Cẩm Lệ, khu dân cư đông đúc, thuận tiện đi lại. , sổ hồng chính chủ. Giá: thỏa thuận LH chính chủ: 0965529468...</span></div>
                                        <div class="item-option">
                                            <div class="item-option-time"><i class="fa fa-clock-o"></i><span>15:00 22/02/2018</span> </div>
                                            <span class="item-option-view">150 người quan tâm</span>
                                            <button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>`
    return str;
    //$(".modal-common-product").append(str);
}
//get type 
function GetTypeRE() {
    $.ajax({
        url: '/Home/GetTypeRE',
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            AddHtmlProduct(1, result);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//get direction (hướng)
function GetDirection() {
    $.ajax({
        url: '/Home/GetDirection',
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            AddHtmlProduct(2, result);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//gét distance
function GetDistance() {
    $.ajax({
        url: '/Home/GetDistance',
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            AddHtmlProduct(3, result);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//add html type, direction, distance
function AddHtmlProduct(check,data) {
    let parent = $(".bs-modal-product .left-select");
    let str =(check !=3)?"":"<div class='be-radio be-radio-color inline'><input type='radio' name='distanceID' id='radio--2,-2' value='-2,-2'><label for='radio--2,-2'>Tất cả</label></div>";
    $(data).each(function (i, obj) {
        if(check != 3)
            str = str + "<div class='be-checkbox inline'><input id='check-" + obj.Value + "' type='checkbox' value='" + obj.Value + "'><label for='check-" + obj.Value + "'>" + obj.Text + "</label></div>";
        else
            str = str + "<div class='be-radio be-radio-color inline'><input type='radio' name='distanceID' id='radio-" + obj.Value + "' value='" + obj.Value + "'><label for='radio-" + obj.Value + "'>" + obj.Text + "</label></div>"
    })
    switch (check) {
        case 1:
            $(".left-select .type-product").children().remove();
            $(".left-select .type-product").append(str);
            if ($(data).length <= 4) {
                $(".left-select .type-product").parent().find(".left-select-detail").hide();
                $(".left-select .type-product").parent().find(".icon-detail").hide();
            }
            break;
        case 2:
            $(".left-select .direction-product").children().remove();
            $(".left-select .direction-product").append(str);
            if ($(data).length <= 4) {
                $(".left-select .direction-product").parent().find(".left-select-detail").hide();
                $(".left-select .direction-product").parent().find(".icon-detail").hide();
            }
            break;
        case 3:
            $(".left-select .distance-product").children().remove();
            $(".left-select .distance-product").append(str);
            if ($(data).length <= 3) {
                $(".left-select .distance-product").parent().find(".left-select-detail").hide();
                $(".left-select .distance-product").parent().find(".icon-detail").hide();
            }
            break;
        default:
            break;
    }
}
//event check type, direction, distance
function eventProduct() {
    $(".modal-common-product").on("click", ".fa-caret-down.icon-detail", function () {
        let parent = $(this).parent().find(".left-select-content");
        $(this).removeClass("fa-caret-down");
        $(this).addClass("fa-caret-up");
        let count = $(parent).children().length;
        let height = ((count - 3) * 30) + 104;
        $(parent).animate({
            "max-height": height + "px"
        }, 500, function () {
            $(this).parent().find(".left-select-detail").hide();
        });
    })
    .on("click", ".fa-caret-up.icon-detail", function () {
        let parent = $(this).parent().find(".left-select-content");
        $(this).removeClass("fa-caret-up");
        $(this).addClass("fa-caret-down");
        $(parent).animate({
            "max-height": "104px"
        }, 500, function () {
            $(this).parent().find(".left-select-detail").show();
        });
    })
    .on("change", ".left-select input[type='checkbox']", function () {
        GetListCheck(this);
    })
    .on("change", ".left-select input[type='radio']", function () {
        let min = $(this).val().split(',')[0];
        let max = $(this).val().split(',')[1];
        console.log("min:" + min + "max:" + max);
        paramProductSearch.DistanceMin = min;
        paramProductSearch.DistanceMax = max;
        paramProductSearch.Start = 1;
        productItem = null;
        GetProductItem(paramProductSearch);
    })
    .on("click", ".item-option-map,.item-title>a,.right-content-item-img>img", function () {
        let id = $(this).parents(".right-content-item").attr("data-id");
        MoveToDetailProduct(id);
        $(".modal-common-product").modal('toggle');
        GetDetaiProduct(id);
    })
}
function GetListCheck(event) {
    let str = "";
    if ($(event).parents(".left-select-content").hasClass("type-product")) {
        $(".type-product input[type='checkbox']:checked").each(function(){
            str = str+$(this).val()+",";
        })
        str = str.substring(0, str.length - 1);
        console.log(str);
        paramProductSearch.Type = str;
    }
    if ($(event).parents(".left-select-content").hasClass("direction-product")) {
        $(".direction-product input[type='checkbox']:checked").each(function () {
            str = str + $(this).val() + ",";
        })
        str = str.substring(0, str.length - 1);
        console.log(str);
        paramProductSearch.Deriction = str;
    }
    paramProductSearch.Start = 1;
    productItem = null;
    GetProductItem(paramProductSearch);
}
//get product item
function GetProductItem(param,check) {
    $.ajax({
        url: '/Home/GetSearchProduct',
        type: 'POST',
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: true,
        success: function (result) {
            if (result.list[0] != null) MoveToAreaOrProduct(result.list[0].ProductREID);
            addHtmlProductItem(result.list);
            LazyData();
            lazyScroll = false;
            $(".bs-modal-product .total-right-content").text(result.total + " kết quả");
            $(".bs-modal-product .total-right-content").attr("data-total",result.total);
            if(check){
                $(".bs-modal-product .left-select").hide();
                $(".bs-modal-product .right-content").css({ "width": "100%" });
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//add html product item
function addHtmlProductItem(data) {
    let str = "";
    if (productItem == null) {
        $(".bs-modal-product .right-content-content .right-content-content-item").children().remove();
        productItem = [];
    }
    $(data).each(function (i,obj) {
        let img = (obj.ProductREImage != null && obj.ProductREImage.length > 0) ? obj.ProductREImage : "/Content/image/default.jpg";
        let count = (obj.ProductREView > 0)?obj.ProductREView:"chưa";
        let acre = (obj.ProductREHeight > 0 && obj.ProductREWidth > 0) ? "<b>DT:</b>" + (obj.ProductREHeight * obj.ProductREWidth) + "m<sup>2</sup>, " : "";
        let direction = (obj.DirectionName != null && obj.DirectionName.length > 0) ? "<b>Hướng:</b>" + obj.DirectionName + ", " : "";
        let road = (obj.InforRoadName != null && obj.InforRoadName.length > 0) ? "<b>Tên đường:</b>" + obj.InforRoadName + ", " : "";
        let roadWidth = (obj.InforRoadWidth != null && obj.InforRoadWidth.length > 0) ? "<b>Đường rộng:</b>" + obj.InforRoadWidth + ", " : "";
        let price = (obj.ProductREPriceStatus && obj.ProductREPriceMin >= -1 && obj.ProductREPriceMax >= -1) ? "<b>Giá:</b>" + priceProduct(obj.ProductREPriceMin, obj.ProductREPriceMax)+", " : ""
        let phone = (obj.TelContact != null && obj.TelContact.length > 0) ? "<b>SDT:</b>" + obj.TelContact : "";
        str = str + '<div class="right-content-item" data-id="' + obj.ProductREID + '">' +
                        '<div class="right-content-item-img"><img src="' + img + '" /></div>' +
                        '<div class="right-content-item-content">' +
                            '<div class="item-title"><a href="javascript:;">' + obj.ProductREName + '</a></div>' +
                            '<div class="common-content">' + acre + '' + direction + '' + road + '' + roadWidth + '</div>' +
                            '<div class="item-content-short"><span>' + obj.ProductREShortDescription + '</span></div>' +
                            '<div class="common-content">' + price + '' + phone + '</div>' +
                            '<div class="item-option">' +
                                '<div class="item-option-time"><i class="fa fa-clock-o"></i><span>' + parseJsonDateTime(obj.ProductREStartTime) + '</span> </div>' +
                                '<span class="item-option-view">' + count + ' người quan tâm</span>' +
                                '<button class="btn btn-default item-option-map"> <i class="fa fa-map"></i> Xem bản đồ</button>' +
                            '</div>' +
                        '</div>' +
                    '</div>';
        productItem.push(obj);
    })
    $(".bs-modal-product .right-content-content .right-content-content-item").append(str);
    
    //LazyData();
    //lazyScroll = false;
}
//convert data
function priceProduct(minPrice, maxPrice) {
    if (maxPrice == -1 || (maxPrice == 0 && minPrice == 0)) return "Thỏa thuận";
    if (maxPrice > minPrice) {
        let priceMax = ConvertPriceToString(maxPrice);
        var split = priceMax.Trim().Split(' ');
        let value = (split.Length > 2) ? split[1] + " " + split[2] : split[1]; //string.Format("{0} {1}", split[1], split[2]) 
        let priceMin = ConvertPriceToString(minPrice);
        priceMin = (priceMin.Contains(value)) ? priceMin.Replace(value, "") : priceMin;
        return priceMin.Trim() + "-" + priceMax.Trim();
        //return string.Format("{0}-{1}", priceMin.Trim(), priceMax.Trim());
    }
    else return ConvertPriceToString(maxPrice); //string.Format("{0}", ConvertPriceToString(maxPrice));
}
function ConvertPriceToString(price) {
    let stringPrice = "";
    let i = 0;
    while (price >= 1000 && i < 4)
    {
        price = price / 1000;
        i = i + 1;
    }
    switch (i)
    {
        case 2:
            stringPrice = price+" tr";//string.Format("{0} {1}", price, "tr");
            break;
        case 3:
            stringPrice = price+" tỷ";//string.Format("{0} {1}", price, "tỷ");
            break;
        case 4:
            stringPrice = price+" ng tỷ";//string.Format("{0} {1}", price, "ng tỷ");
            break;
        default:
            stringPrice = price;//string.Format("{}", price);
            break;
    }
    return stringPrice;
}
// convert string to date
function parseJsonDate(jsonDateString) {
    if (jsonDateString != null && jsonDateString !== "undefined") {
        var date = new Date(parseInt(jsonDateString.replace('/Date(', '')));
        return (date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear());
    }
    return jsonDateString;
}
// convert string to datetime
function parseJsonDateTime(jsonDateString) {
    if (jsonDateString != null && jsonDateString !== "undefined") {
        var date = new Date(parseInt(jsonDateString.replace('/Date(', '')));
        return (date.getHours()+':'+date.getMinutes()+' '+date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear());
    }
    return jsonDateString;
}
//load lazy data
function LazyData() {
    $(".right-content-content").unbind().scroll(function () {
        let top = $(".right-content-content").scrollTop();
        let total=$(".bs-modal-product .total-right-content").attr("data-total");
        if ((top + $(".right-content-content").height()) >= ($(".right-content-content-item").height() - (110 * 2)) && !lazyScroll && productItem.length < total) {
            lazyScroll = true;
            paramProductSearch.Start = paramProductSearch.Start + 1;
            GetProductItem(paramProductSearch);
        }
    })
}
//---------------detail product--------------
function GetDetaiProduct(id) {
    $.ajax({
        url: '/Home/GetDetailProduct?id='+id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            if (result != null && result.ProductREName !="") {
                AddHtmlDetailProduct(result);
                checkViewProduct(result.ProductREID);
            } else {
                CloseDetailProduct();
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function AddHtmlDetailProduct(data) {
    aminateDetailProduct();
    //let acre = (data.ProductREHeight > 0 && data.ProductREWidth > 0) ? "<b>DT:</b>" + (data.ProductREHeight * data.ProductREWidth) + "m<sup>2</sup>, chiều dài:" + data.ProductREHeight + ", mặt tiền:" + data.ProductREWidth+"<br/>" : "";
    //let direction = (data.DirectionName != null && data.DirectionName.length > 0) ? "<b>Hướng:</b>" + data.DirectionName + "<br/>" : "";
    //let road = (data.InforRoadName != null && data.InforRoadName.length > 0) ? "<b>Tên đường:</b>" + data.InforRoadName + "<br/>" : "";
    //let roadWidth = (data.InforRoadWidth != null && data.InforRoadWidth.length > 0) ? "<b>Đường rộng:</b>" + data.InforRoadWidth + "m <br/>" : "";
    //let price = (data.ProductREPriceStatus && data.ProductREPriceMin >= -1 && data.ProductREPriceMax >= -1) ? "<b>Giá:</b>" + priceProduct(data.ProductREPriceMin, data.ProductREPriceMax) + "<br/>" : ""
    //let phone = (data.TelContact != null && data.TelContact.length > 0) ? "<b>SDT:</b>" + data.TelContact + "<br/>" : "";
    let price = (data.ProductREPriceStatus && data.ProductREPriceMin >= -1 && data.ProductREPriceMax >= -1) ? priceProduct(data.ProductREPriceMin, data.ProductREPriceMax) : "";

    let html = "";
    html += (data.ProductREHeight > 0 && data.ProductREWidth > 0) ? "<div><div><i class='fa fa-home' aria-hidden='true'></i></div>Diện tích " + (data.ProductREHeight * data.ProductREWidth) + "m<sup>2</sup> </div>" : "";
    html += (data.DirectionName != null && data.DirectionName.length > 0) ? "<div><div><i class='fa fa-compass' aria-hidden='true'></i></div>" + data.DirectionName + "</div>" : "";
    html += (data.InforRoadWidth != null && data.InforRoadWidth.length > 0) ? "<div><div><i class='fa fa-road' aria-hidden='true'></i></div> Đường " + data.InforRoadWidth + "</div>" : "";
    html += (data.InforRoadName != null && data.InforRoadName.length > 0) ? "<div><div><i class='fa fa-map-marker' aria-hidden='true'></i></div>" + data.InforRoadName + "</div>" : "";
    html += (data.TelContact != null && data.TelContact.length > 0) ? "<div><div><i class='fa fa-mobile' aria-hidden='true'></i></div>" + data.TelContact + "</div>" : "";
    $(data.ListPlace).each(function(){
        html += "<div><div><i class='fa "+this.PlaceIcon+"' aria-hidden='true'></i></div>" + this.PlaceName + "</div>";
    })
    html += "";

    $(".details-product").attr("data-id", data.ProductREID);
    $(".details-product .image-details-product img").attr("src", (data.ProductREImage != null && data.ProductREImage != "") ? data.ProductREImage : "/Content/image/fb630279-c8ab-40f6-a272-6fea841804d7.jpg");
    $(".details-product .title-content").text(data.ProductREName);
    $(".details-product .user-name-content").text(data.NameContact);
    $(".details-product .infor-details-content").children().remove();
    //$(".details-product .infor-details-content").append("<span>" + acre + "" + direction + "" + road + "" + roadWidth + "" + price + "" + phone + "</span>");
    $(".details-product .infor-details-content").append(html);
    $(".details-product .price-checkproduct-content p").show();
    $(".details-product .price-checkproduct-content p .price-product").text(price);
    if (price.length <= 0) $(".details-product .price-checkproduct-content p").hide();
    //album image
    GetImageProduct(data.ProductREID);
    $(".details-product .short-details-content").children().remove();
    $(".details-product .short-details-content").append(data.ProductREShortDescription);
    $(".details-product .full-details-content").children().remove();
    $(".details-product .full-details-content").append(data.ProductREFullDescription);
    $(".details-product .number-view").text((data.ProductREView > 0) ? "Đã có " + data.ProductREView : "Chưa có");
    ChangeTrack(ListTrack);


}
function aminateDetailProduct() {
    let $detail = $(".details-product");
    if ($detail.css('display') == 'none') {
        $detail.css({ "left": "-330px" });
        $detail.show();
        $detail.animate({
            left: "0px",
        }, 1000, function () {
            // Animation complete.
        });
    }
}
function CloseDetailProduct() {
    let $detail = $(".details-product");
    if ($detail.css('display') == 'none') {
    } else {
        $detail.animate({
            left: "-300px",
        }, 1000, function () {
            $detail.hide();
        });
    }
}
function MoveToDetailProduct(id) {
    $.ajax({
        url: '/Home/GetDrawingLatLngByID?id='+id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            if (result != null && result.length > 0) {
                let value1 = getCenterPolygon(result);
                moveToLocationCenter(value1);
                if (map.getZoom() < 19) {
                    SetZoomOption(19);
                }
                getDataSearch();
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function getCenterPolygon(triangleCoords) {
    var bounds = new google.maps.LatLngBounds();
    for (var i = 0; i < triangleCoords.length; i++) {
        bounds.extend(triangleCoords[i]);
    }
    return bounds.getCenter();
}
//---------------End detail product--------------
//---------------view product--------------------
function UpdateViewProduct(id) {
    $.ajax({
        url: '/Home/UpdateViewProduct?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            if (!result) {
                console.log("Fail update view product.");
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function checkViewProduct(id) {
    let idproduct=getCookie("idproduct");
    if (idproduct != "" && idproduct.length > 0) {
        if (idproduct.indexOf(id) >= 0)
            return;
        else {
            idproduct = idproduct + "," + id + ";";
            setCookie("idproduct", idproduct);
            UpdateViewProduct(id);
        }
    } else {
        setCookie("idproduct", id);
        UpdateViewProduct(id);
    }
}
//---------------End view product----------------
//---------------Get product new and selling------
function GetProductNew(param) {
    $.ajax({
        url: '/Home/GetProductFooter',
        type: 'POST',
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: true,
        success: function (result) {
            addHtmlProductItem(result.list);
            lazyDataNew();
            lazyScroll = false;
            $(".bs-modal-product .total-right-content").text(result.total + " kết quả");
            $(".bs-modal-product .total-right-content").attr("data-total", result.total);
            $(".bs-modal-product .left-select").hide();
            $(".bs-modal-product .right-content").css({ "width": "100%" });

        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function lazyDataNew() {
    $(".right-content-content").unbind().scroll(function () {
        let top = $(".right-content-content").scrollTop();
        let total = $(".bs-modal-product .total-right-content").attr("data-total");
        if ((top + $(".right-content-content").height()) >= ($(".right-content-content-item").height() - (110 * 2)) && !lazyScroll && productItem.length < total) {
            lazyScroll = true;
            paramProductSearch.Start = paramProductSearch.Start + 1;
            GetProductNew(paramProductSearch);
        }
    })
}
//---------------End Get product new and selling--
//---------------move area or project-------------
function MoveToAreaOrProduct(id) {
    $.ajax({
        url: '/Home/GetDrawingLatLngByID?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            if (result != null && result.DrawingLat != 0) {
                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < result.length; i++) {
                    bounds.extend(result[i]);
                }
                var myLatlng = bounds.getCenter();
                moveToLocation(myLatlng.lat(), myLatlng.lng());
                SetZoomOption(17);
                getDataSearch();
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//---------------end move area or project---------

//---------------image product-------------------
function GetImageProduct(id) {
    $.ajax({
        url: '/Home/GetImageProduct/' + id,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            console.log(result);
            addHtmlImage(result);
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function addHtmlImage(data) {
    let html = "";
    let url="/Content/UploadedFiles/Product/";
    $(data).each(function () {
        html += "<a href='" + url + this.ImageUrl + "' data-fancybox='group'><img src='" + url + this.ImageUrl + "'></a>"
    })
    $(".album-image").children().remove();
    $(".album-image").append(html);
}
//---------------end product---------------------

