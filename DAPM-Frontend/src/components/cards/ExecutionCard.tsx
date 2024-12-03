import { Button, TableCell, TableRow } from '@mui/material';
import React from 'react'
import { putCommandStart } from '../../services/backendAPI.tsx';


interface ExecutionProps{
    id: string;
    status: string;
    executionTime: string;
    isRunning: boolean;
    onClick: (id: string) => {};
}

const ExecutionCard: React.FC<ExecutionProps> = ({id, status, executionTime, isRunning, onClick}) => {

  return (
    <>
        <TableCell>{id}</TableCell>
        {isRunning ? <TableCell></TableCell> : <></>}
        <TableCell>{status}</TableCell>
        <TableCell>{executionTime}</TableCell>
        <Button variant="outlined" color="primary" onClick={() => onClick(id)}>Start Execution</Button>
    </>
  )
}

export default ExecutionCard