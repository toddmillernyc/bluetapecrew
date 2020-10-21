import { createSlice } from '@reduxjs/toolkit';
import { getProducts } from '../data/repo';

const productsSlice = createSlice({
  name: 'products',
  initialState: { data: [] },
  reducers: {
    updateProducts: (state, action) => {
      const products = action.payload.data.products.map(product => {

        const styles = product.styles.map(style => {
          return {
            styleId: style.id,
            text: `${style.size.text} / ${style.color.text}`
          };
        });
        return {...product, styles};
      })
      state.data = products;
    }
  }
});

export const fetchProductsAsync = () => dispatch => {
  getProducts()
    .then(result => dispatch(productsSlice.actions.updateProducts(result))) 
};

export const selectProducts = state => state.products.data;

export default productsSlice.reducer;