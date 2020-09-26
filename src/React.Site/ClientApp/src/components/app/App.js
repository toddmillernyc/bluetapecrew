import React from 'react';
import Header from '../header/Header'
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import Home from '../home/Home'

import './App.css';

function App() {
  return (
    <Router>
      <div>
        <Header />
        <main role="main" className="container">
        </main>
      </div>

      <Switch>
      <Route exact path="/">
        <Redirect to="/home" />
      </Route>
      <Route path="/home" component={Home} />
      </Switch>
    </Router>
  )
}
export default App
