import React, { useEffect, useState } from 'react'
import { Button, Col, Row, Table } from 'react-bootstrap'
import { getCategories } from '../api/categoriesApi'
import CategoryRow from './CategoryRow'
import { saveCategory } from '../api/categoriesApi'

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

  return (
    <>
      <Row>
        <Col>
          <h1>Categories</h1>
        </Col>
        <Col>
          <Button size="sm" variant="outline-success" className="float-right">
            Add Category
        </Button>
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
              handleSave={handleSave}>
            </CategoryRow>
          ))}
        </tbody>
      </Table>
    </>
  )
}