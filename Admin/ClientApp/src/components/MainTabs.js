import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import { getCategories } from '../modules/Api'
import "react-tabs/style/react-tabs.css";
import Settings from './settings/Settings'

export default class MainTabs extends Component {
  constructor(props) {
    super(props)
    this.state = {
      categories: []
    }
  }
  componentDidMount = async() => {
    this.setState({categories: await getCategories()})
  }

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
          this.state.categories.map((category, index) => {
            return (
              <TabPanel key={index}>
                <h2>{category.name}</h2>
              </TabPanel>
            )
          })
        }
    </Tabs>
    );
  }
}
