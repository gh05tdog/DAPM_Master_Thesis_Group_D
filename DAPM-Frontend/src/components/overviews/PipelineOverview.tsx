import React from 'react';
import {Box, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import {useDispatch, useSelector} from 'react-redux';
import {getPipelines} from '../../state_management/selectors/index.ts';
import {useNavigate} from 'react-router-dom';
import {addNewPipeline, setActivePipeline} from '../../state_management/slices/pipelineSlice.ts';
import AddIcon from '@mui/icons-material/Add';
import {v4 as uuidv4} from "uuid";

const MainContent: React.FC = () => {
    const pipelines = useSelector(getPipelines);
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const navigateToPipeline = (id: string) => {
        dispatch(setActivePipeline(id));
        navigate('/pipeline');
    };
    
    const createNewPipeline = () => {
        dispatch(addNewPipeline({ id: `pipeline-${uuidv4()}`, flowData: { nodes: [], edges: [] } }));
        navigate("/pipeline");
    };

    return (
        <Box
            sx={{
                width: '100%',
                p: 0
            }}
        >
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Organization</TableCell>
                            <TableCell>Repository</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {pipelines.map(({ id, name, status, organizationId, repositoryId, createdBy }) => (
                            <TableRow key={id}>
                                <TableCell>{name}</TableCell>
                                <TableCell>{status}</TableCell>
                                <TableCell>{organizationId}</TableCell>
                                <TableCell>{repositoryId}</TableCell>
                                <TableCell>
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        onClick={() => navigateToPipeline(id)}
                                    >
                                        Edit
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
};

export default MainContent;