import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CircularProgress, Button, Typography } from '@mui/material';

const LoginPage: React.FC = () => {

  // Declare the state for login status
  const [loginStatus, setLoginStatus] = React.useState(false);

  // Get the navigate function from the router
  const navigate = useNavigate();

  // Handle the login action
  const handleLogin = () => 
    setLoginStatus(true);
    navigate('/'); // Redirect to the home page
  
  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20%' }}>
      <Typography variant="h4" gutterBottom>
        Welcome to the Application
      </Typography>
      <Button variant="contained" color="primary" onClick={handleLogin}>
        Login with Keycloak
      </Button>
    </div>
  );
};

export default LoginPage;
