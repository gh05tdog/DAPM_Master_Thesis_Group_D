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

import getUsersFromKeycloak from '../../utils/keycloakUsers.ts';
import { fetchPipelineUsers, 
         removeUserPipeline,
     
         fetchResourceUsers,
         removeUserResource,
         
         fetchRepositoryUsers,
         removeUserRepository,
      
         fetchOrganizationUsers, 
         removeUserOrganization,
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

interface IDInterface {
    ID: string;
}

interface ManagerListProps {
    selectedID: IDInterface | null;
    value: string;
}

export default function ManagerList({ selectedID, value }: ManagerListProps) {
    const [users, setUsers] = useState<User[]>([]);
    const [page, setPage]   = useState(1);

    useEffect(() => {
        const fetchUsersData = async () => {
            try {
                if (selectedID)
                    {
                        const allUsers = await getUsersFromKeycloak();

                        let listUsers: { userId: any; }[]; 

                        if        (value === "pipeline") {
                            listUsers = await fetchPipelineUsers();
                        } else if (value === "resource") {
                            listUsers = await fetchResourceUsers();
                        } else if (value === "repository") {
                            listUsers = await fetchRepositoryUsers();
                        } else if (value === "organization") {
                            listUsers = await fetchOrganizationUsers();
                        } else {
                            listUsers = [];
                            console.log("Manager type not found");
                        }
                        
                    const filteredUsers = listUsers.filter(
                        (ru: { userId: string; }) => ru.userId === selectedID.ID
                    );

                    const usersForList = allUsers.filter((user: { id: any; }) =>
                        filteredUsers.some((ru: { userId: any; }) => ru.userId === user.id)
                    );

                    setUsers(usersForList);
                    setPage(1);
                        
                } else {
                    setUsers([]);
                }
            } catch (error) {
                console.error('Failed to fetch users:', error);
            }
        };
        fetchUsersData();
    }, [selectedID]);


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
                    Please select an available {value || "item"} to view its users.
                </Typography>
            )}
        </Box>
    );
}