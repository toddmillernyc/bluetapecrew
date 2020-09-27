import React from 'react';
import { Nav, Navbar, NavDropdown } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";
import Logo from '../img/logo.png';

export const GET_CATEGORIES = gql`
  {
    categories(where: { published: true }, order_by: { position: ASC }) {
      name
      productCategories{
        product {
          id
          productName
        }
      }
    }
  }
`;

const Header = ({ siteTitle }) => {

  const { data, loading, error } = useQuery(GET_CATEGORIES);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>{JSON.stringify(error)}(</p>;
  if (!data) return <p>Not Found</p>;

  return (
      <Navbar bg="light">
        <Navbar.Brand href="#home">
          <img
            src={Logo}
            width="128"
            className="d-inline-block align-top"
            alt="Ecommerce Website logo"
          />
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">

            {data.categories.map(category => (
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
        </Navbar.Collapse>
      </Navbar>
  )
}

export default Header