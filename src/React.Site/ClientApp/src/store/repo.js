import { ApolloClient,InMemoryCache, gql } from "@apollo/client";

 const client = new ApolloClient({
  uri: 'https://localhost:5001',
  cache: new InMemoryCache()
});

const GET_CATEGORIES = gql`
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
`;
export const getCategories = () => client.query({query:GET_CATEGORIES});

const LOGIN_MUTATION = gql`
mutation login($email: String!, $password: String!) {
  login(email: $email, password: $password) {
    token
  }
}
`;
export const login = credentials => client.mutate({ variables: credentials, mutation: LOGIN_MUTATION });