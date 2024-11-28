import React, { useState, useEffect } from 'react';
import { useSelector } from "react-redux";
import {
    getRepositories,
    getPipelines,
    getResources,
    getOrganizations
} from "../../state_management/selectors/apiSelector.ts";
import { Repository, Resource, Organization } from '../../state_management/states/apiState.ts';
import { PipelineData } from '../../state_management/states/pipelineState.ts';
import {Box, Autocomplete, FormControl, TextField} from "@mui/material";

type ChosenItem = Repository | PipelineData | Resource | Organization;

interface ManageSearchProps {
    setSelectedItem: (item: { item: ChosenItem } | null) => void;
    manageType: 'pipeline' | 'resource' | 'repository' | 'organization';
}

export default function ManageSearch({ setSelectedItem, manageType }: ManageSearchProps) {
    const [options, setOptions] = useState<{ item: ChosenItem }[]>([]);

    const repositories: Repository[] = useSelector(getRepositories);
    const pipelines: PipelineData[] = useSelector(getPipelines);
    const resources: Resource[] = useSelector(getResources);
    const organizations: Organization[] = useSelector(getOrganizations);


    useEffect(() => {
        const fetchData = async () => {
            try {
                let uniqueOptions: { item: ChosenItem }[] = [];

                switch (manageType) {
                    case 'pipeline':
                        uniqueOptions = Array.from(
                            new Set(pipelines.map((item) => item))
                        ).map((pipeline) => ({ item: pipeline }));
                        break;

                    case 'resource':
                        uniqueOptions = Array.from(
                            new Set(resources.map((item) => item))
                        ).map((resource) => ({ item: resource }));
                        break;

                    case 'repository':
                        uniqueOptions = Array.from(
                            new Set(repositories.map((item) => item))
                        ).map((repository) => ({ item: repository }));
                        break;

                    case 'organization':
                        uniqueOptions = Array.from(
                            new Set(organizations.map((item) => item))
                        ).map((organization) => ({ item: organization }));
                        break;

                    default:
                        console.error("Invalid manage type:", manageType);
                        break;
                }

                setOptions(uniqueOptions);
            } catch (error) {
                console.error(`Error processing ${manageType}:`, error);
                setOptions([]);
            }
        };

        fetchData();
    }, [manageType, pipelines, resources, repositories, organizations]);
    
    
    
    return (
        <Box
            data-qa = "manage-searchField"
            sx={{
                width: '80%',
                display: 'flex',
                
            }}
        >
            <FormControl sx={{ flex: 1, bgcolor: 'white' }}>
                <Autocomplete
                    disablePortal
                    options={options}
                    getOptionLabel={(option) => `${manageType}: ${option.item?.name || 'Unnamed'}`}
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
