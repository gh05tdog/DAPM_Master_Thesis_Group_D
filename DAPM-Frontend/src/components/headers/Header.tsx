// src/components/Header.tsx
import * as React from 'react';
import Box from '@mui/material/Box/Box';
import ColorModeIconDropdown from '../../assets/theme/ColorModeIconDropdown.tsx';

interface HeaderProps {
  setMode: (mode: 'light' | 'dark') => void;
  currentMode: 'light' | 'dark';
}

export default function Header({ setMode, currentMode }: HeaderProps) {
  return (
    <Box
      display="horizontal"
      sx={{
        width: '100%',
        alignItems: 'center',
        justifyContent: 'space-between',
        maxWidth: { sm: '100%', md: '1700px' },
        pt: 1.5,
      }}
    >
      <Box display="flex" sx={{ gap: 1 }}>
        <ColorModeIconDropdown setMode={setMode} currentMode={currentMode} />
      </Box>
    </Box>
  );
}
