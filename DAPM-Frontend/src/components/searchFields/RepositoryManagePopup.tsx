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
import { addUserRepository } from '../../../src/services/backendAPI.tsx';
import { getUsersFromKeycloak } from '../../utils/keycloakAdminAPI.ts';

interface UserOption {
    label: string;
    id: string;
}

interface ManageRepositoryPopupProps {
    open: boolean;
    onClose: () => void;
    selectedRepository: { repositoryId: string } | null;
}

function ManageRepositoryPopup({ open, onClose, selectedRepository }: ManageRepositoryPopupProps) {
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
        if (selectedUser && selectedRepository) {
            try {
                await addUserRepository(selectedUser.id, selectedRepository.repositoryId);
                alert('User added successfully! Reload page to see results');

                onClose();
            } catch (error) {
                console.error('Error adding user to repository:', error);
                alert('Failed to add user to repository.');
            }
        } else {
            if (!selectedUser) {
                alert('Please select a user.');
            } else if (!selectedRepository) {
                alert('Please select a repository.');
                onClose();
            }

        }
    }
    return (
        <Dialog data-qa="add-user-popup"
            open={open} onClose={onClose}>
            <DialogTitle>Manage Repository</DialogTitle>
            <DialogContent>
                <p>Give user authority to this repository.</p>
                <FormControl sx={{ width: '100%', bgcolor: 'white' }}>
                    <Autocomplete
                        disablePortal={false}
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

export default ManageRepositoryPopup;
