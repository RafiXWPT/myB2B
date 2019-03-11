import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class EditSecurity extends Component {
    static displayName = EditSecurity.name;

    constructor(props) {
        super(props);

        this.state = { };
    }

    render() {
        return (
            <Form>
            <Form.Group controlId="oldPassword">
                <Form.Label>Old password</Form.Label>
                <Form.Control type="password"></Form.Control>
            </Form.Group>
            <Form.Group controlId="newPassword">
                <Form.Label>New password</Form.Label>
                <Form.Control type="password"></Form.Control>
            </Form.Group>
            <Form.Group controlId="repeatNewPassword">
                <Form.Label>Repeat new password</Form.Label>
                <Form.Control type="password"></Form.Control>
            </Form.Group>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form>         
        );
    }
}