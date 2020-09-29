import React from 'react';
import { Row, Col, ListGroup } from 'react-bootstrap'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons'
import LoginLink from './LoginLink';

const iconStyle = { color: "#8AB7D5" }

const PreHeader = ({ isLoggedIn, logoutCallback, contactEmailAddress, contactPhoneNumber }) => {

  return (
    <Row>
      <Col md={4} sm={6} xs={3}>
        <ListGroup horizontal>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faPhone} style={iconStyle} />
            {contactPhoneNumber}
          </ListGroup.Item>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faEnvelope} style={iconStyle} />
            <a href="mailto:bluetapecrew@gmail.com">{contactEmailAddress}</a>
          </ListGroup.Item>
        </ListGroup>
      </Col>
      <Col md={{ span: 4, offset: 4 }} sm={6} xs={3}>
        <ListGroup horizontal>
          <ListGroup.Item><a href="#">My Account</a></ListGroup.Item>
          <ListGroup.Item><a href="#">Shopping Cart</a></ListGroup.Item>
          <ListGroup.Item>
            <LoginLink
              isLoggedIn={isLoggedIn}
              logoutCallback={logoutCallback}
            />
          </ListGroup.Item>
        </ListGroup>
      </Col>
    </Row>
  )
}
export default PreHeader;
