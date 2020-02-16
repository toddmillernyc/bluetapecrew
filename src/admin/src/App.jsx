import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { BrowserRouter as Router,  Switch,  Route } from "react-router-dom";
import { Container, Navbar, Nav } from 'react-bootstrap';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCheck, faPencilAlt, faPlus, faTimes } from '@fortawesome/free-solid-svg-icons'
import CategoriesGrid from './components/CategoriesGrid'

function App() {
  library.add(faCheck)
  library.add(faPencilAlt)
  library.add(faPlus)
  library.add(faTimes)
  
  return (
    <Router>
      <Container>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">Admin</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Nav.Link href="/categories">Categories</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Switch>
        <Route href={"/" | "/categories"}>
          <CategoriesGrid />
        </Route>
      </Switch>
      </Container>
    </Router>
  );
}
export default App;