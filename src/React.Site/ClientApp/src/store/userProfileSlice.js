import { createSlice } from '@reduxjs/toolkit';

export const userProfileSlice = createSlice({
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
      state.firstName = payload.firstName;
      state.lastName = payload.lastName;
      state.address = payload.address;
      state.city = payload.city;
      state.state = payload.state;
      state.postalCode = payload.postalCod;
    }
  }
});

export const { getProfile } = userProfileSlice.actions;