import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
} from '@mui/material';
import { fetchPipelineUsers, fetchRepositoryUsers, fetchOrganizationUsers, fetchResourceUsers} from '../../../src/services/backendAPI.tsx';



interface ManageSearchProps {
    setSelectedItem: (item: { id: string } | null) => void;
    manageType: string;
}

export default function ManageSearch({setSelectedItem, manageType }: ManageSearchProps) {
    const [options, setOptions] = useState<{ id: string }[]>([]);


    useEffect(() => {
        const fetchData = async () => {
            try {
                let data = [];

                // Fetch data based on manageType
                if (manageType === 'pipeline') {
                    data = await fetchPipelineUsers();
                } else if (manageType === 'resource') {
                    data = await fetchResourceUsers();
                } else if (manageType === 'repository') {
                    data = await fetchRepositoryUsers();
                } else if (manageType === 'organization') {
                    data = await fetchOrganizationUsers();
                } else {
                    console.error('Unknown manage type:', manageType);
                    return; // Exit early if manageType is invalid
                }

                // Process data to get unique options
                const uniqueItems = Array.from(
                    new Set(data.map((item: { id: any }) => item.id)) // Assuming `id` is the key
                ).map(id => ({ id: id as string }));

                // Set options dynamically
                setOptions(uniqueItems);
            } catch (error) {
                console.error(`Error fetching data for ${manageType}:`, error);
            }
        };
        fetchData();
    }, [manageType]); 

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
                    options={options}
                    getOptionLabel={(option) => `${manageType}Id: ${option.Id}`}
                    onChange={(_event, newValue) => {
                        setSelectedItem(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label={`Select ${manageType}`} variant="outlined" />
                    )}
                />
            </FormControl>


        </Box>
    );
}
