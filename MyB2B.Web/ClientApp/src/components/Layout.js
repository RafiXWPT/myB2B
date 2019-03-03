import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { NotificationHelper } from '../NotificationHelper';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <NotificationHelper/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
