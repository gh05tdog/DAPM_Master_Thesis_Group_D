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
import { fetchOrganizationUsers, addUserOrganization } from '../../../src/services/backendAPI.tsx';



interface OrganizationManageSearchProps {
    setSelectedOrganization: (organization : { organizationId: string } | null) => void;
}

export default function OrganizationManageSearch({ setSelectedOrganization }: OrganizationManageSearchProps) {
    const [organizationOptions, setOrganizationOptions] = useState<{ organizationId: string }[]>([]);

    // Fetch data using useEffect
    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await fetchOrganizationUsers();
                const uniqueOrganizations = Array.from(new Set(data.map((item: { organizationId: any; }) => item.organizationId)))
                    .map(organizationId => ({ organizationId: organizationId as string }));
                setOrganizationOptions(uniqueOrganizations);
            } catch (error) {
                console.error("Error fetching organization users:", error);
            }
        };
        fetchData();
    }, []);


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
