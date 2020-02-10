import React from 'react';
import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "react-router-dom";
import HomePage from './components/HomePage';
import CategoriesPage from './components/CategoriesPage';

function App() {
  return (
    <Router>
            <div>
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/categories">Categories</Link>
          </li>
        </ul>
        <hr />
        <Switch>
          <Route exact path="/">
            <HomePage />
          </Route>
          <Route path="/categories">
            <CategoriesPage />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
