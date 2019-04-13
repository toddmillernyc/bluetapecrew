import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import { getProduct, updateProduct } from '../Api'

export default class ProductForm extends Component {
  constructor(props) {
    super(props)

    this.state = {};

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
    await updateProduct(this.props.id, this.state)
  }

  render() {
    const inputClass = "form-control"
    const groupClass = "form-group"
    return (
          <div className={this.props.cardClass}>
            <span className="anchor" id="formRegister"></span>
            <div className="card card-outline-secondary">
              <div className="card-header">
                <h3 className="mb-0">{this.state.productName}</h3>
              </div>
              <div className="card-body">
              <form onSubmit={this.handleSubmit} className="form" role="form">

                  <div className={groupClass}>
                    <label htmlFor="productName">Name</label>
                    <input
                      className={inputClass}
                      placeholder="Product name"
                      name="productName" 
                      type="text" 
                      value={this.state.productName} 
                      onChange={this.handleInputChange} 
                     />
                  </div>
                  
                  <div className={groupClass}>
                    <label htmlFor="linkName">Slug</label>
                    <input
                      name="linkName"
                      placeholder="Slug"
                      className={inputClass}
                      type="text"
                      value={this.state.linkName} 
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
          </div>

    )
  }
}