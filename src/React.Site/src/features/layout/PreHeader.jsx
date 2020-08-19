import React from 'react';

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
                            <li>
                                <a
                                    asp-controller="Manage"
                                    asp-action="Index"
                                    id="manage-account-header-link"
                                    href="https://google.com"
                                >
                                    My Account
                                </a>
                            </li>
                            <li>
                                <a href="~/cart" id="cart-header-link">Shopping Cart</a>
                            </li>
                            <li><a>Login</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}