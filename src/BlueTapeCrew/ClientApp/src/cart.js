export class Cart {

    static get() {
        //fetch('cart/index').then((response) => {
        //    return response.text();
        //})
        //    .then((cartHtml) => {
        //        console.log(cartHtml);
        //    document.getElementsByClassName('top-cart-block')[0].innerHTML = cartHtml;
        //    });

        fetch('api/cart')
            .then(response => response.json())
            .then(vm => {
                console.log(vm);
                document.getElementById('cart-count').textContent = vm.totals.count;
                document.getElementById('cart-total').textContent = vm.totals.total;
            });
    }

    static remove(id) {
        fetch(`Cart/Delete/${id}`, { method: 'POST' })
            .then(() => Cart.get());
    }

    static add() {
        $.post(`api/cart/${$('#StyleId').val()}/${$('#quantity').val()}`)
            .done(function () {
                Cart.get();
            })
            .fail(function (error) {
                console.error(error.responseText);
            });
    }
}