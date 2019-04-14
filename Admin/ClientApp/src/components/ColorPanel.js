
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs'
import React, { Component } from 'react';

export default class ColorPanel extends Component {
    constructor(props) {
      super(props)


    }

    render() {
        return (
        <TabPanel>
        <div className="card card-outline-secondary">
          <div className="card-body">
            <form onSubmit={this.handleSubmit} className="form" role="form">
              <table className="table">
                <thead><tr><th>Id</th><th>Color</th><th></th></tr></thead>
                <tbody>
                {
                                // console.log(this.state)
                                this.state.colors.map(color => {
                                  return(
                                    <tr>
                                      <td>{color.id}</td>
                                      <td>{color.name}</td>
                                      <td></td>
                                    </tr>
                                  )
                                })
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
              </form>
              </div>
              </div>
        </TabPanel>
        )
    }
}  