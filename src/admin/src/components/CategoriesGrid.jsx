import React, { useEffect, useState } from 'react'
import { Col,  Row, Table } from 'react-bootstrap'
import { getCategories } from '../api/categoriesApi'
import CategoryRow from './CategoryRow'
import { createCategory, deleteCategory, saveCategory } from '../api/categoriesApi'
import AddCategoryWidget from './AddCategoryWidget'

export default function CategoriesGrid() {
  const [categories, setCategories] = useState([])

  useEffect(() => {
    getCategories().then((categories) => {
      setCategories(categories)
    })
  }, []);

  function handleSave(updateCategory) {
    saveCategory(updateCategory).then(() => {
      updateCategoriesState(updateCategory)
    })
  }

  function updateCategoriesState(updateCategory) {
    setCategories(categories.map((category) =>
      updateCategory.id === category.id ? updateCategory : category
    ))
  }

  function handleCreate(newCategory) {
    createCategory(newCategory).then((returnCategory) => {
      setCategories([returnCategory].concat(categories))
    })
  }

  function handleDelete(category) {
    deleteCategory(category).then((error) => {
      if(error) alert(error)
      else {
        const newCategories = []
        categories.map((c) => {
          if(c.id !== category.id) newCategories.push(c)})
        setCategories(newCategories)
      }
    })
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
      <Table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Is Published</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category) => (
            <CategoryRow
              key={category.id}
              {...category}
              handleSave={handleSave}
              handleDelete={handleDelete}>
            </CategoryRow>
          ))}
        </tbody>
      </Table>
    </>
  )
}