import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
} from '@mui/material';
import { fetchResourceUsers} from '../../../src/services/backendAPI.tsx';



interface ResourceManageSearchProps {
    setSelectedResource: (resource: { resourceId: string } | null) => void;
}

export default function ResourceManageSearch({ setSelectedResource }: ResourceManageSearchProps) {
    const [resourceOptions, setResourceOptions] = useState<{ resourceId: string }[]>([]);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchResourceUsers();
                const uniqueResources = Array.from(new Set(data.map((item: { resourceId: any; }) => item.resourceId)))
                    .map(resourceId => ({ resourceId: resourceId as string }));
                setResourceOptions(uniqueResources);
            } catch (error) {
                console.error("Error fetching resource users:", error);
            }
        };
        fetchData();
    }, []);


    return (
        <Box
            data-qa = "resource-searchField"
            sx={{
                width: '80%',
                display: 'flex'
                
            }}
        >
            <FormControl sx={{ flex: 1, bgcolor: 'white' }}>
                <Autocomplete
                    disablePortal
                    options={resourceOptions}
                    getOptionLabel={(option) => `ResourceId: ${option.resourceId}`}
                    onChange={(_event, newValue) => {
                        setSelectedResource(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Select Resource" variant="outlined" />
                    )}
                />
            </FormControl>

            
        </Box>
    );
}
