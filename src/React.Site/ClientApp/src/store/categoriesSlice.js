import { createSlice } from '@reduxjs/toolkit';
import { getCategories } from '../data/repo';

const categoriesSlice = createSlice({
  name: 'categories',
  initialState: { data: [] },
  reducers: {
    updateCategories: (state, action) => {
      state.data = action.payload.data.categories;
    }
  }
});

export const getCategoriesAsync = () => dispatch => {
  getCategories()
    .then(result => dispatch(categoriesSlice.actions.updateCategories(result))) 
};

export const selectCategories = state => state.categories.data;

export default categoriesSlice.reducer;