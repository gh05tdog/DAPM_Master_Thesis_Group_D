import React, { useEffect, useState } from 'react'
import { TableRow, TableCell, IconButton, Collapse, Box, Table, TableHead, TableBody, Button } from '@mui/material';
import { KeyboardArrowDown, KeyboardArrowUp, Today } from '@mui/icons-material';
import ExecutionCard from '../cards/ExecutionCard.tsx';

interface ExecutionOverviewProps {
    isOpen: boolean;
    parentPipelineId: string;
}

interface Execution { //This interface should be edited to accompany the correct format of executions.
    id: string;
    status: string;
    executionTime: string;
}

const ExecutionOverview: React.FC<ExecutionOverviewProps> = ({ isOpen, parentPipelineId }) => {
    var isRunning = false; //This should be updated with all exeuctions

    const [executions, setExecutions] = useState<Execution[]>([]);

    useEffect(() => {
        //TODO: Add a function to get all executions of a pipeline
        setExecutions([{ id: "123e4567-e89b-12d3-a456-426614174000", status: "Not started", executionTime: "12/11/2024 09:04" }])
    }, []);

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
                                {/* <TableCell><b>User name</b></TableCell>
                                <TableCell><b>Status</b></TableCell>
                                <TableCell><b>Execution Time</b></TableCell> */}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {/* Replace with actual sub-table data */}
                            <TableRow>
                                {executions.map((exe) => {
                                    return (
                                        <ExecutionCard status={exe.status} id={exe.id} executionTime={exe.executionTime} isRunning={isRunning}></ExecutionCard>
                                    )
                                })}
                                {/* <TableCell>123e4567-e89b-12d3-a456-426614174000</TableCell>
                                {isRunning ? <TableCell></TableCell> : <></>}
                                <TableCell>In Progress...</TableCell>
                                <TableCell>05/11/2024 - 08:40</TableCell> */}
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

