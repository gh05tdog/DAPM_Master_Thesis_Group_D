import React, { useState } from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import DropDownManage from '../buttons/DropDownManage.tsx';
import ColorModeIconDropdown from '../../assets/theme/ColorModeIconDropdown.tsx';
import AddIcon from '@mui/icons-material/Add';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { addNewPipeline } from '../../state_management/slices/pipelineSlice.ts';
import { v4 as uuidv4 } from 'uuid';
import { setActivePipeline } from '../../state_management/slices/pipelineSlice.ts';
import CreateUserModal from '../Modals/users/CreateUserModal.tsx';
import { flexbox } from '@mui/system';
import LogoutButton from '../buttons/LogoutButton.tsx';
import CreateRepositoryModal from '../Modals/Repositories/CreateRepositoryModal.tsx';
import { getActiveOrganisation, getActiveRepository } from '../../state_management/slices/indexSlice.ts';


interface HeaderProps {
    setMode: (mode: 'light' | 'dark') => void;
    currentMode: 'light' | 'dark';
}

export default function Header({ setMode, currentMode }: HeaderProps) {
    const getSelectedorganizationId = useSelector(getActiveOrganisation);
    const getSelectedRepoId = useSelector(getActiveRepository);

    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [isOpen, setIsOpen] = useState(false);
    const [isRepositoryModalOpen, setRepositoryModalIsOpen] = useState(false);
    const returnToOverview = () => {
        navigate("/user");
    };

    const createNewPipeline = () => {
        if (!getSelectedorganizationId?.id) return alert("Please select an Organization");
        if (!getSelectedRepoId?.id) return alert("Please select a Repository");

        const uuid = uuidv4();
        dispatch(addNewPipeline({ id: `${uuid}`, flowData: { nodes: [], edges: [] } }));
        navigate(`/pipelineEditor`);
    };

    return (

        <AppBar
            data-qa="header"
            position="relative"
            sx={{
                bgcolor: 'rgb(54,55,56,1)',
                paddingX: 3,
                paddingY: 1,
                width: 'calc(100%)'
            }}
        >
            <Box sx={{ width: 'auto', display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                <Box sx={{ display: "flex", gap: 2 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        sx={{ borderRadius: 50 }}
                        onClick={returnToOverview}
                    >
                        Overview
                    </Button>
                    <DropDownManage />
                </Box>
                <Box sx={{ display: "flex", gap: 2 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        startIcon={<AddIcon />}
                        sx={{ borderRadius: 50, backgroundColor: 'primary', "&:hover": { backgroundColor: 'primary' } }}
                        onClick={() => createNewPipeline()}
                    >
                        Create New Pipeline
                    </Button>
                    <Box sx={{ display: "flex", gap: 2 }}>
                        <CreateUserModal isOpen={isOpen} onClose={() => setIsOpen(false)} />
                        <Button
                            onClick={() => setRepositoryModalIsOpen(true)}
                            variant="contained"
                            color="primary"
                            startIcon={<AddIcon />}
                            sx={{ borderRadius: 50, backgroundColor: 'primary', "&:hover": { backgroundColor: 'primary' } }}>
                            Create Repository
                        </Button>
                        <CreateRepositoryModal isOpen={isRepositoryModalOpen} onClose={() => setRepositoryModalIsOpen(false)} />
                    </Box>
                    <LogoutButton />
                    <ColorModeIconDropdown setMode={setMode} currentMode={currentMode} />
                </Box>
            </Box>

        </AppBar>
    );
};
