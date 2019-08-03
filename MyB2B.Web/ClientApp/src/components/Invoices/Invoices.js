import React, {Component} from 'react';
import { CompanyInvoices } from './CompanyInvoices';
import { CompanyInvoicesMenu } from './CompanyInvoicesMenu';

export class Invoices extends Component {
    static displayName = Invoices.name;

    constructor(props) {
        super(props);

        this.state = {}
    }

    render() {
        return(
        <div>
            <div className="row">
                <div className="col-md-12">
                    <CompanyInvoicesMenu/>
                </div>
            </div>
            <div className="row">
                <div className="col-md-12">
                    <CompanyInvoices/>
                </div>
            </div>
        </div>
        );
    }
}