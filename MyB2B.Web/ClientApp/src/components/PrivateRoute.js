import React from 'react';
import {Route, Redirect} from 'react-router-dom';
import { AuthorizationService } from './Services/AuthorizationService';

export const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={(props) => (
      AuthorizationService.IsAuthenticated()
        ? <Component {...props} />
        : <Redirect to={{
            pathname: '/log-in',
            state: { from: props.location }
          }} />
    )} />
  )