import { createSlice } from '@reduxjs/toolkit';
import { getProducts } from '../data/repo';

const productsSlice = createSlice({
  name: 'products',
  initialState: { data: [] },
  reducers: {
    updateProducts: (state, action) => {
      state.data = action.payload.data.products;
    }
  }
});

export const fetchProductsAsync = () => dispatch => {
  getProducts()
    .then(result => dispatch(productsSlice.actions.updateProducts(result))) 
};

export const selectProducts = state => state.products.data;

export default productsSlice.reducer;