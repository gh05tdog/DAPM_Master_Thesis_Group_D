import React, {  } from 'react'
import { TableRow, TableCell, IconButton, Collapse, Box, Table, TableHead, TableBody, Button } from '@mui/material';
import { KeyboardArrowDown, KeyboardArrowUp } from '@mui/icons-material';

interface ExecutionOverviewProps {
    isOpen: boolean;
}


const ExecutionOverview: React.FC<ExecutionOverviewProps> = ({ isOpen }) => {

  return (
    <>
        <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0, backgroundColor: '#f5f5f5' }} colSpan={4}>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <Box margin={1} sx={{ backgroundColor: '#f5f5f5', borderRadius: '4px', padding: '8px' }}>
                    <Table size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell><b>Execution ID</b></TableCell>
                                <TableCell><b>Status</b></TableCell>
                                <TableCell><b>Execution Time</b></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {/* Replace with actual sub-table data */}
                            <TableRow>
                                <TableCell>123e4567-e89b-12d3-a456-426614174000</TableCell>
                                <TableCell>In Progress...</TableCell>
                                <TableCell>05/11/2024 - 08:40</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </Box>
            </Collapse>
        </TableCell>
    </TableRow>
    </>
  )
}

export default ExecutionOverview