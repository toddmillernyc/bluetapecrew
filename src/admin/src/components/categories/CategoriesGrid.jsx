import React, { useEffect, useState } from 'react'
import { Col, Row } from 'react-bootstrap'
import CategoryRow from './CategoryRow'
import * as context from '../../apiContext'
import AddCategoryWidget from './AddCategoryWidget'

export default function CategoriesGrid() {

  const [categories, setCategories] = useState([])
  const [imageIds, setImageIds] = useState([])

  useEffect(() => {
    async function init() {
      setImageIds(await context.images.get())
      setCategories(await context.categories.get())
    }
    init()
  }, []);

  async function handleSave(updateCategory) {
    await context.categories.update(updateCategory)
    setCategories(categories.map((category) =>
      updateCategory.id === category.id ? updateCategory : category
    ))
  }

  async function handleCreate(newCategory) {
    const returnCategory = await context.categories.create(newCategory)
    setCategories([returnCategory].concat(categories))
  }

  async function handleDelete(category) {
    const error = await context.categories.del(category.id)
    if (error) alert(error)
    else {
      const newCategories = []
      categories.forEach((c) => {
        if (c.id !== category.id) newCategories.push(c)
      })
      setCategories(newCategories)
    }
  }

  return (
    <>
      <Row>
        <Col>
          <h1>Categories</h1>
        </Col>
        <Col>
          <AddCategoryWidget onCreate={handleCreate}></AddCategoryWidget>
        </Col>
      </Row>
      <Row>
        <Col><h2 className="h4">Name</h2></Col>
        <Col><h2 className="h4">Image</h2></Col>
        <Col><h2 className="h4">Is Published</h2></Col>
        <Col><h2 className="h4">Actions</h2></Col>
      </Row>
      {categories.map((category) => (
        <CategoryRow
          key={category.id}
          {...category}
          imageIds={imageIds}
          handleSave={handleSave}
          handleDelete={handleDelete}>
        </CategoryRow>
      ))}
    </>
  )
}