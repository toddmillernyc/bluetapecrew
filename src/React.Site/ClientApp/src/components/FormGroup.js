import React  from 'react';
import { Form, Col } from 'react-bootstrap';

export const FormGroup = ({ id, label, defaultValue, onChange }) => {
    return(
        <Form.Group controlId={id}>
            <Form.Label>{label}</Form.Label>
            <Form.Control defaultValue={defaultValue} onChange={onChange} />
        </Form.Group>
    )
}

export const FormGroupCol = ({ id, label, defaultValue, onChange }) => {
  return(
      <Form.Group controlId={id} as={Col}>
          <Form.Label>{label}</Form.Label>
          <Form.Control defaultValue={defaultValue} onChange={onChange} />
      </Form.Group>
  )
}
