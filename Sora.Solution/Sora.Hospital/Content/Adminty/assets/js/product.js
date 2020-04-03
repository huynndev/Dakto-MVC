function getProduct(id){
    var result= this.products.find(obj=>{return obj.id===id}); 
    if(result!==undefined)
        return result;
}

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

    $('#product-price').val(formatCurrency(Math.round($('#ICProductPrice').val()).toString()));
})

$('.btn-add-child-product').on('click', function(){
    var id = $(this).attr('data-id');
    var product = getProduct(id);
    $('#board').append(GenerateHtml(product));
    addChildProducts(parseInt(id), 1);
    
    $('#modal-child-product').modal('show');
    $('#modal-products').modal('hide');
    $('#product_' + parseInt(id)).addClass('d-none');
})

function GenerateHtml(product){
    var codeHtml = '\
    <div class="w-100 record-childs">\
        <div class="row w-100 m-0">\
            <div class="col-5 d-flex row m-0 bor-right">\
                <a href="/Admin/Products/Detail/@item.Product.ICProductID" class="d-flex">\
                    <img src="' + product.image + '"/>\
                    <div class="ml-3">\
                        <strong>' + product.name + '</strong> <br />\
                        <span>' + product.type + '</span><br />\
                        <label href="javascript:;" data-id="' + product.id + '" class="text-danger remove-child-product" title="Xóa khỏi sản phẩm con"><i class="feather icon-trash-2"></i></label>\
                    </div>\
                </a>\
            </div>\
            <div class="col-2 bor-right d-flex align-items-center justify-content-end">\
                ' + formatCurrency(product.price) + ' VNĐ\
            </div>\
            <div class="col-2 bor-right d-flex align-items-center justify-content-end">\
                <input class="quantity form-control" data-id="' + product.id + '" type="number" min="1" max="100" value="1" name="quantity" onkeydown="return event.keyCode !== 69">\
            </div>\
            <div class="col-3 d-flex align-items-center justify-content-end total-price">\
                ' + formatCurrency(product.price) + ' VNĐ\
            </div>\
        </div>\
    </div>';
    return codeHtml;
}

function addChildProducts(id, quantity){    
    var indexProduct = this.childProducts.findIndex(x=>x.id === id);
    if(indexProduct < 0){
        let newChild = {
            id : id,
            quantity: quantity
        };
        this.childProducts.push(newChild);
    }else{
        childProducts[indexProduct].quantity = quantity;        
    }
    productPriceTotal();
}

$('#board').on('click', 'input[name="quantity"]', function(){
    let id = $(this).attr('data-id');
    let quantity = $(this).val();
    addChildProducts(parseInt(id), parseInt(quantity));
    let parent = $(this).parents('div.record-childs');
    let product = getProduct(id);
    var total = parseInt(product.price) * parseInt(quantity);
    parent.find('.total-price').html(formatCurrency(total.toString()));
    productPriceTotal();
})

function productPriceTotal(){
    var totalChildProducts = 0;
    this.childProducts.forEach(element => {
        let product = this.getProduct(element.id.toString());
        let total = parseInt(element.quantity) * parseInt(product.price);
        return totalChildProducts += total;
    });
    $('#ICProductPrice').val(totalChildProducts.toString());    
    $('#product-price').val(formatCurrency(totalChildProducts.toString()));   
    $('#ChildProductString').val(JSON.stringify(this.childProducts));
}

$('#board').on('click', '.remove-child-product', function(e){
    e.preventDefault();
    var id = $(this).attr('data-id');
    let parent = $(this).parents('div.record-childs');
    parent.remove();
    $('#product_'+id).removeClass('d-none');
    childProducts = $.grep(childProducts, function(e){ 
        return e.id != id; 
   });
   productPriceTotal();
})

$('#product-price').on('input', function(e){        
    $(this).val(formatCurrency(this.value.replace(/[,VNĐ]/g,'')));
}).on('keypress',function(e){
    if(!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
}).on('paste', function(e){    
    var cb = e.originalEvent.clipboardData || window.clipboardData;      
    if(!$.isNumeric(cb.getData('text'))) e.preventDefault();
});
function formatCurrency(number){
    var n = number.split('').reverse().join("");
    var n2 = n.replace(/\d\d\d(?!$)/g, "$&,");    
    return  n2.split('').reverse().join('');
}