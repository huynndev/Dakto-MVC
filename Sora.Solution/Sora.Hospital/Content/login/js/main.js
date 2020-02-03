
(function ($) {
    "use strict";


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit', function () {
       
        if (checkFormLogin()) {
            let form = new FormData();
            form.append("UserName", $("input[name='UserName']").val());
            form.append("Password", $("input[name='Password']").val());
            checkLoginSevice(form);
        }
        return false;
        
    });


    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {

        if ($(input).val().trim() == '') {
            return false;
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }

    $("input[name='Password']").on('keydown', function (e) {
        if (e.which == 13) {
            if (checkFormLogin()) {
                let form = new FormData();
                form.append("UserName", $("input[name='UserName']").val());
                form.append("Password", $("input[name='Password']").val());
                checkLoginSevice(form);
            }
            return false;
        }
    });

    function checkFormLogin() {
        var check = true;
        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }
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
                    if (returnUrl != "nodata")
                        location.href = returnUrl;
                    else
                        location.href = '../Manager';
                } else {
                    $(".be-login-admin .mes-error").show();
                }
            },
            error: function (error) {
                bootbox.alert("Lỗi khi đăng nhập", function () {
                });
            }
        });
    }

})(jQuery);