import React from 'react';
import { Box, Typography, List, Divider,  } from '@mui/material';
import OrgList from '../lists/OrgList.tsx';
import RepoList from '../lists/RepoList.tsx';
import ResourceList from '../lists/ResourceList.tsx';
import UserList from '../../components/lists/UserLists.tsx';
import { useNavigate } from 'react-router-dom';
const Sidebar: React.FC = () => {
  const navigate = useNavigate();

  return (
    <Box
      data-qa='Sidebar'
      sx={{
        width: 300,
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

      <Box sx={{ flexGrow: 1, p: 2 }}>
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Control Panel</Typography>

        <List>


          <Divider sx={{ my: 2 }} />

          <OrgList />

          <Divider sx={{ my: 2 }} />

          <RepoList />

          <Divider sx={{ my: 2 }} />

          <ResourceList />

          <Divider sx={{ my: 2 }} />

          {/* Will only be displayed if a user has access to view-users */}
          <UserList />

        </List>
      </Box>

      <Box sx={{ p: 2, textAlign: 'center', borderTop: '1px solid', borderColor: 'divider', bgcolor: 'background.paper' }}>
        <Typography variant="body2" color="text.secondary">
          &copy; 2024 Group D
        </Typography>
      </Box>
    </Box>
  )
};

export default Sidebar;
