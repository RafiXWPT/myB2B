import {saveAs} from 'file-saver';
import axios from 'axios';

export class MyB2BRequest {
    static refreshToken = () => {
        var localAuthToken = localStorage.getItem('auth-token');
        if(localAuthToken == null) {
            throw new Error('no token in storage');
        }
        return fetch('api/Account/refresh-token', {
            headers: {'Authorization': 'Bearer ' + localAuthToken} 
         })
         .then(response => response.json())
         .then(data => {
             if(data.shouldRefresh) {
                localStorage.setItem('auth-token', data.authData.token);
             }
         });
    }

    static downloadFile = (url) => {
        MyB2BRequest.refreshToken().then(x => {

            axios({
                url: url,
                method: 'GET',
                headers: {'Authorization': 'Bearer ' + localStorage.getItem('auth-token')},
                responseType: 'blob',
              }).then((response) => {
                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', 'invoice.pdf');
                document.body.appendChild(link);
                link.click();
              });           
        });
    }

    static get = (url, actionFunction, additionalHeaders) => {
        MyB2BRequest.refreshToken().then(x => {
            fetch(url, {
                method: 'GET',
                redirect: 'follow',
                headers: {
                    ...additionalHeaders,
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
                }
            })
            .then(result => {
                if(result.redirected) {
                    window.location = result.url;
                    throw new Error("redirect");
                }
                if(result.ok == false && result.status == 500)
                {
                    window.location = '/error';
                    throw new Error('redirect-error');
                }
                return result;
            })
            .then(result => result.json())
            .then(actionFunction)
            .catch(err => console.log(err)); 
        });                       
    }

    static post = (url, body, actionFunction, additionalHeaders) => {
        MyB2BRequest.refreshToken().then(x => {
            fetch(url, {
                method: 'POST',
                redirect: 'follow',
                body: body,
                headers: {
                    ...additionalHeaders,
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
                }
            })
            .then(result => {
                if(result.redirected) {
                    window.location = result.url;
                    throw new Error("redirect");
                }
                if(result.ok == false && result.status == 500)
                {
                    window.location = '/error';
                    throw new Error('redirect-error');
                }
                return result;
            })
            .then(result => result.json())
            .then(actionFunction)
            .catch(err => console.log(err)); 
        })       
    }
}