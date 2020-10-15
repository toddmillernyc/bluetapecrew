import React from 'react';
import { Row, Col, ListGroup } from 'react-bootstrap'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons'
import LoginLink from './LoginLink';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { selectSiteProfile } from '../store/siteProfileSlice';

const iconStyle = { color: "#8AB7D5" }

const PreHeader = () => {
  
  const vm = useSelector(selectSiteProfile);

  return (
    <Row>
      <Col md={4} xs={12}>
        <ListGroup horizontal>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faPhone} style={iconStyle} />&nbsp;
            {vm.contactPhoneNumber}
          </ListGroup.Item>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faEnvelope} style={iconStyle} />&nbsp;
            <a href={`mailto:${vm.contactEmailAddress}`} >{vm.contactEmailAddress}</a>
          </ListGroup.Item>
        </ListGroup>
      </Col>
      <Col md={{ span: 4, offset: 4 }} sm={6} xs={12}>
        <ListGroup horizontal>
          <ListGroup.Item> <Link to='/account'>My Account</Link></ListGroup.Item>
          <ListGroup.Item><a href="#">Shopping Cart</a></ListGroup.Item>
          <ListGroup.Item><LoginLink /></ListGroup.Item>
        </ListGroup>
      </Col>
    </Row>
  )
}
export default PreHeader;
