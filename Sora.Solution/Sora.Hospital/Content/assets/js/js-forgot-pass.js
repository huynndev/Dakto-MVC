$('form#forgotpass input[name="SendToEmail"]').on('change paste keyup valid invalid', function () {
    this.setCustomValidity('');
    if (requiredIsValid(this.value.trim()) != '') {
        this.setCustomValidity(requiredIsValid(this.value.trim()));
        return;
    }
    this.setCustomValidity(emailIsValid(this.value.trim()));
});
function emailIsValid(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (email && email.trim() != '' && !regex.test(email)) {
        return 'Email không đúng định dạng';
    } else {
        return '';
    }
}

function requiredIsValid(value) {
    if (value.trim() === '') {
        return 'Trường này là bắt buộc';
    } else {
        return '';
    }
}