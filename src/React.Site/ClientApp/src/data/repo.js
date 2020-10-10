import { ApolloClient,InMemoryCache, gql } from "@apollo/client";

 const client = new ApolloClient({
  uri: 'https://localhost:5001',
  cache: new InMemoryCache()
});

export const getCategories = () => client.query({
  query: gql`
    {
      categories(where: { published: true }, order_by: { position: ASC }) {
        name
        productCategories{
          product {
            id
            productName
          }
        }
      }
    }
  `
});

export const login = credentials => client.mutate({ 
  variables: credentials,
  mutation: gql`
    mutation login($email: String!, $password: String!) {
      login(email: $email, password: $password) {
        token
      }
    }
  `
});

export const getSiteProfile = () => client.query({
  query: gql`
  {
    siteProfile {
      contactPhoneNumber
      contactEmailAddress
      siteTitle
    }
  }
  `
});

export const getImage = imageId => client.query({
  variables: { id: imageId },
  query: gql`
  query ImageData($id: Int!) {
    imageData(id: $id) {
      id
      src
    }
  }
`
});

export const getProducts = () => client.query({
  query: gql`
  {
    products {
      id
      name: productName
      imageId
    }
  }
  `
});