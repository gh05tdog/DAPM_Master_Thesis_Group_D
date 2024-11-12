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
import { fetchPipelineUsers } from '../../../src/services/backendAPI.tsx';


const Users = [
    { label: 'Olivia Rhye' },
    { label: 'Ciaran Murray' },
    // ... other users
];

function ManagePipelinePopup({ open, onClose }) {
    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Manage Pipeline</DialogTitle>
            <DialogContent>
                {/* Add any content you want in the popup */}
                <p>Give user authority to this pipeline.</p>
            </DialogContent>
            <FormControl sx={{flex: 0, bgcolor: 'white' }}>
                <Autocomplete 
                    disablePortal

                    options={Users}
                    renderInput={(params) => (
                        <TextField {...params} label="Select User" variant="outlined" />
                    )}
                />
            </FormControl>
            <DialogActions>

                <Box sx={{width: '100%', justifyContent: 'space-between' }} />
                <Button onClick={onClose} color="primary">
                    Add
                </Button>

                <Button onClick={onClose} color="primary">
                    Close
                </Button>
                <Box/>
            </DialogActions>
        </Dialog>
    );
}

export default function PipelineManageSearch({ setSelectedPipeline }) {
    const [pipelineOptions, setPipelineOptions] = useState<{ pipelineId: string }[]>([]); // Changed variable name
    const [openPopup, setOpenPopup] = useState(false);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchPipelineUsers();
                // Extract unique pipelines from the data
                const uniquePipelines = Array.from(new Set(data.map(item => item.pipelineId)))
                    .map(pipelineId => ({ pipelineId: pipelineId as string }));
                setPipelineOptions(uniquePipelines);
            } catch (error) {
                console.error("Error fetching pipeline users:", error);
            }
        };
        fetchData();
    }, []);

    const handleOpenPopup = () => {
        setOpenPopup(true);
    };

    const handleClosePopup = () => {
        setOpenPopup(false);
    };

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
                    onChange={(event, newValue) => {
                        setSelectedPipeline(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Select Pipeline" variant="outlined" />
                    )}
                />
            </FormControl>

            <Button
                variant="contained"
                color="primary"
                sx={{ width: '10%' }}
                onClick={handleOpenPopup}
            >
                Add user
            </Button>

            <ManagePipelinePopup open={openPopup} onClose={handleClosePopup} />
        </Box>
    );
}
