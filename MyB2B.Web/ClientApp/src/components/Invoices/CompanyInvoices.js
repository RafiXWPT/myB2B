import React, {Component} from 'react';
import { MyB2BRequest } from '../Extensions/MyB2BRequest';
import { ApplicationSpinner } from '../Spinner/ApplicationSpinner';

export class CompanyInvoices extends Component {
    static displayName = CompanyInvoices.name;

    constructor(props) {
        super(props);

        this.state = {loading: true};

        MyB2BRequest.get('api/invoice/company-invoices/'+localStorage.getItem('user-company-id'), (result) => {
            if(result.success) {
                this.setState({loading: false});
            }

            this.setState({loading: false});
        });
    }

    render() {
        const {loading} = this.state;
        if(loading) {
            return(<ApplicationSpinner show={loading} identifier="invoices-grid-loader"/>)
        } else {
            return (
                <div>
                    Here will be invoices grid
                </div>
            )
        }
    }
}