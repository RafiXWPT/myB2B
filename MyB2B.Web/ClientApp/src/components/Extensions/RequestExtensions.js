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

    static getWithToken = (url, additionalHeaders) => {
        RequestExtensions.refreshToken().then(x => {
            return fetch(url, {
                method: 'GET',
                headers: {
                    ...additionalHeaders,
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
                }
            }); 
        });               
    }

    static postWithToken = (url, body, additionalHeaders) => {
        RequestExtensions.refreshToken().then(x => {
            return fetch(url, {
                method: 'POST',
                body: body,
                headers: {
                    ...additionalHeaders,
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
                }
            });
        })       
    }
}