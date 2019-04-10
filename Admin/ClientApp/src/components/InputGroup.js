import React, { Component } from 'react';

export default class InputGroup extends Component {

    renderControl(props) {
        switch(props.type) {
            case 'button':
                return <button>{props.label}</button>
            default:
                return <input type="text" className="form-control" id={props.id} />;
        }
      }

      
    render() {
        return (
            <div className="input-group input-group-sm mb-3">
                <div className="input-group-prepend">
                    <span className="input-group-text">{this.props.label}</span>
                </div>
                {this.renderControl(this.props)}
            </div>
        )
    }
}