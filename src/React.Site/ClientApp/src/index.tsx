import 'bootstrap/dist/css/bootstrap.css'

import * as React from 'react'
import * as ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { ConnectedRouter } from 'connected-react-router'
import { createBrowserHistory } from 'history'
import configureStore from './store/configureStore'
import App from './App'
import registerServiceWorker from './registerServiceWorker'

/* Begin Pre-React Code */
import { ThemeLayout } from './classes/ThemeLayout'

import './favicon.ico'
import 'bootstrap/dist/css/bootstrap.css'
//import 'fancybox/dist/css/jquery.fancybox.css'
import './css/googleFontsOne.css'
import './css/googleFontsTwo.css'
import 'owl-carousel/owl-carousel/owl.carousel.css'
import 'font-awesome/css/font-awesome.css'
import 'jquery-uniform/themes/default/css/uniform.default.css'
import 'jquery.rateit/scripts/rateit.css'
import './theme/pages/css/components.css'
import './theme/pages/css/style-shop.css'
import './theme/corporate/css/style.css'
import './theme/corporate/css/style-responsive.css'
import './theme/corporate/css/themes/blue.css'
import './theme/corporate/img/up.png'
import './images/logo.png'
import './images/paypal.jpg'
import { JsApp } from './modules/js-app'
import { Cart } from './modules/cart'

declare global {
    interface Window {
        vm: any
        jQuery: any
    }
}

var $ = require('jquery')
window.jQuery = $
require("@fancyapps/fancybox");
require('bootstrap/dist/js/bootstrap')
require('jquery-slimscroll/jquery.slimscroll.js')
require('owl-carousel/owl-carousel/owl.carousel.js')
require('jquery-zoom/jquery.zoom.js')
require('bootstrap-touchspin')
require('jquery-uniform')
require('jquery.rateit')
require('./theme/corporate/scripts/back-to-top.js')



window.vm = {
    cart: Cart,
    getPrice: JsApp.getPrice
}

$(() => {
    Cart.get();
    const layout = new ThemeLayout($);
    layout.init();
    layout.initTwitter();
    layout.initOWL();
    layout.initImageZoom();
    layout.initTouchspin();
    layout.initUniform();
});
/* End Pre-React Code */

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string
const history = createBrowserHistory({ basename: baseUrl })

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = configureStore(history)

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App />
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root'))

registerServiceWorker();