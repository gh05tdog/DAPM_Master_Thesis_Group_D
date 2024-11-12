import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
    Button,
    MenuItem,
    Select,
    InputLabel,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
} from '@mui/material';
import { fetchPipelineUsers, addUserPipeline } from '../../../src/services/backendAPI.tsx';



interface PipelineManageSearchProps {
    setSelectedPipeline: (pipeline: { pipelineId: string } | null) => void;
}

export default function PipelineManageSearch({ setSelectedPipeline }: PipelineManageSearchProps) {
    const [pipelineOptions, setPipelineOptions] = useState<{ pipelineId: string }[]>([]);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchPipelineUsers();
                // Extract unique pipelines from the data
                const uniquePipelines = Array.from(new Set(data.map((item: { pipelineId: any; }) => item.pipelineId)))
                    .map(pipelineId => ({ pipelineId: pipelineId as string }));
                setPipelineOptions(uniquePipelines);
            } catch (error) {
                console.error("Error fetching pipeline users:", error);
            }
        };
        fetchData();
    }, []);


    return (
        <Box
            sx={{
                color: 'primary.main',
                width: '100%',
                margin: 'auto',
                mt: 4,
                display: 'flex',
                alignItems: 'center',
                gap: 2,
                paddingRight: 10,
            }}
        >
            <FormControl sx={{ flex: 1, bgcolor: 'white' }}>
                <Autocomplete
                    disablePortal
                    options={pipelineOptions}
                    getOptionLabel={(option) => `PipelineId: ${option.pipelineId}`}
                    onChange={(_event, newValue) => {
                        setSelectedPipeline(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Select Pipeline" variant="outlined" />
                    )}
                />
            </FormControl>

            
        </Box>
    );
}
