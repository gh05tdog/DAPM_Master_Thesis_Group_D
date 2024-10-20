import { AppBar, Toolbar, Typography } from '@mui/material';

const Header: React.FC = () => (
    <AppBar
        position="static"
        sx={{ bgcolor: 'rgba(54,55,56,1)'}}
        elevation={0}
    >
        <Toolbar>
            <Typography variant="h6">Pipeline Processing for Dummies Group D</Typography>
        </Toolbar>
    </AppBar>
);

export default Header;