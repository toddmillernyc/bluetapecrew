import React, { useState, useEffect } from 'react';
import { getImage } from '../data/repo';

const ProductImage = ({ imageId, className }) => {
  const [imageSrc, setImageSrc] = useState(0);

  useEffect(() => {
    getImage(imageId).then(result => {
      setImageSrc(result.data.imageData.src);
    });
  });
  return <img className={className} src={imageSrc && `data:image/png;base64,${imageSrc}`} />;
}

export default ProductImage;