import React from 'react';
import { Row, Col, ListGroup } from 'react-bootstrap'
import { useSelector } from 'react-redux';
import { selectSiteProfile } from '../store/siteProfileSlice';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faPhone } from '@fortawesome/free-solid-svg-icons'

const iconStyle = { color: "#8AB7D5" }
const rowStyle = { 
    backgroundColor: 'grey',
    marginTop: '2%',
    paddingTop: '2%'
};
const listGroupStyle = { backgroundColor: 'grey' };

const Footer = () => {

    const vm = useSelector(selectSiteProfile);

    return (
        <Row style={rowStyle}>
            <Col xs={12} sm={6} md={4} lg={4} xl={4}>
                <h5>About Us</h5>
                <p>{vm.aboutUs}</p>
            </Col>
            <Col xs={12} sm={6} md={4} lg={4} xl={4}>
                <h5>Privacy Policy</h5>
            </Col>
            <Col xs={12} sm={6} md={4} lg={4} xl={4}>
                <h5>Contact</h5>
                <ListGroup>
                    <ListGroup.Item style={listGroupStyle}>
                        <FontAwesomeIcon icon={faPhone} style={iconStyle} />&nbsp;
                        {vm.contactPhoneNumber}
                    </ListGroup.Item>
                    <ListGroup.Item style={listGroupStyle}>
                        <FontAwesomeIcon icon={faEnvelope} style={iconStyle} />&nbsp;
                        <a href={`mailto:${vm.contactEmailAddress}`} >{vm.contactEmailAddress}</a>
                    </ListGroup.Item>
                </ListGroup>
            </Col>
        </Row>
    )
}

export default Footer;