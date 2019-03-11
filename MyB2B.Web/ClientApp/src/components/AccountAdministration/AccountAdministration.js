import React, {Component} from 'react';
import {Button, Modal, ButtonGroup} from 'react-bootstrap';
import {EditCompany} from './Company/EditCompany';
import {CompanyDetails} from './Company/CompanyDetails';
import {EditProfile} from './Profile/EditProfile';
import {ProfileDetails} from './Profile/ProfileDetails';
import { SecurityDetails } from './Security/SecurityDetails';
import { EditSecurity } from './Security/EditSecurity';

export class AccountAdministration extends Component {
    static displayName = AccountAdministration.name;

    constructor(props) {
        super(props);

        this.state = {currentEditTitle: '', currentDetailsComponet: <CompanyDetails/>, currentEditComponent: <EditCompany/>, showModal: false};
    }

    handleSwitchToProfile = () => {
        this.setState({ 
            currentEditTitle: 'Edit profile',
            currentDetailsComponet: <ProfileDetails/>,
            currentEditComponent: <EditProfile/>
        });
    }

    handleSwitchToCompany = () => {
        this.setState({ 
            currentEditTitle: 'Edit company',
            currentDetailsComponet: <CompanyDetails/>,
            currentEditComponent: <EditCompany/>
        });
    }

    handleSwitchToSecurity = () => {
        this.setState({ 
            currentEditTitle: 'Edit security',
            currentDetailsComponet: <SecurityDetails/>,
            currentEditComponent: <EditSecurity/>
        });
    }

    handleOpenModal = () => {
        this.setState({showModal: true});
    }

    handleCloseModal = () => {
        this.setState({showModal: false});
    }

    render () {
        return (
        <div>
            <div className="row">
            <div className="col-md-12">
            <Button style={{margin: 5}} className="float-right" size="sm" onClick={this.handleOpenModal}>Edit</Button>
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
            <Modal size="lg" show={this.state.showModal} onHide={this.handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>{this.state.currentEditTitle}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {this.state.currentEditComponent}
            </Modal.Body>
            </Modal>
        </div>
        );
    }
}