//1 add html modal
//2 get infor contact
//3 get data and check data
//4 save contact and track
//5 set infor company

//-----------1
function AddModalContact() {
    let html = `<div class="modal-dialog modal-lg bs-modal-contact" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="logo-header"><img src="/Content/image/logo2.png" /></div>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-5 content-contact">
                            <div class="form-group">
                                <label>Họ và tên <span class="icon-required">*</span></label>
                                <input type="text" placeholder="họ và tên" name="ContactUserName" class ="form-control" value="">
                            </div>
                            <div class="form-group">
                                <label>Số điện thoại <span class="icon-required">*</span></label>
                                <input type="text" placeholder="số điện thoại" name="ContactPhone" class ="form-control" value="">
                            </div>
                            <div class="form-group">
                                <label>Email <span class="icon-required">*</span></label>
                                <input type="text" placeholder="email" name="ContactEmail" class ="form-control" value="">
                            </div>
                            <div class="form-group">
                                <label>Nội dùng <span class="icon-required">*</span></label>
                                <textarea class ="form-control" rows="3" name="ContactContext"></textarea>
                            </div>
                            <button type="button" class="btn btn-primary btn-send">Gửi tin nhắn</button>
                        </div>
                        <div class="col-sm-7 infor-contact">
                            <div class="infor-content">
                                <div class="title-infor-contact">sàn giao dịch  bất động sản bảo ngọc land</div>
                                <div class ="content-infor-contact"><span>A: </span><span class="CompanyAddress">51 Nguyễn Đức Cảnh</span></div>
                                <div class ="content-infor-contact"><span>P: </span><span class="CompanyPhone">01203210203 - 01291233333</span></div>
                                <div class ="content-infor-contact"><span>E: </span><span class="CompanyEmail">infor@bangoc.com</span></div>
                                <div class ="content-infor-contact"><span>w: </span><span class="CompanyWeb">www.baongocland.com</span></div>
                            </div>
                            <div class ="social-infor-contact"><div class ="left-contact"><div class ="skype-infor"><i class ="fa fa-skype" aria-hidden="true"></i></div><span>Skype</span></div><div class ="right-contact CompanySkype">baongocland(liên hệ quá skype) </div></div>
                            <div class ="social-infor-contact"><div class ="left-contact"><div class ="facebook-infor"><i class ="fa fa-facebook" aria-hidden="true"></i></div><span>Facebook</span></div><div class ="right-contact CompanyFacebook"><a href="#">Facebook.com/baongocland.realestate</a></div></div>
                            <div class ="social-infor-contact"><div class ="left-contact"><div class ="twitter-infor"><i class ="fa fa-twitter" aria-hidden="true"></i></div><span>Twitter</span></div><div class ="right-contact CompanyTwitter">baongoclandn(liên lạc qua Twitter) </div></div>
                            <div class ="social-infor-contact"><div class ="left-contact"><div class ="youtube-infor"><i class ="fa fa-youtube" aria-hidden="true"></i></div><span>Youtube</span></div><div class ="right-contact CompanyYoutube"><a href="#">youtube.com/baongoclandn</a></div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>`
    return html;
}
//-----------End 1
//-----------2
//function getInforContact() {
//    $.ajax({
//        url: '/Home/GetTypeRE',
//        type: 'GET',
//        dataType: 'json',
//        contentType: false,
//        processData: false,
//        async: true,
//        success: function (result) {
//            AddHtmlProduct(1, result);
//        },
//        error: function (error) {
//            console.log("ERROR:" + error);
//        }
//    });
//}
//-----------End 2
//-----------3
function EventReset() {
    $(".bs-modal-contact").on("click", ".btn-send", function () {
        if (checkForm()) {
            $('.modal-common').modal('toggle');
            let ContactUserName = $("input[name='ContactUserName']").val();
            let ContactEmail = $("input[name='ContactEmail']").val();
            let ContactContext = $("textarea[name='ContactContext']").val();
            let ContactPhone = $("input[name='ContactPhone']").val();
            let ProductREID = $(".details-product").attr("data-id");
            let prama = { ContactUserName: ContactUserName, ContactEmail: ContactEmail, ContactContext: ContactContext, ContactPhone: ContactPhone, ProductREID: ProductREID };
            SaveInforContact(prama);
        }
    })
    .on("focus", "input", function () {
        $(this).parent().removeClass("has-error");
    })
    .on("focus", "textarea", function () {
        $(this).parent().removeClass("has-error");
    })
}
function checkForm() {
    let check = true;
    let ContactUserName = $("input[name='ContactUserName']").val();
    if (!validateText(ContactUserName, "", 0, 0)) { insertError($("input[name='ContactUserName']"), "input"); check = false; }
    let ContactEmail = $("input[name='ContactEmail']").val();
    if (!validateText(ContactEmail, "email", 0, 1)) { insertError($("input[name='ContactEmail']"), "input"); check = false; }
    let ContactContext = $("textarea[name='ContactContext']").val();
    if (!validateText(ContactContext, "", 0, 1)) { insertError($("textarea[name='ContactContext']"), "input"); check = false; }
    let ContactPhone = $("input[name='ContactPhone']").val();
    if (!validateText(ContactPhone, "phone", 0, 0)) { insertError($("input[name='ContactPhone']"), "input"); check = false; }
    return check;
}
//-----------3 End
//-----------4
function SaveInforContact(prama) {
    $.ajax({
        url: '/Contact/UpdateContact',
        type: 'POST',
        data: JSON.stringify(prama),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: true,
        success: function (result) {
            $('.modal-common').modal('toggle');
            if(result)
                bootbox.alert("Bạn đã gửi liên hệ thành công");
            else
                bootbox.alert("Bạn đã gửi liên hệ thất bại");
        },
        error: function (error) {
            console.log("ERROR:" + error);
        }
    });
}
function SaveTrack() {

}
//-----------End 4
//-----------5
function SetInforSettingContact() {
    $(".bs-modal-contact .CompanyAddress").text(Setting.CompanyAddress);
    $(".bs-modal-contact .CompanyPhone").text(Setting.CompanyPhone);
    $(".bs-modal-contact .CompanyEmail").text(Setting.CompanyEmail);
    $(".bs-modal-contact .CompanyWeb").text(Setting.CompanyWeb);
    $(".bs-modal-contact .CompanySkype").text(Setting.CompanySkype + "(liên hệ qua skype)");
    $(".bs-modal-contact .CompanyFacebook>a").text(Setting.CompanyFacebook);
    $(".bs-modal-contact .CompanyTwitter").text(Setting.CompanyTwitter + "(liên lạc qua Twitter)");
    $(".bs-modal-contact .CompanyYoutube>a").text(Setting.CompanyYoutube);
}
//-----------End 5
