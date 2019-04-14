
import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'  

export default class StylePanel extends Component {
    constructor(props) {
        super(props)
        this.state = {
          colors: []
        }
    }

    render() {
      const inputClass = "form-control form-control-sm"
      const trashButton = "btn btn-danger btn-sm float-right"
        return(
            <TabPanel>
            <div className="card card-outline-secondary">
              <div className="card-body">
              <form onSubmit={this.handleSubmit} className="form" role="form">
                <table className="table">
                    <thead>
                      <tr>
                        <th>Id</th><th>Color</th><th>Size</th><th>Price</th><th></th>
                      </tr>
                    </thead>
                    <tbody>
                    {
                        this.state !=null && this.state.styles != null
                        ? this.state.styles.map(style => {
                          return(
                            <tr key={style.id}>
                              <td>{style.id}</td>
                              <td>{style.colorText}</td>
                              <td>{style.sizeText}</td>
                              <td>{style.price}</td>
                              <td>
                            <button type="button" className={trashButton} onClick={()=>{this.handleTrashClick(style.id)}} >
                              <FontAwesomeIcon icon="trash" onClick={e=>e.preventDefault()} />
                            </button>
                              </td>
                            </tr>
                          )
                          })
                        : null
                      }
                    </tbody>
                </table>
    
                <div className="form-inline">
                  <div className="form-group">
                  <select 
                  name="color"
                  className={inputClass}  
                  onChange={this.handleInputChange}>
                          <option value={0}>Size</option>
                          {
                            this.state.colors != null
                            ? this.state.colors.map(color=> {
                              return(
                                  <option value={color.id} key={color.id}>{color.colorText}</option>
                              )
                            })
                            : null
                          }
                          </select>
                </div>
                <div className="form-group">
                <select 
                  name="size"
                  className={inputClass} 
                  onChange={this.handleInputChange}>
                          <option value={0}>Size</option>
                          {
                            this.state.sizes !=null
                            ? this.state.sizes.map(size=> {
                              return(
                                  <option value={size.id} key={size.id}>{size.sizeText}</option>
                              )
                            })
                          : null
                        }
                          </select>
                </div>
                <div className="form-group">
                <input 
                              name="price"
                              className={inputClass}
                              placeholder="price" 
                              type="USD"
                              onChange={this.handleInputChange}
                            />
                </div>
                <button
                  disabled={!this.state.size && !this.state.color && !this.state.price}
                  type="submit" 
                  className="btn btn-success btn-sm float-right">Add</button>
              </div>
            </form>
            </div>
            </div>
            </TabPanel>
        )
    }
}