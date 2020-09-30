import { createSlice } from '@reduxjs/toolkit';
import { ApolloClient,InMemoryCache, gql} from "@apollo/client";

export const loginSlice = createSlice({
  name: 'login',
  initialState: {
    email: '',
    isLoggedIn: false,
    token: '',
  },
  reducers: {
    login: (state, action) => {
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

export const { login, logout} = loginSlice.actions;

const client = new ApolloClient({
  uri: 'https://localhost:5001',
  cache: new InMemoryCache()
});

export const loginAsync = loginCredentials => dispatch => {

  const LOGIN_MUTATION = gql`
  mutation login($email: String!, $password: String!) {
    login(email: $email, password: $password) {
      token
    }
  }
`;

client.mutate({
  variables: loginCredentials,
  mutation: LOGIN_MUTATION
})
.then(result => {
  const token = result.data?.login?.token;
  if(!token) return;
  dispatch(login({ token, email: loginCredentials.email}));
 })
.catch(error => { console.log(error) });
};

export const selectIsLoggedIn = state => state.login.isLoggedIn;

export default loginSlice.reducer;
