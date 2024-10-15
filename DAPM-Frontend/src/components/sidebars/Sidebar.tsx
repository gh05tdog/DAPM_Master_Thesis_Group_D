import React from 'react';
import { Box, Typography } from '@mui/material';

const Sidebar: React.FC = () => (
    <Box
        sx={{
            width: 250,
            bgcolor: 'background.paper',
            borderRight: '1px solid lightgray',
            height: '100vh',
            position: 'fixed',
        }}
    >
        <Typography variant="h6" sx={{ p: 2 }}>Sidebar</Typography>
        {/* Add your sidebar items here */}
    </Box>
);

export default Sidebar;
