import React from 'react';
import logo from '../../theme/images/logo.png'
import { Cart } from './Cart'
import { Link } from 'react-router-dom';

export const Header = (vm) => {
    return (
        <div className="header">
            <div className="container">
                <a className="site-logo" href="/"><img src={logo} alt={vm.siteTitle} /></a>
                <a href="https://www.google.com" className="mobi-toggler"><i className="fa fa-bars"></i></a>
                <Cart {...vm}></Cart>
                <div className="header-navigation">
                    <ul>
                        {vm && vm.menu && vm.menu.map(menu => {
                            return <li className="dropdown" key={menu.id}>

                                <a className="dropdown-toggle" data-toggle="dropdown" data-target="#" href="/">
                                    {menu.name}
                                </a>

                                <ul className="dropdown-menu">
                                    {menu.items.map(item => {
                                        return <li key={item.slug}>
                                            <Link to="/product">{item.itemName}</Link>
                                            {/* <a href="products/{item.slug}">{item.itemName}</a> */}
                                            </li>
                                    })}
                                </ul>

                            </li>
                        })}
                    </ul>
                </div>
            </div>
        </div>
    )
}