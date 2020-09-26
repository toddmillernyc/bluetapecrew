﻿import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './components/app/App'
import * as serviceWorker from './serviceWorker'
import 'bootstrap/dist/css/bootstrap.min.css';
import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client';

const client = new ApolloClient({
  uri: 'https://localhost:5001',
  cache: new InMemoryCache()
});

const rootElement = document.getElementById('root')
ReactDOM.render(
    <ApolloProvider client={client}>
        <App />
    </ApolloProvider>,
    rootElement
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();