import React, { useState, useEffect } from 'react';
import {
  Box,
  Table,
  TableHead,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Paper,
  Button,
  Typography,
} from '@mui/material';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';
import { fetchPipelineUsers } from '../../../src/services/backendAPI.tsx';


const ITEMS_PER_PAGE = 10;

interface PipelineData {
  id: number;
  name: string;
}

interface User {
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
}

export default function PipelineManageTable({ data }: { data: PipelineData[] }) {
  const [rows, setRows] = useState<PipelineData[]>(data || []);
  const [page, setPage] = useState(1);
  const [users, setUsers] = useState<User[]>([]);

  const handleRemove = (id: number) => {  
    setRows((prevRows: { id: number; name: string }[]) => prevRows.filter((row) => row.id !== id));
  };

  useEffect(() => {
  const fetchUsers = async () => {
    try {
      const userData = await getUsersFromKeycloak();
      setUsers(userData);
    } catch (error) {
      console.error('Failed to fetch users:', error); // Log the error but don't set it in the UI
    }
  };
  fetchUsers();
  fetchPipelineUsers();
}, []);

  // Pagination controls
  const totalPages = Math.ceil(rows.length / ITEMS_PER_PAGE);
  const handleNextPage = () => {
    setPage((prevPage) => Math.min(prevPage + 1, totalPages));
  };
  const handlePrevPage = () => {
    setPage((prevPage) => Math.max(prevPage - 1, 1));
  };
  
  const startIndex = (page - 1) * ITEMS_PER_PAGE;
  const currentRows = rows.slice(startIndex, startIndex + ITEMS_PER_PAGE);


  return (
      <Box sx={{ width: '100%', margin: 'auto', mt: 4 }}>row
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {users.map((user) => (
                  <TableRow key={user.id}>
                    <TableCell>{user.id}</TableCell>
                    <TableCell>{user.firstName}</TableCell>
                    <TableCell align="right">
                      <Button
                          variant="outlined"
                          color="error"
                           //onClick={() => handleRemove(user.id)}
                      >
                        Remove
                      </Button>
                    </TableCell>
                  </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
        
        <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 2 }}>
          <Button
              variant="contained"
              color="primary"
              disabled={page === 1}
              onClick={handlePrevPage}
          >
            Previous Page
          </Button>
          <Typography variant="body1">
            Page {page} of {totalPages}
          </Typography>
          <Button
              variant="contained"
              color="primary"
              disabled={page === totalPages}
              onClick={handleNextPage}
          >
            Next Page
          </Button>
        </Box>
      </Box>
  );
}