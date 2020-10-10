import { createSlice } from '@reduxjs/toolkit';
import { login } from '../data/repo';
import { AUTH_TOKEN } from '../constants'

const loginSlice = createSlice({
  name: 'login',
  initialState: {
    email: '',
    isLoggedIn: false,
    token: '',
  },
  reducers: {
    updateLogin: (state, action) => {
      const loginData = action.payload.data.login;
      if(!loginData) return;
      const token = loginData.token;
      if(!token) return;

      state.email = loginData.email;
      state.isLoggedIn = true;
      state.token = token
      localStorage.clear();
      localStorage.setItem(AUTH_TOKEN, token);
    },
    logout: state => {
      state.email = '';
      state.isLoggedIn = false;
      state.token = ''
      localStorage.clear();
    },
    refreshSession: state => {
      const token = localStorage.getItem(AUTH_TOKEN);
      if(!token) return;
      //todo: validate token if exists
      state.token = token;
      state.isLoggedIn = true;
    }
  },
});

export const { logout } = loginSlice.actions;
const { refreshSession } = loginSlice.actions;

export const loginAsync = loginCredentials => dispatch => {
  const { updateLogin } = loginSlice.actions;
  login(loginCredentials).then(result => {dispatch(updateLogin(result)); })
};

export const refreshSessionAsync = () => dispatch => dispatch(refreshSession());

export const selectIsLoggedIn = state => state.login.isLoggedIn;

export default loginSlice.reducer;
