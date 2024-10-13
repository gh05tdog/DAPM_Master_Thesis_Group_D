import React from 'react';
import { Box, Typography } from '@mui/material';

const Sidebar: React.FC = () => (
    <Box
        sx={{
            width: 250,
            bgcolor: 'rgba(54,55,56,1)',
            height: '100vh',
            position: 'relative', // Changed from 'fixed' to 'relative'
        }}
    >
        <Typography variant="h6" sx={{ p: 2 }}>Sidebar</Typography>
        {/* Add your sidebar items here */}
    </Box>
);

export default Sidebar;