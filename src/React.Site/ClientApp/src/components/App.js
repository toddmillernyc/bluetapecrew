import React, { useState, useEffect } from 'react';
import Header from './Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from './Home'
import PreHeader from './PreHeader'
import { Container } from 'react-bootstrap'
import Login from './Login';
import { AUTH_TOKEN } from '../constants'
import Account from './Account';
import { selectSiteProfile, fetchSiteProfileAsync } from '../store/siteProfileSlice';
import { useSelector, useDispatch } from 'react-redux';

const App =() => {
  const dispatch = useDispatch();
  dispatch(fetchSiteProfileAsync());
  const siteProfile = useSelector(selectSiteProfile);

  const [isLoggedIn, setIsLoggedIn] = useState(false);

  //todo: use state management or session
  const logOutCallback = () => {
    localStorage.clear();
    setIsLoggedIn(false);
  }

  useEffect(() => {
    if(localStorage.getItem(AUTH_TOKEN))
      setIsLoggedIn(true);
  })

  return (
    <Router>
      <Container fluid>
        <PreHeader logOutCallback={logOutCallback} isLoggedin={isLoggedIn} {...siteProfile} />
        <Header {...siteProfile} />
        <Switch>
        <Route exact path="/">
          <Redirect to="/home" />
        </Route>
        <Route path="/home" component={Home} />
        <Route path="/login" component={Login} />
        <Route path="/account" component={Account} />
      </Switch>
      </Container>
    </Router>
  )
}
export default App;
