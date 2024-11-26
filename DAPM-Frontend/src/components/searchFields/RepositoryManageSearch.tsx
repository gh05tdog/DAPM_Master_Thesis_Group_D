import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,

} from '@mui/material';
import { fetchRepositoryUsers} from '../../../src/services/backendAPI.tsx';



interface RepositoryManageSearchProps {
    setSelectedRepository: (repository: { repositoryId: string } | null) => void;
}

export default function RepositoryManageSearch({ setSelectedRepository }: RepositoryManageSearchProps) {
    const [repositoryOptions, setRepositoryOptions] = useState<{ repositoryId: string }[]>([]);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchRepositoryUsers();
                
                const uniqueRepositories = Array.from(new Set(data.map((item: { repositoryId: any; }) => item.repositoryId)))
                    .map(repositoryId => ({ repositoryId: repositoryId as string }));
                setRepositoryOptions(uniqueRepositories);
            } catch (error) {
                console.error("Error fetching repository users:", error);
            }
        };
        fetchData();
    }, []);


    return (
        <Box
            data-qa = "repository-searchField"
            sx={{
                width: '80%',
                display: 'flex'

            }}
        >
            <FormControl sx={{ flex: 1, bgcolor: 'white' }}>
                <Autocomplete
                    disablePortal
                    options={repositoryOptions}
                    getOptionLabel={(option) => `RepositoryId: ${option.repositoryId}`}
                    onChange={(_event, newValue) => {
                        setSelectedRepository(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Select Repository" variant="outlined" />
                    )}
                />
            </FormControl>


        </Box>
    );
}
