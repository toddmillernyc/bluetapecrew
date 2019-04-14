import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs'

export default class SizePanel extends Component {
  constructor(props) {
    super(props)
  }

  render() {

    return(
        <TabPanel>
          <div className="col-xs-6">
            <table className="table table-condensed table-bordered table-hover">
              <thead>
                <tr>
                                <th>Order</th>
                                <th>Size</th>
                                <th></th>
                            </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>XS</td>
                                    <td></td>
                                </tr>
                                <form action="/admin/adminproducts/createsize" method="post"></form>                                <tr>
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
          </TabPanel>
    )
}
}
