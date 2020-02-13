import React from 'react'
import Categories from '../components/categories/Categories'

export default class CategoriesPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: []
    };
  }

  async componentDidMount() {
    const response = await fetch('https://api/categories')
    this.setState({ categories: await response.json()})
  }

  render() {
    return React.createElement(Categories, { categories: this.state.categories });
  }
}