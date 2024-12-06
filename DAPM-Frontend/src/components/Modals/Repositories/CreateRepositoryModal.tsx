import { Box, Button, Checkbox, FormControlLabel, Input, ListItemText, MenuItem, Modal, OutlinedInput, Select } from "@mui/material";
import { FC } from "react";
import useRepositoryCreation from "./hooks/useRepositoryCreation.tsx";
import Spinner from "../../cards/SpinnerCard.tsx";

interface Props {
    isOpen: boolean;
    onClose: () => void;
}

const CreateRepositoryModal: FC<Props> = ({ isOpen, onClose }) => {
    const { formState, handleCreateRepository, isLoading, setFormState } = useRepositoryCreation(onClose);

    return (
        <Modal open={isOpen} onClose={onClose}>
            <form onSubmit={e => { e.preventDefault(); handleCreateRepository(); }}>
                <Box
                    data-qa="RepositoryCreationModal"
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
                    <h1>Create Repository</h1>
                    <Input
                        required
                        value={formState.repository.name}
                        onChange={e => setFormState(prev => ({
                            ...prev,
                            repository: {
                                ...prev.repository,
                                name: e.target.value,
                            }
                        }))}
                        placeholder="Repository name"
                    />
                    <Box sx={{ display: "flex", height: "3em", flexDirection: "row", justifyContent: "space-between", width: "100%" }}>
                        <Button
                            sx={{ bgcolor: "primary.main", color: "white", width: "10em" }}
                            type="submit">
                            {!isLoading ? "Create" : <Spinner />}
                        </Button>
                        
                    </Box>
                </Box>
            </form>
        </Modal>
    );
};

export default CreateRepositoryModal;
