import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
} from '@mui/material';
import { fetchPipelineUsers} from '../../../src/services/backendAPI.tsx';



interface PipelineManageSearchProps {
    setSelectedPipeline: (pipeline: { pipelineId: string } | null) => void;
    manageType: string;
}

export default function PipelineManageSearch({ setSelectedPipeline, manageType }: PipelineManageSearchProps) {
    const [pipelineOptions, setPipelineOptions] = useState<{ pipelineId: string }[]>([]);


    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchPipelineUsers();

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
            data-qa = "pipeline-searchField"
            sx={{
                width: '80%',
                display: 'flex'
                
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
