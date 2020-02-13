import React from 'react'
import PropTypes from 'prop-types'
import { Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const CategoryView = ({ id, imageId, name, published, onEditClick }) => (
    <tr className="row">
        <td className="col">{id}</td>
        <td className="col">{imageId}</td>
        <td className="col">{name}</td>
        <td className="col">{
        published 
        ? 
        <FontAwesomeIcon icon="check" size="xs" />
        : 
        <FontAwesomeIcon icon="times" size="xs" />
        }
        </td>
        <td className="col">
            <Button
                id={id}
                onClick={onEditClick}
                name="edit-button"
                size="sm"
                title="Edit Category">
                <FontAwesomeIcon icon="pencil-alt" size="xs" />
            </Button>
        </td>
    </tr>
)

CategoryView.propTypes = {
    id: PropTypes.number.isRequired,
    imageId: PropTypes.number,
    name: PropTypes.string.isRequired,
    published: PropTypes.bool.isRequired,
    onEditClick: PropTypes.func.isRequired
}

export default CategoryView