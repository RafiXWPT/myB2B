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

    static LogIn = (userId, token) => {
        localStorage.setItem('user-id', userId);
        localStorage.setItem('auth-token', token);
        AuthorizationService.updateAuthStatus().then(x => {
            window.location.href = '/';
        });
    }

    static LogOut = () => {
        localStorage.removeItem('user-id');
        localStorage.removeItem('auth-token');
        localStorage.setItem('is-authenticate', false);
        NavMenu.Instance.updateAuthStatus(false);
        window.location.href = '/log-in';
    }

    static IsAuthenticated = () => {return localStorage.getItem('is-authenticate') == "true"};
}