import React, {useEffect, useState} from 'react';
import {Box, Button, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import {useDispatch, useSelector} from 'react-redux';
import {getPipelines} from '../../state_management/selectors';
import {useNavigate} from 'react-router-dom';
import {addNewPipeline, setActivePipeline} from '../../state_management/slices/pipelineSlice';
import AddIcon from '@mui/icons-material/Add';
import {v4 as uuidv4} from "uuid";
import {getOrganizations, getRepositories} from "../../state_management/selectors/apiSelector.ts";
import {organizationThunk, repositoryThunk} from "../../state_management/slices/apiSlice.ts";
import {pipelineThunk} from "../../state_management/slices/pipelineSlice.ts"
import {Organization, Repository} from "../../state_management/states/apiState.ts";
import {PipelineData} from "../../state_management/states/pipelineState.ts";
import ExecutionOverview from './ExecutionOverview.tsx';
import { KeyboardArrowDown, KeyboardArrowUp } from '@mui/icons-material';

const MainContent: React.FC = () => {
    const dispatch = useDispatch();
    
    const organizations: Organization[] = useSelector(getOrganizations);
    const repositories: Repository[] = useSelector(getRepositories);
    const pipelines: PipelineData[] = useSelector(getPipelines);
    const navigate = useNavigate();
    const [openRows, setOpenRows] = useState<{ [key: string]: boolean }>({});

    const toggleRow = (id: string) => {
        setOpenRows((prev) => ({ ...prev, [id]: !prev[id] }));
    }
    

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
        if (repositories.length > 0) {
            try {
                dispatch(pipelineThunk({organizations, repositories}));
            } catch (error) {
                console.log(error);
            }
        }
    }, [dispatch, repositories]);
    
    const navigateToPipeline = (id: string) => {
        dispatch(setActivePipeline(id));
        navigate('/pipeline');
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
                            <TableCell />
                            <TableCell>Name</TableCell>
                            <TableCell>Pipeline ID</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {pipelines?.map(({ id, name, status }) => (
                            <>
                            <TableRow key={id}>
                                <TableCell>
                                    <IconButton
                                        aria-label="expand row"
                                        size="small"
                                        onClick={() => toggleRow(id)}
                                    >
                                        {openRows[id] ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
                                    </IconButton>
                                </TableCell>
                                <TableCell>{name}</TableCell>
                                <TableCell>{id}</TableCell>
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
                            <ExecutionOverview isOpen={openRows[id]} parentPipelineId={id}/>
                            </>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
};

export default MainContent;