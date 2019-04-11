import React, { Component } from 'react';
import InputGroup from '../components/InputGroup'

export default class DynamicForm extends Component {
    constructor(props) {
        super(props)
        this.state = {
            ui: []
        }
    }
    
    renderControl(schema) {
        return(
            <input />
        )
    }

    renderThing(thing) {
        switch(this.isSection(thing)) {
            case true:
                return (
                    <div>
                        <h1>Section</h1>
                        {this.renderThing(thing)}
                    </div>
                )
            default:
                if(typeof(thing) === "object") {
                    const ui = Object.keys(thing).map(key => {
                        return (
                            <InputGroup key={key} {...thing[key]} />
                        )
                    })
                    return ui
                }

        }
        // var ui = Object.keys(thing).map((key)=> {
        //     const schema = thing[key]
        //     switch(typeof(schema)) {
        //         case "object":
        //             return this.renderControl(schema)
        //         default:
        //             return
        //     }
        // })
        // return ui
    }

    isSection(obj) {
        let is = true
        Object.keys(obj).map(key => { 
            const type = typeof(obj[key])
            if(type != "object") is = false
        })
        return is
    }

    componentDidMount() {
        const schema = this.props.schema
        this.setState({ui: this.renderThing(schema)})
    }
    
    render() {
        return (
            <form>
                {
                    this.state.ui
                }
            </form>
        )
    }
}