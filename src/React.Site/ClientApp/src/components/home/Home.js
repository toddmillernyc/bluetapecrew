import React from 'react';
import { Row, Col } from 'react-bootstrap'
import gql from "graphql-tag";
import { Query } from '@apollo/client/react/components';
import ProductCard from '../product-card/ProductCard';

const GET_PRODUCTS = gql`
{
  products {
    id
    name: productName
    slug
  }
}
`;

function Home() {

  return (
    <Query query={GET_PRODUCTS}>
      {({ loading, error, data }) => {
        if (loading) return <p>Loading...</p>;
        if (error) return <p>{JSON.stringify(error)}(</p>;
        return (
          <Row>
            {data.products.map(product => {
              console.log(product)
              return (
                <Col key={product.id} >
                  <ProductCard {...product} />
                </Col>
              )
            })

            }
            
          </Row>
        )
      }}
    </Query>
  )
}

export default Home;