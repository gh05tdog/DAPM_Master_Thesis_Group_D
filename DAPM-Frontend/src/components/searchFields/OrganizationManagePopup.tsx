import React, { useState, useEffect } from 'react';
import {
    Autocomplete,
    TextField,
    FormControl,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Box,
} from '@mui/material';
import { addUserOrganization } from '../../../src/services/backendAPI.tsx';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';

interface UserOption {
    label: string;
    id: string;
}

interface ManageOrganizationPopupProps {
    open: boolean;
    onClose: () => void;
    selectedOrganization: { organizationId: string } | null;
}

function ManageOrganizationPopup({ open, onClose, selectedOrganization }: ManageOrganizationPopupProps) {
    const [users, setUsers] = useState<UserOption[]>([]);
    const [selectedUser, setSelectedUser] = useState<UserOption | null>(null);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const allUsers = await getUsersFromKeycloak();
                const userOptions = allUsers.map((user: { firstName: any; lastName: any; id: any; }) => ({
                    label: `${user.firstName} ${user.lastName}`,
                    id: user.id,
                }));
                setUsers(userOptions);
            } catch (error) {
                console.error("Error fetching users:", error);
            }
        };
        fetchUsers();
    }, []);

    async function addUser() {
        if (selectedUser && selectedOrganization) {
            try {
                await addUserOrganization(selectedUser.id, selectedOrganization.organizationId);
                alert('User added successfully! Reload page to see results');

                onClose();
            } catch (error) {
                console.error('Error adding user to organization:', error);
                alert('Failed to add user to organization.');
            }
        } else {
            if (!selectedUser) {
                alert('Please select a user.');
            } else if (!selectedOrganization) {
                alert('Please select a organization.');
                onClose();
            }

        }
    }
    return (
        <Dialog data-qa="add-user-popup"
                open={open} onClose={onClose}>
            <DialogTitle>Manage Organization</DialogTitle>
            <DialogContent>
                <p>Give user authority to this organization.</p>
                <FormControl sx={{ width: '100%', bgcolor: 'white' }}>
                    <Autocomplete
                        disablePortal = {false}
                        options={users}
                        value={selectedUser}
                        onChange={(event, newValue) => {
                            setSelectedUser(newValue);
                        }}
                        renderInput={(params) => (
                            <TextField {...params} label="Select User" variant="outlined" />
                        )}
                    />
                </FormControl>
            </DialogContent>
            <DialogActions>
                <Button onClick={addUser} color="primary">
                    Add
                </Button>
                <Button onClick={onClose} color="primary">
                    Close
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default ManageOrganizationPopup;
