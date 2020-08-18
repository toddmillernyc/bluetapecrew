import React from 'react';
import './App.css';

function App() {

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
                <div className="pre-header">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-6 col-sm-6 additional-shop-info">
                                <ul className="list-unstyled list-inline">

                                </ul>
                            </div>
                            <div className="col-md-6 col-sm-6 additional-nav">
                                <ul className="list-unstyled list-inline pull-right">
                                    <li>
                                        <a asp-controller="Manage" asp-action="Index" id="manage-account-header-link" href="https://google.com">My Account</a>
                                    </li>
                                    <li>
                                        <a href="~/cart" id="cart-header-link">Shopping Cart</a>
                                    </li>
                                    <p>login-partial</p>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="header">
                    <div className="container">
                        <a className="site-logo" href="~/">
                            <img src="img/logo.png" alt="[INSERT SITE NAME]" />
                        </a>
                        <a href="https://www.google.com" className="mobi-toggler">
                            <i className="fa fa-bars"></i>
                        </a>
                        <p>CART</p>
                        <div className="header-navigation">
                            <ul>

                                <li className="dropdown">
                                    <a className="dropdown-toggle" data-toggle="dropdown" data-target="#" href="https://www.google.com">

                                    </a>

                                    <ul className="dropdown-menu">


                                    </ul>
                                </li>


                            </ul>
                        </div>
                    </div>
                </div>
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
                                              }
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
