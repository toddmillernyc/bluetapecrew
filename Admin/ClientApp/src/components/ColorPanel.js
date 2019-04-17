import React, { Component } from 'react';

export default class ColorPanel extends Component {
  render = () => 
    <div className="card card-outline-secondary">
      <form onSubmit={this.handleSubmit} className="form">
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
              this.props.styleVm.colors.map(color =>
                <tr key={color.id}>
                  <td>{color.id}</td>
                  <td>{color.colorText}</td>
                  <td></td>
                </tr>
              )
            }
              <tr>
                <td><strong>Add Color:</strong></td>
                <td><input type="text" name="size" /></td>
                <td>
                  <input type="hidden" name="productId" value="1" />
                  <button type="submit" className="btn btn-success">Save</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </form>
    </div>
  }  