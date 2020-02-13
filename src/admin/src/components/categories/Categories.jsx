import React from 'react'
import PropTypes from 'prop-types'
import Category from './Category'
import { Table } from 'react-bootstrap';

const Categories = ({ categories, saveCategory }) =>
        <div>
            <h1>Categories</h1>
            <Table striped bordered hover>
                <thead>
                    <tr className="row">
                        <th className="col">Id</th>
                        <th className="col">ImageId</th>
                        <th className="col">Name</th>
                        <th className="col">Published</th>
                        <th className="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
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


Categories.propTypes = {
    categories: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            imageId: PropTypes.number,
            name: PropTypes.string.isRequired,
            published: PropTypes.bool.isRequired,
        }).isRequired,
    ).isRequired,
    saveCategory: PropTypes.func.isRequired
}

export default Categories