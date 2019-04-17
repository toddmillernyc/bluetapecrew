import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs'

import './css/ProductImages.css'

export default class ProductImages extends Component {
render = () =>
<Tabs className="card card-outline-secondary">
  <TabList className="card-header">
    <Tab>
      <img src={`data:image/png;base64,${this.props.imageVm.mainImage.thumbnail}`} alt="main thumbnail" />
    </Tab>
    {
      this.props.imageVm.images.map((image, index) => 
      <Tab key={index}>
        <img src={`data:image/png;base64,${image.thumbnail}`} alt="additional thumbnail" />
      </Tab>)
    }
  </TabList>
  <TabPanel>
    <img src={`data:image/png;base64,${this.props.imageVm.mainImage.data}`} alt="main product" />
      <button className="btn btn-danger align-top">
        <FontAwesomeIcon icon="trash" onClick={e=>e.preventDefault()} />  
      </button>
                    </TabPanel>
  {
    this.props.imageVm.images.map((image, index) => 
    <TabPanel key={index}>
      <img src={`data:image/png;base64,${image.data}`} alt="additional product" />
      <button className="btn btn-danger align-top">
      <FontAwesomeIcon
        icon="trash" 
        onClick={e=>e.preventDefault()} 
      />
      </button>
    </TabPanel>)
  }
</Tabs>
}