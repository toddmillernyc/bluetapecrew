import React, { useState } from 'react'
import PropTypes from 'prop-types';
import DisplayInput from '../shared/DisplayInput'
import DisplaySwitch from '../shared/DisplaySwitch'
import EditActions from '../shared/EditActions'
import { Col, Row } from 'react-bootstrap'
import ImagePicker from '../shared/ImagePicker'
import { isCompositeComponent } from 'react-dom/test-utils';

const CategoryRow = ({ id, imageId, name, published, imageIds, handleSave, handleDelete }) => {

  const [edit, setEdit] = useState(false)
  const [publishedValue, setPublishedValue] = useState(published)
  const [nameValue, setNameValue] = useState(name)
  const [imageIdValue, setImageIdValue] = useState(imageId ?? 0)

  const buttonActions = {
    
    onCancel: () => {
      setEdit(!edit)
      setPublishedValue(published)
      setNameValue(name)
      setImageIdValue(imageId ?? 0)
    },

    onDelete: () => {
      if (window.confirm(`Are you sure you want to delete the ${name} category?`)) {
        handleDelete({ id })
        return true
      } else return false
    },

    onEdit: () => setEdit(!edit),

    onSave: () => {
      setEdit(!edit)
      handleSave({
        id: id,
        name: nameValue,
        published: publishedValue
      })
    }
  }

  const imageChange  = () => {

  }

  return (
    <>
      <Row><Col><hr /></Col></Row>
      <Row>
        <Col>
          <DisplayInput value={nameValue} isEditMode={edit} changeCallback={(event => setNameValue(event.target.value))} />
        </Col>
        <Col>
          <ImagePicker imageId={imageIdValue} isEditMode={edit} imageIds={imageIds} />
        </Col>
        <Col>
          <DisplaySwitch value={publishedValue} isEditMode={edit} changeCallback={() => setPublishedValue(!publishedValue)} />
        </Col>
        <Col>
          <EditActions {...buttonActions} />
        </Col>
      </Row>
    </>
  )
}

CategoryRow.propTypes = {
  id: PropTypes.number.isRequired,
  imageId: PropTypes.number,
  name: PropTypes.string.isRequired,
  published: PropTypes.bool.isRequired,
  handleSave: PropTypes.func.isRequired,
  handleDelete: PropTypes.func.isRequired,
}

export default CategoryRow