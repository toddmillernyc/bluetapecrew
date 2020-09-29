import React, { useState, useEffect } from 'react';
import Header from './Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from './Home'
import PreHeader from './PreHeader'
import { Container } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";
import Login from './Login';
import { AUTH_TOKEN } from '../constants'

export const GET_PROFILE = gql`
{
  siteProfile {
    contactPhoneNumber
    contactEmailAddress
    siteTitle
  }
}
`;

const App =() => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  
  const { data } = useQuery(GET_PROFILE);
  const profile = data?.siteProfile;

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
        <PreHeader logOutCallback={logOutCallback} isLoggedin={isLoggedIn} {...profile} />
        <Header {...profile} />
        <Switch>
        <Route exact path="/">
          <Redirect to="/home" />
        </Route>
        <Route path="/home" component={Home} />
        <Route path="/login" component={Login} />
      </Switch>
      </Container>
    </Router>
  )
}
export default App;
