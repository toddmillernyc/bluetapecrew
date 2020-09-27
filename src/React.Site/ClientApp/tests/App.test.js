import React from 'react';
import { render, waitFor } from '@testing-library/react';
import App from '../src/components/App';
import { MockedProvider } from '@apollo/client/testing';
import GraphQLMocks from '../tests/__mocks__/graphQLMock';
import '@testing-library/jest-dom/extend-expect';

//todo: this should be moved to a test on the Pre-Header Component
//used this to set up mocks at root

test('logo alt text renders as "Ecommerce Website logo" ', async () => {
  const { getByAltText } = render(
  <MockedProvider mocks={GraphQLMocks} addTypename={false}>
    <App />
  </MockedProvider>);

  await waitFor(() => {
    const linkElement = getByAltText('Ecommerce Website logo');
    expect(linkElement).toBeInTheDocument();
  });
});
