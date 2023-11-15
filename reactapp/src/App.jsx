import React, { Component } from 'react';
import {
    BrowserRouter as Router,
    Routes,
    Route
} from 'react-router-dom';

import AuthorizationPage from './pages/AuthorizationPage';
import GamePage from './pages/GamePage';

export default class App extends Component {
    render() {
        return (
            <Router>
                <div>
                    <Routes>
                        <Route path="/authorization" element={<AuthorizationPage />} />
                        <Route path="/game" element={<GamePage />} />
                    </Routes>
                </div>
            </Router>
        );
    }
}
