// src/components/Header.tsx
import * as React from 'react';
import Box from '@mui/material/Box/Box';
import ColorModeIconDropdown from '../../assets/theme/ColorModeIconDropdown.tsx';
import {Button} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import {useNavigate} from 'react-router-dom';
import {useDispatch} from 'react-redux';
import {addNewPipeline} from '../../state_management/slices/pipelineSlice.ts';
import {v4 as uuidv4} from 'uuid';
import {setActivePipeline} from '../../state_management/slices/pipelineSlice.ts';


interface HeaderProps {
    setMode: (mode: 'light' | 'dark') => void;
    currentMode: 'light' | 'dark';
}

export default function Header({setMode, currentMode}: HeaderProps) {

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const createNewPipeline = () => {
        dispatch(addNewPipeline({id: `pipeline-${uuidv4()}`, flowData: {nodes: [], edges: []}}));
        navigate(`/pipeline/pipeline-${uuidv4()}`);
    };

    return (
        <Box
            sx={{
                display: 'flex',
                width: '100%',
                alignItems: 'center',
                color: 'primary',
                justifyContent: 'space-between',
                maxWidth: {sm: '100%', md: '1700px'},
                pt: 1.5,
                px: 2, // Optional: padding for overall header spacing
            }}
        >
            {/* Left Spacer Box */}
            <Box sx={{flexGrow: 1}}></Box>

            {/* Center-aligned Button */}
            <Box sx={{flexGrow: 1, textAlign: 'center'}}>
                <Button
                    variant="contained"
                    color="success"
                    startIcon={<AddIcon/>}
                    sx={{borderRadius: 50, backgroundColor: '#4caf50', "&:hover": {backgroundColor: '#388e3c'}}}
                    onClick={() => createNewPipeline()} // Adjust to create a new pipeline logic
                >
                    Create New Pipeline
                </Button>
            </Box>

            {/* Right-aligned Color Mode Icon */}
            <Box sx={{ml: 'auto'}}>
                <ColorModeIconDropdown setMode={setMode} currentMode={currentMode}/>
            </Box>
        </Box>
    );
}
