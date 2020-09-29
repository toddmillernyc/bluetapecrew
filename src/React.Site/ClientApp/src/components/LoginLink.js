import React from 'react';
import { Link } from "react-router-dom";

const LoginLink = ({ isLoggedIn, logoutCallback }) => {
  return (
      isLoggedIn
          ? <a onClick={logoutCallback} href="#">Log Out</a>
          : <Link to='/login'>Log In</Link>
  )
}

export default LoginLink;