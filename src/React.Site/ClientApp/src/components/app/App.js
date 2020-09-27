import React from 'react';
import Header from '../header/Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from '../home/Home'
import PreHeader from '../header/PreHeader'
import { Container } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";

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
      </Switch>
      </Container>
    </Router>
  )
}
export default App;
