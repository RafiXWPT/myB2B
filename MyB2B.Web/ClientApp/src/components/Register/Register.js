import React, { Component } from 'react';
import {Form, Button} from 'react-bootstrap';
import {NotificationContainer, NotificationManager} from 'react-notifications';
import '../../../node_modules/react-notifications/lib/notifications.css';
import "./Register.css";

export class Register extends Component {
  static displayName = Register.name;

  constructor (props) {
    super(props);

    this.state = { username: "", email: "", password: "", confirmPassword: "", samePassword: false, displaySamePasswordError: false, displayErrors: false};
    this.handleSubmit.bind(this);
  }

  validateEmptyForm() {
     return this.state.username.length > 0 &&
     this.state.email.length > 0 &&
     this.state.password.length > 0 &&
     this.state.confirmPassword.length > 0;
  }

  formChange = event => {
    this.setState({[event.target.id]: event.target.value});
  }

  handleSubmit = event => {
    event.preventDefault();

    if(!event.target.checkValidity()) {
      this.setState({displayErrors: true});
      NotificationManager.error('Form has errors.')
      return;
    } 

    this.setState({displayErrors: false});

    if(this.state.password !== this.state.confirmPassword) {
      this.setState({confirmPassword: '', displaySamePasswordError: true});
      NotificationManager.error('Passwords must be the same');
      return;
    }

    this.setState({displaySamePasswordError: false});
    
    fetch('api/Account/register', {
      method: 'POST', 
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify(this.state)
    })
    .then(response => response.json())
    .then(data => {
      if(data.success) {
        NotificationManager.success("User registered");
       } else {
        this.setState({ password: "", confirmPassword: "" });
        NotificationManager.error(data.errorMessage);
      }
    })
    .catch(err => console.log);
  }

  render () {
    const {displayErrors, displaySamePasswordError} = this.state;
    return (
    <div className="Register">
        <form noValidate onSubmit={this.handleSubmit} className={displayErrors ? 'displayErrors' : ''}>
        <Form.Group controlId="username" bssize="large">
          <Form.Label>Username:</Form.Label>
          <Form.Control autoFocus type="text" value={this.state.username} onChange={this.formChange} />
        </Form.Group>
        <Form.Group controlId="email" bssize="large">
          <Form.Label>Email:</Form.Label>
          <Form.Control type="email" value={this.state.email} onChange={this.formChange} />
        </Form.Group>
        <Form.Group controlId="password" bssize="large">
          <Form.Label>Password:</Form.Label>
          <Form.Control type="password" value={this.state.password} onChange={this.formChange} />
        </Form.Group>
        <Form.Group controlId="confirmPassword" bssize="large">
          <Form.Label>Confirm Password:</Form.Label>
          <Form.Control type="password" value={this.state.confirmPassword} onChange={this.formChange} className={displaySamePasswordError ? 'samePassword' : ''} />
        </Form.Group>
        <Button block bssize="large" disabled={!this.validateEmptyForm()} type="submit">Login</Button>
        </form>
        <NotificationContainer/>
    </div>
    );
  }
}
