// src/components/LandingPage.tsx
import React from 'react';
import { Button, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const LandingPage: React.FC = () => {
  const navigate = useNavigate(); // Hook for navigation

  const handleButtonClick = () => {
    navigate('/login'); // Navigate to the overview page
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20%' }}>
      <Typography variant="h4" gutterBottom>
        Welcome to the Application
      </Typography>
      <Button variant="contained" color="primary" onClick={handleButtonClick}>
        Go to Overview
      </Button>
    </div>
  );
};

export default LandingPage;
