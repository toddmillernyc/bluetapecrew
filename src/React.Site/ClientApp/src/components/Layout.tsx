import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import { Header } from './Header'
import PreHeader from './PreHeader'

export default (props: { children?: React.ReactNode }) => (
        <React.Fragment>
        <div className="ecommerce">
            <PreHeader></PreHeader>
            <Header></Header>
        </div>
        <NavMenu/>
        <Container>
            {props.children}
        </Container>
    </React.Fragment>
);
