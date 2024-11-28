import React, {useState } from 'react';
import {Button, ThemeProvider} from '@mui/material';
import Header from '../components/headers/Header.tsx';
import ManageSearch from '../components/searchFields/ManageSearch.tsx';
import { Box } from "@mui/material";
import ManagerList from '../components/lists/ManagerList.tsx';
import {createTheme} from "@mui/material/styles";
import { useSearchParams } from 'react-router-dom';
import {PipelineData} from "../state_management/states/pipelineState.js";
import {Organization, Repository, Resource} from "../state_management/states/apiState.js";

type SelectedItem =
    | { repository: Repository }
    | { pipeline: PipelineData }
    | { resource: Resource }
    | { organization: Organization }
    | null;

export default function ManagePage() {
    
    const [searchParams] = useSearchParams();
    const manageType = searchParams.get('manageType');
    const [openPopup, setOpenPopup] = useState(false);
    const [mode, setMode] = useState<'light' | 'dark'>('light');
    
    const [selectedItem, setSelectedItem] = useState<{ repository:Repository } | null>(null);

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
                <ManageSearch setSelectedItem={setSelectedItem} manageType={manageType} />

                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>
            </Box>
            
            <Box data-qa = "pipeline-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
                <ManagerList selectedID={selectedItem?.repository} value={manageType} />
            </Box>
        </ThemeProvider>
    )
};