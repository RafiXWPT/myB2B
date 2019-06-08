import React, {Component} from 'react';
import {Button, ButtonGroup} from 'react-bootstrap';
import {CompanyDetails} from './Company/CompanyDetails';
import {ProfileDetails} from './Profile/ProfileDetails';
import { SecurityDetails } from './Security/SecurityDetails';

export class AccountAdministration extends Component {
    static displayName = AccountAdministration.name;

    constructor(props) {
        super(props);

        this.state = {currentDetailsComponet: <CompanyDetails/>};
    }

    handleSwitchToProfile = () => {
        this.setState({ 
            currentDetailsComponet: <ProfileDetails/>
        });
    }

    handleSwitchToCompany = () => {
        this.setState({ 
            currentDetailsComponet: <CompanyDetails/>
        });
    }

    handleSwitchToSecurity = () => {
        this.setState({ 
            currentDetailsComponet: <SecurityDetails/>
        });
    }

    render () {
        return (
        <div>
            <div className="row">
            <div className="col-md-12">
            </div>    
                <div className="col-md-3">
                <ButtonGroup vertical className="btn-block">
                    <Button style={{margin: 5}} size="sm" block onClick={this.handleSwitchToCompany}>User company</Button>
                    <Button style={{margin: 5}} size="sm" block onClick={this.handleSwitchToProfile}>User profile</Button>
                    <Button style={{margin: 5}} size="sm" block onClick={this.handleSwitchToSecurity}>Security</Button>
                </ButtonGroup>
                </div>

                <div className="col-md-9">
                    {this.state.currentDetailsComponet}
                </div>
            </div>
        </div>
        );
    }
}