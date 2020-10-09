import { createSlice } from '@reduxjs/toolkit';
import { getSiteProfile } from './repo';

export const siteProfileSlice = createSlice({
  name: 'siteProfileSlice',
  initialState: { contactEmailAddress: '', contactPhoneNumber: '',  siteTitle: '' },
  reducers: {
    act: (state, action) => {

      console.log(state)
      console.log(action)
    }
  }
});

export const { act } = siteProfileSlice.actions;

export const fetchSiteProfileAsync = () => dispatch => {
  getSiteProfile().then(result => {
    dispatch(act(result))
  }) 

};

export const selectSiteProfile = state => state.siteProfile;

export default siteProfileSlice.reducer;