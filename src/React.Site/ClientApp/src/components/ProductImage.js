import React from 'react';

import { useSelector, useDispatch } from 'react-redux';
import { fetchImageAsync, selectImage } from '../store/imagesSlice';

const ProductImage = ({ imageId, className }) => {
  const dispatch = useDispatch();
  dispatch(fetchImageAsync(imageId));
  const imageSrc = useSelector(selectImage)[imageId];
  return <img className={className} src={imageSrc && `data:image/png;base64,${imageSrc}`} />;
}

export default ProductImage;