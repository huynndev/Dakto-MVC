//1 add html profile and change pass
//2 event profile and change pass
//3 get and data form
//4 save data profile
//5 save data change pass
//------------1
function AddHtmlProfile() {
    let str = `<div class ="modal-dialog modal-lg bs-modal-profile" role="document">
                    <div class ="modal-content">
                        <div class ="modal-header">
                            <div class ="img"><img src="/Content/image/logo.png" /></div>
                        </div>
                        <div class ="modal-body">
                            <div class ="parent-image-profile">
                                <div class ="avater-image">
                                    <img src="/Content/image/logo-profile.jpg" />
                                    <input type="file" id="imageUploadProfile" name="Profile-avater" accept="image/*" style="display:none"/>
                                </div>
                                <div class ="context-profile">
                                    <div class ="form-group">
                                        <label>Tên hiển thị <span class ="icon-required">*</span></label>
                                        <input type="text" placeholder="Tên hiển thị" name="DisplayName" autocomplete="off" class ="form-control">
                                    </div>
                                    <div class ="form-group">
                                        <label>Tên đăng nhập <span class ="icon-required">*</span></label>
                                        <input type="text" placeholder="Tên đăng nhập" name="UserName" autocomplete="off" class ="form-control">
                                    </div>
                                </div>
                            </div>
                            <div class ="form-group">
                                <label>Email <span class ="icon-required">*</span></label>
                                <input type="text" placeholder="Email" name="Email" autocomplete="off" class ="form-control">
                            </div>
                            <div class ="form-group">
                                <label>Số điện thoại <span class ="icon-required">*</span></label>
                                <input type="text" placeholder="Số điện thoại" name="Phone" autocomplete="off" class ="form-control">
                            </div>
                            <div class ="form-group profile-submit">
                                <button type="button" class ="btn btn-primary btn-profile-save">Chỉnh sửa</button>
                                <button type="button" data-dismiss="modal" class ="btn btn-primary btn-profile-cancel">Đóng</button>
                            </div>
                        </div>
                    </div>
                </div>`
    return str;
}
function AddHtmlChangePass() {
    let str = `<div class ="modal-dialog modal-lg bs-modal-changePass" role="document">
                    <div class ="modal-content">
                        <div class ="modal-header">
                            <div class ="img"><img src="/Content/image/logo.png" /></div>
                        </div>
                        <div class ="modal-body">
                            <div class ="form-group">
                                <label>Mật khẩu hiện tại <span class ="icon-required">*</span></label>
                                <input type="password" placeholder="Mật khẩu hiện tại" name="PassCurrent" autocomplete="off" class ="form-control">
                            </div>
                            <div class ="form-group">
                                <label>Mật khẩu mới <span class ="icon-required">*</span></label>
                                <input type="password" placeholder="Mật khẩu mới" name="PassNew" autocomplete="off" class ="form-control">
                            </div>
                            <div class ="form-group">
                                <label>Nhập lại mật khẩu mới <span class ="icon-required">*</span></label>
                                <input type="password" placeholder="Nhập lại" name="PassNewReview" autocomplete="off" class ="form-control">
                            </div>
                            <div class ="form-group profile-submit">
                                <button type="button" class ="btn btn-primary btn-change-pass">Chỉnh sửa</button>
                                <button type="button" data-dismiss="modal" class ="btn btn-primary btn-profile-cancel">Đóng</button>
                            </div>
                        </div>
                    </div>
                </div>`
    return str;
}
//------------End 1
//------------2
function EventProfile() {
    $(".bs-modal-profile").on("click", ".avater-image", function () {
        console.log("s");
        $("input[name='Profile-avater']")[0].click();
    })
    .on("change", "input[name='Profile-avater']", function () {
        let check = $(this).val();
        if ((check.trim()).length > 1) {
            var files = $("#imageUploadProfile").get(0).files;
            let base64 = "";
            if (files.length > 0) {
                previewFile(files[0]);
            }
        }
    })
    .on("click", ".btn-profile-save", function () {
        if (checkFormProfile()) {
            let form = new FormData();
            var files = $("#imageUploadProfile").get(0).files;
            if (files.length > 0) {
                form.append("Avatar", files[0]);
            } else {
                form.append("Avatar", $(".avater-image > img").attr("src"));
            }
            form.append("DisplayName", $("input[name='DisplayName']").val());
            form.append("UserName", $("input[name='UserName']").val());
            form.append("Email", $("input[name='Email']").val());
            form.append("Phone", $("input[name='Phone']").val());
            ProfileEdit(form);
        }
    })
    $(".bs-modal-changePass").on("click", ".btn-change-pass", function () {
        if (checkFormChangePass()) {
            let form = new FormData();
            form.append("PassCurrent", $("input[name='PassCurrent']").val());
            form.append("PassNew", $("input[name='PassNew']").val());
            SeviceChangePass(form);
        }
    })
}
//------------End 2
//------------3
function GetDataProfile() {
    let user = (typeof $(".user-id").attr("data-id") != "undefined") ? $(".user-id").attr("data-id") : "";
    if (user != "" && typeof user != "undefined") {
        $.ajax({
            url: '/Proflie/GetUserByID?id=' + user,
            type: 'GET',
            dataType: 'json',
            contentType: false,
            processData: false,
            async: true,
            success: function (result) {
                if (result != null) {
                    $("input[name='DisplayName']").val(result.DisplayName);
                    $("input[name='UserName']").val(result.UserName);
                    $("input[name='Email']").val(result.Email);
                    $("input[name='Phone']").val(result.Phone);
                    if (result.Avatar != null && result.Avatar != "") {
                        var date = new Date();
                        $(".avater-image > img").attr("src", result.Avatar + "?" + date);
                    }
                }
            },
            error: function (error) {
                console.log("ERROR:" + error);
            }
        });
    }
}
//------------End 3
//------------4
//sevice profile
function ProfileEdit(form) {
    $.ajax({
        url: '/Proflie/ProjectEdit',
        type: 'POST',
        data: form,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                location.reload();
                //$('.modal-common-user').modal("hide");
                //location.reload();
            } else {
                bootbox.alert("Bạn đăng ký thất bại");
            }
        },
        error: function (error) {
            // $('.loading-screen').addClass('hide');
            bootbox.alert("Lỗi khi đăng nhập", function () {
            });
        }
    });
}
//get file image
function previewFile(file) {
    var reader = new FileReader();
    reader.addEventListener("load", function () {
        $(".avater-image>img").attr("src", reader.result)
    }, false);
    if (file) {
        reader.readAsDataURL(file);
    }
}
//check input profile
function checkFormProfile() {
    let check = true;
    let UserName = $("input[name='UserName']").val();
    if (!validateText(UserName, "", 0, 0)) { insertError($("input[name='UserName']"), "input"); check = false; }
    let DisplayName = $("input[name='DisplayName']").val();
    if (!validateText(DisplayName, "", 0, 0)) { insertError($("input[name='DisplayName']"), "input"); check = false; }
    let Email = $("input[name='Email']").val();
    if (!validateText(Email, "email", 0, 0)) { insertError($("input[name='Email']"), "input"); check = false; }
    let Phone = $("input[name='Phone']").val();
    if (!validateText(Phone, "phone", 0, 20)) { insertError($("input[name='Phone']"), "input"); check = false; }
    let count = $(".bs-modal-register .form-group.has-error").length;
    if (count > 0) check = false;
    return check;
}
//------------End 4
//------------5
function checkFormChangePass() {
    let check = true;
    let PassCurrent = $("input[name='PassCurrent']").val();
    if (!validateText(PassCurrent, "", 0, 0)) { insertError($("input[name='PassCurrent']"), "input"); check = false; }
    let PassNew = $("input[name='PassNew']").val();
    if (!validateText(PassNew, "", 0, 0)) { insertError($("input[name='PassNew']"), "input"); check = false; }
    let PassNewReview = $("input[name='PassNewReview']").val();
    if (PassNew != PassNewReview) { insertError($("input[name='PassNewReview']"), "input"); check = false; }
    return check;
}
function SeviceChangePass(form) {
    $.ajax({
        url: '/Proflie/ChangePass',
        type: 'POST',
        data: form,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                //location.reload();
                $('.modal-common-user').modal("hide");
                //location.reload();
            } else {
                bootbox.alert("Bạn đăng ký thất bại");
            }
        },
        error: function (error) {
            // $('.loading-screen').addClass('hide');
            bootbox.alert("Lỗi khi đăng nhập", function () {
            });
        }
    });
}
//------------End 5