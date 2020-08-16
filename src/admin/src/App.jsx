import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { BrowserRouter as Router,  Switch,  Route } from "react-router-dom";
import { Container, Navbar, Nav } from 'react-bootstrap';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faArrowLeft, faArrowRight, faCheck, faPencilAlt, faPlus, faTimes } from '@fortawesome/free-solid-svg-icons'
import CategoriesGrid from './components/categories/CategoriesGrid'
import ProductsGrid from './components/products/ProductsGrid';

function App() {
  library.add(faArrowLeft)
  library.add(faArrowRight)
  library.add(faCheck)
  library.add(faPencilAlt)
  library.add(faPlus)
  library.add(faTimes)


  return (
    <Router>
      <Container fluid>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">Admin</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
          <Nav.Link href="/categories">Categories</Nav.Link>
          <Nav.Link href="/products">Products</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Switch>
      <Route path={"/categories"}>
          <CategoriesGrid />
        </Route>
        <Route path={"/products"}>
          <ProductsGrid />
        </Route>
      </Switch>
      </Container>
    </Router>
  );
}
export default App;