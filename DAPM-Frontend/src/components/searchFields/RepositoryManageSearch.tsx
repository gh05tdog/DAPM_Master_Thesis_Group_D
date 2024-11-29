import React, { useState, useEffect } from 'react';
import { useSelector } from "react-redux";
import { getRepositories} from "../../state_management/selectors/apiSelector.ts";


import {
    Autocomplete,
    TextField,
    Box,
    FormControl,

} from '@mui/material';
import { Repository } from '../../state_management/states/apiState.ts';


interface RepositoryManageSearchProps {
    setSelectedRepository: (repository: {repository: Repository }| null) => void;
}

export default function RepositoryManageSearch({ setSelectedRepository }: RepositoryManageSearchProps) {
    const [repositoryOptions, setRepositoryOptions] = useState<{ repository: Repository }[]>([]);
    const repositories = useSelector(getRepositories);
    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const uniqueRepositories = Array.from(new Set(repositories.map((item) => item)))
                    .map(repository => ({ repository: repository }));
                setRepositoryOptions(uniqueRepositories);
            } catch (error) {
                console.error("Error fetching repository users:", error);
            }
        };
        fetchData();
    }, [repositories]);


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
                    getOptionLabel={(option) => `Repository name: ${option.repository.name}`}
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
