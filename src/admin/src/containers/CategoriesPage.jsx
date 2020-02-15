import React from 'react'
import Categories from '../components/categories/Categories'

export default class CategoriesPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: []
    };
    this.saveCategory = this.saveCategory.bind(this)
    this.baseUrl = "https://localhost:44320"
  }

  async componentDidMount() {
    const response = await fetch(`${this.baseUrl}/categories`)
    this.setState({ categories: await response.json()})
  }

  async saveCategory(category) {
    const response = await fetch(`${this.baseUrl}/categories`, 
    {
      method: 'PUT',
      body: JSON.stringify(category),
      headers: {
        'Content-Type': 'application/json',
      }
    })
    
    let updatedCategories = []
    this.state.categories.forEach(stateCategory => {
      if(stateCategory.id === category.id) {
        updatedCategories.push(category)
      }
      else {
        updatedCategories.push(stateCategory)
      }
    })
    this.setState({ categories: updatedCategories })
  }

  render() {
    return React.createElement(Categories, { 
      categories: this.state.categories,
      saveCategory: this.saveCategory
    });
  }
}