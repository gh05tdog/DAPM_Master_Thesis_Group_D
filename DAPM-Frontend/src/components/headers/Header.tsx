import { AppBar, Toolbar, Typography, IconButton, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import AccountCircle from '@mui/icons-material/AccountCircle';

const Header: React.FC = () => (
    <AppBar
        position="static"
        sx={{ bgcolor: 'rgba(54,55,56,1)', paddingX: 3 }}
        elevation={3}
    >
        <Toolbar sx={{ justifyContent: 'space-between' }}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}>
                </Typography>
            </Box>

            <Box>
                <IconButton color="inherit" aria-label="account">
                    <AccountCircle />
                </IconButton>
            </Box>
        </Toolbar>
    </AppBar>
);

export default Header;
