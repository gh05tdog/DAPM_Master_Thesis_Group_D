import { Box, Button, Checkbox, FormControlLabel, Input, ListItemText, MenuItem, Modal, OutlinedInput, Select } from "@mui/material";
import { FC } from "react";
import useUserCreation from "./hooks/useUserCreation.tsx";
import Spinner from "../../cards/SpinnerCard.tsx";

interface Props {
    isOpen: boolean;
    onClose: () => void;
}

const CreateUserModal: FC<Props> = ({ isOpen, onClose }) => {
    const { formState, displayRoles, handleRoleStringChange, handleCreateUser, roles, isLoading, setFormState } = useUserCreation(onClose);

    return (
        <Modal open={isOpen} onClose={onClose}>
            <form onSubmit={e => { e.preventDefault(); handleCreateUser(); }}>
                <Box
                    data-qa="UserCreationModal"
                    sx={{
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
                    }}>
                    <h1>Create User</h1>
                    <Input
                        required
                        value={formState.user.username}
                        onChange={e => setFormState(prev => ({
                            ...prev,
                            user: {
                                ...prev.user,
                                username: e.target.value,
                                credentials: [{ ...prev.user.credentials[0], value: e.target.value }]
                            }
                        }))}
                        placeholder="Username"
                    />
                    <Input
                        value={formState.user.email}
                        onChange={e => setFormState(prev => ({ ...prev, user: { ...prev.user, email: e.target.value } }))}
                        placeholder="Email"
                    />
                    <Input
                        value={formState.user.firstName}
                        onChange={e => setFormState(prev => ({ ...prev, user: { ...prev.user, firstName: e.target.value } }))}
                        placeholder="First Name"
                    />
                    <Input
                        value={formState.user.lastName}
                        onChange={e => setFormState(prev => ({ ...prev, user: { ...prev.user, lastName: e.target.value } }))}
                        placeholder="Last Name"
                    />
                    <h4 style={{ margin: 0, padding: 0, fontWeight: 300, color: "gray" }}>Roles</h4>
                    <Select
                        multiple
                        value={formState.roles.map(r => r.id) || []}
                        onChange={handleRoleStringChange}
                        input={<OutlinedInput />}
                        renderValue={() => displayRoles.join(", ")}
                        MenuProps={{
                            PaperProps: {
                                style: {
                                    maxHeight: 192,
                                    width: 250,
                                },
                            },
                        }}>
                        {roles.map((role) => (
                            <MenuItem key={role.id} value={role.id}>
                                <Checkbox checked={formState.roles.some(r => r.id === role.id)} />
                                <ListItemText primary={role.name} />
                            </MenuItem>
                        ))}
                    </Select>
                    <Box sx={{ display: "flex", height: "3em", flexDirection: "row", justifyContent: "space-between", width: "100%" }}>
                        <Button
                            sx={{ bgcolor: "primary.main", color: "white", width: "10em" }}
                            type="submit">
                            {!isLoading ? "Create" : <Spinner />}
                        </Button>
                        <FormControlLabel
                            control={<Checkbox
                                onChange={e => setFormState(prev => ({
                                    ...prev,
                                    user: { ...prev.user, enabled: e.target.checked }
                                }))}
                                checked={formState.user.enabled} />}
                            label="Enabled"
                        />
                    </Box>
                </Box>
            </form>
        </Modal>
    );
};

export default CreateUserModal;
