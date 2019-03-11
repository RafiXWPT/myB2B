import React, {Component} from 'react';
import {Form, Col, Button} from 'react-bootstrap';

export class EditCompany extends Component {
    static displayName = EditCompany.name;

    constructor(props) {
        super(props);

        this.state = { companyName: ""};
    }

    render() {
        return (
            <Form>
                <Form.Row>
                    <Form.Group as={Col} controlId="CompanyName">
                    <Form.Label>Company Name</Form.Label>
                    <Form.Control placeholder="eg. SQuare Solutions"/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="ShortCode">
                    <Form.Label>Company short code</Form.Label>
                    <Form.Control placeholder="eg. SQS"/>
                    </Form.Group>
                </Form.Row>

                <Form.Row>
                    <Form.Group as={Col} controlId="CompanyNip">
                    <Form.Label>Company Nip</Form.Label>
                    <Form.Control placeholder="eg. 1584448245"/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="CompanyRegon">
                    <Form.Label>Company Regon</Form.Label>
                    <Form.Control placeholder="eg. 557340530"/>
                    </Form.Group>
                </Form.Row>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form>         
        );
    }
}