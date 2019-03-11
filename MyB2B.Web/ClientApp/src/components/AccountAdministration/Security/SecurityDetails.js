import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class SecurityDetails extends Component {
    static displayName = SecurityDetails.name;

    constructor(props) {
        super(props);

        this.state = { firstName: "", lastName: ""};
    }

    render() {
        return (
            <div>
                Here will be security details (readonly).
            </div>   
        );
    }
}