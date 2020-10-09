import { createSlice } from '@reduxjs/toolkit';
import { getSiteProfile } from './repo';

export const siteProfileSlice = createSlice({
  name: 'siteProfile',
  initialState: {     
    contactEmailAddress: '',
    contactPhoneNumber: '', 
    siteTitle: ''
  },
  reducers: {
    updateSiteProfile: (state, action) => {
      const siteProfile = action.payload;
      state.contactEmailAddress = siteProfile.contactEmailAddress;
      state.contactPhoneNumber = siteProfile.contactPhoneNumber;
      state.siteTitle = siteProfile.siteTitle;
    }
  }
});

export const { updateSiteProfile } = siteProfileSlice.actions;

export const fetchSiteProfileAsync = () => dispatch => {
  getSiteProfile().then(result => {
    dispatch(updateSiteProfile(result.data.siteProfile))
  }) 

};

export const selectSiteProfile = state => state.siteProfile;

export default siteProfileSlice.reducer;