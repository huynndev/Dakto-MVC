var $btn_add = $('.btn-addContent');
var $btn_edit = $('.btn-editContent');
var $btn_detele = $('.btn-deleteContent');

function AddContentFirstLevel(viewIndex) {
    $('#applyModalLongTitle').text('Thêm mới nội dung');

    $('#FK_SolutionId').val(solutionId);
    $('#FK_ParentId').val('');
    $('#ViewLevel').val(1);
    $('#ViewIndex').val(viewIndex);
    $('#Title').val('');
    $('#Content').val('');
    $('#Lang').val(lang);
    $('#isCreate').val(true);

    $('#modal_edit').modal('show');
}

$btn_add.on('click', function () {
    $this = $(this);
    var parentId = $this.attr('data-id');
    var viewIndex = $this.attr('data-index');
    var viewLevel = $this.attr('data-level');
    $('#applyModalLongTitle').text('Thêm mới nội dung');

    $('#FK_SolutionId').val(solutionId);
    $('#FK_ParentId').val(parentId);
    $('#ViewLevel').val(viewLevel);
    $('#ViewIndex').val(viewIndex);
    $('#Title').val('');
    $('#Content').val('');
    $('#Lang').val(lang);
    $('#isCreate').val(true);

    $('#modal_edit').modal('show');
})

$btn_edit.on('click', function () {
    $this = $(this);
    var contentId = $this.attr('data-id');
    $('#applyModalLongTitle').text('Chỉnh sửa nội dung');

    $.ajax({
        url: '/admin/solution/loadcontent?solutionContentId=' + contentId,
        method: 'GET',
        success: function (data) {
            console.log(data)
            $('#SASolutionContentId').val(data.SASolutionContentId);
            $('#FK_SolutionId').val(data.FK_SolutionId);
            $('#FK_ParentId').val(data.FK_ParentId);
            $('#ViewLevel').val(data.ViewLevel);
            $('#ViewIndex').val(data.ViewIndex);
            $('#Title').val(data.Title);
            $('#Content').val(data.Content);
            $('#Lang').val(lang);
            $('#isCreate').val(false);
            $('#modal_edit').modal('show');
        },
        error: function (xhr) {
            alert('ERROR');
        }
    })
})

$btn_detele.on('click', function () {
    $this = $(this);
    var contentId = $this.attr('data-id');
    swal({
        title: "Bạn có muốn xóa?",
        text: "Dữ liệu sẽ được xóa khỏi hệ thống!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Xóa",
        cancelButtonText: "Hủy",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: '/admin/solution/DeleteContent',
                method: 'DELETE',
                data: { solutionContentId: contentId },
                success: function (data) {
                    swal({
                        title: "",
                        text: "Xóa thành công",
                        type: "success",
                        showCancelButton: false,
                        confirmButtonClass: "btn-primary",
                        confirmButtonText: "Ok",
                        closeOnConfirm: false
                    },
                        function () {
                            location.reload();
                        });
                },
                error: function () {
                    swal("", "Xóa thất bại, vui lòng thử lại!", "error")
                }
            });
        });
})