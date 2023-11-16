import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";

import ApiService from '../api/ApiService';
import JwtHelper from '../utils/helpers/JwtHelper';
import URLConstants from '../utils/constants/URLConstants';

export default function AuthorizationPage() {
    const [state, setState] = useState({ login: "vasil", password: "vasil" });
    
    const navigate = useNavigate();

    const handleChange = event => {
        const {name, value} = event.target;
        setState({...state, [name]: value});
    }

    const loginButtonClick = async () => {
        try {
            const response = await ApiService.post(URLConstants.LOGIN_URL, state);
            JwtHelper.setToken(response.data.token);
            navigate("/game");
        } catch (error) {
            
        }
    }

    return (
        <div>
            <input
                name="login"
                type="text"
                placeholder="Логин"
                defaultValue={state.login}
                onChange={handleChange} />

            <input
                name="password"
                type="password"
                placeholder="Пароль"
                defaultValue={state.password}
                onChange={handleChange} />

            <button onClick={loginButtonClick}>Войти</button>
        </div>
    );
}
