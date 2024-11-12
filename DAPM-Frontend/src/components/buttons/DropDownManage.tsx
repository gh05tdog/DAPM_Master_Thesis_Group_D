import React, { useState } from 'react';
import {Button, Divider, Menu, MenuProps} from "@mui/material";
import {styled} from "styled-components";
import {useNavigate} from "react-router-dom";

const StyledMenu = styled((props: MenuProps) => (
    <Menu
        sx={{
            '& .MuiPaper-root': {
                boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)',
            },
        }}
        elevation={0}
        anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'center',
        }}
        transformOrigin={{
            vertical: 'top',
            horizontal: 'center',
        }}
        {...props}
    />
    
))(({ theme }) => ({
    '& .MuiPaper-root': {
        borderRadius: 6,
        marginTop: 2,
        minWidth: 180,
    },
}));

export default function DropDownManage() {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const navigate = useNavigate();

    const navigateToManagePipeline = () => {
        navigate('/manage-pipeline')
    };


    return (
        <div>
            <Button
                variant={"contained"}
                aria-expanded={Boolean(anchorEl) ? 'true' : undefined}
                onClick={handleClick}
            >
                Manage
            </Button>
            <StyledMenu
                id="basic-menu"
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <Button sx={{width: '100%'}} 
                        onClick={navigateToManagePipeline}>
                    Pipeline
                </Button>
                <Divider />
                <Button sx={{width: '100%'}} 
                        onClick={handleClose}>
                    Organization</Button>
                <Divider />
                <Button sx={{width: '100%'}} 
                        onClick={handleClose}>
                    Repository</Button>
                <Divider />
                <Button sx={{width: '100%'}} 
                        onClick={handleClose}>
                    Resource</Button>
                
            </StyledMenu>
        </div>
    );
}

