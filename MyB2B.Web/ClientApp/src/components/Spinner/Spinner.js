import React, {Component} from 'react';
import { spinnerService } from './SpinnerService';

export class Spinner extends Component {

    constructor(props, context) {
        super(props, context);

        if(!this.props.name) {
            throw new Error('Spinner component must have a name prop.');
        }

        if(!this.props.loadingImage && !this.props.children) {
            throw new Error('Spinner component must have either a loading image props or children to display');
        }

        this.state = {
            show: this.props.hasOwnProperty('show') ? this.props.show : false
        }

        if(this.props.hasOwnProperty('spinnerService')) {
            this.spinnerService = this.props.spinnerService;
        } else {
            this.spinnerService = spinnerService;
        }

        this.spinnerService._register(this);
    }

    componentWillUnmount() {
        this.spinnerService._unregister(this);
    }

    get name() {
        return this.props.name;
    }

    get group() {
        return this.props.group;
    }

    get show() {
        return this.state.show;
    }

    set show(show) {
        this.setState({show});
    }

    render() {
        let divStyle = {display: 'inline-block'};
        if(this.state.show) {
            const {loadingImage} = this.props;
            return (
                <div style={divStyle}>
                {loadingImage && <img src={loadingImage} alt=""/>}
                {this.props.children}
                </div>
            )
        }
        return (<div style={divStyle}/>)
    }
}