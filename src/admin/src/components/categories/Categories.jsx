import React, { useEffect, useState } from 'react';
import Category from './Category'
import { Button, Col, Row, Table } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import CategoryNew from './CategoryNew'
import { getCategories, saveCategory } from '../../api/categoriesApi'

const Categories = () => {
    const [categories, setCategories] = useState([])

    useEffect(() => { async function runAsync() {
        setCategories(await getCategories())
    } runAsync() }, [])

    return (
        <div>
            <Row>
                <Col>
                    <h1>Categories</h1>
                </Col>
                <Col>
                    <Button size="sm" className="float-right" variant="success">
                        <FontAwesomeIcon icon="plus" size="xs" /> New Category
        </Button>
                </Col>
            </Row>
            <Table striped bordered hover>
                <thead>
                    <tr className="row">
                        {/* <th className="col">Id</th>
            <th className="col">ImageId</th> */}
                        <th className="col">Name</th>
                        <th className="col">Published</th>
                        <th className="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <CategoryNew></CategoryNew>
                    {
                        categories.map(category => (
                            <Category
                                key={category.id}
                                {...category}
                                onSaveCategory={saveCategory} />
                        ))
                    }
                </tbody>
            </Table>
        </div>
    )
}

export default Categories