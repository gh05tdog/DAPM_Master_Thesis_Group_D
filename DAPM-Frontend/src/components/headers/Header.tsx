import React from 'react';
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import DropDownManage from '../buttons/DropDownManage.tsx';
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

      const returnToOverview = () => {
           navigate("/user");
       };

      const createNewPipeline = () => {
          dispatch(addNewPipeline({ id: `pipeline-${uuidv4()}`, flowData: { nodes: [], edges: [] } }));
          navigate(`/pipeline/pipeline-${uuidv4()}`);
      };
    
      const navigateToManage = () => {
        navigate('/manage-pipeline')
      }
    

       return (

           <AppBar
               data-qa = "header"
               position="relative"
               sx={{
                   bgcolor: 'rgba(54,55,56,1)',
                   paddingX: 3,
                   width: 'calc(100%)'
               }}
           >
        <Toolbar sx={{marginLeft: '230px', justifyContent: 'space-between' }}>
            <Button variant="contained"
                    color="primary"
                    sx={{marginRight: 2}}
                    onClick = {returnToOverview}>
                Overview

            </Button>

            <Button
                variant="contained"
                color="success"
                startIcon={<AddIcon/>}
                sx={{borderRadius: 50, backgroundColor: '#4caf50', "&:hover": {backgroundColor: '#388e3c'}}}
                onClick={() => createNewPipeline()} // Adjust to create a new pipeline logic
            >
                Create New Pipeline
            </Button>
            
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}> 
                </Typography>
            </Box>
            
            <DropDownManage/>

            {/* Right-aligned Color Mode Icon */}
            <Box sx={{ml: 'auto'}}>
                <ColorModeIconDropdown setMode={setMode} currentMode={currentMode}/>
            </Box>
        </Toolbar>
    </AppBar>
);
};
