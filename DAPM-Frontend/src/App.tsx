// src/App.tsx
import React from 'react';
import { ThemeProvider, createTheme } from '@mui/material';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './stores.ts';

import UserPage from './routes/OverviewPage.tsx';
import PipelineComposer from './routes/PipeLineComposer.tsx';
// Import LoginPage if needed in the future
// import LoginPage from './routes/LoginPage.tsx';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const App: React.FC = () => {
  // Directly render the routes without authentication logic
  return (
    <ThemeProvider theme={darkTheme}>
      <Provider store={store}>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Navigate to="/user" />} />
            <Route path="/user" element={<UserPage />} />
            <Route path="/pipeline" element={<PipelineComposer />} />
          </Routes>
        </BrowserRouter>
      </Provider>
    </ThemeProvider>
  );
};

export default App;
