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
import ManagePopup from "../components/searchFields/ManagePopup.tsx";

type ChosenItem = Repository | PipelineData | Resource | Organization;

export default function ManagePage()  {
    
    const [searchParams] = useSearchParams();
    const manageType = searchParams.get('manageType');
    const [openPopup, setOpenPopup] = useState(false);
    const [mode, setMode] = useState<'light' | 'dark'>('light');

    const [selectedItem, setSelectedItem] = useState<{ item: ChosenItem } | null>(null);

    const handleOpenPopup = () => {
        setOpenPopup(true);
    };
    
    const handleClosePopup = () => {
        setOpenPopup(false);
    };

    const selectedID = (() => {
        if (!selectedItem || !manageType) return null;

        const item = selectedItem.item;

        return item?.id || null; // Return only the `id` property or `null` if not available
    })();
    
    const theme = createTheme({
        palette: {
            mode: mode,
        },
    });

    return (
        <
            ThemeProvider theme={theme}>
            <Header setMode={setMode} currentMode={mode} />
            <Box data-qa = 'ManagerPage' 
            sx={{display: 'flex', flexDirection: 'column', justifyContent: 'space-between', padding : '16px'}}
            >
            <Box
                   
                sx={{display: 'flex', flexDirection: 'row', justifyContent: 'space-between'}}>
                <ManageSearch setSelectedItem={setSelectedItem} manageType={manageType} />
                <ManagePopup open={openPopup} onClose={handleClosePopup} selectedID={selectedID} manageType={manageType}/>

                <Button variant="contained" color="primary" sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>
            </Box>
            
            <Box data-qa = "pipeline-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '0px' }}>
                <ManagerList selectedID={selectedID} value={manageType} />
            </Box>
            </Box>
        </ThemeProvider>
    )
};