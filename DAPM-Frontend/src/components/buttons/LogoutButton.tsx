import { Box, Typography, IconButton, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { logout } from '../../utils/keycloak.ts';
import { LogoutOutlined } from '@mui/icons-material';
import { flexbox } from "@mui/system";


const LogoutButton = () => {
    const navigate = useNavigate();
    const handleLogout = async () => {
        try {
            //logout();
            navigate("/logout");
        } catch (error) {
            console.error("Logout failed:", error);
        }
    };

    return (
        <Button
            data-qa="logoutButton"
            onClick={handleLogout}
            color="primary"
            variant="contained"
            sx={{ borderRadius: "2em", backgroundColor: 'primary.main', "&:hover": { backgroundColor: 'primary' } }}>
            <Box sx={{ display: "flex", direction: "row", gap: 2, p: 0.5 }}>
                Logout
                <LogoutOutlined />
            </Box>
        </Button >
    );
};

export default LogoutButton;
