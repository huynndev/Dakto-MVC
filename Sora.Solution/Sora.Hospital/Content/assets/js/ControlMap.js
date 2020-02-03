//---------------------------
//-------hoa tokio-----------
//---------------------------
//1) load web get readuis with zoom sercen and save zoom, lat, lng start, readuis
//2) function check readuis research
//3) function check zoom research
//4) function get and convert readuis to meters
//5) function search
//6) move map
var editDrawingManagers = [];
//var AreSelling = null;
//var SellingSoon = null;
//var Sold = null;
var infoWindow;
var valueStart={
	zoom:0,
	lat:0,
	lng:0,
	radius:0
}
//----------1
//get value start after load page
function getValueStart(){
	let lat = map.getCenter().lat();
	let lng = map.getCenter().lng();
	let zoom = map.getZoom();
	let radius = getRadius();
	setValuesStart(lat,lng,zoom,radius);
}
//save value start
function setValuesStart(lat,lng,zoom,radius){
	valueStart.lat=lat;
	valueStart.lng=lng;
	valueStart.zoom=zoom;
	valueStart.radius=radius;
}
//----------End 1
//----------2
//check radius research
function checkRadiusSearch(){
	let centerOld=new google.maps.LatLng(valueStart.lat, valueStart.lng);
	let centerNew=new google.maps.LatLng(map.getCenter().lat(), map.getCenter().lng());
	let distance=getDistance(centerOld,centerNew);
	let radius = getRadius();
	return (distance>=(radius/3))?true:false;
}
//-----------End 2
//-----------3
function checkZoomSearch(){
	let zoom =map.getZoom();
	if(zoom < valueStart.zoom) return true;
	return false;
}
//-----------End 3
//-----------4
//get radius with NorthEast and SouthWest
function getRadius(){
	let point_a =new google.maps.LatLng(map.getBounds().getNorthEast().lat(), map.getCenter().lng()); //width (Ox)
	let point_b =new google.maps.LatLng(map.getCenter().lat(), map.getCenter().lng());//center (O)
	let radius_1=getDistance(point_a,point_b);
	let point_c = new google.maps.LatLng(map.getCenter().lat(),map.getBounds().getSouthWest().lng());//height (Oy)
	let radius_2=getDistance(point_c,point_b);
	if(radius_1 > radius_2) return radius_1;
	return radius_2;
}
//from point to meter
function getDistance(point_a, point_b){
		let distance_meter = google.maps.geometry.spherical.computeDistanceBetween(point_a, point_b);
		return distance_meter;
}
//-----------End 4
//-----------5
//get data search
function getDataSearch(){
	let lat = map.getCenter().lat();
	let lng = map.getCenter().lng();
	let zoom = map.getZoom();
	let radius = getRadius();
	setDataSearch(lat,lng,zoom,radius);
}
//set data search
function setDataSearch(lat,lng,zoom,radius){
	setValuesStart(lat,lng,zoom,radius)
	//let param={lat:lat,lng:lng,radius:radius};
	serviceSearch(lat, lng, radius);
}
//ajax search
function serviceSearch(lat, lng, radius) {
    $.ajax({
        url: '/Home/GetSearchDrawing?lat=' + lat + '&lng='+lng+'&radius='+radius+'',
        type: 'GET',
        //data: { lat: lat, lng: lng, radius: radius },
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            romveAllMap();
            for (var i = result.length - 1; i >= 0; i--) {
                let obj = result[i];
                let color = (obj.ProductREStatus == 0) ? Setting.AreSelling : ((obj.ProductREStatus == 1) ? Setting.SellingSoon : ((obj.ProductREStatus == 4) ? obj.ProductREShortDescription : Setting.Sold));
                let idProduct = obj.ProductREID;
                addDrawingMaps(obj, color, idProduct, obj.ProductRECode, i);
            }
            //$(result).each(function (i, obj) {
            //    let color = (obj.ProductREStatus == 0) ? Setting.AreSelling : ((obj.ProductREStatus == 1) ? Setting.SellingSoon : Setting.Sold);
            //    let idProduct = obj.ProductREID;
            //    addDrawingMaps(obj, color, idProduct, obj.ProductRECode,i);
            //})
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
//add drawing on maps
function addDrawingMaps(data, color, idProduct, name, index)
{
    let objectLatLon = JSON.parse(data.ProductRELatLon);
    //let i = 1000;
    //for (var i = objectLatLon.length - 1; i >= 0; i--) {
    //    let id = objectLatLon[i].id;
    //    let type = objectLatLon[i].type;
    //    let value = objectLatLon[i].value;
    //    //setObjectDrawing(value, id, type);
    //    editDrawing(value, id, type, color, color,idProduct,name,i);
    //}
        $(objectLatLon).each(function (i, obj) {
            let id = obj.id;
            let type = obj.type;
            let value = obj.value;
            //setObjectDrawing(value, id, type);
            editDrawing(value, id, type, color, color, idProduct, name, index);
        });
}
// edit drawing
function editDrawing(triangleCoords, id, type, color, fill, idProduct,name,i) {
    var editDrawingManager = new google.maps.Polygon({
        paths: triangleCoords,
        zIndex: google.maps.Polygon.MAX_ZINDEX + i,
        id: id,
        type: type,
        strokeColor: color,
        strokeOpacity: 1,
        strokeWeight: 1,
        fillColor: fill,
        fillOpacity: 0.35,
        idProduct: idProduct,
        name: name
    });
    editDrawingManagers.push(editDrawingManager);
    //google.maps.event.addListener(editDrawingManager, "dragend", function () {
    //    //console.log("dragend");
    //    let vertices1 = this.getPath();
    //    //let contentString1 = [];
    //    //for (var i = 0; i < vertices1.getLength() ; i++) {
    //    //    var xy = vertices1.getAt(i);
    //    //    contentString1.push({ 'lat': xy.lat(), 'lng': xy.lng() });
    //    //}
    //    setObjectDrawing(convertString(vertices1), this.id, this.type);
    //    //console.log(this.id+","+ this.type);
    //});
    google.maps.event.addListener(editDrawingManager, 'click', function () {
        GetDetaiProduct(this.idProduct);
    //    setSelection(this);
        //console.log(this.id + "," + this.type);
    //    let vertices = this.getPath();
    //    var thisEdit = this;
    //    google.maps.event.addListener(vertices, 'set_at', function () {
    //        console.log("set_at");
    //        setObjectDrawing(convertString(vertices), thisEdit.id, thisEdit.type);
    //        console.log(thisEdit.id + "," + thisEdit.type);
    //    });

    //    google.maps.event.addListener(vertices, 'insert_at', function () {
    //        console.log("insert_at");
    //        setObjectDrawing(convertString(vertices), thisEdit.id, thisEdit.type);
    //        console.log(thisEdit.id + "," + thisEdit.type);
    //    });
    //    google.maps.event.addListener(vertices, 'remove_at', function () {
    //        console.log("remove_at");
    //        setObjectDrawing(convertString(vertices), thisEdit.id, thisEdit.type);
    //        console.log(thisEdit.id + "," + thisEdit.type);
    //    });
    });
    //var infowindow = new google.maps.InfoWindow({
    //    content: "hssss"
    //});
    var bounds = new google.maps.LatLngBounds();
    for (var i = 0; i < triangleCoords.length; i++) {
        bounds.extend(triangleCoords[i]);
    }

    var myLatlng = bounds.getCenter();
    let minZoom=(Setting.MinZoom.length > 0) ? parseInt(Setting.MinZoom) : 16;
    var mapLabel = new MapLabel({
        text: name,
        position: myLatlng,
        map: map,
        fontSize: 15,
        align: 'left',
        minZoom: (minZoom+1)
    });
    mapLabel.set('position', myLatlng);

    google.maps.event.addListener(editDrawingManager, 'mouseover', function (e) {
        showArrays(this.name, e);
        editDrawingManager.set('fillColor', Setting.ColorHover);
        editDrawingManager.set('strokeColor', Setting.ColorHover);
    });
    google.maps.event.addListener(editDrawingManager, 'mouseout', function (e) {
        hideArray();
        editDrawingManager.set('fillColor', color);
        editDrawingManager.set('strokeColor', color);
    });
    //google.maps.event.addListener(editDrawingManager, 'drawingmode_changed', clearSelection);
    //google.maps.event.addListener(map, 'click', clearSelection);
    editDrawingManager.setMap(map);
    //editDrawingManager.addListener('mousemove', showArrays);
    infoWindow = new google.maps.InfoWindow;
}
function showArrays(thi, event) {
    infoWindow.setContent(thi);
    infoWindow.setPosition(event.latLng);
    infoWindow.open(map);
}
function hideArray() {
    infoWindow.close(map)
}
function romveAllMap() {
    for (var i = 0; i < editDrawingManagers.length; i++) {
        editDrawingManagers[i].setMap(null);
    }
}
//-----------End 5
//-----------6
function moveToLocation(lat, lng) {
    if (lat <= -1 || lng <= -1) return;
    var center = new google.maps.LatLng(lat, lng);
    map.panTo(center);
    //map.panTo(center);
    //map.setCenter(center);
}
function moveToLocationCenter(center) {
    map.panTo(center);
}
function ZoomOption() {
    map.setZoom(Number(Setting.ZoomMap));
}
function SetZoomOption(zoom) {
    map.setZoom(Number(zoom));
}
//-----------End 6
//-----------get setting color status
//function getSettingColor() {
//    $.ajax({
//        url: '/SettingAdmin/GetSettingStatus',
//        type: 'GET',
//        dataType: 'json',
//        contentType: false,
//        processData: false,
//        async: true,
//        success: function (result) {
//            AreSelling = (result.find(function (obj) { return obj.SettingKey === "AreSelling"; })).SettingValue;
//            SellingSoon = (result.find(function (obj) { return obj.SettingKey === "SellingSoon"; })).SettingValue;
//            Sold = (result.find(function (obj) { return obj.SettingKey === "Sold"; })).SettingValue;
//        },
//        error: function (error) {
//            console.log("ERROR:" + error);
//        }
//    });
//}
//function getSettingPage() {
//    $.ajax({
//        url: '/Setting/GetSetting',
//        type: 'GET',
//        dataType: 'json',
//        contentType: false,
//        processData: false,
//        async: true,
//        success: function (result) {
//            Setting = result;
//            ZoomOption();
//        },
//        error: function (error) {
//            console.log("ERROR:" + error);
//        }
//    });
//}

function GetShapeManager(id) {
    $.ajax({
        url: '/Product/GetShapeAll',
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: true,
        success: function (result) {
            romveAllMap();
            for (var i = result.length - 1; i >= 0; i--) {
                let obj = result[i];
                let color = (obj.ProductREStatus == 0) ? Setting.AreSelling : ((obj.ProductREStatus == 1) ? Setting.SellingSoon : ((obj.ProductREStatus == 4) ? obj.ProductREShortDescription : Setting.Sold));
                let idProduct = obj.ProductREID;
                addDrawingMaps(obj, color, idProduct, obj.ProductRECode, i);
            }
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}