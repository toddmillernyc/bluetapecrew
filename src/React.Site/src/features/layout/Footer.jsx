import React from 'react';
import paypal from '../../theme/images/paypal.jpg'

export const Footer = (vm) => {

    const productPopupStyle = {
        display: 'none',
        width: '700px'
    }
    
    function subscribe() {
        console.log("subscribe")
    }

return (
    <div className="pre-footer">
        <div className="container">
            <div className="row">
                <div className="col-md-3 col-sm-6 pre-footer-col">
                    <h2>About us</h2>
                    <p>{vm.aboutUs}</p>
                </div>
                <div className="col-md-3 col-sm-6 pre-footer-col">
                    <h2>Contact Us</h2>

                    <address className="margin-bottom-40" >
                        Email: <a href={"mailto:" + vm.contactEmail}>{vm.contactEmail}</a><br />
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
                        {vm.faceBookUrl && <li><a className="facebook" data-original-title="facebook" href={vm.faceBookUrl} target="_blank"> </a></li>}
                        {vm.twitterHandle && <li><a className="twitter" data-original-title="twitter" href={vm.twitterHandle} target="_blank"> </a></li>}
                        {vm.linkedInUrl && <li><a className="linkedin" data-original-title="linkedin" href={vm.linkedInUrl} target="_blank"> </a></li>}
                    </ul>
                </div>
                <div className="col-md-6 col-sm-6">
                    <div className="pre-footer-subscribe-box pull-right">
                        <h2>Newsletter</h2>
                        <div className="input-group">
                            <input type="text" placeholder="youremail@mail.com" className="form-control" id="subscriber-email" />
                            <span className="input-group-btn">
                                <button className="btn btn-primary" onClick={subscribe}>Subscribe</button>
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
                        &copy; {new Date().getFullYear()} - {vm.siteTitle} | {vm.copyrightText} <a href={vm.copyrightUrl}>{vm.copyrightLinktext}</a>
                    </div>
                    <div className="col-md-6 col-sm-6">
                        <ul className="list-unstyled list-inline pull-right">
                            <li><img src={paypal} alt="We accept PayPal" title="We accept PayPal" /></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="product-pop-up" style={productPopupStyle}></div>
    </div>    
)
}