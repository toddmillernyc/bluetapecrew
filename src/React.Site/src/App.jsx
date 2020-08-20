import React, { useEffect, useState } from 'react';

import { ThemeLayout } from './theme/ThemeLayout'
import { Layout } from './theme/Layout'
import { CartWidget } from './theme/CartWidget'
import { PreHeader } from './features/layout/PreHeader'
import { Header } from './features/layout/Header'
import { Footer } from './features/layout/Footer'
import { Account, Cart, Home, Login, Product } from './pages'
import { Route, Switch } from 'react-router-dom';

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

        window.jQuery(() => {
            CartWidget.get();
            const layout = new ThemeLayout(window.jQuery);
            layout.init();
            layout.initTwitter();
            //layout.initOWL(window.jQuery);
            //layout.initTouchspin();
            layout.initUniform();
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
