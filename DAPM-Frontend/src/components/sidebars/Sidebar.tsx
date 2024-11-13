import React from 'react';
import { Box, Typography, List, ListItem, ListItemIcon, Divider } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';
import UserList from '../../components/lists/UserLists.tsx';

const Sidebar: React.FC = () => (
  <Box
  sx={{
    width: 250,
    position: 'flex',
    top: 0, 
    left: 0, 
    height: '100vh',
    bgcolor: 'background.paper',
    borderRight: '1px solid lightgray',
    overflowY: 'auto', 
    zIndex: 1, 
}}
  >
    <Box sx={{ p: 2, textAlign: 'center', bgcolor: 'primary.main', color: 'primary.contrastText', borderBottom: '1px solid', borderColor: 'divider' }}>
      <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Control Panel</Typography>
    </Box>

    <Box sx={{ flexGrow: 1, p: 2 }}>
      <List>
        <ListItem button>
          <ListItemIcon>
            <ArrowBackIcon color="primary" />
          </ListItemIcon>
          <Typography variant="body1" color="text.primary">Back</Typography>
        </ListItem>

        <Divider sx={{ my: 2 }} />

        <OrgList />

        <Divider sx={{ my: 2 }} />
        
        <RepoList />


        <ResourceList />

        <Divider sx={{ my: 2 }} />

        {/* Will only be displayed if a user has access to view-users */}
        <UserList />
        
      </List>
    </Box>

    <Box sx={{p: 2, textAlign: 'center', borderTop: '1px solid', borderColor: 'divider', bgcolor: 'background.paper' }}>
      <Typography variant="body2" color="text.secondary">
        &copy; 2024 Group D
      </Typography>
    </Box>
  </Box>
);

export default Sidebar;
