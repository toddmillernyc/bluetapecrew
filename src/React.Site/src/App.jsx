import React, { useEffect, useState } from 'react';
import { ThemeLayout } from './theme/ThemeLayout'
import { Layout } from './theme/Layout'
import { Cart } from './theme/Cart'
import { PreHeader } from './features/layout/PreHeader'
import { Header } from './features/layout/Header'

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
import './theme/images/logo.png'
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

    const productPopupStyle = {
        display: 'none',
        width: '700px'
    }

    const subscribe = () => {
        //todo: subscribe to newsletter
    }

    return (
        <div className="App">
            <div className="ecommerce">
                <PreHeader {...vm}></PreHeader>
                <Header {...vm}></Header>
                <p>Body</p>
                <div className="pre-footer">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3 col-sm-6 pre-footer-col">
                                <h2>About us</h2>
                                <p>layoutModel.AboutUs</p>
                            </div>
                            <div className="col-md-3 col-sm-6 pre-footer-col">
                                <h2>Contact Us</h2>

                                <address className="margin-bottom-40" >
                                    Email: <a href="@layoutModel.ContactEmail">@layoutModel.ContactEmail</a><br />
                                </address>
                            </div>
                            <div className="col-md-3 col-sm-6 pre-footer-col">
                                <h2 className="margin-bottom-0">Latest Tweets</h2>
                                <a href="https://wwww.google.com"
                                    className="twitter-timeline"
                                    data-theme="dark"
                                    data-tweet-limit="2">Latest Tweets</a>
                            </div>
                            <div className="col-md-3 col-sm-6 pre-footer-col">
                                <h2>Custom Shirts</h2>
                                <p>Contact us about custom shirt orders.</p>
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-md-6 col-sm-6">
                                <ul className="social-icons">

                                    <li><a className="facebook" data-original-title="facebook" href="@layoutModel.FaceBookUrl"> </a></li>

                                    <li><a className="twitter" data-original-title="twitter" href="https://twitter.com/@layoutModel.TwitterHandle"> </a></li>

                                    <li><a className="linkedin" data-original-title="linkedin" href="@layoutModel.LinkedInUrl"> </a></li>
                                              
                                          </ul>
                            </div>
                            <div className="col-md-6 col-sm-6">

                                <div className="pre-footer-subscribe-box pull-right">
                                    <h2>Newsletter</h2>
                                    <div className="input-group">
                                        <input type="text" placeholder="youremail@mail.com" className="form-control" id="subscriber-email" />
                                        <span className="input-group-btn">
                                            <button className="btn btn-primary" onClick={subscribe()}>Subscribe</button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="footer">
                        <div className="container">
                            <div className="row">
                                <div className="col-md-6 col-sm-6 padding-top-10">
                                    &copy; @DateTime.Now.Year - @layoutModel.SiteTitle | @layoutModel.CopyrightText <a href="@layoutModel.CopyrightUrl">@layoutModel.CopyrightLinktext</a>
                                </div>
                                <div className="col-md-6 col-sm-6">
                                    <ul className="list-unstyled list-inline pull-right">
                                        <li><img src="img/paypal.jpg" alt="We accept PayPal" title="We accept PayPal" /></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="product-pop-up" style={productPopupStyle}></div>
                </div>
            </div>
        </div>
    );
}

export default App;
