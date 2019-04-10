import React, { Component } from 'react';
import { getCategories } from '../modules/Api'
import "react-tabs/style/react-tabs.css";
import MainTabs from '../components/MainTabs'

export class Home extends Component {
  constructor (props) {
    super(props);
    this.state = { 
      categories: {}
    };
  }
  
  componentDidMount = async() => {
    this.setState({categories: await getCategories()})
  }

  render () {
    return (
      <div>
        <MainTabs categories={this.state.categories}/>
      </div>
    );
  }
}
