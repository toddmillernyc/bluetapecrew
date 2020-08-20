import React, { useEffect, useState } from 'react';

import { ThemeLayout } from './theme/ThemeLayout'
import { Layout } from './theme/Layout'
import { CartWidget } from './theme/CartWidget'
import { PreHeader } from './features/layout/PreHeader'
import { Header } from './features/layout/Header'
import { Footer } from './features/layout/Footer'
import { Account, Cart, Home, Login, Product } from './pages'
import { Route, Switch } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.css'
import 'jquery.fancybox/source/jquery.fancybox.css'
import './theme/css/googleFontsOne.css'
import './theme/css/googleFontsTwo.css'
// import 'owl-carousel/owl-carousel/owl.carousel.css'
import 'font-awesome/css/font-awesome.css'
import 'jquery-uniform/themes/default/css/uniform.default.css'
import 'jquery.rateit/scripts/rateit.css'

import './theme/css/components.css'
import './theme/css/style-shop.css'
import './theme/css/style.css'
import './theme/css/style-responsive.css'
import './theme/css/blue.css'
import './theme/images/up.png'
import './theme/images/paypal.jpg'

function App() {
    const [vm, setVm] = useState({})

    useEffect(() => {
        
        async function getLayoutViewModel() {
            const response = await fetch('https://localhost:5001/layout')
            const json = await response.json()
            setVm(json)
        }

        window.vm = {
            cart: CartWidget,
            getPrice: Layout.getPrice
        }

        // var $ = require('jquery')
        // window.jQuery = $

        // $(() => {
        //     CartWidget.get();
        //     //const layout = new ThemeLayout($);
        //     //layout.init();
        //     //layout.initTwitter();
        //     //layout.initOWL($);
        //     //layout.initImageZoom();
        //     //layout.initTouchspin();
        //     //layout.initUniform();
        // });

        getLayoutViewModel()
    }, []);

    const subscribe = () => {
        //todo: subscribe to newsletter
    }

    return (
        <div className="App">
            <div className="ecommerce">
                <PreHeader {...vm}></PreHeader>
                <Header {...vm}></Header>
                <Switch>
                    <Route exact path="/" component={Home} />)} />
                    <Route path="/account" component={Account} />
                    <Route path="/cart" component={Cart} />
                    <Route path="/login" component={Login} />
                    <Route path="/product" component={Product} />
                </Switch>
                <Footer {...vm}></Footer>
            </div>
        </div>
    );
}

export default App;
