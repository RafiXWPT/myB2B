import React, {Component} from 'react';
import {Button, Modal, ButtonGroup} from 'react-bootstrap';
import {EditCompany} from './Company/EditCompany';
import {CompanyDetails} from './Company/CompanyDetails';
import {EditProfile} from './Profile/EditProfile';
import {ProfileDetails} from './Profile/ProfileDetails';

export class AccountAdministration extends Component {
    static displayName = AccountAdministration.name;

    constructor(props) {
        super(props);

        this.state = {currentEditTitle: '', currentDetailsComponet: <ProfileDetails/>, currentEditComponent: <EditProfile/>, showModal: false};
    }

    handleSwitchToProfile = () => {
        this.setState({ 
            currentEditTitle: 'Edit profile',
            currentDetailsComponet: <ProfileDetails/>,
            currentEditComponent: <EditProfile/>
        });
    }

    handleSwitchCompany = () => {
        this.setState({ 
            currentEditTitle: 'Edit company',
            currentDetailsComponet: <CompanyDetails/>,
            currentEditComponent: <EditCompany/>
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
            <div class="row">
            <div className="col-md-12">
            <Button style={{margin: 5}} className="float-right" size="sm" onClick={this.handleOpenModal}>Edit</Button>
            </div>    
                <div class="col-md-3">
                    <ButtonGroup vertical className="btn-block">
                        <Button style={{margin: 5}} block onClick={this.handleSwitchToProfile}>User profile</Button>
                        <Button style={{margin: 5}} block onClick={this.handleSwitchCompany}>User company</Button>
                    </ButtonGroup>
                </div>
                <div class="col-md-9">
                    {this.state.currentDetailsComponet}
                </div>
            </div>
            <Modal show={this.state.showModal} onHide={this.handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>{this.state.currentEditTitle}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div>{this.state.currentEditComponent}</div>
            </Modal.Body>
            </Modal>
        </div>
        );
    }
}