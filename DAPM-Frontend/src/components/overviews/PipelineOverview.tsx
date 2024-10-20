import React from 'react';
import { Box, Typography } from '@mui/material';
import Pipeline1 from '../pipelines/Pipeline1.tsx';
import Pipeline2 from '../pipelines/Pipeline2.tsx';
import Pipeline3 from '../pipelines/Pipeline3.tsx';
import Pipeline4 from '../pipelines/Pipeline4.tsx';

const MainContent: React.FC = () => {
    return (
        <Box
            component="main"
            sx={{
                flexGrow: 1,  // Ensures this box takes up available space
                bgcolor: 'background.default',
                p: 3,
                marginLeft: '250px',  // Leaves space for the sidebar
                display: 'flex',
                flexDirection: 'column',  // Ensures content inside stretches vertically if needed
                height: '100vh',  // Take full viewport height
            }}
        >
            <Typography variant="h4">Main Content Area</Typography>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    gap: '5px',
                    flexGrow: 1,  // Ensures the pipelines take up the remaining space
                }}
            >
                <Box sx={{ flexGrow: 1, flexBasis: 0 }}>
                    <Pipeline1 />
                </Box>
                <Box sx={{ flexGrow: 1, flexBasis: 0 }}>
                    <Pipeline2 />
                </Box>
                <Box sx={{ flexGrow: 1, flexBasis: 0 }}>
                    <Pipeline3 />
                </Box>
                <Box sx={{ flexGrow: 1, flexBasis: 0 }}>
                    <Pipeline4 />
                </Box>
            </Box>
        </Box>
    );
};

export default MainContent;
