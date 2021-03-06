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
        email
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
      aboutUs
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
      styles {
        id
        price
        size {
          order: sizeOrder
          text: sizeText
        }
        color {
          text: colorText
        }
      }
    }
  }
  `
});

export const getUserProfile = email => client.query({
  variables: { email },
  query: gql`
    query UserProfile($email: String!) {
      userProfile(email: $email) {
        id
        firstName
        lastName
        address
        city
        state
        postalCode
      }
    }
  `
});

export const updateUser = user => client.mutate({ 
  variables: { userProfile: user },
  mutation: gql`
    mutation UpdateUser($userProfile: UserProfileInput) {
      updateUser(user: $userProfile) {
        id
        firstName
        lastName
        address
        city
        state
        postalCode
      }
    }
  `
});