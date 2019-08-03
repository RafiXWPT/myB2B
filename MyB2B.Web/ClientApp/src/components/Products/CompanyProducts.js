import React, {Component} from 'react';

export class CompanyProducts extends Component {
    static displayName = CompanyProducts.name;

    constructor(props) {
        super(props);

        this.state = {loading: true};
    }

    render() {
        return (
            <div>Here will be products list</div>
        )
    }
}