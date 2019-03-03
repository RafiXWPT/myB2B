import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class EditProfile extends Component {
    static displayName = EditProfile.name;

    constructor(props) {
        super(props);

        this.state = { firstName: "", lastName: ""};
    }

    render() {
        return (
            <Form>
            <Form.Group controlId="firstName">
                <Form.Label>First Name</Form.Label>
                <Form.Control type="text" placeholder="your first name"></Form.Control>
                <Form.Text className="text-muted">
                Your first name.
                </Form.Text>
            </Form.Group>
            <Form.Group controlId="lastName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control type="text" placeholder="your last name"></Form.Control>
                <Form.Text className="text-muted">
                Your last name.
                </Form.Text>
            </Form.Group>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form>         
        );
    }
}