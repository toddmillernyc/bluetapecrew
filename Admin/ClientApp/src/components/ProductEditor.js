import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import ProductStyles from './ProductStyles'
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
    const imageVm = await api.getProductImages(id)
    this.setState({
      product: await api.getProduct(id),
      styleVm: await api.getProductStyles(id),
      imageVm
    })
    
  }

  render () {
    const cardClass = "col-sm"
    return (
      this.state.product.id > 0
      ? <div className="container-fluid">
          <div className="row">
            <ProductStyles cardClass={cardClass} {...this.state} />
            <ProductImages cardClass={cardClass} imageVm={this.state.imageVm} />
          </div>
        </div>
      : null
    )
  }
}
