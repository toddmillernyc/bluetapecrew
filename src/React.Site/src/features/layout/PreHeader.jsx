import React from 'react';
import { Link } from 'react-router-dom';

export const PreHeader = (vm) => {
    return (
        <div className="pre-header">
            <div className="container">
                <div className="row">
                    <div className="col-md-6 col-sm-6 additional-shop-info">
                        <ul className="list-unstyled list-inline">
                            <li><i className="fa fa-phone"></i><span>{vm.contactPhone}</span></li>
                            <li><i className="fa fa-envelope"></i><a href="mailto:{vm.contactEmail}">{vm.contactEmail}</a></li>
                        </ul>
                    </div>
                    <div className="col-md-6 col-sm-6 additional-nav">
                        <ul className="list-unstyled list-inline pull-right">
                            <li><Link to="/account">My Account</Link></li>
                            <li><Link to="/cart" id="cart-header-link">Shopping Cart</Link></li>
                            <li><Link to="/login">Login</Link></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}