import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import {AuthorizationService} from './Services/AuthorizationService';
import './NavMenu.css';

export class PublicMenuItems extends Component {
  render () {
    return (<React.Fragment>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
            </React.Fragment>)
  }
}

export class PrivateMenuItems extends Component {
  render() {
    return (<React.Fragment>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/invoices">Invoices</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/test-invoice-generator">Invoice</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/test-token">Token API Test</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/account-administration">Account</NavLink>
                </NavItem>
    </React.Fragment>)
  }
}

export class NavMenu extends Component {
  static displayName = NavMenu.name;
  static Instance = null;

  constructor (props) {
    super(props);

    NavMenu.Instance = this;
    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.updateAuthStatus = this.updateAuthStatus.bind(this);
    this.state = {
      collapsed: true,
      isAuthenticated: AuthorizationService.IsAuthenticated()
    };
  }

  updateAuthStatus (status) {
    this.setState({isAuthenticated: status});
  }

  handleLogOut () {
    AuthorizationService.LogOut();
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">myB2B.Web {this.state.i}</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
              <PublicMenuItems/>
              {this.state.isAuthenticated ? <PrivateMenuItems/> : <React.Fragment/>}
              {this.state.isAuthenticated
                ? <NavItem><NavLink className="text-dark" href="#" onClick={this.handleLogOut}>Log out</NavLink></NavItem> 
                : <React.Fragment>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/log-in">Log-in</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/register">Register</NavLink>
                </NavItem>
                </React.Fragment>}
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
