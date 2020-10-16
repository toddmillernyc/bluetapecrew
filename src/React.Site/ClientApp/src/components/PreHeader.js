import React from 'react';
import { Row, Col, ListGroup } from 'react-bootstrap'
import LoginLink from './LoginLink';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { selectSiteProfile } from '../store/siteProfileSlice';

const PreHeader = () => {
  
  const vm = useSelector(selectSiteProfile);

  return (
    <Row className=".d-none .d-sm-block">

    </Row>
  )
}
export default PreHeader;
