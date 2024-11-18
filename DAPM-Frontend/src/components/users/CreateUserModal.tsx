import { Box, Button, Input, Modal } from "@mui/material";
import { FC, useCallback } from "react";

interface Props {
    isOpen: boolean;
    onClose: () => void;
}

const CreateUserModal: FC<Props> = ({ isOpen, onClose }) => {

    const handleCreateUser = useCallback(async () => {

    }, [])

    return (
        <Modal open={isOpen} onClose={onClose}>
            <Box sx={{ display: "flex", direction: "column" }}>
                <h1>Create User</h1>
                <Input placeholder="Username" />
                <Input placeholder="First Name" />
                <Input placeholder="Last Name" />

                <Button type="submit">Create</Button>
            </Box>
        </Modal>
    )
}

export default CreateUserModal;