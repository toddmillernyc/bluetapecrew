import React, { Component } from 'react';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import { getCategories } from '../modules/Api'
import "react-tabs/style/react-tabs.css";
import Settings from '../components/Settings'

export default class MainTabs extends Component {

  componentDidMount = async() => {
    this.setState({categories: await getCategories()})
  }

  render () {
    return (
      <Tabs>
      <TabList>
        <Tab>Site Settings</Tab>
        {
           Object.keys(this.props.categories).map((key, index) => ( 
            <Tab key={index}>{this.state.categories[key]}</Tab>
          ))
        }
      </TabList>
  
      <TabPanel>
        <h2>SiteSettings</h2>
        <Settings />
      </TabPanel>
      {
           Object.keys(this.props.categories).map((key, index) => ( 
            <TabPanel key={index}>
              <h2>{this.props.categories[key]}</h2>
            </TabPanel>
          ))
        }
    </Tabs>
    );
  }
}
