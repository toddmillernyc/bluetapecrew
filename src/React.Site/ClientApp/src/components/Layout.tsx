import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import PreHeader from './PreHeader'

export default (props: { children?: React.ReactNode }) => (
        <React.Fragment>
        <div className="ecommerce">
            <PreHeader></PreHeader>
        </div>
        <NavMenu/>
        <Container>
            {props.children}
        </Container>
    </React.Fragment>
);
