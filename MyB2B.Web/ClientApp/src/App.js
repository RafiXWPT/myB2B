import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { LogIn } from './components/LogIn/LogIn';
import { Register } from './components/Register/Register';
import { TestToken } from './components/TestToken/TestToken';
import { AccountAdministration } from './components/AccountAdministration/AccountAdministration';

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/log-in' component={LogIn} />
        <Route path='/register' component={Register} />
        <Route path='/account-administration' component={AccountAdministration} />
        <Route path='/test-token' component={TestToken} />
      </Layout>
    );
  }
}
