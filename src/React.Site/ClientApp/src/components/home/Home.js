import React from 'react';
import { Row, Col } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";
import ProductCard from '../product-card/ProductCard';

const GET_PRODUCTS = gql`
{
  products {
    id
    name: productName
  }
}
`;

const Home = () => {

  const { data, loading, error } = useQuery(GET_PRODUCTS);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>{JSON.stringify(error)}(</p>;
  if (!data) return <p>Not Found</p>;

  return (
    <Row>
      {data.products.map(product => <Col key={product.id} >
        <ProductCard {...product} />
      </Col>)
      }
    </Row>
  )
}

export default Home;