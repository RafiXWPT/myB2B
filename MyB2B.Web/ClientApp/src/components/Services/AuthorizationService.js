import React, {Component} from 'react';
import { MyB2BRequest } from '../Extensions/MyB2BRequest';

export class AuthorizationService extends Component {
    
    constructor(props) {
        super(props);

        this.state = {isAuthenticated: false};

        MyB2BRequest.refreshToken().then(result => {
            AuthorizationService.SetAuthenticationState(result.isAuthenticated);
        });
    }

    static SetAuthenticationState = (state) => {
        this.setState({isAuthenticated: state});
    }

    static IsAuthenticated = () => {
        return this.state.isAuthenticated;
    }
}