import React, { useEffect } from 'react';
import { Card } from 'react-bootstrap'

const ProductCard = ({ name, imageId }) => {
  return(
    <Card style={{ width: '18rem' }}>
    <Card.Img variant="top" src="holder.js/100px180?text=Image cap" />
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