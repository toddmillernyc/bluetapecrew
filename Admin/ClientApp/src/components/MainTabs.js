import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import { getCategories, getImage } from '../modules/Api'
import "react-tabs/style/react-tabs.css";
import Settings from './settings/Settings'

export default class MainTabs extends Component {
  constructor(props) {
    super(props)
    this.state = {
      categories: []
    }
  }
  componentDidMount = async() => this.setState({categories:  await getCategories()})
  render () {
    return (
      <Tabs>
      <TabList>
        <Tab>Site Settings</Tab>
        {
          this.state.categories.map((category, index) => {
            return (<Tab key={index}>{category.name}</Tab>)
          })
        }
      </TabList>
  
      <TabPanel>
        <h2>SiteSettings</h2>
        <Settings />
      </TabPanel>
      {
          this.state.categories.map((item, index) => {
            return (
              <TabPanel key={index}>
                <h2>{item.name}</h2>
                <table className="table">
                  <thead>
                    <tr>
                      <th>Main Image</th>
                      <th>Product</th>
                      <th>Description</th>
                    </tr>
                  </thead>
                  <tbody>
                  {
                    item.products.map((itm, index) => {
                      return (
                        <tr key={index}>
                          <td>
                            <img src={"http://localhost/images/productthumb/" + itm.imageId} />
                          </td>
                          <td>
                            <a href="~/Admin/AdminProducts/Edit/@itm.Id" className="bold label label-info">{itm.name}</a>
                          </td>
                          <td>
                            <p className="text text-danger">{itm.description}</p>
                          </td>
                        </tr>
                      )
                  })
                  } 
                  </tbody>
                </table>
                <hr />
              </TabPanel>
            )
          })
        }
    </Tabs>
    );
  }
}
