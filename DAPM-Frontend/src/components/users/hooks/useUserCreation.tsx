import { SelectChangeEvent } from "@mui/material";
import { useReducer, useCallback, useEffect, useState } from "react";
import { createUser, getRolesFromKeycloak } from "../../../utils/keycloakAdminAPI.ts";
import { RoleRepresentation } from "../../../utils/types/keycloakTypes.ts";

interface FormState {
    username?: string;
    firstName?: string;
    lasName?: string;
    roleIds?: string[];
}

type Action =
    | { type: 'UPDATE'; key: string; value: any }
    | { type: 'RESET'; payload: FormState };

const formReducer = (state: FormState, action: Action): FormState => {
    switch (action.type) {
        case 'UPDATE':
            return {
                ...state,
                [action.key]: action.value,
            };
        case 'RESET':
            return {
                ...action.payload,
            };
        default:
            throw new Error(`Unhandled action type: ${action}`);
    }
};

const useUserCreation = () => {
    const [roles, setRoles] = useState<RoleRepresentation[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const initialFormState: FormState = {
        username: "",
        firstName: "",
        lasName: "",
        roleIds: [],
    };
    const [formState, dispatch] = useReducer(formReducer, initialFormState);
    const [displayRoles, setDisplayRoles] = useState<string[]>([]);
    const handleRoleStringChange = (event: SelectChangeEvent<string[]>) => {
        const { target: { value } } = event;

        dispatch({ type: 'UPDATE', key: 'roleIds', value });
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
            const roles = await getRolesFromKeycloak();
            setRoles(roles);
        } catch (error) {
            console.error('Error fetching roles:', error);
        } finally {
            setIsLoading(false);
        };
    }

    const handleCreateUser = useCallback(async () => {
        try {
            setIsLoading(true);
            createUser({})
        } catch (error) {
            console.error('Error creating user:', error);
        } finally {
            setIsLoading(false);
        }
    }, []);

    useEffect(() => {
        getUserRoles();
    }, []);

    return { handleRoleStringChange, formState, roles, handleCreateUser, dispatch, displayRoles, isLoading };
};

export default useUserCreation;
