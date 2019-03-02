import React, {Component} from 'react';
import {Button} from 'react-bootstrap';
import {MyB2BRequest} from '../Extensions/MyB2BRequest'

export class InvoiceGenerator extends Component {
    static displayName = InvoiceGenerator.name;

    constructor(props) {
        super(props);

        this.state = {isGenerating: false, getIdentifier: null}
    }

    handleTestGenerateInvoice = event => {
        MyB2BRequest.post('api/Invoice/generate-invoice-test', JSON.stringify({id: '123'}), result => {
            MyB2BRequest.downloadFile('api/Invoice/download-invoice-test?id='+result.identifier);
        });
    }

    render() {
        return  (
            <div>
                <Button block bssize="large" onClick={this.handleTestGenerateInvoice}>Download Invoice</Button>
            </div>
        )
    }
}