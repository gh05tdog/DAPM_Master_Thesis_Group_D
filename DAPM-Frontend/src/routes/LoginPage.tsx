// src/routes/LoginPage.tsx
import React from 'react';
import { Button, Typography, Box } from '@mui/material';

const LoginPage: React.FC = () => {
  // Handle login
  const handleLogin = async () => {
    try {
      const keycloakConfig = await import('../keycloak.mts');
      keycloakConfig.default.login(); // Redirect to Keycloak login page
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

  // Handle logout
  const handleLogout = async () => {
    try {
      const keycloakConfig = await import('../keycloak.mts');
      keycloakConfig.default.logout(); // Redirect to Keycloak logout page
    } catch (error) {
      console.error('Logout failed:', error);
    }
  };

  return (
    <Box display="flex" flexDirection="column" alignItems="center" mt={5}>
      <Typography variant="h4" gutterBottom>
        Welcome to the Application
      </Typography>
      <Button
        variant="contained"
        color="primary"
        onClick={handleLogin}
        style={{ marginTop: '20px' }}
      >
        Login
      </Button>
      <Button
        variant="contained"
        color="secondary"
        onClick={handleLogout}
        style={{ marginTop: '20px' }}
      >
        Logout
      </Button>
    </Box>
  );
};

export default LoginPage;
