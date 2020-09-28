import React from 'react';
import { gql, useQuery } from "@apollo/client";

export const GET_IMAGE = gql`
  query ImageData($id: Int!) {
    imageData(id: $id) {
      src
    }
  }
`;

const ProductImage = ({ imageId, className }) => {
  
  const { data, loading, error } = useQuery(GET_IMAGE, { variables: { id: imageId } });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{JSON.stringify(error)}(</p>;
  if (!data) return <p>Not Found</p>;

  return(
    <img src={`data:image/png;base64,${data.imageData.src}`} className={className}   />
  )
}

export default ProductImage;