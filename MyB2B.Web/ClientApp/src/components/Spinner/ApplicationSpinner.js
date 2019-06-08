import React, {Component} from 'react';
import { Spinner } from './Spinner';

export class ApplicationSpinner extends Component {
    render() {
       return (<Spinner name={this.props.identifier} show={this.props.show}><div className="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></Spinner>);
    }
}

export class GlobalSpinner extends Component {
    render() {
       return (<Spinner name={this.props.identifier} show={this.props.show}><div className="lds-roller lds-global-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></Spinner>);
    }
}