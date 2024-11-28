import React, { useState, useEffect } from 'react';
import { useSelector } from "react-redux";
import {
    getRepositories,
    getPipelines,
    getResources,
    getOrganizations
} from "../../state_management/selectors/apiSelector.ts";
import { Repository, Pipeline, Resource, Organization } from '../../state_management/states/apiState.ts';
import {Box, Autocomplete, FormControl, TextField} from "@mui/material";

type ChosenItem = Repository | Pipeline | Resource | Organization;

interface ManageSearchProps {
    setSelectedItem: (item: { item: ChosenItem } | null) => void;
    manageType: 'pipeline' | 'resource' | 'repository' | 'organization';
}

export default function ManageSearch({ setSelectedItem, manageType }: ManageSearchProps) {
    const [options, setOptions] = useState<{ item: ChosenItem }[]>([]);

    // Fetch entities from Redux store
    const repositories: Repository[] = useSelector(getRepositories);
    const pipelines: Pipeline[] = useSelector(getPipelines);
    const resources: Resource[] = useSelector(getResources);
    const organizations: Organization[] = useSelector(getOrganizations);


    useEffect(() => {
        const fetchData = async () => {
            try {
                if (manageType === 'repository') {
                    // Deduplicate repositories
                    const uniqueRepositories = Array.from(
                        new Set(repositories.map((item) => item.id)) // Using `id` as the unique key
                    ).map((id) => {
                        const repository = repositories.find((item) => item.id === id);
                        return repository ? { item: repository } : null;
                    }).filter((option): option is { item: Repository } => option !== null); // Remove null values

                    setOptions(uniqueRepositories); // Set deduplicated options
                }
            } catch (error) {
                console.error("Error processing repositories:", error);
                setOptions([]);
            }
        };

        fetchData();
    }, [manageType, repositories]);
    
    
    
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
                    getOptionLabel={(option) => `${manageType}Id: ${option.item?.id || 'Unnamed'}`}
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
