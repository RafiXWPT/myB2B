import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class EditCompany extends Component {
    static displayName = EditCompany.name;

    constructor(props) {
        super(props);

        this.state = { companyName: ""};
    }

    render() {
        return (
            <Form>
            <Form.Group controlId="companyName">
                <Form.Label>Company Name</Form.Label>
                <Form.Control type="text" placeholder="your company name"></Form.Control>
                <Form.Text className="text-muted">
                Your company name.
                </Form.Text>
            </Form.Group>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form>         
        );
    }
}