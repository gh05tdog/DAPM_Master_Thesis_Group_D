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

export default function PipelineManageTable({ selectedPipeline }) {
  const [users, setUsers] = useState([]);
  const [page, setPage] = useState(1);

  useEffect(() => {
    const fetchUsersData = async () => {
      try {
        if (selectedPipeline) {
          const allUsers = await getUsersFromKeycloak(); // Array of user objects
          const pipelineUsers = await fetchPipelineUsers(); // Array of {userId, pipelineId}
          
          // Filter pipelineUsers for the selected pipeline
          const filteredPipelineUsers = pipelineUsers.filter(
            (pu) => pu.pipelineId === selectedPipeline.pipelineId
          );
          
          // Map userIds to user details
          const usersForPipeline = allUsers.filter((user) =>
            filteredPipelineUsers.some((pu) => pu.userId === user.id)
          );
          setUsers(usersForPipeline);
          setPage(1); // Reset to first page when pipeline changes
        } else {
          setUsers([]);
        }
      } catch (error) {
        console.error('Failed to fetch users:', error);
      }
    };
    fetchUsersData();
  }, [selectedPipeline]);

  // Pagination controls
  const totalPages = Math.ceil(users.length / ITEMS_PER_PAGE);
  const handleNextPage = () => {
    setPage((prevPage) => Math.min(prevPage + 1, totalPages));
  };
  const handlePrevPage = () => {
    setPage((prevPage) => Math.max(prevPage - 1, 1));
  };
  
  const startIndex = (page - 1) * ITEMS_PER_PAGE;
  const currentUsers = users.slice(startIndex, startIndex + ITEMS_PER_PAGE);

  return (
    <Box sx={{ width: '100%', margin: 'auto', mt: 4 }}>
      {selectedPipeline ? (
        <>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>ID</TableCell>
                  <TableCell>First Name</TableCell>
                  <TableCell>Last Name</TableCell>
                  <TableCell>Username</TableCell>
                  <TableCell align="right">Actions</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {currentUsers.map((user) => (
                  <TableRow key={user.id}>
                    <TableCell>{user.id}</TableCell>
                    <TableCell>{user.firstName}</TableCell>
                    <TableCell>{user.lastName}</TableCell>
                    <TableCell>{user.username}</TableCell>
                    <TableCell align="right">
                      <Button
                        variant="outlined"
                        color="error"
                        // onClick={() => handleRemove(user.id)}
                      >
                        Remove
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
                {currentUsers.length === 0 && (
                  <TableRow>
                    <TableCell colSpan={5} align="center">
                      No users found for the selected pipeline.
                    </TableCell>
                  </TableRow>
                )}
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
              Page {page} of {totalPages || 1}
            </Typography>
            <Button
              variant="contained"
              color="primary"
              disabled={page === totalPages || totalPages === 0}
              onClick={handleNextPage}
            >
              Next Page
            </Button>
          </Box>
        </>
      ) : (
        <Typography variant="h6" align="center">
          Please select a pipeline to view its users.
        </Typography>
      )}
    </Box>
  );
}
