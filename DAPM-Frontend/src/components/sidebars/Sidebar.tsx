import React from 'react';
import { Box, Typography, List, ListItem, ListItemIcon, Divider } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';

const Sidebar: React.FC = () => (
  <Box
  sx={{
    width: 250,
    position: 'fixed',
    top: 0, 
    left: 0, 
    height: '100vh',
    bgcolor: 'background.paper',
    borderRight: '1px solid lightgray',
    overflowY: 'auto', 
    zIndex: 1, 
}}
  >
    <Box sx={{ p: 2, textAlign: 'center', bgcolor: 'primary.main', color: 'white', borderBottom: '1px solid lightgray' }}>
      <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Control Panel</Typography>
    </Box>

    <Box sx={{ flexGrow: 1, p: 2 }}>
      <List>
        <ListItem button>
          <ListItemIcon>
            <ArrowBackIcon color="primary" />
          </ListItemIcon>
          <Typography variant="body1">Back</Typography>
        </ListItem>

        <Divider sx={{ my: 2 }} />

        <OrgList />

        <Divider sx={{ my: 2 }} />
        
        <RepoList />

        <Divider sx={{ my: 2 }} />

        <ResourceList />
      </List>
    </Box>

    <Box sx={{ p: 2, textAlign: 'center', bgcolor: 'background.paper', borderTop: '1px solid lightgray' }}>
      <Typography variant="body2" color="textSecondary">
        &copy; 2024 Group D
      </Typography>
    </Box>
  </Box>
);

export default Sidebar;
