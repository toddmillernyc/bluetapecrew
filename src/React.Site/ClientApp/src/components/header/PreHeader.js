import React from 'react';
import { Row, Col } from 'react-bootstrap'
import gql from "graphql-tag";
import { Query } from '@apollo/client/react/components';

const GET_PROFILE = gql`
{
  siteProfiles {
    contactPhoneNumber
    contactEmailAddress
  }
}
`;

function PreHeader() {

  return (

    <Query query={GET_PROFILE}>
      {({ loading, error, data }) => {
        if (loading) return <p>Loading...</p>;
        if (error) return <p>{JSON.stringify(error)}(</p>;
        let settings = data.siteProfiles[0];
        return (
          <Row>
            <Col>
            {settings.contactPhoneNumber} | {settings.contactEmailAddress}
            </Col>
          </Row>
        )
      }}
    </Query>
  )
}

export default PreHeader;
