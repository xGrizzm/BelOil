import React, { Component } from 'react';

export default class GamePage extends Component {
    constructor(props) {
        super(props);
        this.state = { login: "", password: "" };
    }

    render() {
        return (
            <div>
                GAME PAGE
            </div>
        );
    }
}
