import React from 'react';
import { Card } from 'react-bootstrap'
import ProductImage from './ProductImage';

const ProductCard = ({ name, imageId }) => {
  return(
    <Card style={{ width: '18rem' }}>
    <ProductImage imageId={imageId} className="card-img-top" />
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