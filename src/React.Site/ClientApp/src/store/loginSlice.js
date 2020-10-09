import { createSlice } from '@reduxjs/toolkit';
import { login } from './repo';

export const loginSlice = createSlice({
  name: 'login',
  initialState: {
    email: '',
    isLoggedIn: false,
    token: '',
  },
  reducers: {
    updateLogin: (state, action) => {
      const payload = action.payload;
      state.email = payload.email;
      state.isLoggedIn = payload.token != null;
      state.token = payload.token
    },
    logout: state => {
      state.email = '';
      state.isLoggedIn = false;
      state.token = ''
    }
  },
});

export const { updateLogin, logout } = loginSlice.actions;

export const loginAsync = loginCredentials => dispatch => {

  login(loginCredentials).then(result => {
      const token = result.data?.login?.token;
      if (!token) return;
      dispatch(updateLogin({ token, email: loginCredentials.email }));
    })
    .catch(error => { 
      console.log(error)
    });
    
};

export const selectIsLoggedIn = state => state.login.isLoggedIn;

export default loginSlice.reducer;
