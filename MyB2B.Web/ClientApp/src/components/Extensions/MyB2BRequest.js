import axios from 'axios';

export class MyB2BRequest {
    static refreshToken = async () => {
        var localAuthToken = localStorage.getItem('auth-token');
        if(localAuthToken == null) {
            return false;
        }

        var refreshTokenResult = await fetch('api/Authentication/refresh-token', {
            headers: {'Authorization': 'Bearer ' + localAuthToken} 
        });

        if(refreshTokenResult.status == 401) {
            window.location.href = '/log-in';
            return false;
        } else {
            var jsonResponse = await refreshTokenResult.json();
            if(jsonResponse.ForceTokenInvalidate) {
                 localStorage.removeItem('auth-token');
            } else if(jsonResponse.shouldRefresh) {
                localStorage.setItem('auth-token', jsonResponse.data.authData.token);
            }
            return true;
        }
    }

    static downloadFile = async (url) => {
        var isTokenRefreshed = await MyB2BRequest.refreshToken();
        if(isTokenRefreshed == false) {
            return;
        }

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
    }

    static get = async (url, actionFunction, additionalHeaders) => {
        var isTokenRefreshed = await MyB2BRequest.refreshToken();
        if(isTokenRefreshed == false) {
            return;
        }

        var fetchResult = await fetch(url, {
            method: 'GET',
            redirect: 'follow',
            headers: {
                ...additionalHeaders,
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
            }
        });

        if(!MyB2BRequest.checkResponseStatus(fetchResult)) {
            return;
        }

        var fetchJson = await fetchResult.json();
        actionFunction(fetchJson);                 
    }

    static post = async (url, body, actionFunction, additionalHeaders) => {

        var isTokenRefreshed = await MyB2BRequest.refreshToken();
        if(isTokenRefreshed == false) {
            return;
        }

        var fetchResult = await fetch(url, {
            method: 'POST',
            redirect: 'follow',
            body: body,
            headers: {
                ...additionalHeaders,
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
            }
        });

        if(!MyB2BRequest.checkResponseStatus(fetchResult)) {
            return;
        }

        var fetchJson = await fetchResult.json();
        actionFunction(fetchJson);
    }

    static checkResponseStatus = (response) => {
        if(response.status == 200) {
            return true;
        }

        if(response.redirected) {
            window.location = response.result.url;
            return false;
        }

        if(response.status == 404) {
            window.location = '/not-found';
            return false;
        }

        if(response.ok == false && response.result.status == 500) {
            window.location = '/error';
            return false;
        }

        throw new Error("State not handled.");
    }
}