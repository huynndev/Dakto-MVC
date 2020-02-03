var $emailForm = $("#articleSentmail");

$emailForm.submit(function (event) {
    event.preventDefault();
    $('#confirm-mail').modal('hide');
    $('#img-loading').css('display', 'block');
    var sendDto = new Object();
    sendDto.subject = $('#txtTitle').val();
    sendDto.ArticleId = $('#ArticleId').val();
    sendDto.body = $('#txtSentMail').html();
    sendDto.SendTo = $('input[name="sendTo"]:checked').val();

    $.ajax({
        url: '/admin/articles/sendmail',
        method: 'POST',
        data: JSON.stringify(sendDto),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#img-loading').css('display', 'none');
            swal("Gửi Mail thành công", "", "success");
        },
        error: function (xhr) {
            $('#img-loading').css('display', 'none');
            swal("Không gửi được Mail", "Vui lòng thử lại!", "error");
        }
    })
   
});