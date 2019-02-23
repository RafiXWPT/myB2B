import React, { Component } from 'react';
import {Form, Button} from 'react-bootstrap';
import {NotificationContainer, NotificationManager} from 'react-notifications';
import './notifications.css';
import "./LogIn.css";

export class LogIn extends Component {
  static displayName = LogIn.name;

  constructor (props) {
    super(props);

    this.state = { username: "", password: "" };
    this.handleSubmit.bind(this);
  }

  validateEmptyForm() {
      return this.state.username.length > 0 && this.state.password.length > 0;
  }

  formChange = event => {
      this.setState({[event.target.id]: event.target.value});
  }

  handleSubmit = event => {
    event.preventDefault();
      fetch('api/Account/authenticate', {
        method: 'POST', 
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(this.state)
      })
      .then(response => response.json())
      .then(data => {
        if(data.success) {
          NotificationManager.success("User logged in");
        } else {
          this.setState({ username: "", password: "" });
          NotificationManager.error(data.errorMessage);
        }
      })
      .catch(err => console.log);
  }

  render () {
    return (
    <div className="Login">
        <form onSubmit={this.handleSubmit}>
        <Form.Group controlId="username" bssize="large">
          <Form.Label>Username:</Form.Label>
          <Form.Control autoFocus type="text" value={this.state.username} onChange={this.formChange} />
        </Form.Group>
        <Form.Group controlId="password" bssize="large">
          <Form.Label>Password:</Form.Label>
          <Form.Control type="password" value={this.state.password} onChange={this.formChange} />
        </Form.Group>
        <Button block bssize="large" disabled={!this.validateEmptyForm()} type="submit">Login</Button>
        </form>
        <NotificationContainer/>
    </div>
    );
  }
}
