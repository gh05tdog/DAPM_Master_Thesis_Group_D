import React, { useState, useEffect } from 'react';
import { Box,
         Table,
         TableHead,
         TableBody,
         TableCell,
         TableContainer,
         TableRow,
         Paper,
         Button,
         Typography 
       } from '@mui/material';

import { getUsersFromKeycloak } from '../../utils/keycloakAdminAPI.ts';
import { fetchPipelineUsers, 
         fetchResourceUsers,
         fetchRepositoryUsers,
         fetchOrganizationUsers, 
         } from '../../../src/services/backendAPI.tsx';
import NextPageButton from '../buttons/NextPageButton.tsx';
import PreviousPageButton from '../buttons/PreviousPageButton.tsx';
import RemoveUserButton from '../buttons/removeUserButton.tsx';

const ITEMS_PER_PAGE = 10;

interface User {
    id: string;
    firstName: string;
    lastName: string;
    username: string;
}

interface ManagerListProps {
    selectedID: string | null;
    value: string;
}

export default function ManagerList({ selectedID, value }: ManagerListProps) {
    const [users, setUsers] = useState<User[]>([]);
    const [page, setPage]   = useState(1);

    useEffect(() => {
        const fetchUsersData = async () => {
            try {
                if (!selectedID) {
                    setUsers([]);
                    return;
                }

                const allUsers = await getUsersFromKeycloak();
                let fetchedUsers: { userId: string }[] = [];

                switch (value) {
                    case 'pipeline':
                        fetchedUsers = await fetchPipelineUsers();
                        break;
                    case 'resource':
                        fetchedUsers = await fetchResourceUsers();
                        break;
                    case 'repository':
                        fetchedUsers = await fetchRepositoryUsers();
                        break;
                    case 'organization':
                        fetchedUsers = await fetchOrganizationUsers();
                        break;
                    default:
                        console.error(`Unsupported value type: ${value}`);
                        setUsers([]);
                        return;
                }

                const filteredUsers = fetchedUsers.filter((pu) => {
                    const idField = {
                        pipeline: 'pipelineId',
                        resource: 'resourceId',
                        repository: 'repositoryId',
                        organization: 'organizationId'
                    }[value];

                    return pu[idField as keyof typeof pu] === selectedID;
                });

                const usersForList = allUsers.filter((user: { id: string }) =>
                    filteredUsers.some((fu) => fu.userId === user.id)
                );

                setUsers(usersForList);
                setPage(1);
            } catch (error) {
                console.error('Error fetching users:', error);
                setUsers([]);
            }
        };

        fetchUsersData();
    }, [selectedID, value]);


    const totalPages = Math.ceil(users.length / ITEMS_PER_PAGE);
    const startIndex = (page - 1) * ITEMS_PER_PAGE;
    const currentUsers = users.slice(startIndex, startIndex + ITEMS_PER_PAGE);

    return (
        <Box data-qa = "Manager-table"
             sx={{ width: '100%', margin: 'auto', mt: 4 }}>
            {selectedID? (
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
                                            
                                            <RemoveUserButton user={user} users={users} manageType={value} selectedID={selectedID} setUsers={setUsers} />
                                            
                                        </TableCell>
                                    </TableRow>
                                ))}
                                {currentUsers.length === 0 && (
                                    <TableRow>
                                        <TableCell colSpan={5} align="center">
                                            No users found for the selected entry.
                                        </TableCell>
                                    </TableRow>
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>

                    <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 2 }}>
                        <PreviousPageButton currentPage={page} setPage={setPage} totalPages={totalPages} />
                        <Typography variant="body1">
                            Page {page} of {totalPages || 1}
                        </Typography>
                        <NextPageButton currentPage={page} setPage={setPage} totalPages={totalPages} />
                    </Box>
                </>
            ) : (
                <Typography variant="h6" align="center">
                    Please select an available {value} to view its users.
                </Typography>
            )}
        </Box>
    );
}