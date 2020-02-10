import React from "react";
import { Col, Row, Table } from 'react-bootstrap';

export default class CategoriesPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            categories: []
        };
      }
    
      async componentDidMount() {
        try {
          const response = await fetch('https://api/categories');
          var data = await response.json();
          console.log(data);
          if (response.ok) {
            this.setState({categories: data});
          }
          else {
            throw Error(response.statusText);
          }
        } catch (error) {
          console.log(error);
        }
    }

    render() {
        
        return (
            <Row>
                <Col>
                <h1>Categories</h1>
                <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>ImageId</th>
                        <th>Name</th>
                        <th>Published</th>
                    </tr>
                </thead>
                <tbody>
                {
                    this.state.categories !=null &&
                    this.state.categories.map((category, index) => {
                        return <tr key={index}>
                                    <td>{category.id}</td>
                                    <td>{category.imageId}</td>
                                    <td>{category.name}</td>
                                    <td>{category.published ? "X" : "" }</td>
                                </tr>
                    })
                }
                </tbody>
            </Table>
                </Col>
            </Row>
        );
    }
}