import React from 'react';
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { useNavigate } from 'react-router-dom';
import DropDownManage from '../buttons/DropDownManage.tsx';
import PipelineGrid from "../overviews/old_PipelineOverview.js";
import { addNewPipeline, setImageData } from '../../state_management/slices/pipelineSlice.ts';
import {v4 as uuidv4} from "uuid";
import {useDispatch} from "react-redux";

interface PipelineOverviewPageProps {
    userInfo: any;
  }

  export default function Header({userInfo}: PipelineOverviewPageProps) {

       const navigate = useNavigate();
       const dispatch = useDispatch();

      const returnToOverview = () => {
           navigate("/user");
       };

      const createNewPipeline = () => {
          dispatch(addNewPipeline({ id: `pipeline-${uuidv4()}`, flowData: { nodes: [], edges: [] } }));
          navigate("/pipeline");  // Navigate to pipeline editor after creation
      };
    
      const navigateToManage = () => {
        navigate('/manage-pipeline')
      }
    

       return (

           <AppBar
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

            <Button variant="contained"
                    color="primary"
                    sx={{marginRight: 2}}
                    onClick = {createNewPipeline}>
                Create new pipeline

            </Button>
            
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}> 
                </Typography>
            </Box>
            
            <DropDownManage/>

            <Box>
                <IconButton color="inherit" aria-label="account">
                    <AccountCircle />
                    <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}>
                        {userInfo?.name}
                    </Typography>
                </IconButton>
            </Box>
        </Toolbar>
    </AppBar>
);
};
