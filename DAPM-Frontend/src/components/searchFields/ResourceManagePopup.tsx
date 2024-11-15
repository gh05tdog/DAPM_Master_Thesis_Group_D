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
import { addUserResource } from '../../../src/services/backendAPI.tsx';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';

interface UserOption {
    label: string;
    id: string;
}

interface ManageResourcePopupProps {
    open: boolean;
    onClose: () => void;
    selectedResource: { resourceId: string } | null;
}

function ManageResourcePopup({ open, onClose, selectedResource }: ManageResourcePopupProps) {
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
        if (selectedUser && selectedResource) {
            try {
                await addUserResource(selectedUser.id, selectedResource.resourceId);
                alert('User added successfully! Reload page to see results');

                onClose();
            } catch (error) {
                console.error('Error adding user to resource:', error);
                alert('Failed to add user to resource.');
            }
        } else {
            if (!selectedUser) {
                alert('Please select a user.');
            } else if (!selectedResource) {
                alert('Please select a resource.');
                onClose();
            }

        }
    }
    return (
        <Dialog data-qa="add-user-popup"
                open={open} onClose={onClose}>
            <DialogTitle>Manage Resource</DialogTitle>
            <DialogContent>
                <p>Give user authority to this resource.</p>
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

export default ManageResourcePopup;
