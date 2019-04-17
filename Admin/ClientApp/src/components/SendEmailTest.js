import React, { Component } from 'react';

export default class SendEmailTest extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isValid: false,
            status: 0,
            value: ""
        }
        this._onChange = this._onChange.bind(this);
    }

    _onChange(event) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        const isValid = re.test(String(event.target.value).toLowerCase());
        this.setState({
            isValid: isValid,
            value: event.target.value,
            status: 0,
            error: ""
        })
    }

    _onClick = async() => {
        try {
            const result = await fetch("http://localhost/api/emailtest?email="+ this.state.value)
            
            this.setState({status: result.status})
        }
        catch(error) {
            this.setState({error: error})
        }
    }

    getStatusMessage(status) {
        switch(status) {
            case 0: return null
            case 200: return <span className="text text-success">Email Sent Successfully</span>
            default: return <span className="text text-danger">Unable to send Email</span>
        }
    }

    render() {
        return (
            <div>
                <div className="input-group input-group-sm mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text">Test Send Email</span>
                    </div>
                    <input
                        onChange={this._onChange}
                        type="email" 
                        className="form-control" 
                        placeholder="recipient@gmail.com"/>
                    <div className="input-group-append">
                        <button
                            type="button"
                            className={
                                this.state.isValid
                                ? "btn btn-sm btn-success"
                                : "btn btn-sm btn-warning"
                            }
                            disabled={!this.state.isValid}
                            onClick={this._onClick}
                        >Send</button>
                    </div>
                </div>
                { this.getStatusMessage(this.state.status) }
                {
                    this.state.error
                    ? <span className="text text-danger">Error Sending Email statusCode: {this.state.error}}</span>
                    : null
                }
            </div>
         )
    }
}
