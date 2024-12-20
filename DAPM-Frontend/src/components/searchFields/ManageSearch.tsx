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
    const [selectedOption, setSelectedOption] = useState<{ item: ChosenItem } | null>(null);

    const repositories: Repository[] = useSelector(getRepositories) || [];
    const pipelines: PipelineData[] = useSelector(getPipelines) || [];
    const resources: Resource[] = useSelector(getResources) || [];
    const organizations: Organization[] = useSelector(getOrganizations) || [];
    

    useEffect(() => {
                let uniqueOptions: { item: ChosenItem }[] = [];

                const removeDuplicates = <T extends { id?: string | number }>(items: T[]) => {
                    const seen = new Map<string | number, T>();
                    items.forEach((item) => {
                        const key = item.id ?? JSON.stringify(item);
                        if (!seen.has(key)) {
                            seen.set(key, item);
                        }
                    });
                    return Array.from(seen.values());
                };

                switch (manageType) {
                    case 'pipeline':
                        uniqueOptions = removeDuplicates(pipelines).map((pipeline) => ({ item: pipeline }));
                        break;

                    case 'resource':
                        uniqueOptions = removeDuplicates(resources).map((resource) => ({ item: resource }));
                        break;

                    case 'repository':
                        uniqueOptions = removeDuplicates(repositories).map((repository) => ({ item: repository }));
                        break;

                    case 'organization':
                        uniqueOptions = removeDuplicates(organizations).map((organization) => ({ item: organization }));
                        break;

                    default:
                        console.error("Invalid manage type:", manageType);
                        break;
                }

                setOptions(uniqueOptions);
                setSelectedOption(null);
                setSelectedItem(null);
         
    }, [manageType, pipelines, resources, repositories, organizations]);




    const handleSelection = (_event: any, newValue: { item: ChosenItem } | null) => {
        setSelectedOption(newValue);
        setSelectedItem(newValue);
    };

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
                    value={selectedOption}
                    options={options}
                    getOptionLabel={(option) => `${manageType}: ${option.item?.name || 'Unnamed'}`}
                    onChange={handleSelection}
                    renderInput={(params) => (
                        <TextField {...params} label={`Select ${manageType}`} variant="outlined" />
                    )}
                />
            </FormControl>
        </Box>
    );
}
