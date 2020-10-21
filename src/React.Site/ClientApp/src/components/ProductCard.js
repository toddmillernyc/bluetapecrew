import React from 'react';
import { Card, ListGroup } from 'react-bootstrap'
import ProductImage from './ProductImage';

const ProductCard = ({ name, imageId, styles }) => {
  return(
    <Card style={{ width: '18rem', borderTop: 0, borderLeft:0, borderRight:0 }}>
    <Card.Header style={{paddingBottom:0, paddingLeft: 0, paddingTop: 0}}>
      <Card.Title style={{marginBottom: 0, paddingBottom: 0}}>
        {name}
      </Card.Title>
    </Card.Header>
    <ProductImage imageId={imageId} className="card-img-top" />
    <Card.Body>
    <ListGroup>
        <ListGroup.Item>
            
        </ListGroup.Item>
      </ListGroup>
    </Card.Body>
  </Card>
  )
}
export default ProductCard;