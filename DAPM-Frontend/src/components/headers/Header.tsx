import React from 'react';
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { useNavigate } from 'react-router-dom';

interface PipelineOverviewPageProps {
    userInfo: any;
  }

  export default function Header({userInfo}: PipelineOverviewPageProps) {

       const navigate = useNavigate();
       
       const returnToOverview = () => {
           navigate("/user");
       };

       const navigateToPipeline = () => {
        navigate('/pipeline');
      };
    
      const navigateToManage = () => {
        navigate('/manage-pipeline')
      }
    

       return (

           <AppBar
        position="static"
        sx={{ bgcolor: 'rgba(54,55,56,1)', paddingX: 3 }}
        elevation={3}
    >
        <Toolbar sx={{ justifyContent: 'space-between' }}>
            <Button variant="contained"
                    color="primary"
                    sx={{marginRight: 2}}
                    onClick = {returnToOverview}>
                Go back

            </Button>
            
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}> 
                </Typography>
            </Box>

            <Button variant="contained" color="primary" sx={{ marginRight: 2 }} onClick={navigateToManage}>
                  Pipeline Manager
              </Button>
            

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
