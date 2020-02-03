
$('#ul-selection').on('mouseenter',
    '.ms-elem-selection',
    function (e) {
        e.preventDefault();
        changeSelectionHover($(this).attr('name'));
    });
$('#ul-selectable').on('mouseenter',
    '.ms-elem-selectable',
    function (e) {
        e.preventDefault();
        changeHover($(this).attr('name'));
    });

function changeHover(e) {
    $('.ms-selectable').find('.ms-elem-selectable').removeClass('ms-hover');
    $('li[name="' + e + '"]').addClass('ms-hover');
}

function changeSelectionHover(e) {
    $('.ms-selection').find('.ms-selected').removeClass('ms-hover');
    $('li[name="' + e + '"]').addClass('ms-hover');
}

function showPopup(text, value) {
    $('#permission-name').html(' <label class="w-25 my-auto">Tên nhóm quyền: </label> <label class="col-form-label w-75" id="permission-desc">' + text + ' </label> <label class="col-form-label w-75" id="permission-name" style="display:none;">' + value + ' </label>');
    $('#ul-selection').html('');
    $('#ul-selectable').html('');
    if (window[value].length > 0) {
        var code = '';
        for (var i = 0; i < window[value].length; i++) {
            code += '<li class="ms-elem-selection ms-selected" name="' + window[value][i].Id + '"><span>' + window[value][i].Value + '</span></li>';
        }
        $('#ul-selection').html(code);
    }
    code = '';
    if (listUsers.length > 0) {
        for (var i = 0; i < listUsers.length; i++) {
            var temp = window[value].findIndex(x => x.Id === listUsers[i].UserID);
            if (window[value].findIndex(x => x.Id === listUsers[i].UserID) < 0) {
                code += '<li class="ms-elem-selectable" name="' + listUsers[i].UserID + '"><span>' + listUsers[i].DisplayName + '</span></li>';
            }
        }
        $('#ul-selectable').html(code);
    }
}

$('#ul-selectable').on('click',
    '.ms-elem-selectable',
    function (e) {
        e.preventDefault();
        changeOnClick($(this).attr('name'));
    });
function changeOnClick(e) {
    var value = $('li[name="' + e + '"] span').html();
    $('li[name="' + e + '"]').remove();
    $('#ul-selection').append('<li class="ms-elem-selection ms-selected" name="' + e + '"><span>' + value + '</span></li>');
}

$('#ul-selection').on('click',
    '.ms-elem-selection',
    function (e) {
        e.preventDefault();
        changeOnClickSelection($(this).attr('name'));
    });
function changeOnClickSelection(e) {
    var value = $('li[name="' + e + '"] span').html();
    $('li[name="' + e + '"]').remove();
    $('#ul-selectable').append('<li class="ms-elem-selectable" name="' + e + '"><span>' + value + '</span></li>');
}

$('#btn-cancel').on('click',
    function () {
        $('#editUserPermissionModal').modal('hide');
    });

$('#btn-submit').on('click', function () {
    var arrayUserId = [];
    var permissionName = $('label#permission-name').text().trim();
    var permissionDesc = $('label#permission-desc').text().trim();
    $('ul#ul-selection li').each(function () {

        arrayUserId.push($(this).attr('name'));
    });
    $.ajax({
        url: '/admin/settingadmin/ChangeUserPermission',
        method: 'POST',
        data: {
            listId: arrayUserId,
            permissionName: permissionName,
            permissionDesc: permissionDesc
        },
        success: function (data) {
            $('#editUserPermissionModal').modal('hide');
            swal({
                title: 'Phân quyền người dùng thành công.',
                //text: "Your will not be able to recover this imaginary file!",
                type: 'success',
                confirmButtonClass: 'btn-default',
                confirmButtonText: 'Đóng',
                closeOnConfirm: false
            },
                function () {
                    location.reload();
                });
        },
        error: function (data) {
            $('#editUserPermissionModal').modal('hide');
            swal({
                    title: 'Phân quyền người dùng thất bại.',
                    //text: "Your will not be able to recover this imaginary file!",
                    type: 'error',
                    confirmButtonClass: 'btn-default',
                    confirmButtonText: 'Đóng',
                    closeOnConfirm: false
                },
                function () {
                    location.reload();
                });
        }
    });
    console.log(arrayUserId);
});

$('#select-all').on('click', function() {
    $('#ul-selectable li').remove();
    $('#ul-selection li').remove();
    var code = '';
    if (listUsers.length > 0) {
        for (var i = 0; i < listUsers.length; i++) {
            code += '<li class="ms-elem-selection ms-selected" name="' + listUsers[i].UserID + '"><span>' + listUsers[i].DisplayName + '</span></li>';
        }
        $('#ul-selection').html(code);
    }
})

$('#deselect-all').on('click', function () {
    $('#ul-selectable li').remove();
    $('#ul-selection li').remove();
    var code = '';
    if (listUsers.length > 0) {
        for (var i = 0; i < listUsers.length; i++) {
            code += '<li class="ms-elem-selectable" name="' + listUsers[i].UserID + '"><span>' + listUsers[i].DisplayName + '</span></li>';
        }
        $('#ul-selectable').html(code);
    }
})