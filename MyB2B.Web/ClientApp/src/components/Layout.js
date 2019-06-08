import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { NotificationHelper } from '../NotificationHelper';
import { GlobalSpinner } from './Spinner/ApplicationSpinner';
import '../libs/spinner.css';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <GlobalSpinner identifier="global-spinner"/>
        <NotificationHelper/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
