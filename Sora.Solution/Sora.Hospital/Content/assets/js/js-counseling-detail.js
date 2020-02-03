$('#timepicker').timepicker({
    icons: {
        up: 'ti-angle-up',
        down: 'ti-angle-down'
    },
    showMeridian: false
}).on('changeTime.timepicker', function (e) {
    var h = e.time.hours;
    var m = e.time.minutes;
    m += h * 60;
    if (m > 675 && m < 720) {
        $('#timepicker').timepicker('setTime', '11:30');
        return false;
    }
    if (m >= 720 && m < 795) {
        $('#timepicker').timepicker('setTime', '13:15');
        return false;
    }
    if (m < 555) {
        $('#timepicker').timepicker('setTime', '9:15');
        return false;
    }
    if (m > 1050) {
        $('#timepicker').timepicker('setTime', '17:30');
        return false;
    }
});

$('.carousel').carousel({
    interval: false
});
function removeClassImage() {
    $('#carouselTagertRqsPicture1').removeClass('active');
    $('#carouselTagertRqsPicture2').removeClass('active');
    $('#carouselTagertRqsPicture3').removeClass('active');
    $('#carouselTagertRqsPicture4').removeClass('active');
    $('#carouselTagertRqsPicture5').removeClass('active');
    $('#carouselTagertRqsPicture6').removeClass('active');

    $('#carouselRqsPicture1').removeClass('active');
    $('#carouselRqsPicture2').removeClass('active');
    $('#carouselRqsPicture3').removeClass('active');
    $('#carouselRqsPicture4').removeClass('active');
    $('#carouselRqsPicture5').removeClass('active');
    $('#carouselRqsPicture6').removeClass('active');
}

$('#vỉewImageFullRqsPicture1').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture1').addClass('active');
    $('#carouselRqsPicture1').addClass('active');
})
$('#vỉewImageFullRqsPicture2').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture2').addClass('active');
    $('#carouselRqsPicture2').addClass('active');
})
$('#vỉewImageFullRqsPicture3').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture3').addClass('active');
    $('#carouselRqsPicture3').addClass('active');
})
$('#vỉewImageFullRqsPicture4').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture4').addClass('active');
    $('#carouselRqsPicture4').addClass('active');
})
$('#vỉewImageFullRqsPicture5').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture5').addClass('active');
    $('#carouselRqsPicture5').addClass('active');
})
$('#vỉewImageFullRqsPicture6').click(function () {
    removeClassImage();
    $('#carouselTagertRqsPicture6').addClass('active');
    $('#carouselRqsPicture6').addClass('active');
})