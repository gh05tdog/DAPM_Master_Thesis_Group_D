import { Box, Typography, IconButton } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { logout } from '../../utils/keycloak.ts';
import { LogoutOutlined } from '@mui/icons-material';


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
        <IconButton
            data-qa="logout button"
            onClick={handleLogout}
            sx={{ color: "white" }} ><LogoutOutlined />
        </IconButton>
    );
};

export default LogoutButton;
