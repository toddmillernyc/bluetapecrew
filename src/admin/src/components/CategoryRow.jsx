import React, { useState } from 'react'
import { Button } from 'react-bootstrap'
import Switch from 'react-switch'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import PropTypes from 'prop-types';

const CategoryRow = ({ id, name, published, handleSave, handleDelete }) => {
  
  const [edit, setEdit] = useState(false)
  const [nameValue, setNameValue] = useState(name)
  const [publishedValue, setPublishedValue] = useState(published)

  function toggleEdit() { setEdit(!edit) }
  
  function handleCancel() {
    toggleEdit()
    setPublishedValue(published)
    setNameValue(name)
  }

  function onSaveClick() { 
    toggleEdit()
    handleSave({
      id: id,
      name: nameValue,
      published: publishedValue
    })
  }
  
  function handleNameChange(event) {
    setNameValue(event.target.value)
  }

  function onDeleteClick() {
    var txt;
    var confirmDelete = window.confirm(`Are you sure you want to delete the ${name} category?`);
    if (confirmDelete) {
      handleDelete({id})
    }
  }

  return (
    <tr>
      <td>
          {
            edit
            ? <input defaultValue={nameValue} onChange={handleNameChange} />
            : nameValue
          }
      </td>
      <td>
          {
            edit
            ? <Switch 
                height={21}
                width={42}
                checked={publishedValue}
                onChange={()=> setPublishedValue(!publishedValue) }>
              </Switch>
            : <FontAwesomeIcon 
                className={ publishedValue ? "text-success" : "text-danger" }
                icon={ publishedValue ? "check" : "times" } />
          }
      </td>
      <td>
        {
          edit
          ? <div className="float-right">
              <Button className="mr-2" size="sm" variant="outline-success" onClick={onSaveClick}>
                Save
              </Button>
              <Button size="sm" variant="outline-danger" className="mr-2" onClick={onDeleteClick}>
                Delete
              </Button>
              <Button size="sm" variant="outline-secondary" onClick={handleCancel}>
                Cancel
              </Button>
            </div>
          : <Button size="sm" variant="outline-info" onClick={toggleEdit} className="float-right">
              Edit
            </Button>
        }
      </td>
    </tr>
  )
}

CategoryRow.propTypes = {
  id: PropTypes.number.isRequired,
  name: PropTypes.string.isRequired,
  published: PropTypes.bool.isRequired,
  handleSave: PropTypes.func.isRequired,
  handleDelete: PropTypes.func.isRequired,
}

export default CategoryRow