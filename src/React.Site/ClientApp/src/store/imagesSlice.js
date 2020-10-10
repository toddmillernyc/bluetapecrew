import { createSlice } from '@reduxjs/toolkit';
import { getImage } from '../data/repo';

const imagesSlice = createSlice({
  name: 'images',
  initialState: {
  },
  reducers: {
    addImage: (state, action) => {
      const image = action.payload.data.imageData;
      state[image.id] = image.src;
    }
  }
});

export const fetchImageAsync = imageId => dispatch => {
  const { addImage } = imagesSlice.actions;
  getImage(imageId).then(result => { dispatch(addImage(result)); }) 
};

export const selectImage = state => state.images;
   
export default imagesSlice.reducer;