import { configureStore } from '@reduxjs/toolkit';
import loginReducer from './loginSlice';
import categoriesReducer from './categoriesSlice';

export default configureStore({
  reducer: {
    login: loginReducer,
    categories: categoriesReducer
  }
});
