import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { LogIn } from './components/LogIn/LogIn';
import { Register } from './components/Register/Register';
import { TestToken } from './components/TestToken/TestToken';

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/log-in' component={LogIn} />
        <Route path='/register' component={Register} />
        <Route path='/test-token' component={TestToken} />
      </Layout>
    );
  }
}
