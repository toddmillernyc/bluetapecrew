import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import ProductStyles from './ProductStyles'
import ProductForm from './ProductForm'
import { getProduct, getProductStyles } from '../Api'

export class ProductEditor extends Component {
  constructor(props) {
    super(props)
    this.state = {
      product: {},
      styleVm: {}
    }
  }

  componentDidMount = async() => {
    const id = this.props.match.params.id
    this.setState({
      product: await getProduct(id),
      styleVm: await getProductStyles(id)
    })
    
  }

  render () {
    const cardClass = "col-sm"
    return (
      this.state.product.id > 0
      ?  <div className="row">
          <ProductForm   cardClass={cardClass} {...this.state.product} />
          <ProductStyles cardClass={cardClass} {...this.state.styleVm} />
        </div>
      : null
    )
  }
}
