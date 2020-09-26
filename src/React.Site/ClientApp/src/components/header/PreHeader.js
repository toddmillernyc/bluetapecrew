import React from 'react';
import { Row, Col, Navbar, Nav, NavItem, ListGroup } from 'react-bootstrap'
import gql from "graphql-tag";
import { Query } from '@apollo/client/react/components';
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

function PreHeader() {

  return (

    <Query query={GET_PROFILE}>
      {({ loading, error, data }) => {
        if (loading) return <p>Loading...</p>;
        if (error) return <p>{JSON.stringify(error)}(</p>;
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
      }}
    </Query>
  )
}

export default PreHeader;
