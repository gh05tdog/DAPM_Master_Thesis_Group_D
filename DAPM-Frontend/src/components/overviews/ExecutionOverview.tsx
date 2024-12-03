import React, { useEffect, useState } from 'react'
import { TableRow, TableCell, IconButton, Collapse, Box, Table, TableHead, TableBody, Button } from '@mui/material';
import { KeyboardArrowDown, KeyboardArrowUp, Today } from '@mui/icons-material';
import ExecutionCard from '../cards/ExecutionCard.tsx';
import { fetchPipelineExecutions, fetchExecutionStatus, putCommandStart } from '../../services/backendAPI.tsx';
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
        var executionStatusResponse = await fetchExecutionStatus(pipelineOrgId, pipelineRepoId, parentPipelineId, executionId);

        const executionStatus = executionStatusResponse.result.status;


        return executionStatus;
        
    };

    const fetchData = async () => {
        console.log("Fetching data for pipeline: ", pipelineOrgId, pipelineRepoId, parentPipelineId);
        try {
            var pipelineExecutions = await fetchPipelineExecutions(pipelineOrgId, pipelineRepoId, parentPipelineId);    
        } catch (error) {
            console.log("Error fetching pipeline executions: ", error);
        }
        
        // Transform the data to match Execution interface
        const executionIds = pipelineExecutions?.result?.executions?.[0]?.executionIds || [];

        var formattedExecutions: Execution[] = [];  
        for (var executionId of executionIds) { 
            var status = await fetchStatus(executionId);
            const newExecution: Execution = {
                id: executionId,
                status: status.state,
                executionTime: status.executionTime
            }
            formattedExecutions.push(newExecution);
        } 
        setExecutions(formattedExecutions);
    };

    const handleStartExecution = async (exeId: string) => {
        try{
            const response = await putCommandStart(pipelineOrgId, pipelineRepoId, parentPipelineId, exeId);
        } catch (error) {
            console.log("Error starting execution: ", error);
        }
    }


    useEffect(() => {
        fetchData();

        const interval = setInterval(() => {
            fetchData();
            
        }, 60*1000);

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
                                            onClick={() => handleStartExecution(exe.id)}
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

