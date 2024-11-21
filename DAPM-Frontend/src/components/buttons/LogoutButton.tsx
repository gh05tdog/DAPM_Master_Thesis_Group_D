import { Box,Typography, IconButton  } from "@mui/material";
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
        <Box sx={{ display: "flex", alignItems: "center", justifyContent: "center", gap: 3, p: 2, textAlign: 'center', bgcolor: 'primary.main', color: 'primary.contrastText', borderBottom: '1px solid', borderColor: 'divider' }}>
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Control Panel</Typography>
        <IconButton 
            data-qa="logout button" 
            onClick={handleLogout} 
            sx={{ color: "white" }} ><LogoutOutlined /></IconButton>
      </Box>
    );
};

export default LogoutButton;
