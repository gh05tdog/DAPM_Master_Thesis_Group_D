import React, { useState } from 'react';
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
import {useNavigate} from "react-router-dom";

// Sample data for rows
const Users = [
  { id: 1, name: 'Olivia Rhye' },
  { id: 2, name: 'Ciaran Murray' },
  { id: 3, name: 'Marina Macdonald' },
  { id: 4, name: 'Charles Fulton' },
  { id: 5, name: 'Jay Hoper' },
  { id: 6, name: 'Steve Hampton' },
  { id: 7, name: 'Liam Peterson' },
  { id: 8, name: 'Ava Martinez' },
  { id: 9, name: 'Mia Robinson' },
  { id: 10, name: 'Sophia Johnson' },
  { id: 11, name: 'James Brown' },
  { id: 12, name: 'Emily Davis' },
];

const ITEMS_PER_PAGE = 10;

export default function SimpleTable() {
  const [rows, setRows] = useState(Users);
  const [page, setPage] = useState(1);

  const handleRemove = (id: number) => {
    setRows((prevRows) => prevRows.filter((row) => row.id !== id));
  };

  // Pagination controls
  const totalPages = Math.ceil(rows.length / ITEMS_PER_PAGE);
  const handleNextPage = () => {
    setPage((prevPage) => Math.min(prevPage + 1, totalPages));
  };
  const handlePrevPage = () => {
    setPage((prevPage) => Math.max(prevPage - 1, 1));
  };
  
  const navigate = useNavigate();

  const startIndex = (page - 1) * ITEMS_PER_PAGE;
  const currentRows = rows.slice(startIndex, startIndex + ITEMS_PER_PAGE);


  return (
      <Box sx={{ width: '100%', margin: 'auto', mt: 4 }}>
        <TableContainer component={Paper}>
          <Table aria-label="Simple Table">
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {currentRows.map((row) => (
                  <TableRow key={row.id}>
                    <TableCell>{row.id}</TableCell>
                    <TableCell>{row.name}</TableCell>
                    <TableCell align="right">
                      <Button
                          variant="outlined"
                          color="error"
                          onClick={() => handleRemove(row.id)}
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