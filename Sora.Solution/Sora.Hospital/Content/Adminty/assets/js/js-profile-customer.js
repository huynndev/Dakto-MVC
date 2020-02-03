
$('#check-change-pass').change(function () {
    if (!this.checked) {
        $('#div-change-pass').addClass('d-none');
        $('#oldpassword').prop('required', false);
        $('#newpassword').prop('required', false);
        $('#retypenewpassword').prop('required', false);
        document.querySelector('#oldpassword').setCustomValidity('');
        document.querySelector('#newpassword').setCustomValidity('');
        document.querySelector('#retypenewpassword').setCustomValidity('');
        $(this).attr('value', 'false');
    } else {
        $('#div-change-pass').removeClass('d-none');
        $('#oldpassword').prop('required', true);
        $('#newpassword').prop('required', true);
        $('#retypenewpassword').prop('required', true);
        $(this).attr('value', 'true');
    }
})

// function validate email
function emailIsValid(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return 'Email không đúng định dạng';
    } else {
        return '';
    }
}
// function validate phone
function numberPhoneIsValid(phoneNumber) {
    var regex = /^([0-9]{10})|(\([0-9]{3}\)\s+[0-9]{3}\-[0-9]{4})+$/;
    if (!regex.test(phoneNumber)) {
        return 'Số điện thoại chỉ chứa các số 0-9, + hoặc -';
    } else
        if (phoneNumber.length < 9) {
            return 'Số điện thoại phải nhiều hơn 9 số';
        } else
            if (phoneNumber.length > 14) {
                return 'Số điện thoại phải ít hơn 14 số';
            }
    return '';
}

function passwordValid(password) {
    if (password.length < 8) {
        return 'Mật khẩu phải có ít nhật 8 ký tự';
    }
    return '';
}

function retypePasswordValid(passwordRetype, password) {
    if (password !== passwordRetype) {
        return 'Mật khẩu không khớp';
    }
    return '';
}


//validate form

var elements = $('form#form-infor').find('input');
for (var i = 0; i < elements.length; i++) {
    $(elements[i]).on('change valid invalid', function (e) {

        e.target.setCustomValidity('');
        if (e.target.value.trim() === '') {
            e.target.setCustomValidity('Trường này là bắt buộc');
        }
    });
    $(elements[i]).on('input',
        function (e) {
            e.target.setCustomValidity('');
        });
}

$('form#form-infor #email').on('change valid invalid', function () {
    this.setCustomValidity('');
    if (this.value.trim() !== '') {
        this.setCustomValidity(emailIsValid(this.value.trim()));
    }
});

$('form#form-infor #phone').on('change valid invalid', function () {
    this.setCustomValidity('');
    if (this.value.trim() === '') {
        this.setCustomValidity('Trường này là bắt buộc');
    } else {
        this.setCustomValidity(numberPhoneIsValid(this.value.trim()));
    }
});

$('form#form-infor #newpassword').on('change valid invalid', function () {
    if ($('#newpassword').prop('required')) {
        this.setCustomValidity('');
        if (this.value.trim() === '') {
            this.setCustomValidity('Trường này là bắt buộc');
        } else {
            this.setCustomValidity(passwordValid(this.value.trim()));
        }
    }

});

$('form#form-infor #newpassword').on('change valid invalid', function () {
    if ($('#newpassword').prop('required')) {
        this.setCustomValidity('');
        if (this.value.trim() === '') {
            this.setCustomValidity('Trường này là bắt buộc');
        } else {
            this.setCustomValidity(passwordValid(this.value.trim()));
        }
    }

});

$('form#form-infor #retypenewpassword').on('change valid invalid', function () {
    if ($('#retypenewpassword').prop('required')) {
        this.setCustomValidity('');
        if (this.value.trim() === '') {
            this.setCustomValidity('Trường này là bắt buộc');
        } else {
            this.setCustomValidity(retypePasswordValid(this.value, $('#newpassword').val()));
        }
    }
});

