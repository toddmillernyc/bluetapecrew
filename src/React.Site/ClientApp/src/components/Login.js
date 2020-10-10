import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Row, Col } from 'react-bootstrap';
import { loginAsync, selectIsLoggedIn } from '../store/loginSlice';

const Login = () => {
  const isLoggedIn  = useSelector(selectIsLoggedIn);
  const dispatch = useDispatch();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const submitLoginForm = async e => {
    e.preventDefault();
    dispatch(loginAsync({ email, password }));
    setEmail('');
    setPassword('');
  }

  return isLoggedIn
    ? <span>You are logged in</span>
    : <Row>
      <Col>
        <form onSubmit={ submitLoginForm }>
          <div className="form-group">
            <label htmlFor="email">Email:</label>
            <input 
              value={email}
              onChange={e=>setEmail(e.target.value)}
              type="email"
              className="form-control"
              placeholder="Enter email"
              autoComplete="username"
            />
          </div>
          <div className="form-group">
            <label htmlFor="pwd">Password:</label>
            <input
              value={password}
              onChange={e=>setPassword(e.target.value)}
              type="password"
              className="form-control"
              placeholder="Enter password"
              autoComplete="current-password" />
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