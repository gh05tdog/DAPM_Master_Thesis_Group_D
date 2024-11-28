import { Button, TableCell, TableRow } from '@mui/material';
import React from 'react'

interface ExecutionProps{
    id: string;
    status: string;
    executionTime: string;
    isRunning: boolean;
}


const ExecutionCard: React.FC<ExecutionProps> = ({id, status, executionTime, isRunning}) => {
  return (
    <>
        <TableCell>{id}</TableCell>
        {isRunning ? <TableCell></TableCell> : <></>}
        <TableCell>{status}</TableCell>
        <TableCell>{executionTime}</TableCell>
        <Button variant="outlined" color="primary" onClick={() => console.log("Eww")}>Start Execution</Button>
    </>
  )
}

export default ExecutionCard