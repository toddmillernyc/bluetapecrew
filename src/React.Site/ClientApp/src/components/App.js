import React from 'react';
import Header from './Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from './Home'
import { Container } from 'react-bootstrap'
import Login from './Login';
import Account from './Account';
import { refreshSessionAsync } from '../store/loginSlice';
import { useDispatch } from 'react-redux';
import { fetchSiteProfileAsync } from '../store/siteProfileSlice';
import Footer from './Footer';

const App = () => {
  
  const dispatch = useDispatch();
  dispatch(refreshSessionAsync());
  dispatch(fetchSiteProfileAsync());

  return (
    <Router>
      <Container fluid>
        <Header />
        <Switch>
          <Route exact path="/">
            <Redirect to="/home" />
          </Route>
          <Route path="/home" component={Home} />
          <Route path="/login" component={Login} />
          <Route path="/account" component={Account} />
        </Switch>
        <hr />
        <Footer />
      </Container>
    </Router>
  )
}
export default App;
