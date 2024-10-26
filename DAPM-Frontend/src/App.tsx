// src/App.tsx
import React from 'react';
import { CssVarsProvider, extendTheme } from '@mui/joy/styles';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import PipelineOverviewPage from './views/PipelineOverviewPage.tsx';
import OrderDashboard from './views/xOrderDashboard.tsx';

const theme = extendTheme({
  colorSchemes: {
    light: {
      palette: {
        primary: { 500: '#1976d2' },
        background: { surface: '#f5f5f5' },
      },
    },
  },
});

const App: React.FC = () => {
  return (
    <CssVarsProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/dashboard" />} />
          <Route path="/dashboard" element={<OrderDashboard />} />
          <Route path="/overview" element={<PipelineOverviewPage user={null} />} />
        </Routes>
      </BrowserRouter>
    </CssVarsProvider>
  );
};

export default App;
