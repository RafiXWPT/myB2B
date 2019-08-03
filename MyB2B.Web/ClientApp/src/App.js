import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { LogIn } from './components/LogIn/LogIn';
import { Register } from './components/Register/Register';
import { TestToken } from './components/TestToken/TestToken';
import { AccountAdministration } from './components/AccountAdministration/AccountAdministration';
import {PrivateRoute} from './components/PrivateRoute';
import { InvoiceGenerator } from './components/InvoiceGenerator/InvoiceGenerator';
import { Invoices } from './components/Invoices/Invoices';

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/log-in' component={LogIn} />
        <Route path='/register' component={Register} />
        <PrivateRoute path='/invoices' component={Invoices} />
        <PrivateRoute path='/account-administration' component={AccountAdministration} />
        <PrivateRoute path='/test-token' component={TestToken} />
        <PrivateRoute path='/test-invoice-generator' component={InvoiceGenerator} />
      </Layout>
    );
  }
}
