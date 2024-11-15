import React, { useState, useEffect } from 'react';
import {
    Box,
    Table,
    TableHead,
    TableBody,
    TableCell,
    TableContainer,
    TableRow,
    Paper,
    Button,
    Typography,
} from '@mui/material';
import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';
import { fetchOrganizationUsers, removeUserOrganization } from '../../../src/services/backendAPI.tsx';

const ITEMS_PER_PAGE = 10;

interface User {
    id: string;
    firstName: string;
    lastName: string;
    username: string;
}

interface Organization {
    organizationId: string;
}

interface OrganizationManageTableProps {
    selectedOrganization: Organization | null;
}

export default function OrganizationManageTable({ selectedOrganization }: OrganizationManageTableProps) {
    const [users, setUsers] = useState<User[]>([]);
    const [page, setPage] = useState(1);

    useEffect(() => {
        const fetchUsersData = async () => {
            try {
                if (selectedOrganization) {
                    const allUsers = await getUsersFromKeycloak();
                    const organizationUsers = await fetchOrganizationUsers();


                    const filteredOrganizationUsers = organizationUsers.filter(
                        (pu: { organizationId: string; }) => pu.organizationId === selectedOrganization.organizationId
                    );


                    const usersForOrganization = allUsers.filter((user: { id: any; }) =>
                        filteredOrganizationUsers.some((pu: { userId: any; }) => pu.userId === user.id)
                    );
                    setUsers(usersForOrganization);
                    setPage(1);
                } else {
                    setUsers([]);
                }
            } catch (error) {
                console.error('Failed to fetch users:', error);
            }
        };
        fetchUsersData();
    }, [selectedOrganization]);


    const totalPages = Math.ceil(users.length / ITEMS_PER_PAGE);
    const handleNextPage = () => {
        setPage((prevPage) => Math.min(prevPage + 1, totalPages));
    };
    const handlePrevPage = () => {
        setPage((prevPage) => Math.max(prevPage - 1, 1));
    };

    const startIndex = (page - 1) * ITEMS_PER_PAGE;
    const currentUsers = users.slice(startIndex, startIndex + ITEMS_PER_PAGE);

    function handleRemove(id: string): void {
        if (selectedOrganization) {
            removeUserOrganization(id, selectedOrganization.organizationId);

            const newUsers = users.filter((user) => user.id !== id);
            setUsers(newUsers);
        }
    }

    return (
        <Box data-qa = "organizationManager-user-table"
             sx={{ width: '100%', margin: 'auto', mt: 4 }}>
            {selectedOrganization ? (
                <>
                    <TableContainer component={Paper}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>ID</TableCell>
                                    <TableCell>First Name</TableCell>
                                    <TableCell>Last Name</TableCell>
                                    <TableCell>Username</TableCell>
                                    <TableCell align="right">Actions</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {currentUsers.map((user) => (
                                    <TableRow key={user.id}>
                                        <TableCell>{user.id}</TableCell>
                                        <TableCell>{user.firstName}</TableCell>
                                        <TableCell>{user.lastName}</TableCell>
                                        <TableCell>{user.username}</TableCell>
                                        <TableCell align="right">
                                            <Button
                                                variant="outlined"
                                                color="error"
                                                onClick={() => handleRemove(user.id)}
                                            >
                                                Remove
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                                {currentUsers.length === 0 && (
                                    <TableRow>
                                        <TableCell colSpan={5} align="center">
                                            No users found for the selected organization.
                                        </TableCell>
                                    </TableRow>
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>

                    <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 2 }}>
                        <Button
                            variant="contained"
                            color="primary"
                            disabled={page === 1}
                            onClick={handlePrevPage}
                        >
                            Previous Page
                        </Button>
                        <Typography variant="body1">
                            Page {page} of {totalPages || 1}
                        </Typography>
                        <Button
                            variant="contained"
                            color="primary"
                            disabled={page === totalPages || totalPages === 0}
                            onClick={handleNextPage}
                        >
                            Next Page
                        </Button>
                    </Box>
                </>
            ) : (
                <Typography variant="h6" align="center">
                    Please select a organization to view its users.
                </Typography>
            )}
        </Box>
    );
}
