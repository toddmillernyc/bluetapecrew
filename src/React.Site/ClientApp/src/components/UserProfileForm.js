import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Button, Col, Form, Row } from 'react-bootstrap';
import { fetchUserProfileAsync, setUserProfileAsync } from '../store/userProfileSlice';
import { selectEmail } from '../store/loginSlice';
import { userProfileSelect } from '../store/userProfileSlice';
import { FormGroup, FormGroupCol } from './FormGroup';

const UserProfileForm = () => {
  const [vm, setVm] = useState({});

  const dispatch = useDispatch();
  const email = useSelector(selectEmail);
  const profile = useSelector(userProfileSelect);

  useEffect(() => {
    if (!profile.id) dispatch(fetchUserProfileAsync(email));
    else if(!vm.id) setVm(profile);
  });

  const onSubmit = event => {
    event.preventDefault();
    dispatch(setUserProfileAsync(vm));
  }

  const handleChange = e => {
    const { id, value } = e.target;
    const updateVm = { ...vm, [id]: value };
    setVm(updateVm)
  }

  const titleStyle = { marginBottom: '5%', marginTop: '8%' };
  
  return (email && profile.id &&
    <Row>
      <Col className="d-none d-lg-block" />
      <Col>
        <Form onSubmit={onSubmit}>
          <h1 className="h3" style={titleStyle}>Your Profile</h1>
          <FormGroup id="firstName" label="First Name" defaultValue={vm.firstName} onChange={handleChange} />
          <FormGroup id="lastName" label="Last Name" defaultValue={vm.lastName} onChange={handleChange} />
          <FormGroup id="address" label="Address" defaultValue={vm.address} onChange={handleChange} />
          <Form.Row>
            <FormGroupCol id="city" label="City" defaultValue={vm.city} onChange={handleChange} />
            <FormGroupCol id="state" label="State" defaultValue={vm.state} onChange={handleChange} />
            <FormGroupCol id="postalCode" label="Zip" defaultValue={vm.postalCode} onChange={handleChange} />
          </Form.Row>
          <Button variant="primary" type="submit">Save</Button>
        </Form>
      </Col>
      <Col className="d-none d-lg-block" />
    </Row>
  )
}

export default UserProfileForm;