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
import { addUserPipeline } from '../../../src/services/backendAPI.tsx';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';

interface UserOption {
    label: string;
    id: string;
}

interface ManagePipelinePopupProps {
    open: boolean;
    onClose: () => void;
    selectedID: { ID: string } | null;
}

function ManagePipelinePopup({ open, onClose, selectedID }: ManagePipelinePopupProps) {
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
        if (selectedUser && selectedID) {
            try {
                
                
                await addUserPipeline(selectedUser.id, selectedPipeline.pipelineId);
                alert('User added successfully! Reload page to see results');

                onClose();
            } catch (error) {
                console.error('Error adding user to pipeline:', error);
                alert('Failed to add user to pipeline.');
            }
        } else {
            if (!selectedUser) {
                alert('Please select a user.');
            } else if (!selectedPipeline) {
                alert('Please select a pipeline.');
                onClose();
            }

        }
    }
    return (
        <Dialog data-qa="add-user-popup"
                open={open} onClose={onClose}>
            <DialogTitle>Manage Pipeline</DialogTitle>
            <DialogContent>
                <p>Give user authority to this pipeline.</p>
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

export default ManagePipelinePopup;
