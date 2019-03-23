import React, {Component} from 'react';
import {Button} from 'react-bootstrap';
import {MyB2BRequest} from '../Extensions/MyB2BRequest'

export class TestToken extends Component {
    static displayName = TestToken.name;

    constructor(props) {
        super(props);

        this.state = {token: ""}
    }

    testGetToken = event => {
        MyB2BRequest.get('api/Authentication/get-test-token',
        result => {
            console.log(result);
        });           
    }

    testPostToken = event => {
        MyB2BRequest.post('api/Authentication/post-test-token',
        JSON.stringify({data: 'X'}), result => {
            console.log(result);
        });
    }

    getToken = event => {
        fetch('api/Authentication/authenticate', {
            method: 'POST', 
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({username: 'token.test', password: 'token.test'})
          })
          .then(response => response.json())
          .then(result => {
            if(result.success) {
                localStorage.setItem('user-id', result.data.userId);
                localStorage.setItem('auth-token', result.data.token);
            } else {
            }
          })
          .catch(err => console.log);
    }

    render () {
        return (
            <div>
                <div className="col-md-12" style={{textAlign:"center"}}>User: token.test / Password: token.test</div>
                <div className="col-md-12" style={{textAlign:"center"}}>User must be registered. Check results in console.</div>
                <Button block bssize="large" onClick={this.testGetToken}>Test Get</Button>
                <Button block bssize="large" onClick={this.testPostToken}>Test Post</Button>
                <Button block bssize="large" onClick={this.getToken}>Get Token</Button>
            </div>
        );
    }
}