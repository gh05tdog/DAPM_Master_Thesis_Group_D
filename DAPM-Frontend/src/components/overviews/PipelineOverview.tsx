import React, { useEffect, useState } from 'react';
import { Box, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { getPipelines } from '../../state_management/selectors/index.ts';
import { useNavigate } from 'react-router-dom';
import Spinner from '../cards/SpinnerCard.tsx';

import { addNewPipeline, setActivePipeline } from '../../state_management/slices/pipelineSlice';
import { getOrganizations, getRepositories } from "../../state_management/selectors/apiSelector.ts";
import { pipelineThunk } from "../../state_management/slices/pipelineSlice.ts"
import { Organization, Repository } from "../../state_management/states/apiState.ts";
import { PipelineData } from "../../state_management/states/pipelineState.ts";

const MainContent: React.FC = () => {
    const dispatch = useDispatch();
    const [loading, setLoading] = useState<boolean>(true);
    const organizations: Organization[] = useSelector(getOrganizations);
    const repositories: Repository[] = useSelector(getRepositories);
    const pipelines: PipelineData[] = useSelector(getPipelines);
    const navigate = useNavigate();

    useEffect(() => {
        if (repositories && repositories.length > 0) {
            try {
                dispatch(pipelineThunk({ organizations, repositories }));
            } catch (error) {
                console.log(error);
            } finally{
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
            <div>
                <Spinner />
             </div>
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
                            <TableCell>Name</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {pipelines?.map(({ id, name, status }) => (
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