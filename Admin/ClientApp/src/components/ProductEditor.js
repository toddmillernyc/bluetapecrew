import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import ProductStyles from './ProductStyles'
import ProductForm from './ProductForm'
import { getProductStyles } from '../Api'

export class ProductEditor extends Component {
  constructor(props) {
    super(props)

    this.state = {
      styles: []
    }

    this.id = this.props.match.params.id
    this.cardClass = "col-sm"
  }

  componentDidMount = async() => {
    this.setState({styles: await getProductStyles(this.id)}, ()=>{
      console.log(this.state)
    })
  }

  render () {
    return (
      <div className="row">
        <ProductForm   id={this.id} cardClass={this.cardClass} />
        <ProductStyles id={this.id} cardClass={this.cardClass} model={this.state.styles} />
      </div>
    )
  }
}
