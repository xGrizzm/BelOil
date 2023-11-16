import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  RouterProvider,
  createBrowserRouter,
  redirect
} from 'react-router-dom'

import AuthorizationPage from './pages/AuthorizationPage'
import GamePage from './pages/GamePage'

import JwtHelper from './utils/helpers/JwtHelper';

import 'bootstrap/dist/css/bootstrap.css';

const router = createBrowserRouter([
  {
    path: "/authorization",
    element: <AuthorizationPage />
  },
  {
    path: "/game",
    element: <GamePage />,
    loader: async () => {
      if (!JwtHelper.getToken()) {
        return redirect("/authorization");
      } else 
        return null;
    }
  }
]);

String.prototype.format = function () {
  var args = arguments;
  return this.replace(/{([0-9]+)}/g, function (match, index) {
    return typeof args[index] == 'undefined' ? match : args[index];
  });
};

ReactDOM.createRoot(document.getElementById('root')).render(
  <RouterProvider router={router} />
)
