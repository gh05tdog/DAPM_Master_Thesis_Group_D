﻿import React, {useState } from 'react';
import {Button, ThemeProvider} from '@mui/material';
import Header from '../components/headers/Header.tsx';
import PipelineManageSearch from '../components/searchFields/PipelineManageSearch.tsx';
import { Box } from "@mui/material";
import ManagerList from '../components/lists/ManagerList.tsx';
import {createTheme} from "@mui/material/styles";
import { useSearchParams } from 'react-router-dom';

interface ManagerPageProps {
    manageType: string;
}

export default function ManagePage() {
    const [openPopup, setOpenPopup] = useState(false);
    const [mode, setMode] = useState<'light' | 'dark'>('light');
    
    const [selectedID, setSelectedID] = useState<{ ID: string } | null>(null);
    
    const [searchParams] = useSearchParams();
    const manageType = searchParams.get('manageType');
    const handleClosePopup = () => {
        setOpenPopup(false);
    };

    const handleOpenPopup = () => {
        setOpenPopup(true);
    };

    const theme = createTheme({
        palette: {
            mode: mode,
        },
    });

    return (
        <
            ThemeProvider theme={theme}>
            <Header setMode={setMode} currentMode={mode} />

            <Box
                data-qa = 'ManagerPage'    
                sx={{display: 'flex', flexDirection: 'row', justifyContent: 'space-between'}}>
                <PipelineManageSearch setSelectedID={setSelectedID} />

                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>
                {/*  <PipelineMangePopup open={openPopup} onClose={handleClosePopup} selectedPipeline={selectedID} />  */}
            </Box>
            
            <Box data-qa = "pipeline-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
                <ManagerList selectedID={selectedID} value={manageType} />
            </Box>
        </ThemeProvider>
    )
};