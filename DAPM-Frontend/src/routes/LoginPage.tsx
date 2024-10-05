import React, { useEffect } from 'react';
import { useKeycloak } from '@react-keycloak/web';
import { CircularProgress, Button, Typography } from '@mui/material';

const LoginPage: React.FC = () => {
  const { keycloak, initialized } = useKeycloak();

  useEffect(() => {
    // If already authenticated, navigate to the home page
    if (initialized && keycloak.authenticated) {
      window.location.href = "/";
    }
  }, [keycloak, initialized]);

  const handleLogin = () => {
    if (!keycloak.authenticated) {
      keycloak.login();
    }
  };

  if (!initialized) {
    // Show a loading spinner while Keycloak is initializing
    return <CircularProgress />;
  }

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
