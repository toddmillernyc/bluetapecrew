// tests/__mocks__/graphQLMock.js
import { GET_PROFILE } from '../../src/components/App';
import { GET_CATEGORIES } from '../../src/components/Header';
import { GET_PRODUCTS } from '../../src/components/Home';
import { GET_IMAGE } from '../../src/components/ProductCard';

module.exports = [
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