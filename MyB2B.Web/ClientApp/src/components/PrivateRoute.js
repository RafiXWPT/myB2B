import React, {Component} from 'react';
import {BrowserRouter as Router, Route, Link, Redirect} from 'react-router-dom';
import { AuthorizationService } from './Services/AuthorizationService';

export const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={(props) => (
      localStorage.getItem('auth-token') != null
        ? <Component {...props} />
        : <Redirect to={{
            pathname: '/log-in',
            state: { from: props.location }
          }} />
    )} />
  )