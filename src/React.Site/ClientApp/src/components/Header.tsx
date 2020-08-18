import * as React from 'react';
import { FunctionComponent } from 'react';

export const Header: FunctionComponent<{ initial?: number }> = ({ initial = 0 }) => {
    // since we pass a number here, clicks is going to be a number.
    // setClicks is a function that accepts either a number or a function returning
    // a number
    return (
        <div className="header">
            <div className="container">
                <a className="site-logo" href="#">
                    <img src="img/logo.png" alt="Metronic Shop UI"></img>
                </a>
                <a href="#" className="mobi-toggler">
                    <i className="fa fa-bars"></i>
                </a>
                <p>cart</p>
                <div className="header-navigation">
                    <ul>

                    </ul>
                </div>
            </div>
        </div>)
}