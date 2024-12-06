import React, { useState, useEffect } from 'react';
import {  useSelector } from "react-redux";
import { getOrganizations} from "../../state_management/selectors/apiSelector.ts";
import {
    Autocomplete,
    TextField,
    Box,
    FormControl,
} from '@mui/material';
import { fetchOrganizationUsers } from '../../../src/services/backendAPI.tsx';



interface OrganizationManageSearchProps {
    setSelectedOrganization: (organization : { organizationId: string } | null) => void;
}

export default function OrganizationManageSearch({ setSelectedOrganization }: OrganizationManageSearchProps) {
    const [organizationOptions, setOrganizationOptions] = useState<{ organizationId: string }[]>([]);
    const organizations = useSelector(getOrganizations); // Adjust state path as needed
    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchOrganizationUsers();

                const uniqueOrganizations = Array.from(new Set(organizations.map((item) => item.id)))
                    .map(organizationId => ({ organizationId: organizationId as string }));
                setOrganizationOptions(uniqueOrganizations);
            } catch (error) {
                console.error("Error fetching organization users:", error);
            }
        };
        fetchData();
    }, [organizations]);


    return (
        <Box
            data-qa = "organization-searchField"
            sx={{
                width: '80%',
                display: 'flex'

            }}
        >
            <FormControl sx={{ flex: 1, bgcolor: 'white' }}>
                <Autocomplete
                    disablePortal
                    options={organizationOptions}
                    getOptionLabel={(option) => `OrganizationId: ${option.organizationId}`}
                    onChange={(_event, newValue) => {
                        setSelectedOrganization(newValue);
                    }}
                    renderInput={(params) => (
                        <TextField {...params} label="Select Organization" variant="outlined" />
                    )}
                />
            </FormControl>


        </Box>
    );
}
