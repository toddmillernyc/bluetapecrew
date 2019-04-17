import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";
import { deleteSytle } from '../Api'
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs'
import ColorPanel from './ColorPanel'
import StylePanel from './StylePanel';
import SizePanel from './SizePanel';
import ProductForm from './ProductForm'

export default class ProductStyles extends Component {
  constructor(props) {
    super(props)
    this.state = {}
    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleTrashClick = this.handleTrashClick.bind(this);
  }

  componentDidMount = async() => {
    this.setState(this.props)
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;
    this.setState({[name]: value});

    // if(name === "color" || name === "size") {
      
    // }
  }

  handleSubmit(event){
    event.preventDefault()
  }

  handleTrashClick = async(id) => {
    if (window.confirm("You sure ?")) { 
      const response = await deleteSytle(id)
      if(response.status===200) {
          window.location.reload()
      }
    }
  }

  render = () =>
    <div className={this.props.cardClass}>
      <Tabs className="card card-outline-secondary">
        <TabList className="card-header">
        <Tab>
            <h5 className="mb-0">Product</h5>
          </Tab>
          <Tab>
            <h5 className="mb-0">Styles</h5>
          </Tab>
          <Tab>
            <h5 className="mb-0">Colors</h5>
          </Tab>
          <Tab>
            <h5 className="mb-0">Sizes</h5>
          </Tab>
        </TabList>
        <TabPanel>
        </TabPanel>
          <ProductForm {...this.props} />
        <TabPanel>
          <StylePanel {...this.props} />  
        </TabPanel>
        <TabPanel>
          <ColorPanel { ...this.props } />
        </TabPanel>
        <TabPanel>
          <SizePanel { ...this.props }  />
        </TabPanel>
      </Tabs>
    </div>
  }