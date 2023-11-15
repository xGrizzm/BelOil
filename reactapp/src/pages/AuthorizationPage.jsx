import React, { Component } from 'react';

export default class AuthorizationPage extends Component {
    constructor(props) {
        super(props);
        this.state = { login: "", password: "" };
    }

    static handleChange = event => {
        const {name, value} = event.target;
        setState({
            [name]: value
        });
    }

    static loginButtonClick = () => {
        history.push("/game");
    }

    render() {
        return (
            <div>
                <input
                    name="login"
                    type="text"
                    placeholder="Логин"
                    defaultValue={this.state.login}
                    onChange={this.handleChange} />

                <input
                    name="password"
                    type="password"
                    placeholder="Пароль"
                    defaultValue={this.state.password}
                    onChange={this.handleChange} />

                <button onClick={this.loginButtonClick}>Войти</button>
            </div>
        );
    }
}
