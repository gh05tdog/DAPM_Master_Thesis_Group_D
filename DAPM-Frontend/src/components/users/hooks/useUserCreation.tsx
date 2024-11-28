import { SelectChangeEvent } from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { createUser, getRolesFromKeycloak, getUser, updateUserRoles } from "../../../utils/keycloakAdminAPI.ts";
import { RoleRepresentation, UserRepresentation } from "../../../utils/types/keycloakTypes.ts";

interface UserCreationForm {
    user: UserRepresentation;
    roles: RoleRepresentation[];
}

const initialFormState: UserCreationForm = {
    user: {
        username: "",
        firstName: "",
        lastName: "",
        enabled: true,
        email: "",
        credentials: [{ temporary: true, type: "password", value: "" }],
    },
    roles: [],
};

const useUserCreation = (onClose: () => void) => {
    const [roles, setRoles] = useState<RoleRepresentation[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [formState, setFormState] = useState<UserCreationForm>(initialFormState);
    const [displayRoles, setDisplayRoles] = useState<string[]>([]);

    const handleRoleStringChange = (event: SelectChangeEvent<(string | undefined)[]>) => {
        const { target: { value } } = event;

        const selectedRoles = (value as string[]).map(roleId => roles.find(r => r.id === roleId) as RoleRepresentation);

        setFormState(prevState => ({
            ...prevState,
            roles: selectedRoles,
        }));

        setDisplayRoles(
            (value as string[]).reduce<string[]>((acc, roleId) => {
                const role = roles.find(r => r.id === roleId);
                if (role?.name) {
                    acc.push(role.name);
                }
                return acc;
            }, [])
        );
    };

    const getUserRoles = async () => {
        try {
            setIsLoading(true);
            const fetchedRoles = await getRolesFromKeycloak();
            setRoles(fetchedRoles);
        } catch (error) {
            console.error("Error fetching roles:", error);
        } finally {
            setIsLoading(false);
        }
    };

    const sleep = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));
    const handleCreateUser = useCallback(async () => {
        try {
            if (!formState.user.username) return alert("Please enter a username");
            if (!formState.roles.length) return alert("Please select at least one role");
            setIsLoading(true);
            await createUser(formState.user);
            const user = await getUser(formState.user.username);
            setFormState({ ...initialFormState });
            // Just for feel  
            await sleep(500);
            await updateUserRoles(user.id as string, formState.roles);
            onClose();
            alert("User created successfully");
        } catch (error) {
            console.error("Error creating user:", error);
            alert("Failed to create user");
        } finally {
            setIsLoading(false);
        }
    }, [formState.roles, formState.user, onClose]);

    useEffect(() => {
        getUserRoles();
    }, []);

    return {
        handleRoleStringChange,
        formState,
        roles,
        handleCreateUser,
        setFormState,
        displayRoles,
        isLoading,
    };
};

export default useUserCreation;
