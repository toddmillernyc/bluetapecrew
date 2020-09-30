import React from 'react';
import { Row, Col } from 'react-bootstrap'
import { Link } from "react-router-dom";
import { useSelector } from 'react-redux';
import { selectIsLoggedIn } from '../store/loginSlice';

const Account = () => {

  const isLoggedIn = useSelector(selectIsLoggedIn);

  return (
    <Row>
      {
        isLoggedIn

          ? <Col>
              Account Page
            </Col>

          : <Col>
              <p>Please log in to view your account.
              <br />
              <Link to='/login'>LogIn Link</Link></p>
            </Col>
      }
    </Row>
  );
}

export default Account;