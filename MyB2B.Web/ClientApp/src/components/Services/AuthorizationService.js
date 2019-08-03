import {Component} from 'react';
import { MyB2BRequest } from '../Extensions/MyB2BRequest';
import { NavMenu } from '../NavMenu';

export class AuthorizationService extends Component {
    static updateAuthStatus = () => {
        return MyB2BRequest.refreshToken().then(result => {
            localStorage.setItem('is-authenticate', result);
            NavMenu.Instance.updateAuthStatus(result);
        });
    }

    static LogIn = (loginResult) => {
        localStorage.setItem('user-id', loginResult.data.userId);
        localStorage.setItem('user-company-id', loginResult.data.companyId)
        localStorage.setItem('auth-token', loginResult.data.token);
        AuthorizationService.updateAuthStatus().then(x => {
            window.location.href = '/';
        });
    }

    static LogOut = () => {
        localStorage.removeItem('user-id');
        localStorage.removeItem('user-company-id')
        localStorage.removeItem('auth-token');
        localStorage.setItem('is-authenticate', false);
        NavMenu.Instance.updateAuthStatus(false);
        window.location.href = '/log-in';
    }

    static IsAuthenticated = () => {return localStorage.getItem('is-authenticate') == "true"};
}