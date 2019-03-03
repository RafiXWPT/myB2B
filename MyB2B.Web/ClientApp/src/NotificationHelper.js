import React, { Component } from 'react';
import { NotificationManager, NotificationContainer } from 'react-notifications';
import "./libs/notifications.css";

export class NotificationHelper extends Component {
    static Instance = null;
    constructor(props) {
        super(props);
        NotificationHelper.Instance = this;
    }

    success(message) {
        NotificationManager.success(message);
    }

    error(message) {
        NotificationManager.error(message);
    }

    render() {
        return <NotificationContainer/>
    }
}