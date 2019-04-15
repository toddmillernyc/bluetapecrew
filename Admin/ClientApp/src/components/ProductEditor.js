import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import ProductStyles from './ProductStyles'
import ProductForm from './ProductForm'
import * as api from '../Api'
import ProductImages from './ProductImages'

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
    const images = await api.getProductImages(id)
    this.setState({
      product: await api.getProduct(id),
      styleVm: await api.getProductStyles(id),
      images
    }, () => console.log(images))
    
  }

  render () {
    const cardClass = "col-sm"
    return (
      this.state.product.id > 0
      ? <div className="container-fluid">
          <div className="row">
            <ProductForm   cardClass={cardClass} {...this.state.product} />
            <ProductStyles cardClass={cardClass} {...this.state.styleVm} />
          </div>
          <div className="row">
            <ProductImages cardClass={cardClass} images={this.state.images} />
          </div>
        </div>
      : null
    )
  }
}
