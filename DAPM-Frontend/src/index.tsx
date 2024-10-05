import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Login from './routes/LoginPage'; // This wraps your App with ReactKeycloakProvider

ReactDOM.render(
  <React.StrictMode>
    <Login />
  </React.StrictMode>,
  document.getElementById('root')
);
