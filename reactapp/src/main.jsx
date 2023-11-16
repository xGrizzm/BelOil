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

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
)
