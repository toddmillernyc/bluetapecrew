import React from 'react'
import PropTypes from 'prop-types'
import { Button } from 'react-bootstrap';
import Switch from "react-switch";

const CategoryEdit = ({ id, imageId, name, isPublished, onCancelEditClick, onPublishChange, onNameChange, onSave }) => (
    <tr className="row">
        <td className="col">{id}</td>
        <td className="col">{imageId}</td>
        <td className="col">
            <input
                type="text" 
                placeholder="Name"
                defaultValue={name}
                onChange={onNameChange} />
        </td>
        <td className="col">
            <Switch 
                height={21}
                width={42}
                onChange={onPublishChange} 
                checked={isPublished} />
        </td>
        <td className="col">
            <Button size="sm" title="Save Category" variant="success" className="mr-2" onClick={onSave}>Save</Button>
            <Button
                size="sm"
                title="Cancel Edit Category"
                variant="outline-secondary"
                onClick={onCancelEditClick}>Cancel</Button>
        </td>
    </tr>
)
CategoryEdit.propTypes = {
    id: PropTypes.number.isRequired,
    imageId: PropTypes.number,
    name: PropTypes.string.isRequired,
    isPublished: PropTypes.bool.isRequired,
    onCancelEditClick: PropTypes.func.isRequired,
    onPublishChange: PropTypes.func.isRequired,
    onNameChange: PropTypes.func.isRequired,
    onSave: PropTypes.func.isRequired
}
export default CategoryEdit