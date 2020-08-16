import React, { useState } from 'react'
import EditActions from '../shared/EditActions'
import DisplayInput from '../shared/DisplayInput'
import DisplayTextArea from '../shared/DisplayTextArea'

const ProductRow = ({ id, slug, productName, description, handleSave, handleDelete }) => {

  const [isEditMode, setEditMode] = useState(false)
  const [slugValue, setSlug] = useState(slug)
  const [nameValue, setName] = useState(productName)
  const [descriptionValue, sateDescription] = useState(description)

  function toggleEdit() { setEditMode(!isEditMode) }

  function handleCancel() {
    toggleEdit()
    setName(productName)
  }

  function onSaveClick() {
    toggleEdit()
    handleSave({
      id: id,
      slug: slugValue,
      productName: nameValue,
      description: descriptionValue
    })
  }

  function onDeleteClick() {
    if (window.confirm(`Are you sure you want to delete the ${productName} category?`))
      handleDelete({ id })
  }

  const buttonActions = {
    onCancel: () => setEditMode(false),
    onDelete: () => {

    },
    onEdit: () => setEditMode(true),
    onSave: () => {

    }
  }

  const productNameProps = { isEditMode, value: nameValue, changeCallback: (event) => setName(event.target.value) }
  const slugProps = { isEditMode, value: slugValue, changeCallback: (event) => setSlug(event.target.value) }
  const descriptionProps = { isEditMode, value: descriptionValue, changeCallback: (event) => setSlug(event.target.value) }

  return (
    <tr>
      <td>
        <DisplayInput {...productNameProps} />
      </td>
      <td>
        <DisplayInput {...slugProps} />
      </td>
      <td>
        <DisplayTextArea {...descriptionProps} />
      </td>
      <td>
        <EditActions {...buttonActions} />
      </td>
    </tr>
  )

  // ProductRow.propTypes = {
  //   id: PropTypes.number.isRequired,
  //   name: PropTypes.string.isRequired,
  //   published: PropTypes.bool.isRequired,
  //   handleSave: PropTypes.func.isRequired,
  //   handleDelete: PropTypes.func.isRequired,
  // }
}

export default ProductRow