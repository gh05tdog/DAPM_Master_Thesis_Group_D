import React from 'react';
import { AppBar, Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { useNavigate } from 'react-router-dom';

interface PipelineOverviewPageProps {
    userInfo: any;
  }

const Header: React.FC<PipelineOverviewPageProps> = ({ userInfo }) => {

       const navigate = useNavigate();
       
       const returnToOverview = () => {
           navigate("/user");
       };

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

export default Header;
