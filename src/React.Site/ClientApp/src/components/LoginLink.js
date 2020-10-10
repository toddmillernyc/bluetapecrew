import React from 'react';
import { Link } from "react-router-dom";
import { useSelector, useDispatch } from 'react-redux';
import { logout, selectIsLoggedIn } from '../store/loginSlice';

const LoginLink = () => {
  const isLoggedIn  = useSelector(selectIsLoggedIn);
  const dispatch = useDispatch();
  return (
      isLoggedIn
          ? <a onClick={() => dispatch(logout())} href="#">Log Out</a>
          : <Link to='/login'>Log In</Link>
  )
}

export default LoginLink;