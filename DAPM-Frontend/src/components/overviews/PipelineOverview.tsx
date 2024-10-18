import React from 'react';
import BackButton from '../buttons/BackButton.tsx'
import { Box, Typography } from '@mui/material';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';
import PipelineGrid from '../overviews/old_PipelineOverview.tsx';

const MainContent: React.FC = () => {
    return (
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
        <>
            <BackButton />
            <OrgList />
            <RepoList />
            <ResourceList />
            <PipelineGrid />
        </>
        }
    </Box>
    );
};


export default MainContent;
