import React, { useEffect, useState } from 'react'
import { Col,  Row, Table } from 'react-bootstrap'
import ProductRow from './ProductRow'
import * as context from '../../apiContext'

//import AddProductWidget from './AddProductWidget'

export default function ProductsGrid() {
  
  const [products, setProducts] = useState([])

  useEffect(() => {
    context.products.get().then((products) => {
      console.log(products)
      setProducts(products)
    })
  }, []);

  async function handleSave(updateProduct) {
    await context.products.save(updateProduct)
    setProducts(products.map((product) =>
      updateProduct.id === product.id ? updateProduct : product
    ))
  }

  // async function handleCreate(newProduct) {
  //   const returnProduct = await createProduct(newProduct)
  //   setProducts([returnProduct].concat(products))
  // }

  async function handleDelete(product) {
    const error = await context.products.del(product.id)
    if(error) alert(error)
    else {
      const newProducts = []
      products.forEach((p) => {
        if(p.id !== product.id) newProducts.push(p)})
      setProducts(newProducts)
    }
  }

  return (
    <>
      <Row>
        <Col>
          <h1>Products</h1>
        </Col>
        <Col>
          {/* <AddProductWidget onCreate={handleCreate}></AddProductWidget> */}
        </Col>
      </Row>
      <Table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Slug</th>
            <th>Actions</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <ProductRow
              key={product.id}
              {...product}
              handleSave={handleSave}
              handleDelete={handleDelete}>
            </ProductRow>
          ))}
        </tbody>
      </Table>
    </>
  )
}