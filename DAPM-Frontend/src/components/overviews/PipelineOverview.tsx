import React from 'react';
import BackButton from '../OverviewPage/Buttons/BackButton'
import { Box, Typography } from '@mui/material';

const MainContent: React.FC = () => (
    <Box
        component="main"
        sx={{
            flexGrow: 1,
            bgcolor: 'background.default',
            p: 3,
            marginLeft: '250px', // Leave space for the sidebar
        }}
    >
        <Typography variant="h4">Main Content Area</Typography>
        {/* Add your main content here */
        <BackButton />
        }
    </Box>
);

export default MainContent;