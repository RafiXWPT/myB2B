export class RequestExtensions {
    static refreshToken = () => {
        return fetch('api/Account/refresh-token', {
            headers: {'Authorization': 'Bearer ' + localStorage.getItem('auth-token')} 
         })
         .then(response => response.json())
         .then(data => {
             if(data.shouldRefresh) {
                localStorage.setItem('auth-token', data.authData.token);
             }
         });
    }

    static getWithToken = (url, actionFunction, additionalHeaders) => {
        RequestExtensions.refreshToken().then(x => {
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

    static postWithToken = (url, body, actionFunction, additionalHeaders) => {
        RequestExtensions.refreshToken().then(x => {
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