import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class ProfileDetails extends Component {
    static displayName = ProfileDetails.name;

    constructor(props) {
        super(props);

        this.state = { firstName: "", lastName: ""};
    }

    render() {
        return (
            <div>
                Here will be profile details (readonly).
            </div>   
        );
    }
}