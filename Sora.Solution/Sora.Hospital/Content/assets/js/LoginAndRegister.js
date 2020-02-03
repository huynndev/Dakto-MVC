//1 add html login or register
//2 get data, check, login
//3 get data, check, register
//4 event login and register
//------------1
function addHtmlLoginAndRegister(check) {
    if (check) { //register
        let str = `<div class="modal-dialog modal-lg bs-modal-register" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="img"><img src="/Content/image/logo.png" /></div>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <input type="text" placeholder="Tên đăng nhập" name="UserName" autocomplete="off" class ="form-control">
                                </div>
                                <div class="form-group">
                                    <input type="text" placeholder="Email" name="Email" autocomplete="off" class ="form-control">
                                </div>
                                <div class="form-group">
                                    <input type="text" placeholder="Số điện thoại" name="Phone" autocomplete="off" class ="form-control">
                                </div>
                                <div class="form-group">
                                    <input type="password" placeholder="Mật khẩu" name="Password" class ="form-control">
                                </div>
                                <div class="form-group register-submit">
                                    <button type="submit" class="btn btn-primary">Đăng ký</button>
                                </div>
                            </div>
                        </div>
                    </div>`
        return str;
    } else {
        let str1 = `<div class="modal-dialog modal-lg bs-modal-login" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="img"><img src="/Content/image/logo.png" /></div>
                            </div>
                            <div class ="modal-body">
                                <div class ="mes-error" style="display: none;">Bạn đã nhập sai tên đăng nhập hoặc mật khẩu</div>
                                <div class="form-group">
                                    <input type="text" name="UserName" placeholder="Tên đăng nhập hoặc số điện thoại" autocomplete="off" class ="form-control">
                                </div>
                                <div class="form-group">
                                    <input type="password" name="Password" placeholder="Mật khẩu" class ="form-control">
                                </div>
                                <div class="form-group row login-tools">
                                    <div class="col-xs-6 login-remember">
                                        <div class="be-checkbox">
                                            <input type="checkbox" id="remember">
                                            <label for="remember">Nhớ mật khẩu</label>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 login-forgot-password"><a href="#">Đăng ký</a></div>
                                </div>
                                <div class="form-group login-submit">
                                    <button type="submit" class="btn btn-primary">Đăng nhập</button>
                                </div>
                            </div>
                        </div>
                    </div>`
        return str1;
    }
}
//------------End 1
//------------2
function checkFormLogin() {
    let check = true;
    let UserName = $("input[name='UserName']").val();
    if (!validateText(UserName, "", 0, 0)) { insertError($("input[name='UserName']"), "input"); check = false; }
    let Password = $("input[name='Password']").val();
    if (!validateText(Password, "", 0, 0)) { insertError($("input[name='Password']"), "input"); check = false; }
    return check;
}
function checkLoginSevice(form) {
    $.ajax({
        url: '/Account/CheckLogin',
        type: 'POST',
        data: form,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                $('.modal-common-user').modal("hide");
                location.reload();
            } else {
                $(".bs-modal-login .mes-error").show();
            }
        },
        error: function (error) {
            // $('.loading-screen').addClass('hide');
            bootbox.alert("Lỗi khi đăng nhập", function () {
            });
        }
    });
}
//------------End 2
//------------3
function checkFormRegister() {
    let check = true;
    let UserName = $("input[name='UserName']").val();
    if (!validateText(UserName, "", 0, 0)) { insertError($("input[name='UserName']"), "input"); check = false; }
    let Password = $("input[name='Password']").val();
    if (!validateText(Password, "", 0, 0)) { insertError($("input[name='Password']"), "input"); check = false; }
    let Email = $("input[name='Email']").val();
    if (!validateText(Email, "email", 0, 0)) { insertError($("input[name='Email']"), "input"); check = false; }
    let Phone = $("input[name='Phone']").val();
    if (!validateText(Phone, "phone", 0, 20)) { insertError($("input[name='Phone']"), "input"); check = false; }
    let count = $(".bs-modal-register .form-group.has-error").length;
    if (count > 0) check = false;
    return check;
}
function RigesterUser(form) {
    $.ajax({
        url: '/Account/RegisterUser',
        type: 'POST',
        data: form,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                $('.modal-common-user').modal("hide");
                location.reload();
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
function checkRegisterSevice(value) {
    let check = false;
    $.ajax({
        url: '/Account/CheckRegister?value=' + value,
        type: 'GET',
        dataType: 'json',
        contentType: false,
        processData: false,
        async: false,
        success: function (result) {
            check = result;
        },
        error: function (error) {
            // $('.loading-screen').addClass('hide');
            bootbox.alert("Lỗi khi đăng nhập", function () {
            });
        }
    });
    return check;
}
//------------End 3
//------------4
function eventLogiAndRegister() {
    $(".bs-modal-login").on("click", ".login-submit>button", function () {
        if (checkFormLogin()) {
            let form = new FormData();
            form.append("UserName", $("input[name='UserName']").val());
            form.append("Password", $("input[name='Password']").val());
            checkLoginSevice(form);
        }
    })
    .on("focus", "input", function () {
        $(this).parent().removeClass("has-error");
    })
    $(".bs-modal-register").on("click", ".register-submit>button", function () {
        if (checkFormRegister()) {
            let form = new FormData();
            form.append("UserName", $("input[name='UserName']").val());
            form.append("Email", $("input[name='Email']").val());
            form.append("Phone", $("input[name='Phone']").val());
            form.append("Password", $("input[name='Password']").val());
            RigesterUser(form);
        }
    })
    .on("focus", "input", function () {
        $(this).parent().removeClass("has-error");
    })
    .on("focusout", "input[name='UserName'],input[name='Email'],input[name='Phone']", function () {
        let value = $(this).val();
        if (value !="" && checkRegisterSevice(value)) {
            insertError($(this), "input"); 
        }
    })
}
//------------End 4
