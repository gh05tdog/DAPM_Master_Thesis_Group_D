// src/assets/theme/ColorModeIconDropdown.tsx
import * as React from 'react';
import DarkModeIcon from '@mui/icons-material/DarkModeRounded';
import LightModeIcon from '@mui/icons-material/LightModeRounded';
import IconButton from '@mui/material/IconButton/IconButton';
import Menu from '@mui/material/Menu/Menu';
import MenuItem from '@mui/material/MenuItem/MenuItem';
import Button from '@mui/material';

interface ColorModeIconDropdownProps {
  setMode: (mode: 'light' | 'dark') => void;
  currentMode: 'light' | 'dark';
}

export default function ColorModeIconDropdown({ setMode, currentMode }: ColorModeIconDropdownProps) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleModeChange = (mode: 'light' | 'dark') => {
    setMode(mode);
    handleClose();
  };

  const icon = currentMode === 'light' ? <LightModeIcon /> : <DarkModeIcon />;

  return (
    <IconButton sx={{ borderRadius: 50, backgroundColor: '#4caf50', "&:hover": { backgroundColor: '#388e3c' } }}>
      <IconButton onClick={handleClick} size="small" aria-haspopup="true">
        {icon}
      </IconButton>
      <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
        <MenuItem selected={currentMode === 'light'} onClick={() => handleModeChange('light')}>
          Light Mode
        </MenuItem>
        <MenuItem selected={currentMode === 'dark'} onClick={() => handleModeChange('dark')}>
          Dark Mode
        </MenuItem>
      </Menu>
    </IconButton>
  );
}
