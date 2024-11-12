import React, { useEffect, useState } from 'react';
import { List, ListItem, ListItemText, Typography, Accordion, AccordionSummary, AccordionDetails, Box } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';

interface User {
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
}

const UserList: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const userData = await getUsersFromKeycloak();
        setUsers(userData);
      } catch (error) {
        console.error('Failed to fetch users:', error); // Log the error but don't set it in the UI
        setError('error'); // Only setting a generic error status for internal control
      } finally {
        setLoading(false);
      }
    };
    fetchUsers();
  }, []);

  if (loading) return <Typography>Loading...</Typography>;

  // Do not render the Accordion if there is an error
  if (error) return null;

  return (
    <Accordion defaultExpanded sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="user-list-content"
        id="user-list-header"
        sx={{ bgcolor: 'primary.main', color: 'white', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Users</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          <List>
            {users.map((user) => (
              <ListItem key={user.id}>
                <ListItemText
                  primary={`${user.firstName ?? ''} ${user.lastName ?? ''}`}
                  secondary={`User ID: ${user.id}`}
                />
              </ListItem>
            ))}
          </List>
        </Box>
      </AccordionDetails>
    </Accordion>
  );
};

export default UserList;
