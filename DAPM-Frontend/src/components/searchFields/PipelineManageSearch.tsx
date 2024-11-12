import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
    Button,
    MenuItem,
    SelectChangeEvent,
    Select,
    InputLabel,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
} from '@mui/material';
import { fetchPipelineUsers } from '../../../src/services/backendAPI.tsx';

const authority = [
    {
        value: '1',
        label: 'Observe',
    },
    {
        value: '2',
        label: 'Run',
    },
    {
        value: '3',
        label: 'Edit',
    },
    {
        value: '4',
        label: 'Manage',
    },
]

const Users = [
    { label: 'Olivia Rhye' },
    { label: 'Ciaran Murray' },
    { label: 'Marina Macdonald' },
    { label: 'Charles Fulton' },
    { label: 'Jay Hoper' },
    { label: 'Steve Hampton' },
    { label: 'Liam Peterson' },
    { label: 'Ava Martinez' },
    { label: 'Mia Robinson' },
    { label: 'Sophia Johnson' },
    { label: 'James Brown' },
    { label: 'Emily Davis' },
];



// Popup Component
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

            <FormControl sx={{bgcolor: 'white', mt: 2}}>
                <InputLabel>Select Authority</InputLabel>
                <Select
                    variant="filled"
                    label="Select Authority"
                    displayEmpty
                    sx={{
                        textAlign: 'left',
                        justifyContent: 'flex-start',
                    }}
                >
                    <MenuItem value="" disabled>
                        Select Authority
                    </MenuItem>
                    <MenuItem value={1}>Observe</MenuItem>
                    <MenuItem value={2}>Run</MenuItem>
                    <MenuItem value={3}>Edit</MenuItem>
                    <MenuItem value={4}>Manage</MenuItem>
                </Select>
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


  export default function PipelineManageSearch() {
    const [authority, setAuthority] = useState('');
    const [pipeline, setPipeline] = useState<{ pipelineId: string }[]>([]);
    const [openPopup, setOpenPopup] = useState(false);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchPipelineUsers();
                setPipeline(data);
            } catch (error) {
                console.error("Error fetching pipeline users:", error);
            }
        };
        fetchData();
    }, []);

    const handleAuthorityChange = (event: SelectChangeEvent) => {
        setAuthority(event.target.value);
    };

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
                    options={pipeline}
                    getOptionLabel={(option) => `PipelineId: ${option.pipelineId}`}

                    renderInput={(params) => (
                        <TextField {...params} label="Select Pipeline" variant="outlined" />
                    )}
                />
            </FormControl>

            <FormControl sx={{ width: '20%', bgcolor: 'white' }}>
                <InputLabel>Select Authority</InputLabel>
                <Select
                    value={authority}
                    onChange={handleAuthorityChange}
                    variant="filled"
                    label="Select Authority"
                >
                    <MenuItem value={1}>Observe</MenuItem>
                    <MenuItem value={2}>Run</MenuItem>
                    <MenuItem value={3}>Edit</MenuItem>
                    <MenuItem value={4}>Manage</MenuItem>
                </Select>
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