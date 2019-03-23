import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { NotificationHelper } from '../NotificationHelper';
import { Spinner } from './Spinner/Spinner';
import '../libs/spinner.css';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Spinner name="global-spinner"><div class="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></Spinner>
        <NotificationHelper/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
