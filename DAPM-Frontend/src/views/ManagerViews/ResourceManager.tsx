import React, { useEffect, useState } from 'react';
import {
    Button, ThemeProvider,
} from '@mui/material';
import Header from '../../components/headers/Header.tsx';
import ResourceManageSearch from "../../components/searchFields/ResourceManageSearch.tsx";
import ResourceManagePopup from "../../components/searchFields/ResourceManagePopup.tsx";
import { Box } from "@mui/material";
import ResourceManageTable from "../../components/overviews/ResourceManageTable.tsx";
import {createTheme} from "@mui/material/styles";

interface ResourceOverviewPageProps {
    user: any;
}

const ResourceManager: React.FC<ResourceOverviewPageProps> = ({ user }) => {
    const [info, setInfo] = useState<any>(null);
    const [selectedResource, setSelectedResource] = useState<{ resourceId: string } | null>(null);
    const [openPopup, setOpenPopup] = useState(false);
    const [mode, setMode] = useState<'light' | 'dark'>('light');

    useEffect(() => {
        const getUserInfo = async () => {
            const response = await user;
            setInfo(response);
        };
        getUserInfo();
    }, [user]);

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
        <ThemeProvider theme={theme}>
            <Header setMode={setMode} currentMode={mode} />

            <Box sx={{display: 'flex', flexDirection: 'row', justifyContent: 'space-between'}}>
                <ResourceManageSearch setSelectedResource={setSelectedResource} />

                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>

            </Box>


            <Box data-qa = "resource-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
                <ResourceManageTable selectedResource={selectedResource} />
                <ResourceManagePopup open={openPopup} onClose={handleClosePopup} selectedResource={selectedResource} />
            </Box>
        </ThemeProvider>
    )
};

export default ResourceManager;