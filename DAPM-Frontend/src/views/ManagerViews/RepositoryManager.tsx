import React, { useEffect, useState } from 'react';
import {
    Button, ThemeProvider,
} from '@mui/material';
import Header from '../../components/headers/Header.tsx';
import RepositoryManageSearch from '../../components/searchFields/RepositoryManageSearch.tsx';
import RepositoryManagePopup from "../../components/searchFields/RepositoryManagePopup.tsx";
import { Box } from "@mui/material";
import RepositoryManageTable from '../../components/overviews/RepositoryManageTable.tsx';
import {createTheme} from "@mui/material/styles";

interface RepositoryOverviewPageProps {
    user: any;
}

const RepositoryManager: React.FC<RepositoryOverviewPageProps> = ({ user }) => {
    const [info, setInfo] = useState<any>(null);
    const [selectedRepository, setSelectedRepository] = useState<{ repositoryId: string } | null>(null);
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
                <RepositoryManageSearch setSelectedRepository={setSelectedRepository} />

                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '10%' }}
                    onClick={handleOpenPopup}
                >
                    Add user
                </Button>

            </Box>


            <Box data-qa = "repository-manager"
                 sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
                <RepositoryManageTable selectedRepository={selectedRepository} />
                <RepositoryManagePopup open={openPopup} onClose={handleClosePopup} selectedRepository={selectedRepository} />
            </Box>
        </ThemeProvider>
    )
};

export default RepositoryManager;