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
import { addUserPipeline, addUserRepository, addUserOrganization, addUserResource } from '../../../src/services/backendAPI.tsx';
import { getUsersFromKeycloak } from '../../utils/keycloakAdminAPI.ts';

interface UserOption {
    label: string;
    id: string;
}

type AddUserFunction = (userId: string, id: string) => Promise<void>;

interface ManagePipelinePopupProps {
    open: boolean;
    onClose: () => void;
    selectedID: string;
    manageType: string;
}

function ManagePopup({ open, onClose, selectedID, manageType }: ManagePipelinePopupProps) {
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
                
                const addUserFunctions: Record<string, AddUserFunction> = {
                    pipeline: addUserPipeline,
                    resource: addUserResource,
                    repository: addUserRepository,
                    organization: addUserOrganization,
                };

                await addUserFunctions[manageType](selectedUser.id, selectedID);

                alert(`User added successfully to ${manageType}! Reload page to see results.`);
                onClose();
            } catch (error) {
                alert(`Failed to add user to ${manageType}.`);
            }
        } else {
            if (!selectedUser) {
                alert('Please select a user.');
            } else if (!selectedID) {
                alert(`Please select a ${manageType}.`);
            }
            onClose();
        }
    }
    return (
        <Dialog data-qa="add-user-popup"
                open={open} onClose={onClose}>
            <DialogTitle>Manage {manageType}</DialogTitle>
            <DialogContent>
                <p>Give user authority to this {manageType}.</p>
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

export default ManagePopup;
