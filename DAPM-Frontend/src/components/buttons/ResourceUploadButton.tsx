import { Box, Button, FormControl, FormLabel, MenuItem, Modal, Select, TextField, Typography } from '@mui/material';
import React from 'react';
import { putResource } from '../../services/backendAPI.tsx';

export interface UploadButtonProps {
    orgId: string,
    repId: string,
}

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

const ResourceUploadButton = ({ orgId, repId }: UploadButtonProps) => {

    const dataTypes = ["eventLog", "bpmnModel", "petriNet"]

    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const formData = new FormData(event.currentTarget);
        const formEntries = Object.fromEntries(formData.entries());


        if (formData.get('ResourceFile')) {
            try {
                const result = await putResource(orgId, repId, formData);
            } catch (error) {
                console.error('Error uploading resource:', error);
            }
        } else {
            console.error('No file selected.');
        }

        alert("Form Submitted");
    };

    return (
        <div>
            <Button sx={{ backgroundColor: "gray", padding: "1px", color: "black" }} onClick={handleOpen}>+</Button>
            <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-create-repository"
                aria-describedby="modal-create-repository"
            >
                <Box sx={style}>
                    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                        <Typography id="modal-modal-title" variant="h6" component="h2" sx={{ color: 'white' }}>
                            Upload Resource
                        </Typography>
                        <form onSubmit={handleSubmit}>
                            <FormControl fullWidth margin="normal">
                                <FormLabel>Resource name</FormLabel>
                                <TextField name="Name" />

                                <FormLabel>Resource type</FormLabel>
                                <Select
                                    name="ResourceType"
                                    labelId="resourceType-select-lable"
                                    id="resourceType-select"
                                    sx={{ width: '100%' }}>
                                    {dataTypes.map((resource) => <MenuItem value={resource}>{resource}</MenuItem>)}
                                </Select>

                                <FormLabel>Upload File</FormLabel>
                                <input type="file" name="ResourceFile" />
                            </FormControl>

                            <Button type="submit" sx={{ backgroundColor: "gray", padding: "1px", color: "black" }}>Submit</Button>
                        </form>
                    </Box>
                </Box>
            </Modal>
        </div>
    );
}

export default ResourceUploadButton;
