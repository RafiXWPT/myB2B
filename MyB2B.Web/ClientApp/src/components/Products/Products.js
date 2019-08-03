import React, {Component} from 'react';
import {CompanyProductsMenu} from './CompanyProductsMenu';
import {CompanyProducts} from './CompanyProducts';

export class Products extends Component {
    static displayName = Products.name;

    constructor(props) {
        super(props);

        this.state = {};
    }

    render() {
        return(
            <div>
            <div className="row">
                <div className="col-md-12">
                    <CompanyProductsMenu/>
                </div>
            </div>
            <div className="row">
                <div className="col-md-12">
                    <CompanyProducts/>
                </div>
            </div>
        </div>
        );
    }
}