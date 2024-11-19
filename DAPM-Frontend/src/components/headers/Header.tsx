import React, { useState } from 'react';
import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material';
import DropDownManage from '../buttons/DropDownManage.tsx';
import ColorModeIconDropdown from '../../assets/theme/ColorModeIconDropdown.tsx';
import AddIcon from '@mui/icons-material/Add';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { addNewPipeline } from '../../state_management/slices/pipelineSlice.ts';
import { v4 as uuidv4 } from 'uuid';
import { setActivePipeline } from '../../state_management/slices/pipelineSlice.ts';
import CreateUserModal from '../users/CreateUserModal.tsx';


interface HeaderProps {
    setMode: (mode: 'light' | 'dark') => void;
    currentMode: 'light' | 'dark';
}

export default function Header({ setMode, currentMode }: HeaderProps) {

    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [isOpen, setIsOpen] = useState(false);
    const returnToOverview = () => {
        navigate("/user");
    };

    const createNewPipeline = () => {
        dispatch(addNewPipeline({ id: `pipeline-${uuidv4()}`, flowData: { nodes: [], edges: [] } }));
        navigate(`/pipeline/pipeline-${uuidv4()}`);
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
            <Box sx={{ width: 'auto', display: 'flex', flexDirection: 'row', justifyContent: 'space-between', marginLeft: '230px' }}>
                <Button
                    variant="contained"
                    color="primary"
                    sx={{ borderRadius: 50 }}
                    onClick={returnToOverview}
                >
                    Overview
                </Button>

                <Button
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon />}
                    sx={{ borderRadius: 50, backgroundColor: 'primary', "&:hover": { backgroundColor: 'primary' } }}
                    onClick={() => createNewPipeline()}
                >
                    Create New Pipeline
                </Button>

                <DropDownManage />
                <Button
                    onClick={() => setIsOpen(true)}
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon />}
                    sx={{ borderRadius: 50, backgroundColor: 'primary', "&:hover": { backgroundColor: 'primary' } }}>
                    Create User
                </Button>
                <CreateUserModal isOpen={isOpen} onClose={() => setIsOpen(false)} />
                <ColorModeIconDropdown setMode={setMode} currentMode={currentMode} />

            </Box>
        </AppBar>
    );
};
