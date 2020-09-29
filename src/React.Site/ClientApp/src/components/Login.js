import React, { useState, useEffect } from 'react';
import { AUTH_TOKEN } from '../constants'
import { gql, useMutation } from "@apollo/client";
import { Row, Col } from 'react-bootstrap'

const SIGNUP_MUTATION = gql`
  mutation SignupMutation($email: String!, $password: String!, $name: String!) {
    signup(email: $email, password: $password, name: $name) {
      token
    }
  }
`

const LOGIN_MUTATION = gql`
  mutation login($email: String!, $password: String!) {
    login(email: $email, password: $password) {
      token
    }
  }
`;

const Login = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [login] = useMutation(LOGIN_MUTATION);

  useEffect(() => {
    if(localStorage.getItem(AUTH_TOKEN))
      setIsLoggedIn(true);
  })

  const submitLoginForm = async e => {
    e.preventDefault();
    const { data } = await login({ variables: { email, password } });
    if(data)
      localStorage.setItem(AUTH_TOKEN, data.login.token)
  }

  return isLoggedIn
    ? <span>You are logged in</span>
    : <Row>
      <Col>
        <form onSubmit={submitLoginForm}>
          <div className="form-group">
            <label htmlFor="email">Email:</label>
            <input 
              value={email}
              onChange={e=>setEmail(e.target.value)}
              type="email"
              className="form-control"
              placeholder="Enter email"
            />
          </div>
          <div className="form-group">
            <label htmlFor="pwd">Password:</label>
            <input
              value={password}
              onChange={e=>setPassword(e.target.value)}
              type="password"
              className="form-control"
              placeholder="Enter password" />
          </div>
          <div className="form-group form-check">
            <label className="form-check-label"></label>
            <input className="form-check-input" type="checkbox" name="remember" /> Remember me
          </div>
          <button type="submit" className="btn btn-primary">Submit</button>
        </form>
      </Col>
      <Col></Col>
      <Col></Col>
      <Col></Col>
      <Col></Col>
      <Col></Col>
    </Row>

  
}

export default Login;