import React, { useEffect, useState } from 'react';
import { Box, Button, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { getPipelines } from '../../state_management/selectors/index.ts';
import { useNavigate } from 'react-router-dom';
import Spinner from '../cards/SpinnerCard.tsx';

import { addNewPipeline, setActivePipeline } from '../../state_management/slices/pipelineSlice';
import { getOrganizations, getRepositories } from "../../state_management/selectors/apiSelector.ts";
import { pipelineThunk } from "../../state_management/slices/pipelineSlice.ts"
import { Organization, Repository } from "../../state_management/states/apiState.ts";
import { PipelineData } from "../../state_management/states/pipelineState.ts";
import ExecutionOverview from './ExecutionOverview.tsx';
import { KeyboardArrowDown, KeyboardArrowUp } from '@mui/icons-material';

const MainContent: React.FC = () => {
    const dispatch = useDispatch();
    const [loading, setLoading] = useState<boolean>(false);
    const organizations: Organization[] = useSelector(getOrganizations);
    const repositories: Repository[] = useSelector(getRepositories);
    const pipelines: PipelineData[] = useSelector(getPipelines);
    const navigate = useNavigate();
    const [openRows, setOpenRows] = useState<{ [key: string]: boolean }>({});
    const toggleRow = (id: string) => setOpenRows({ ...openRows, [id]: !openRows[id] });


    useEffect(() => {
        if (repositories && repositories.length > 0) {
            setLoading(true);
            try {
                dispatch(pipelineThunk({ organizations, repositories }));
            } catch (error) {
                console.log(error);
            } finally {
                setLoading(false);
            }
        }

    }, [dispatch, repositories]);

    const navigateToPipeline = (id: string) => {
        console.log("pipeline-Id", id);
        dispatch(setActivePipeline(id));
        navigate(`/pipeline/${id}`);
    };

    if (loading) {
        return (
            <Box sx={{ flexGrow: 1, display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                <Spinner color="white" />
            </Box>
        )
    }

    return (
        <Box
            data-qa="mainWindow"
            sx={{
                width: '100%',
                height: '100%',
                p: 2
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
                        {pipelines?.map(({ id, name, orgId, repoId, status }) => (
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
                            <ExecutionOverview isOpen={openRows[id]} parentPipelineId={id} pipelineOrgId= {orgId} pipelineRepoId= {repoId} />
                            </>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
};

export default MainContent;