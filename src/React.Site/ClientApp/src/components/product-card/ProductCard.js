import React from 'react';
import { Card } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";

export const GET_IMAGE = gql`
  query ImageData($id: Int!) {
    imageData(id: $id) {
      src
    }
  }
`;

const ProductCard = ({ name, imageId }) => {

const { data, loading, error } = useQuery(GET_IMAGE, { variables: { id: imageId } });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{JSON.stringify(error)}(</p>;
  if (!data) return <p>Not Found</p>;
  
  let base64Image = data.imageData.src;
  let src = `data:image/png;base64,${base64Image}`

  return(
    <Card style={{ width: '18rem' }}>
    <Card.Img variant="top" src={src} />
    <Card.Body>
      <Card.Title>{name}</Card.Title>
      <Card.Text>
      </Card.Text>
    </Card.Body>
    <Card.Body>
      <Card.Link href="#">Card Link</Card.Link>
      <Card.Link href="#">Another Link</Card.Link>
    </Card.Body>
  </Card>
  )
}
export default ProductCard;