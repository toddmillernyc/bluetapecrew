import React, { useState } from 'react'
import { Button, Form } from 'react-bootstrap'
import Switch from 'react-switch'
import PropTypes from 'prop-types';

const CategoryWidget = ({ onCreate }) => {

  const [showForm, setShowForm] = useState(false)
  const [published, setPublished] = useState(false)
  const [name, setName] = useState("")

  function handleCancel() {
    setShowForm(false)
    setPublished(false)
  }

  function handleSave() {
    setShowForm(false)
    setPublished(false)
    onCreate({ name, published })
  }

  return (
    <>
      {
        showForm
          ? <Form inline className="float-right">
              <Form.Group controlId="formGroupEmail" size="sm">
                <Form.Label className="badge badge-secondary mr-2">Add Category: </Form.Label>
                <Form.Control 
                  type="text"
                  placeholder="Name"
                  className="mr-4"
                  size="sm"
                  onChange={(event)=>setName(event.target.value)}/>
                <Form.Label className="badge badge-secondary mr-2">Publish: </Form.Label>
                <Switch
                  checked={published}
                  className="mr-4"
                  onChange={() => { setPublished(!published) }}></Switch>
                <Button
                  size="sm"
                  variant="outline-success"
                  className="mr-2"
                  onClick={handleSave}>Save</Button>
                <Button
                  size="sm"
                  height={31}
                  variant="outline-secondary"
                  onClick={handleCancel}>Cancel
                  </Button>
              </Form.Group>
            </Form>

          : <Button
              size="sm"
              variant="outline-success"
              className="float-right"
              onClick={() => { setShowForm(!showForm) }}>
              Add Category
              </Button>
      }
    </>
  )
}

CategoryWidget.propTypes = {
  onCreate: PropTypes.func.isRequired
}

export default CategoryWidget