import React, { Component } from 'react';

export default class ProductImages extends Component {
  constructor(props) {
    super(props)
    console.log(this.props)
  }

  onSubmit = () => {console.log("submit")}
  handleClick = () => {}

  render = () =>
    <div className="col-md-4">
      <h1>Images</h1>
      <h2>Main Image</h2>
      <form onSubmit={this.onSubmit}>
        <div className="form-group">
            <div className="col-md-10">
                <input type="file" name="file"/>
                <input type="submit" value="Upload" className="btn btn-default" />
            </div>
        </div>
        <div id="imageWrapper">
            <img src="main image"/>
        </div>
      </form>
      <hr/>
      <h2>Additional Images</h2>
      <div className="row">
          <div className="col-xs-12">
            <h3>Add New Image</h3>
            <form encType="multipart/form-data">
                <input type="file" name="file"/>
                <input type="submit" value="Upload" className="btn btn-default"/>
            </form>
          </div>
        <hr/>
    </div>
    {
      this.props.images.map((image, index) => 
        <div key={index} className="row" id="imageRow@(item.Id)">
          <hr/>
          <div className="col-xs-9">
            <img className="listImage" src="data:@item.MimeType;base64,@Convert.ToBase64String(item.ImageData)"/>
          </div>
          <div className="col-xs-3">
            <button 
              className="btn rounded-2 btn-danger" 
              onClick={this.handleClick}>
                <i className="fa fa-remove"></i></button>
          </div>
        </div>        
      )
    }
    </div>
  }  


