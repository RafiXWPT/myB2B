import React, {Component} from 'react';
import {Form, Button, Col} from 'react-bootstrap';
import { MyB2BRequest } from '../../Extensions/MyB2BRequest';
import { NotificationManager } from 'react-notifications';
import { ApplicationSpinner } from '../../Spinner/ApplicationSpinner';

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
            if (result.success) {
                this.setState({
                    companyName: result.data.companyName, 
                    shortCode: result.data.shortCode, 
                    companyNip: result.data.companyNip, 
                    companyRegon: result.data.companyRegon, 
                    country: result.data.country || "Poland", 
                    city: result.data.city, 
                    zipCode: result.data.zipCode, 
                    street: result.data.street, 
                    number: result.data.number,
                    loading: false,
                    validated: false
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
        event.stopPropagation();

        const form = event.currentTarget;
        if(form.checkValidity() === false) {
            this.setState({ validated: true });
            return;
        }

        this.setState({ validated: true });

        MyB2BRequest.post('api/AccountAdministration/update-company', JSON.stringify(this.state), (result) => {
            if(result.success === true) {
                NotificationManager.success("Company details saved.");
            } else {
                NotificationManager.error(result.errorMessage);
            }
        });
    } 

    render() {
        const { validated, loading } = this.state;

        if(loading)
            return(<ApplicationSpinner show={loading} identifier="test-spinner"/>)
        else
        return (
            <Form noValidate validated={validated} onSubmit={this.handleSubmit}>
                <Form.Row>
                    <Form.Group as={Col} controlId="CompanyName">
                    <Form.Label>Company Name</Form.Label>
                    <Form.Control required placeholder="eg. SQuare Solutions" value={this.state.companyName} onChange={this.onCompanyNameChange}/>
                        <Form.Control.Feedback type="invalid">
                            Write your company full name.
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group as={Col} md="3" controlId="ShortCode">
                    <Form.Label>Company short code</Form.Label>
                    <Form.Control required placeholder="eg. SQS" value={this.state.shortCode} onChange={this.onShortCodeChange}/>
                        <Form.Control.Feedback type="invalid">
                            Write your company code.
                        </Form.Control.Feedback>
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                <Form.Group as={Col} md="3" controlId="Country">
                    <Form.Label>Country</Form.Label>
                    <Form.Control as="select" value={this.state.country || "Poland"} onChange={this.onCountryChange}>
                    { this.loadSelectOptions() }                   
                    </Form.Control>
                    </Form.Group>

                    <Form.Group as={Col} controlId="CompanyNip">
                    <Form.Label>Company Nip</Form.Label>
                    <Form.Control required placeholder="eg. 1584448245" value={this.state.companyNip} onChange={this.onCompanyNipChange}/>
                        <Form.Control.Feedback type="invalid">
                            Write your company NIP.
                        </Form.Control.Feedback>                  
                    </Form.Group>

                    <Form.Group as={Col} controlId="CompanyRegon">
                    <Form.Label>Company Regon</Form.Label>
                    <Form.Control required placeholder="eg. 557340530" value={this.state.companyRegon} onChange={this.onCompanyRegonChange}/>
                    <Form.Control.Feedback type="invalid">
                            Write your company REGON.
                        </Form.Control.Feedback>  
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Col} md="2" controlId="ZipCode">
                    <Form.Label>ZipCode</Form.Label>
                    <Form.Control required placeholder="eg. 31-000" value={this.state.zipCode} onChange={this.onZipCodeChange}/>
                    </Form.Group>

                    <Form.Group as={Col} md="3" controlId="City">
                    <Form.Label>City</Form.Label>
                    <Form.Control required placeholder="eg. Cracow" value={this.state.city} onChange={this.onCityChange}/>
                    </Form.Group>

                    <Form.Group as={Col} md="5" controlId="Street">
                    <Form.Label>Street</Form.Label>
                    <Form.Control required placeholder="eg. Main Square" value={this.state.street} onChange={this.onStreetChange}/>
                    </Form.Group>

                    <Form.Group as={Col} md="2" controlId="Number">
                    <Form.Label>Number</Form.Label>
                    <Form.Control required placeholder="eg. 13A/12" value={this.state.number} onChange={this.onNumberChange}/>
                    </Form.Group>
                </Form.Row>
            <Button block bssize="large" type="submit">Submit</Button>
            </Form> 
        );
    }
}