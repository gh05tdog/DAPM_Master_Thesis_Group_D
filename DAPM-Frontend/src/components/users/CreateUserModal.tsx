import { Box, Button, Checkbox, Input, ListItemText, MenuItem, Modal, OutlinedInput, Select, SelectChangeEvent } from "@mui/material";
import { FC, useCallback, useEffect, useState } from "react";
import { getRolesFromKeycloak } from "../../utils/keycloakAdminAPI.ts";
import { RoleRepresentation } from "../../utils/types/keycloakTypes.ts";
import useUserCreation from "./hooks/useUserCreation.tsx";
import { borderColor, borderRadius, padding } from "@mui/system";
import Header from "../headers/Header.tsx";
import { CheckBox } from "@mui/icons-material";

interface Props {
    isOpen: boolean;
    onClose: () => void;
}

const CreateUserModal: FC<Props> = ({ isOpen, onClose }) => {

    const { formState, displayRoles, handleRoleStringChange, handleCreateUser, roles } = useUserCreation();

    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 400,
        bgcolor: 'background.paper',
        border: '3px solid',
        borderRadius: 8,
        borderColor: 'primary.main',
        boxShadow: 24,
        p: 4,
        gap: 2,
        display: "flex",
        flexDirection: "column",
    };



    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        PaperProps: {
            style: {
                maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
                width: 250,
            },
        },
    };

    return (
        <Modal open={isOpen} onClose={onClose} >
            <form onSubmit={handleCreateUser}>
                <Box sx={style}>
                    <h1>Create User</h1>
                    <Input placeholder="Username" />
                    <Input placeholder="First Name" />
                    <Input placeholder="Last Name" />
                    <h4 style={{ margin: 0, padding: 0, fontWeight: 300 }}>Roles</h4>
                    <Select
                        multiple
                        value={formState.roleIds}
                        onChange={handleRoleStringChange}
                        input={<OutlinedInput sx={{
                            colors: {
                                placeholder: 'red',
                            },
                        }} placeholder="Roles" />}
                        renderValue={() => displayRoles.join(', ')}
                        MenuProps={MenuProps}
                    >
                        {roles.map((role) => (
                            <MenuItem key={role.id} value={role.id}>
                                <Checkbox checked={role.name ? displayRoles.includes(role.name) : false} />
                                <ListItemText primary={role.name} />
                            </MenuItem>
                        ))}
                    </Select>
                    <Box sx={{ display: "flex", height: "3em", flexDirection: "row", justifyContent: "space-between", width: "100%" }}>
                        <Button sx={{ bgcolor: "primary.main", color: "white", width: "10em" }} type="submit">Create</Button>
                        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginRight: "1em" }}>
                            <CheckBox />
                            <p>Enabled</p>
                        </Box>
                    </Box>
                </Box>
            </form>
        </Modal >
    )
}

export default CreateUserModal;