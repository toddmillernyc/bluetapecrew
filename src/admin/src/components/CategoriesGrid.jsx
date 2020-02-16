import React, { useEffect, useState } from 'react'
import { Button, Col, Form, Row, Table } from 'react-bootstrap'
import { getCategories } from '../api/categoriesApi'
import CategoryRow from './CategoryRow'
import { saveCategory } from '../api/categoriesApi'
import Swtich from 'react-switch'

export default function CategoriesGrid() {
  const [categories, setCategories] = useState([])
  const [addCategory, setAddCategory] = useState(true)

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
          {
            addCategory
              ? <Form inline>
                <Form.Group controlId="formGroupEmail" size="sm">
                  <Form.Control type="text" placeholder="Category" />

                </Form.Group>
              </Form>
              : <Button
                size="sm"
                variant="outline-success"
                className="float-right"
                onClick={() => { setAddCategory(!addCategory) }}>
                Add Category
            </Button>
          }

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