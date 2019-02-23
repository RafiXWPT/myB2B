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
        MyB2BRequest.get('api/Account/get-test-token',
        result => {
            console.log(result);
        });           
    }

    testPostToken = event => {
        MyB2BRequest.post('api/Account/post-test-token',
        JSON.stringify({data: 'X'}), result => {
            console.log(result);
        });
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
                <div className="col-md-12" style={{textAlign:"center"}}>User: token.test / Password: token.test</div>
                <div className="col-md-12" style={{textAlign:"center"}}>User must be registered. Check results in console.</div>
                <Button block bssize="large" onClick={this.testGetToken}>Test Get</Button>
                <Button block bssize="large" onClick={this.testPostToken}>Test Post</Button>
                <Button block bssize="large" onClick={this.getToken}>Get Token</Button>
            </div>
        );
    }
}