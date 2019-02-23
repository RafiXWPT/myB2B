export class RequestExtensions {
    static getWithToken = (url, additionalHeaders) => {
        return fetch(url, {
            method: 'GET',
            headers: {
                ...additionalHeaders,
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
            }
        });
    }

    static postWithToken = (url, body, additionalHeaders) => {
        return fetch(url, {
            method: 'POST',
            body: body,
            headers: {
                ...additionalHeaders,
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('auth-token')
            }
        });
    }
}