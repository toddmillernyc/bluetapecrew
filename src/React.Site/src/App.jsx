import React, { useEffect, useState } from 'react';
import { ThemeLayout } from './theme/ThemeLayout'
import { Layout } from './theme/Layout'
import { Cart } from './theme/Cart'
import { PreHeader } from './features/layout/PreHeader'
import { Header } from './features/layout/Header'
import { Footer } from './features/layout/Footer'

import 'bootstrap/dist/css/bootstrap.css'
import 'jquery.fancybox/source/jquery.fancybox.css'
import './theme/css/googleFontsOne.css'
import './theme/css/googleFontsTwo.css'
import 'owl-carousel/owl-carousel/owl.carousel.css'
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
            console.log(json)
        }

        window.vm = {
            cart: Cart,
            getPrice: Layout.getPrice
        }

        var $ = require('jquery')
        window.jQuery = $

        $(() => {
            Cart.get();
            const layout = new ThemeLayout($);
            //layout.init();
            layout.initTwitter();
            //layout.initOWL($);
            //layout.initImageZoom();
            //layout.initTouchspin();
            //layout.initUniform();
        });

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
                <p>Body</p>
                <Footer {...vm}></Footer>
            </div>
        </div>
    );
}

export default App;
