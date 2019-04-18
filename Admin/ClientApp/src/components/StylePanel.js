import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { addStyle, deleteSytle } from '../Api'

export default class StylePanel extends Component {
  constructor(props) {
    super(props)

    this.state = {
      color: 0,
      size: 0,
      price: ""
    }

    this.inputClass = "form-control form-control-sm"
    this.trashButton = "btn btn-danger btn-sm float-right"
    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);    
  }

  handleInputChange(event) {
    const target = event.target;
    this.setState({
      [target.name]: target.value
    });
  }

  handleTrashClick(id) {
    deleteSytle(id)
    window.location.reload()
  }

  handleSubmit(event) {
    event.preventDefault()
    addStyle({
      productId: this.props.product.id,
      sizeId: this.state.size,
      colorId: this.state.color,
      price: this.state.price
    })
    window.location.reload()
  }

  render = () =>
    <div className="card card-outline-secondary">
      <div className="card-body">
        <form onSubmit={this.handleSubmit} className="form">
          <table className="table">
            <thead>
              <tr>
                <th>Id</th><th>Color</th><th>Size</th><th>Price</th><th></th>
              </tr>
            </thead>
            <tbody>
            {
              this.props.styleVm.styles.map(style =>
                <tr key={style.id}>
                  <td>{style.id}</td>
                  <td>{style.colorText}</td>
                  <td>{style.sizeText}</td>
                  <td>{style.price}</td>
                  <td>
                    <button type="button" 
                      className={this.trashButton} 
                      onClick={()=>{this.handleTrashClick(style.id)}} >
                      <FontAwesomeIcon icon="trash" onClick={e=>e.preventDefault()} />
                    </button>
                  </td>
                </tr>
              )
            }
            </tbody>
          </table>
          <div className="form-inline">
            <div className="form-group">
              <select 
                name="color"
                className={this.inputClass}  
                onChange={this.handleInputChange}>
                <option value={0}>Color</option>
                {
                  this.props.styleVm.colors.map(color=>
                  <option 
                    value={color.id} 
                    key={color.id}>
                      {color.colorText}
                  </option>)
                }
              </select>
            </div>
            <div className="form-group">
              <select 
                name="size"
                className={this.inputClass} 
                onChange={this.handleInputChange}>
                <option value={0}>Size</option>
                {
                  this.props.styleVm.sizes.map(size=> 
                  <option 
                    value={size.id}
                    key={size.id}>{size.sizeText}
                  </option>)
                }
              </select>
            </div>
            <div className="form-group">
              <input 
                name="price"
                className={this.inputClass}
                placeholder="price" 
                type="USD"
                onChange={this.handleInputChange}
              />
            </div>
            <button
              disabled={
                this.state.color === 0 || this.state.size === 0 || this.state.price === ""
              }
              type="submit" 
              className="btn btn-success btn-sm float-right">Save</button>
              </div>
          </form>
        </div>
      </div>

}