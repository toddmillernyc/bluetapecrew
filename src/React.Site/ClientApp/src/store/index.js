import { configureStore } from '@reduxjs/toolkit';
import loginReducer from './loginSlice';
import categoriesReducer from './categoriesSlice';
import siteProfileSliceRecuder from './siteProfileSlice';

export default configureStore({
  reducer: {
    login: loginReducer,
    categories: categoriesReducer,
    siteProfile: siteProfileSliceRecuder
  }
});
