import { configureStore } from '@reduxjs/toolkit';
import loginReducer from './loginSlice';
import categoriesReducer from './categoriesSlice';
import siteProfileSliceRecuder from './siteProfileSlice';
import imagesSliceReducer from './imagesSlice';
import productsSliceReducer from './productsSlice';

export default configureStore({
  reducer: {
    login: loginReducer,
    categories: categoriesReducer,
    siteProfile: siteProfileSliceRecuder,
    images: imagesSliceReducer,
    products: productsSliceReducer
  }
});
