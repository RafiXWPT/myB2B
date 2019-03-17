import React, {Component} from 'react';
import {Form, Button, Col} from 'react-bootstrap';
import { MyB2BRequest } from '../../Extensions/MyB2BRequest';
import { NotificationManager } from 'react-notifications';

export class CompanyDetails extends Component {
    static displayName = CompanyDetails.name;

    constructor(props) {
        super(props);

        this.state = { 
            loading: true,
            companyName: "", 
            shortCode: "", 
            companyNip: "", 
            companyRegon: "", 
            country: "Poland", 
            city: "", 
            zipCode: "", 
            street: "", 
            number: ""};

        MyB2BRequest.get('api/AccountAdministration/get-user-company', (result) => {
            if(result.IsOk) {
                this.setState({
                    companyName: result.value.CompanyName, 
                    shortCode: result.value.ShortCode, 
                    companyNip: result.value.CompanyNip, 
                    companyRegon: result.value.CompanyRegon, 
                    country: result.value.Country, 
                    city: result.value.City, 
                    zipCode: result.value.ZipCode, 
                    street: result.value.Street, 
                    number: result.value.Number,
                    loading: false
                });
            }
        });
    }

    loadSelectOptions = () => {
        return ["Poland", "England", "Germany"].map(function(opt) {
            return (<option key={opt}>{opt}</option>);
        })
    }

    onCompanyNameChange = (event) => this.setState({companyName: event.target.value});
    onShortCodeChange = (event) => this.setState({shortCode: event.target.value});
    onCompanyNipChange = (event) => this.setState({companyNip: event.target.value});
    onCompanyRegonChange = (event) => this.setState({companyRegon: event.target.value});
    onCountryChange = (event) => this.setState({country: event.target.value});
    onCityChange = (event) => this.setState({city: event.target.value});
    onZipCodeChange = (event) => this.setState({zipCode: event.target.value});
    onStreetChange = (event) => this.setState({street: event.target.value});
    onNumberChange = (event) => this.setState({number: event.target.value});

    handleSubmit = (event) => {
        event.preventDefault();
        MyB2BRequest.post('api/AccountAdministration/update-company', JSON.stringify(this.state), (result) => {
            if(result.success == true) {
                NotificationManager.success("Company details saved.");
            }
        });
    } 

    render() {
        return (
            <Form onSubmit={this.handleSubmit}>
                <Form.Row>
                    <Form.Group as={Col} controlId="CompanyName">
                    <Form.Label>Company Name</Form.Label>
                    <Form.Control placeholder="eg. SQuare Solutions" value={this.state.companyName} onChange={this.onCompanyNameChange}/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="ShortCode">
                    <Form.Label>Company short code</Form.Label>
                    <Form.Control placeholder="eg. SQS" value={this.state.shortCode} onChange={this.onShortCodeChange}/>
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Col} controlId="CompanyNip">
                    <Form.Label>Company Nip</Form.Label>
                    <Form.Control placeholder="eg. 1584448245" value={this.state.companyNip} onChange={this.onCompanyNipChange}/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="CompanyRegon">
                    <Form.Label>Company Regon</Form.Label>
                    <Form.Control placeholder="eg. 557340530" value={this.state.companyRegon} onChange={this.onCompanyRegonChange}/>
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Col} controlId="Country">
                    <Form.Label>Country</Form.Label>
                    <Form.Control as="select" value={this.state.country || "Poland"} onChange={this.onCountryChange}>
                    { this.loadSelectOptions() }                   
                    </Form.Control>
                    </Form.Group>

                    <Form.Group as={Col} controlId="City">
                    <Form.Label>City</Form.Label>
                    <Form.Control placeholder="eg. Cracow" value={this.state.city} onChange={this.onCityChange}/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="ZipCode">
                    <Form.Label>ZipCode</Form.Label>
                    <Form.Control placeholder="eg. 31-000" value={this.state.zipCode} onChange={this.onZipCodeChange}/>
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                <Form.Group as={Col} controlId="Street">
                    <Form.Label>Street</Form.Label>
                    <Form.Control placeholder="eg. Main Square" value={this.state.street} onChange={this.onStreetChange}/>
                    </Form.Group>

                    <Form.Group as={Col} controlId="Number">
                    <Form.Label>Number</Form.Label>
                    <Form.Control placeholder="eg. 13A/12" value={this.state.number} onChange={this.onNumberChange}/>
                    </Form.Group>
                </Form.Row>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form> 
        );
    }
}