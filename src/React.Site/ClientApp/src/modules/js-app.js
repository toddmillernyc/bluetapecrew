export class JsApp {

    constructor($) {
        this.$ = $
    }

    $

    static getPrice(id) {
        this.$.get(`Products/GetStylePrice/${id}`,
            function (data) {
                this.$('#price').html(data);
            });
    }

    static subscribe() {
        const subscriberEmail = document.getElementById('subscriber-email');
        if (subscriberEmail.value) {
            fetch(`api/subscribe?emailAddress=${subscriberEmail.value}`, { method: 'POST' })
                .then((response) => {
                    if (response.ok) {
                        alert('Thank you for subscribing');
                    } else {
                        alert('There was an error subscribing');
                    }
                    subscriberEmail.value = '';
                });
        }
    }
}
