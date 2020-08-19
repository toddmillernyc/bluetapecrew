export class CartWidget {

    constructor($) {
        this.$ = $
    }

    $

    static get() {
        fetch('api/cart')
            .then(response => response.json())
            .then(vm => {
                document.getElementById('cart-count').textContent = vm.totals.count;
                document.getElementById('cart-total').textContent = vm.totals.total;

                const cart = document.getElementById('cart-items');
                cart.innerHTML = '';

                for (let item of vm.items) {
                    item.productLink = `products/${item.slug}`;

                    const li = document.createElement('li');
                    li.appendChild(getImageLink(item));
                    li.appendChild(getQuantity(item));
                    li.appendChild(getProductLink(item));

                    const em = document.createElement('em');
                    em.textContent = item.price;
                    li.appendChild(em);
                    li.appendChild(getRemoveButton(item));
                    li.appendChild(document.createElement('br'));
                    li.innerHTML += item.styleText;

                    cart.appendChild(li);
                }
            });

        function getImageLink(item) {
            const a = document.createElement('a');
            a.href = item.productLink;
            const img = new Image();
            img.src = `data:image/bmp;base64,${item.imageData}`;
            a.appendChild(img);
            return a;
        }

        function getQuantity(item) {
            const span = document.createElement('span');
            span.className = 'cart-content-count';
            span.textContent = `x ${item.quantity}`;
            return span;
        }

        function getProductLink(item) {
            const strong = document.createElement('strong');
            const a = document.createElement('a');
            a.href = item.productLink;
            a.textContent = item.productName;
            strong.appendChild(a);
            return strong;
        }

        function getRemoveButton(item) {
            const button = document.createElement('button');
            button.type = 'button';
            button.className = 'del-goods';

            button.setAttribute('onclick', `vm.cart.remove(${item.id})`);
            return button;
        }
    }

    static remove(id, refresh) {
        fetch(`Cart/Delete/${id}`, { method: 'POST' })
            .then(
                () => {
                    if (refresh) {
                        window.location.reload();
                    } else {
                        CartWidget.get();
                    }
                });
    }

    static add() {
        this.$.post(`api/cart/${this.$('#StyleId').val()}/${this.$('#quantity').val()}`)
            .done(function () {
                CartWidget.get();
            })
            .fail(function (error) {
                console.error(error.responseText);
            });
    }
}