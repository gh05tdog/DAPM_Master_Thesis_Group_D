import React, {useEffect} from 'react';
import {Box, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import {useDispatch, useSelector} from 'react-redux';
import {getPipelines} from '../../state_management/selectors';
import {useNavigate} from 'react-router-dom';
import {addNewPipeline, setActivePipeline} from '../../state_management/slices/pipelineSlice';
import AddIcon from '@mui/icons-material/Add';
import {v4 as uuidv4} from "uuid";
import {getOrganizations, getRepositories} from "../../state_management/selectors/apiSelector.ts";
import {organizationThunk, repositoryThunk, pipelineThunk} from "../../state_management/slices/apiSlice.ts";

const MainContent: React.FC = () => {
    const dispatch = useDispatch();
    
    const organizations = useSelector(getOrganizations);
    const repositories = useSelector(getRepositories);
    const pipelines = useSelector(getPipelines);
    const navigate = useNavigate();
    

    useEffect(() => {
        dispatch(organizationThunk());
    }, [dispatch]);

    useEffect(() => {
        if (organizations.length > 0) {
            try {
                dispatch(repositoryThunk(organizations));
            } catch (error) {
                console.error(error);
            }
        }
    }, [dispatch, organizations]);
    
    useEffect(() => {
        if (pipelines.length > 0) {
            try {
                dispatch(pipelineThunk({organizations, repositories}));
            } catch (error) {
                console.log(error);
            }
        }
    }, [dispatch, pipelines]);
    
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
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {pipelines.map(({ id, name, status }) => (
                            <TableRow key={id}>
                                <TableCell>{name}</TableCell>
                                <TableCell>{status}</TableCell>
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