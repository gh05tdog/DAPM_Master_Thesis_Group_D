import React from 'react';

import Pipelines1 from '../MainpageComponents/MainContentComponents/Pipelines1'
import Pipelines2 from '../MainpageComponents/MainContentComponents/Pipelines2'
import Pipelines3 from '../MainpageComponents/MainContentComponents/Pipelines3'
import Pipelines4 from '../MainpageComponents/MainContentComponents/Pipelines4'
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
        {
            <div style={{display: 'flex', flexDirection: 'row', gap: '5px'}}>
                <Pipelines1/>
                <Pipelines2/>
                <Pipelines3/>
                <Pipelines4/>
            </div>
        }
    </Box>
);

export default MainContent;