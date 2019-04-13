import React, { Component } from 'react';
import "react-tabs/style/react-tabs.css";

export default class FormGroup extends Component {
  constructor (props) {
    super(props)
  }
  render () {
    return (
        <div className="form-group">
          <label htmlFor={this.props.id}>{this.props.label}</label>
          {
            this.props.type === "textarea"
            ? <textarea 
                id={this.props.id} 
                className="form-control"
                value={this.props.value}
                onChange={this.props.onChange}
                ></textarea>
            : <input
                id={this.props.id}
                className="form-control"
                type={this.props.type}
                value={this.props.value}
                onChange={this.props.onChange}
                />
          }
        </div>
    )
  }
}