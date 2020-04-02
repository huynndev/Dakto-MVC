$(document).ready(function(){
    $('#btn-child-product').on('click', function(){
        $('#modal-child-product').modal('show');
    });

    $('#btn-show-products').on('click', function(){
        $('#modal-child-product').modal('hide');
        $('#modal-products').modal('show');
    })

    $('#btn-cancel-modal-products').on('click', function(){
        $('#modal-child-product').modal('show');
    })
})
