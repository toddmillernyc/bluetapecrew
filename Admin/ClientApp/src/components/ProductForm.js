import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import { updateProduct } from '../Api'

export default class ProductForm extends Component {
  constructor(props) {
    super(props)
    this.state = this.props.product;
    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount = async() => {
    this.setState(this.props)
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  }

  handleSubmit = async(event) => {
    event.preventDefault()
    await updateProduct(this.state.id, this.state)
  }

  render() {
      const inputClass = "form-control"
      const groupClass = "form-group"
    return (
            <div className="card card-outline-secondary">
              <div className="card-body">
              <form onSubmit={this.handleSubmit} className="form">
                  <div className={groupClass}>
                    <label htmlFor="name">Product Name</label>
                    <input
                      className={inputClass}
                      placeholder="Product name"
                      name="name" 
                      type="text" 
                      value={this.state.name} 
                      onChange={this.handleInputChange} 
                     />
                  </div>
                  
                  <div className={groupClass}>
                    <label htmlFor="slug">Slug</label>
                    <input
                      name="slug"
                      placeholder="Slug"
                      className={inputClass}
                      type="text"
                      value={this.state.slug} 
                      onChange={this.handleInputChange} 
                     />
                  </div>

                  <div className="form-group">
                    <label htmlFor="description">Description</label> 
                    <textarea 
                    rows={10}
                    name="description"
                    placeholder="description"
                    value={this.state.description} 
                    onChange={this.handleInputChange} 
                    className={inputClass} >
                  </textarea>
                  </div>
                  <div className="form-group">
                    <button 
                      className="btn btn-success btn-lg float-right" 
                      type="submit">Save</button>
                  </div>
                </form>
              </div>
            </div>
    )
  }
}