import React from 'react';
import { Row, Col, ListGroup } from 'react-bootstrap'
import { gql, useQuery } from "@apollo/client";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons'

const GET_PROFILE = gql`
{
  siteProfile {
    contactPhoneNumber
    contactEmailAddress
  }
}
`;

const iconStyle = { color: "#8AB7D5" }

const PreHeader = () => {
  const { data, loading, error } = useQuery(GET_PROFILE);
  if (loading) return <p>Loading...</p>;
  if (error) return <p>{JSON.stringify(error)}(</p>;
  if(!data) return <p>Not Found</p>;
  let settings = data.siteProfile;

  return (
    <Row>
      <Col md={4} sm={6} xs={3}>
        <ListGroup horizontal>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faPhone} style={iconStyle} />
            {settings.contactPhoneNumber}
          </ListGroup.Item>
          <ListGroup.Item>
            <FontAwesomeIcon icon={faEnvelope} style={iconStyle} />
            <a href="mailto:bluetapecrew@gmail.com">{settings.contactEmailAddress}</a>
          </ListGroup.Item>
        </ListGroup>
      </Col>
      <Col md={{ span: 4, offset: 4 }} sm={6} xs={3}>
        <ListGroup horizontal>
          <ListGroup.Item><a href="#">My Account</a></ListGroup.Item>
          <ListGroup.Item><a href="#">Shopping Cart</a></ListGroup.Item>
          <ListGroup.Item><a href="#">Log In</a></ListGroup.Item>
        </ListGroup>
      </Col>
    </Row>
  )
}
export default PreHeader;
