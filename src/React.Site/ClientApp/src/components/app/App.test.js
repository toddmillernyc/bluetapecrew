import React from 'react';
import { render } from '@testing-library/react';
import App from './App';
import { MockedProvider } from '@apollo/client/testing';

test('renders learn react link', () => {
  const { container } = render(
    <MockedProvider mocks={[]}>
      <App />
    </MockedProvider>
  );

  console.log(container)
  //expect(linkElement).toBeInTheDocument();
});
