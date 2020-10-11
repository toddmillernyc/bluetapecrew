import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Button, Col, Form, } from 'react-bootstrap';
import { fetchUserProfileAsync, setUserProfileAsync } from '../store/userProfileSlice';
import { selectEmail } from '../store/loginSlice';
import { userProfileSelect } from '../store/userProfileSlice';

const UserProfileForm = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [address, setAddress] = useState('');
  const [city, setCity] = useState('');
  const [state, setState] = useState('');
  const [postalCode, setPostalCode] = useState('');

  const dispatch = useDispatch();
  const email = useSelector(selectEmail);
  const profile = useSelector(userProfileSelect);

  useEffect(() => {
    if(email) {
      dispatch(fetchUserProfileAsync(email));
      if(profile) {
        setFirstName(profile.firstName);
        setLastName(profile.lastName);
        setAddress(profile.address);
        setCity(profile.city);
        setState(profile.state);
        setPostalCode(profile.postalCode);
      }
    }
  });

  const onSubmit = event => {
    event.preventDefault();
    dispatch(setUserProfileAsync({
      firstName,
      lastName,
      address,
      city,
      state,
      postalCode
    }));
  }

  return(email && profile &&
    <Form onSubmit={onSubmit}>

      <Form.Group controlId="userProfileFirstName">
        <Form.Label>First Name</Form.Label>
        <Form.Control value={firstName} onChange={e => setFirstName(e.target.value)} />
      </Form.Group>

      <Form.Group controlId="userProfileLastName">
        <Form.Label>Last Name</Form.Label>
        <Form.Control value={lastName} onChange={e => setLastName(e.target.value)} />
      </Form.Group>

      <Form.Group controlId="userProfileAddress">
        <Form.Label>Address</Form.Label>
        <Form.Control value={address} onChange={e => setAddress(e.target.value)} />
      </Form.Group>

      <Form.Row>
        <Form.Group as={Col} controlId="userProfileCity">
          <Form.Label>City</Form.Label>
          <Form.Control value={city} onChange={e => setCity(e.target.value)} />
        </Form.Group>

        <Form.Group as={Col} controlId="userProfileState">
          <Form.Label>State</Form.Label>
          <Form.Control value={state} onChange={e => setState(e.target.value)} />
        </Form.Group>

        <Form.Group as={Col} controlId="userPostalCode">
          <Form.Label>Postal Code</Form.Label>
          <Form.Control value={postalCode} onChange={e => setPostalCode(e.target.value)} />
        </Form.Group>
      </Form.Row>

      <Button variant="primary" type="submit">
        Submit
  </Button>
    </Form>
  )
}

export default UserProfileForm;