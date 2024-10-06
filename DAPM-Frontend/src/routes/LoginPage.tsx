// src/routes/LoginPage.tsx
import React from 'react';
import { Button, Typography, Box } from '@mui/material';
import { login, logout } from '../keycloak.ts';

const LoginPage: React.FC = () => {
  return (
    <Box display="flex" flexDirection="column" alignItems="center" mt={5}>
      <Typography variant="h4" gutterBottom>
        Welcome to the Application
      </Typography>
      <Button
        variant="contained"
        color="primary"
        onClick={login}
        style={{ marginTop: '20px' }}
      >
        Login
      </Button>
      <Button
        variant="contained"
        color="secondary"
        onClick={logout}
        style={{ marginTop: '20px' }}
      >
        Logout
      </Button>
    </Box>
  );
};

export default LoginPage;
