import React from 'react';
import { Col, Nav, Navbar, NavDropdown, ListGroup, Form, Button } from 'react-bootstrap'
import Logo from '../img/logo.png';
import { Link } from "react-router-dom";
import { useSelector, useDispatch } from 'react-redux';
import { selectCategories, getCategoriesAsync } from '../store/categoriesSlice';
import { selectSiteProfile } from '../store/siteProfileSlice';
import LoginLink from './LoginLink';

const Header = () => {

  const dispatch = useDispatch();
  dispatch(getCategoriesAsync());
  const categories = useSelector(selectCategories);
  const siteProfile = useSelector(selectSiteProfile);

  const navItemStyle = {
    margin: '2%',
    padding: 0
  }

  return (
    <Navbar bg="light" expand="lg">
      <Navbar.Brand>
        <Link to='/home'>
          <img
            src={Logo}
            width="128"
            className="d-inline-block align-top"
            alt={`${siteProfile.siteTitle}`}
          />
        </Link>
      </Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          {categories.map(category => (
            <NavDropdown key={category.name} title={category.name} id="basic-nav-dropdown">
              {
                category.productCategories.map(productCategory => {
                  const product = productCategory.product
                  return <NavDropdown.Item key={product.productName}>{product.productName}</NavDropdown.Item>
                })
              }
            </NavDropdown>
          ))
          }
        </Nav>
        <Nav className="ml-auto">
          <Nav.Item style={navItemStyle}>
            <Link to='/account'>My Account</Link>
          </Nav.Item>
          <Nav.Item style={navItemStyle}>
            <a href="#">Shopping Cart</a>
          </Nav.Item>
          <Nav.Item style={navItemStyle}>
            <LoginLink/>
          </Nav.Item>
      </Nav>
      </Navbar.Collapse>
    </Navbar>
  )
}

export default Header