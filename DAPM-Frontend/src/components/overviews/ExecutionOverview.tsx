import React, { useEffect, useState } from 'react'
import { TableRow, TableCell, IconButton, Collapse, Box, Table, TableHead, TableBody, Button } from '@mui/material';
import { KeyboardArrowDown, KeyboardArrowUp, Today } from '@mui/icons-material';
import ExecutionCard from '../cards/ExecutionCard.tsx';
import { fetchPipelineExecutions, fetchExecutionStatus } from '../../services/backendAPI.tsx';
import { log } from 'console';
import { exec } from 'child_process';

interface ExecutionOverviewProps {
    isOpen: boolean;
    parentPipelineId: string;
    pipelineOrgId: string;
    pipelineRepoId: string;
}

interface Execution { //This interface should be edited to accompany the correct format of executions.
    id: string;
    status: string;
    executionTime: string;
}

const ExecutionOverview: React.FC<ExecutionOverviewProps> = ({ isOpen, parentPipelineId, pipelineOrgId, pipelineRepoId }) => {
    var isRunning = false; //This should be updated with all exeuctions

    const [executions, setExecutions] = useState<Execution[]>([]);

    const fetchStatus = async (executionId: string) => {  
        console.log("Fetching status for execution ID: ", executionId);
        var executionStatusResponse = await fetchExecutionStatus(pipelineOrgId, pipelineRepoId, parentPipelineId, executionId);

        const executionStatus = executionStatusResponse.result.status;

        console.log("Execution Status: ", executionStatus);

        return executionStatus;
        
    };

    const fetchData = async () => { 

        var pipelineExecutions = await fetchPipelineExecutions(pipelineOrgId, pipelineRepoId, parentPipelineId);

        // Transform the data to match Execution interface
        const executionIds = pipelineExecutions?.result?.executions?.[0]?.executionIds || [];

        var formattedExecutions: Execution[] = [];  

        console.log("Execution IDs: ", executionIds);

        for (var executionId of executionIds) { 
            var status = await fetchStatus(executionId);
            console.log("Execution ID: ", executionId, "Status: ", status);
            const newExecution: Execution = {
                id: executionId,
                status: status,
                executionTime: new Date().toISOString()
            }
            formattedExecutions.push(newExecution);
        } 

        // const formattedExecutions: Execution[] = executionIds.map((id : string) => ({
        //     id: id,
        //     status: "Not started", 
        //     executionTime: new Date().toISOString() 
        // }));

        
        console.log("Formatted Executions: ", formattedExecutions);
        setExecutions(formattedExecutions);
        console.log("Executions: ", executions);
    };


    useEffect(() => {
        fetchData();
        //setExecutions([{ id: "123e4567-e89b-12d3-a456-426614174000", status: "Not started", executionTime: "12/11/2024 09:04" }])

        const interval = setInterval(() => {
            fetchData();
            
        }, 5000);

        // Cleanup function
        return () => {
            clearInterval(interval);
        };

    }, []);

  return (
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
                            {executions && executions.length > 0 ? (
                                executions.map((exe) => (
                                    <TableRow key={exe.id}>
                                        <ExecutionCard 
                                            status={exe.status} 
                                            id={exe.id} 
                                            executionTime={exe.executionTime} 
                                            isRunning={isRunning}
                                        />
                               
                                    </TableRow>
                                ))
                            ) : (
                                <TableRow>
                                    No executions found
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                </Box>
            </Collapse>
        </TableCell>
    </TableRow>
  )
}

export default ExecutionOverview

