import React from 'react';
import { Box, Typography } from '@mui/material';
import BackButton from '../buttons/BackButton.tsx';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';


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
        <BackButton />
        <OrgList />
        <RepoList />
        <ResourceList />
    </Box>
);

export default Sidebar;
