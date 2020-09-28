import React from 'react';
import Header from './Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from './Home'
import PreHeader from './PreHeader'
import { Container } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";
import Login from './Login';

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
  const { data } = useQuery(GET_PROFILE);
  const profile = data?.siteProfile;

  return (
    <Router>
      <Container fluid>
        <PreHeader {...profile} />
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
