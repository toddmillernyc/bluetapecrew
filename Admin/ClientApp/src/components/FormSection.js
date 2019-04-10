import React, { Component } from 'react';
import InputGroup from '../components/InputGroup'

export default class FormSection extends Component {

    render() {
        return (
            <div>
				<h4>{this.props.title}</h4>
				{
					this.props.fields.map((field, index)=> {
						return <InputGroup key={index} {...field} />
 					})
				}
            </div>
        )
    }
}