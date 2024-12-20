﻿import {Button} from "@mui/material";
import React from "react";
import {
    removeUserOrganization,
    removeUserPipeline,
    removeUserRepository,
    removeUserResource
} from '../../../src/services/backendAPI.tsx';

interface User {
    id: string;
    firstName: string;
    lastName: string;
    username: string;
}

interface RemoveUserButtonPageProps {
    user: User;
    users: User[];
    manageType: string;
    selectedID: string
    setUsers: (users: User[]) => void;
}



export default function RemoveUserButton({user, users, manageType, selectedID, setUsers}: RemoveUserButtonPageProps) {

    function handleRemove(id: string): void {
        if (selectedID) {
            if (manageType === "pipeline") {
                removeUserPipeline(id, selectedID);
            }
            else if (manageType === "resource") {
                removeUserResource(id, selectedID);
            }
            else if (manageType === "repository") {
                removeUserRepository(id, selectedID);
            }
            else if (manageType === "organization") {
                removeUserOrganization(id, selectedID);
            }

            const newUsers = users.filter((user) => user.id !== id);
            setUsers(newUsers);
        }
    }
    
    return (
        <Button
            variant="outlined"
            color="error"
            onClick={() => handleRemove(user.id)}
        >
            Remove
        </Button>
    );
}