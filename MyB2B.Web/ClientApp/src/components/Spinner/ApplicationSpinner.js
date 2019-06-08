import React, {Component} from 'react';
import { Spinner } from './Spinner';

export class ApplicationSpinner extends Component {

    constructor(props) {
        super(props);

        this.state = {
            ownClassName: this.props.hasOwnProperty('className') ? this.props.className : "lds-roller"
        }
    }

    render() {
       return (<Spinner name={this.props.identifier} show={this.props.show}><div className={this.state.ownClassName}><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></Spinner>);
    }
}

export class GlobalSpinner extends Component {
    render() {
       return (<ApplicationSpinner identifier="global-spinner" className="lds-roller lds-global-roller" show={this.props.show}/>);
    }
}