function changeProvince() {
    $.ajax({
        method: 'GET',
        url: '/Customer/GetDistrictByProvince',
        data: {
            provinceId: parseInt($('#CustomerCity option:selected').attr('name'))
        }
    }).done(function (res) {
        var data = $.parseJSON(res);
        var options = '<option name="" value="" >Chọn quận/huyện...</option>';
        $.each(data, function (i, item) {
            options = options + '<option name="' + item.Key + '" value="' + item.Value + '">' + item.Value + '</option>';
        });
        $('#district').html(options);
    });
}

//=======================================================================================
// connect ERP
var erpCustomer = new Object();
var currentPage = 1;
var pageSize = 20;
var search = '';
var isloading = false;
$('.form-infor').on('click', '#connect-erp', function () {
    erpCustomer = loadERPCustomer(currentPage, pageSize, search);
    var codeHtml = '';
    for (var i = 0; i < erpCustomer.Items.length; i++) {
        codeHtml += '<option value=' + erpCustomer.Items[i].Id + '>' + erpCustomer.Items[i].CustomerName + '</option>';
    }
    $('#list-customer #option-default').after(codeHtml);
    isloading = erpCustomer.TotalPages == currentPage;
    $('#modalConnectERP').modal('show');
    $('#list-customer').selectpicker('refresh');
    $('#bs-select-1').on('scroll', function () {
        if (!isloading) {
            var $this = $(this);
            var $results = $("#bs-select-1 ul");
            if ($this.scrollTop() + $this.height() == $results.height()) {
                isloading = true;
                currentPage++;
                appendERPCustomer();
            }
        }
    })
});

function appendERPCustomer() {
    erpCustomer = loadERPCustomer(currentPage, pageSize, search);
    isloading = erpCustomer.TotalPages == currentPage;
    appendListCustomer(erpCustomer);
    console.log(erpCustomer);
    $('#list-customer').selectpicker('refresh');
}

function loadERPCustomer(page, pageSize, search) {
    var result;
    $.ajax({
        url: '/admin/customer/getallerpcustomer?page=' + page + '&pagesize=' + pageSize + '&search=' + search,
        method: 'GET',
        success: function (data) {
            result = data.Result;
        },
        async: false
    })
    return result;
}

function appendListCustomer(data) {
    var codeHtml = '';
    for (var i = 0; i < data.Items.length; i++) {
        codeHtml += '<option value=' + data.Items[i].Id + '>' + data.Items[i].CustomerName + '</option>';
    }
    $('#list-customer').append(codeHtml);
}

function loadMoreErpCustomer() {
    currentPage++;
    erpCustomer = loadERPCustomer(currentPage, pageSize, search);
    appendListCustomer();
    $('#list-customer').selectpicker('refresh');
}

$('body').on('keyup', '.bootstrap-select .dropdown-menu .bs-searchbox input', function () {
    search = $('.bootstrap-select .dropdown-menu .bs-searchbox input').val();
    currentPage = 1;
    erpCustomer = loadERPCustomer(currentPage, pageSize, search);
    isloading = erpCustomer.TotalPages == currentPage;
    var codeHtml = '<option id="option-default">Chọn khách hàng</option>';
    for (var i = 0; i < erpCustomer.Items.length; i++) {
        codeHtml += '<option value=' + erpCustomer.Items[i].Id + '>' + erpCustomer.Items[i].CustomerName + '</option>';
    }
    $('#list-customer').html(codeHtml);
    $('#list-customer').selectpicker('refresh');
})

$('#choose-customer').click(function () {
    $(this).addClass('active');
    $('#tab-choose-customer').addClass('active');
    $('#create').removeClass('active');
    $('#tab-create').removeClass('active');
})
$('#create').click(function () {
    $(this).addClass('active');
    $('#tab-create').addClass('active');
    $('#tab-choose-customer').removeClass('active');
    $('#choose-customer').removeClass('active');
})

//$('#form-connect-erpcustomer').submit(function (e) {
//    e.preventDefault();
//    alert($('select[name="erpCustomerId"]').val());
//})