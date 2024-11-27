import React, { useEffect, useState } from 'react';
import {
    Button, ThemeProvider,
} from '@mui/material';
import Header from '../../components/headers/Header.tsx';
import OrganizationManageSearch from '../../components/searchFields/OrganizationManageSearch.tsx';
import OrganizationManagePopup from '../../components/searchFields/OrganizationManagePopup.tsx';
import { Box } from "@mui/material";
import OrganizationManageTable from '../../components/overviews/OrganizationManageTable.tsx';
import {createTheme} from "@mui/material/styles";

interface OrganizationOverviewPageProps {
    user: any;
}

const OrganizationManager: React.FC<OrganizationOverviewPageProps> = ({ user }) => {
    const [info, setInfo] = useState<any>(null);
    const [selectedOrganization, setSelectedOrganization] = useState<{ organizationId: string } | null>(null);
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
                <OrganizationManageSearch setSelectedOrganization={setSelectedOrganization} />

                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>

            </Box>


            <Box data-qa = "organization-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
                <OrganizationManageTable selectedOrganization={selectedOrganization} />
                <OrganizationManagePopup open={openPopup} onClose={handleClosePopup} selectedOrganization={selectedOrganization} />
            </Box>
        </ThemeProvider>
    )
};

export default OrganizationManager;