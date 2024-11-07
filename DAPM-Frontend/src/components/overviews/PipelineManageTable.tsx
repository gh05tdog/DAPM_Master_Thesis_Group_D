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

// Sample data for rows
const initialRows = [
  { id: '123', name: 'Olivia Rhye' },
  { id: '456', name: 'Ciaran Murray' },
  { id: '789', name: 'Marina Macdonald' },
  // Add more rows as needed
];

export default function SimpleTable() {
  const [rows, setRows] = useState(initialRows);

  const handleRemove = (id: string) => {
    setRows((prevRows) => prevRows.filter((row) => row.id !== id));
  };

  return (
      <TableContainer component={Paper} sx={{ width: '100%', margin: 'auto', mt: 4 }}>
        <Table aria-label="Simple Table">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
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
  );
}