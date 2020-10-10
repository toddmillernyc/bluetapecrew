import React from 'react';
import { Row, Col } from 'react-bootstrap'
import ProductCard from './ProductCard';
import { useDispatch, useSelector } from 'react-redux';
import { fetchProductsAsync, selectProducts } from '../store/productsSlice';

const Home = () => {
  const dispatch = useDispatch();
  dispatch(fetchProductsAsync());
  
  const products = useSelector(selectProducts)

  return (
    <Row>
      {products.map(product => 
          <Col key={product.id} >
            <ProductCard {...product} />
          </Col>)
}
    </Row>
  )
}

export default Home;