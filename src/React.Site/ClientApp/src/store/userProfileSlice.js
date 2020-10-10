import { createSlice } from '@reduxjs/toolkit';
import { getUserProfile } from '../data/repo';

const userProfileSlice = createSlice({
  name: 'userProfile',
  initialState: {
    firstName: '',
    lastName: '',
    address: '',
    city: '',
    state: '',
    postalCode: ''
  },
  reducers: {
    getProfile: (state, action) => {
      const payload = action.payload;
      console.log(payload)
      // state.firstName = payload.firstName;
      // state.lastName = payload.lastName;
      // state.address = payload.address;
      // state.city = payload.city;
      // state.state = payload.state;
      // state.postalCode = payload.postalCod;
    }
  }
});


export const fetchUserProfileAsync = email => dispatch => {
  const { getProfile } = userProfileSlice.actions;
  getUserProfile(email).then(result => dispatch(getProfile(result)));
}

export const userProfileSelect = state => state.userProfile;

export default userProfileSlice.reducer;
