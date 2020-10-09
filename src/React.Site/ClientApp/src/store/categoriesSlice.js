import { createSlice } from '@reduxjs/toolkit';
import { getCategories } from './repo';

export const categoriesSlice = createSlice({
  name: 'categories',
  initialState: { data: [] },
  reducers: {
    updateCategories: (state, action) => {
      state.data = action.payload;
    }
  }
});

export const { updateCategories } = categoriesSlice.actions;

export const getCategoriesAsync = () => dispatch => {
  getCategories().then(result => {
    dispatch(updateCategories(result.data.categories));
  }) 
};

export const selectCategories = state => state.categories.data;

export default categoriesSlice.reducer;