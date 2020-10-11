import { createSlice } from '@reduxjs/toolkit';
import { getUserProfile, updateUser } from '../data/repo';

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
      const { userProfile } = action.payload.data;
      state.firstName = userProfile.firstName;
      state.lastName = userProfile.lastName;
      state.address = userProfile.address;
      state.city = userProfile.city;
      state.state = userProfile.state;
      state.postalCode = userProfile.postalCode;
    },
    setProfile: (state, action) => {
      console.log(action);
    }
  }
});

const { getProfile, setProfile } = userProfileSlice.actions;

export const fetchUserProfileAsync = email => dispatch => {
  getUserProfile(email).then(result => dispatch(getProfile(result)));
}

export const setUserProfileAsync = userProfile => dispatch => {
  updateUser(userProfile).then(result => dispatch(setProfile(result)));
}

export const userProfileSelect = state => state.userProfile;

export default userProfileSlice.reducer;
