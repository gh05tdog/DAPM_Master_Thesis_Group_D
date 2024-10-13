import React from 'react';
import { Box, Typography } from '@mui/material';

const Sidebar: React.FC = () => (
    <Box
        sx={{
            width: 250,
            bgcolor: 'rgba(54,55,56,1)',
            borderRight: '1px solid lightgray',
            height: '100vh',
            position: 'fixed',
            elevation: '0'
        }}
    >
        <Typography variant="h6" sx={{ p: 2 }}>Sidebar</Typography>
        {/* Add your sidebar items here */}
    </Box>
);

export default Sidebar;
