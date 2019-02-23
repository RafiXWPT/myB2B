import React, {Component} from 'react';
import {Button} from 'react-bootstrap';
import {RequestExtensions} from '../Extensions/RequestExtensions'

export class TestToken extends Component {
    static displayName = TestToken.name;

    constructor(props) {
        super(props);

        this.state = {token: ""}
    }

    testGetToken = event => {
        RequestExtensions.getWithToken('api/Account/get-test-token')
        .then(response => response.json())
        .then(data => {
            var x = 0;
        })
        .catch(err => console.log);      
    }

    testPostToken = event => {
        RequestExtensions.postWithToken('api/Account/post-test-token', JSON.stringify({data: 'X'}))
        .then(response => response.json())
        .then(data => {
            var y = 0;
        })
        .catch(err => console.log);
    }

    getToken = event => {
        fetch('api/Account/authenticate', {
            method: 'POST', 
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({username: 'token.test', password: 'token.test'})
          })
          .then(response => response.json())
          .then(data => {
            if(data.success) {
                localStorage.setItem('user-id', data.result.userId);
                localStorage.setItem('auth-token', data.result.token);
                var i = 0;
            } else {
                var j = 0;
            }
          })
          .catch(err => console.log);
    }

    render () {
        return (
            <div>
                <Button block bssize="large" onClick={this.testGetToken}>Test Get</Button>
                <Button block bssize="large" onClick={this.testPostToken}>Test Post</Button>
                <Button block bssize="large" onClick={this.getToken}>Get Token</Button>
            </div>
        );
    }
}