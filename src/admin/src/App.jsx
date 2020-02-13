import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import React from 'react';
import CategoriesPage from './containers/CategoriesPage';
import HomePage from './components/HomePage';
import { BrowserRouter as Router,  Switch,  Route } from "react-router-dom";
import { Container, Navbar, Nav } from 'react-bootstrap';

import { library } from '@fortawesome/fontawesome-svg-core'
import { faCheck, faPencilAlt, faTimes } from '@fortawesome/free-solid-svg-icons'


function App() {
  library.add(faCheck)
  library.add(faPencilAlt)
  library.add(faTimes)
  return (
    <Router>
      <Container>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">React-Bootstrap</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link href="/">Home</Nav.Link>
            <Nav.Link href="/categories">Categories</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Switch>
        <Route exact path="/">
          <HomePage />
        </Route>
        <Route path="/categories">
          <CategoriesPage />
        </Route>
      </Switch>
      </Container>
    </Router>
  );
}
export default App;