import React from 'react';
import logo from '../../theme/images/logo.png'

export const Cart = (vm) => {
    var cartItemsStyle = {
        'height': '250px'
    }
    return (
        <div className="top-cart-block" id="cart-widget">
            <div className="top-cart-info">
                <a href="" className="top-cart-info-count"><span id="cart-count"></span> items</a>
                <a href="" className="top-cart-info-value">$ <span id="cart-total"></span></a>
            </div>
            <i className="fa fa-shopping-cart"></i>
            <div className="top-cart-content-wrapper">
                <div className="top-cart-content">
                    <ul className="scroller" style={{ cartItemsStyle}} id="cart-items"></ul>
                    <div className="text-right">
                        {
                            vm.items
                                ?
                                <>
                                    <a href="~/cart" className="btn btn-default">View Cart</a>
                                    <a href="~/checkout" className="btn btn-info" id="go-to-checkout-button">Checkout</a>
                                </>
                                : <p>Your cart is empty.  Add some stuff.</p>
                        }
                    </div>
                </div>
            </div>
        </div>
)
}