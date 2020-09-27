import React from 'react';
import { render, waitFor } from '@testing-library/react';
import App, { GET_PROFILE } from '../src/components/app/App';
import { GET_CATEGORIES } from '../src/components/header/Header';
import { GET_PRODUCTS } from '../src/components/home/Home';
import { MockedProvider } from '@apollo/client/testing';
import { GET_IMAGE } from '../src/components/product-card/ProductCard';
import '@testing-library/jest-dom/extend-expect';

//todo: this should be moved to a test on the Pre-Header Component
//used this to set up mocks at root

test('logo alt text renders as "Ecommerce Website logo" ', async () => {
  const mocks = [
    {
      request: { query: GET_PROFILE },
      result: {
        data: {
          siteProfile: { siteTitle: 'Ecommerce Website' }
        },
      },
    },
    {
      request: { query: GET_CATEGORIES },
      result: {
        data: {
          categories: [
            {
              name: 'Casual Wear',
              productCategories: [
                {
                  product: {
                    id: 2,
                    productName: "Blue Jeans"
                  }
                }
              ]
            }
          ]
        }
      },
    },
    {
      request: { query: GET_PRODUCTS },
      result: {
        data: {
          products: [
            { id: 1, name: 'T-Shirt', imageId: 1 }
          ]
        },
      },
    },
    {
      request: { query: GET_IMAGE, variables: { id: 1 } },
      result: {
        data: {
          imageData: {
            src: "base64StringImageSourceMock"
          }
        },
      },
    }
  ];

  const { getByAltText } = render(
  <MockedProvider mocks={mocks} addTypename={false}>
    <App />
  </MockedProvider>);

  await waitFor(() => {
    const linkElement = getByAltText('Ecommerce Website logo');
    expect(linkElement).toBeInTheDocument();
  });
});
