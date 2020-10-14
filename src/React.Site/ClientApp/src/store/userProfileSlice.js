import { createSlice } from '@reduxjs/toolkit';
import { getUserProfile, updateUser } from '../data/repo';

const userProfileSlice = createSlice({
  name: 'userProfile',
  initialState: {
    id: '',
    firstName: '',
    lastName: '',
    address: '',
    city: '',
    state: '',
    postalCode: ''
  },
  //todo: make these reducers DRY -- having issue sending state to mapping funciton
  reducers: {
    getProfile: (state, action) => {
      const { userProfile } = action.payload.data;
      state.id = userProfile.id;
      state.firstName = userProfile.firstName;
      state.lastName = userProfile.lastName;
      state.address = userProfile.address;
      state.city = userProfile.city;
      state.state = userProfile.state;
      state.postalCode = userProfile.postalCode;
    },
    setProfile: (state, action) => {
      const { updateUser } = action.payload.data;
      state.id = updateUser.id;
      state.firstName = updateUser.firstName;
      state.lastName = updateUser.lastName;
      state.address = updateUser.address;
      state.city = updateUser.city;
      state.state = updateUser.state;
      state.postalCode = updateUser.postalCode;
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
