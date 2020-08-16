import { Button } from 'react-bootstrap'
import PropTypes from 'prop-types';
import React, { useState } from 'react'

const Cancel = "Cancel"
const Delete = "Delete"
const Edit = "Edit"
const Save = "Save"

const EditActions = ({ onEdit, onSave, onDelete, onCancel }) => {

  const [isEditMode, setEditMode] = useState(false)

  function click(event) {

    const text = event.target.innerHTML

    switch (text) {

      case Cancel:
        onCancel()
        setEditMode(false)
        break

      case Delete:
        if(onDelete()) {
          setEditMode(false)
        }
        break

      case Edit:
        setEditMode(true)
        onEdit()
        break
      
      case Save:
        setEditMode(false)
        onSave()
        break

      default:
        console.log(text)
    }
  }

  return (
    isEditMode
      ? <>

        <Button className="mr-2" size="sm" variant="outline-success" onClick={click}>
          {Save}
        </Button>

        <Button size="sm" variant="outline-danger" className="mr-2" onClick={click}>
          {Delete}
        </Button>

        <Button size="sm" variant="outline-secondary" onClick={click}>
          {Cancel}
        </Button>
        
        </>

      : <Button size="sm" variant="outline-info" onClick={click}>
          {Edit}
        </Button>
  )
}

EditActions.prototypes = {
  onEdit: PropTypes.func.isRequired,
  onSave: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired
}

export default EditActions