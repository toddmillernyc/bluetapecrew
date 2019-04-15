import React, { Component } from 'react';

export default class ColorPanel extends Component {
  constructor(props) {
    super(props)
  }

  render = () => 
    <div className="card card-outline-secondary">
      <form onSubmit={this.handleSubmit} className="form" role="form">
        <div className="card-body">    
          <table className="table">
            <thead>
              <tr>
                <th>Id</th>
                <th>Color</th>
                <th></th>
                </tr>
            </thead>
            <tbody>
            {
              this.props.colors.map(color =>
                <tr>
                  <td>{color.id}</td>
                  <td>{color.colorText}</td>
                  <td></td>
                </tr>
              )
            }
              <tr>
                <td><strong>Add New Size:</strong></td>
                <td><input type="text" name="size" /></td>
                <td>
                  <input type="hidden" name="productId" value="1" />
                  <button type="submit" className="btn btn-success">Add Size</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </form>
    </div>
  }  