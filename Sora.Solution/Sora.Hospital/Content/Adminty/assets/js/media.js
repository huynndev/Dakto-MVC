function showPopupEditMedia(mediaGroupId) {
    $.ajax({
        method: 'GET',
        url: '/Media/GetMediaGroup',
        data: {
            mediaGroupId: mediaGroupId
        }
    }).done(function (res) {
        var data = $.parseJSON(res);
        $('#editMedia input[name="MainMedia.MediaGroupId"]').val(data.MainMedia.MediaGroupId);
        $('#editMedia input[name="MainMedia.IsDefault"]').val(data.MainMedia.IsDefault);
        $('#editMedia input[name="MainMedia.IsTranslate"]').val(data.MainMedia.IsTranslate);
        $('#editMedia input[name="MainMedia.ContentType"]').val(data.MainMedia.ContentType);
        $('#editMedia input[name="MainMedia.Lang"]').val(data.MainMedia.Lang);

        $('#editMedia input[name="TranslateMedias[0].MediaGroupId"]').val(data.TranslateMedias[0].MediaGroupId);
        $('#editMedia input[name="TranslateMedias[0].IsDefault"]').val(data.TranslateMedias[0].IsDefault);
        $('#editMedia input[name="TranslateMedias[0].IsTranslate"]').val(data.TranslateMedias[0].IsTranslate);
        $('#editMedia input[name="TranslateMedias[0].ContentType"]').val(data.TranslateMedias[0].ContentType);
        $('#editMedia input[name="TranslateMedias[0].Lang"]').val(data.TranslateMedias[0].Lang);
        $('#editMedia input[name="TranslateMedias[0].ReferId"]').val(data.TranslateMedias[0].ReferId);

        $('#editMedia input[name="MainMedia.PositionName"]').val(data.MainMedia.PositionName);
        $('#editMedia input[name="MainMedia.PositionCode"]').val(data.MainMedia.PositionCode);

        $('#editMedia input[name="TranslateMedias[0].PositionName"]').val(data.TranslateMedias[0].PositionName);
        $('#editMedia input[name="TranslateMedias[0].PositionCode"]').val(data.TranslateMedias[0].PositionCode);
        
        $('#editMedia').modal('show');
    });
}

function showPopupEditMediaItem(mediaItemId, contentType) {
    $.ajax({
        method: 'GET',
        url: '/Media/GetMediaItem',
        data: {
            mediaItemId: mediaItemId
        }
    }).done(function (res) {
        var data = $.parseJSON(res);

        $('#titleMediaItem').text('Chỉnh sửa ' + contentType);

        $('#createMediaItem input[name="Lang"]').val(data.Lang);
        $('#createMediaItem input[name="MediaItemId"]').val(data.MediaItemId);
        $('#createMediaItem input[name="IsTranslate"]').val(data.IsTranslate);

        $('#createMediaItem input[name="IsDefault"]').val(data.IsDefault);
        $('#createMediaItem input[name="OpenNewTab"]').val(data.OpenNewTab);
        $('#createMediaItem input[name="MediaTitle"]').val(data.MediaTitle);
        $('#createMediaItem input[name="Description"]').val(data.Description);
        $('#createMediaItem input[name="ViewIndex"]').val(data.ViewIndex);
        $('#createMediaItem input[name="FileName"]').val(data.FileName);
        var src = url + data.FileName;
        $('#mediaItemImgRqsPicture1Display').attr('src', src);
        $('#sp-file-name-1').text(data.FileName);

        $('#createMediaItem input[name="Hyperlink"]').val(data.Hyperlink);
        if (data.FileName != null && data.FileName !== '') {
            $('#rqsPicture1Display').removeClass('d-none');
        }
        
        $('#createMediaItem input[name="filename1"]').prop('required', false);
        $('#createMediaItem').modal('show');
    });


}

function showPopupCreateMediaItem(lang, contentType) {
    $('#createMediaItem input[name="MediaItemId"]').val('');
    $('#createMediaItem input[name="IsTranslate"]').val('');

    $('#createMediaItem input[name="IsDefault"]').val('');
    $('#createMediaItem input[name="OpenNewTab"]').val('');
    $('#createMediaItem input[name="MediaTitle"]').val('');
    $('#createMediaItem input[name="Description"]').val('');
    $('#createMediaItem input[name="ViewIndex"]').val('');
    $('#createMediaItem input[name="FileName"]').val('');
    $('#createMediaItem input[name="Hyperlink"]').val('');

    $('#createMediaItem input[name="Lang"]').val(lang);
    $('#titleMediaItem').text('Thêm mới ' + contentType);
    $('#createMediaItem input[name="filename1"]').prop('required', true);
    $('#createMediaItem').modal('show');
}