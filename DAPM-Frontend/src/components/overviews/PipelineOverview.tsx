import React from 'react';
import BackButton from '../buttons/BackButton.tsx';
import { Box, Typography } from '@mui/material';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';
import Spinner from '../cards/SpinnerCard.tsx';
import Pipeline1 from '../pipelines/Pipeline1.tsx';
import Pipeline2 from '../pipelines/Pipeline2.tsx';
import Pipeline3 from '../pipelines/Pipeline3.tsx';
import Pipeline4 from '../pipelines/Pipeline4.tsx';


const MainContent: React.FC = () => {
    return (
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    gap: '5px',
                    flexGrow: 1,  
                    width: '100%',
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
    );
};

export default MainContent;
