export class Cart {

    static get() {
        fetch('api/cart')
            .then(response => response.json())
            .then(vm => {
                document.getElementById('cart-count').textContent = vm.totals.count;
                document.getElementById('cart-total').textContent = enforceTwoDecimalPlaces(vm.totals.total);

                const cart = document.getElementById('cart-items');
                cart.innerHTML = '';

                for (let item of vm.items) {
                    item.productLink = `products/${item.slug}`;

                    const li = document.createElement('li');
                    li.appendChild(getImageLink(item));
                    li.appendChild(getQuantity(item));
                    li.appendChild(getProductLink(item));

                    const em = document.createElement('em');
                    em.textContent = enforceTwoDecimalPlaces(item.subTotal);
                    li.appendChild(em);
                    li.appendChild(getRemoveButton(item));
                    li.appendChild(document.createElement('br'));
                    li.innerHTML += item.styleText;

                    cart.appendChild(li);
                }
            });

        function enforceTwoDecimalPlaces(amt) {
            const tokens = amt.toString().split('.');
            if (tokens.length === 1) return `${amt}.00`;
            if (tokens.length === 2) {
                if (tokens[1].length === 1) return `${amt}0`;
            }
            return amt;
        }

        function getImageLink(item) {
            const a = document.createElement('a');
            a.href = item.productLink;
            const img = new Image();
            if (item.imageData) {
                img.src = `data:image/bmp;base64,${item.imageData}`;
            } else {
                img.src = `images/productthumb/${item.imageId}`;
                img.style.maxWidth = '37px';
            }
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
                        location.reload();
                    } else {
                        Cart.get();
                    }
                });
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