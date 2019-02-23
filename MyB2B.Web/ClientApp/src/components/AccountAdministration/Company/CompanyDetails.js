import React, {Component} from 'react';
import {Form, Button} from 'react-bootstrap';

export class CompanyDetails extends Component {
    static displayName = CompanyDetails.name;

    constructor(props) {
        super(props);

        this.state = { companyName: ""};
    }

    render() {
        return (
            <div>
                Here will be company details (readonly).
            </div>   
        );
    }
}