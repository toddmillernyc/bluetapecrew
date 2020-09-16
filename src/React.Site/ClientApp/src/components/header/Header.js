import React from 'react';
import { Nav, Navbar, NavDropdown, Form, FormControl, Button } from 'react-bootstrap'
import { Query } from '@apollo/client/react/components';
import gql from "graphql-tag";

const GET_CATEGORIES = gql`
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

function Header() {
  return (
    <>
    <Navbar bg="light" expand="lg">
      adfv
    </Navbar>
    <Navbar bg="light" expand="lg">
      <Navbar.Brand href="#home">React-Bootstrap</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <Query query={GET_CATEGORIES}>
            {({ loading, error, data }) => {
              if (loading) return <p>Loading...</p>;
              if (error) return <p>Error :(</p>;

              return data.categories.map(category => (
                <NavDropdown key={category.name} title={category.name} id="basic-nav-dropdown">
                  {
                    category.productCategories.map(productCategory => {
                      const product = productCategory.product
                    return <NavDropdown.Item key={product.productName}>{product.productName}</NavDropdown.Item>
                    })
                  }
                </NavDropdown>
              ));
            }}
          </Query>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
    </>
  )
}

export default Header