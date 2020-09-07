require('bootstrap-touchspin');

$(document).ready(function () {
    initQuantityControl();
});

function initQuantityControl() {
    $('.product-quantity .form-control').TouchSpin({
        buttondown_class: 'btn quantity-down',
        buttonup_class: 'btn quantity-up'
    });
    $('.quantity-down').html("<i class='fa fa-angle-down'></i>");
    $('.quantity-up').html("<i class='fa fa-angle-up'></i>");
}