import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";

export default class ProductStyles extends Component {
  constructor(props) {
    super(props)
      console.log(this.props.model)
    }

  render () {

    return (

      <div className={this.props.cardClass}>
        <span className="anchor" id="formRegister"></span>
        <div className="card card-outline-secondary">
          <div className="card-header">
            <h3 className="mb-0">Styles</h3>
          </div>
          <div className="card-body">
          <form onSubmit={this.handleSubmit} className="form" role="form">

          <h4>Styles</h4>
            <table className="table">
                <thead>
                  <tr>
                    <th>Id</th><th>Color</th><th>Size</th><th>Price</th>
                  </tr>
                </thead>
                <tbody>
                  {
                    this.props.model.styles.map(style => {
                      return(
                      <tr>
                        <td>{style.id}</td>
                        <td>{style.colorText}</td>
                        <td>{style.sizeText}</td>
                        <td>{style.price}</td>
                      </tr>
                      )
                    })
                  }
                                  <tr>
                    <td></td>
                    <td>Color</td>
                    <td>Size</td>
                    <td>Price</td>
                  </tr>
                </tbody>
            </table>
        </form>
        </div>
        </div>
        </div>


    )
  }
}