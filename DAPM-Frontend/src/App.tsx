// src/App.tsx
import React, { useState, useEffect } from 'react';
import { ThemeProvider, createTheme } from '@mui/material';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './stores.ts';

import PipelineOverviewPage from './routes/PipelineOverviewPage.tsx';
import PipelineComposer from './routes/PipeLineComposer.tsx';
import LoginPage from './routes/LoginPage.tsx';
import keycloakConfig, { initKeycloak } from './keycloak.ts';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const App: React.FC = () => {
  const [initialized, setInitialized] = useState(false);
  const [authenticated, setAuthenticated] = useState(false);

  useEffect(() => {
    const initialize = async () => {
      await initKeycloak();
      setInitialized(true);
      // Assuming initKeycloak sets the keycloakConfig.authenticated value
      setAuthenticated(keycloakConfig.authenticated ?? false); // Update this based on your keycloak logic
    };

    initialize();
  }, []);

  if (!initialized) {
    return <div>Loading...</div>;
  }

  return (
    <ThemeProvider theme={darkTheme}>
      <Provider store={store}>
        <BrowserRouter>
          <Routes>
            {!authenticated ? (
              <Route path="/user" element={<LoginPage />} />
            ) : (
              <>
                {/* Automatically redirect to /user when authenticated */}
                <Route path="/" element={<Navigate to="/user" />} />
                <Route path="/user" element={<PipelineOverviewPage />} />
                <Route path="/pipeline" element={<PipelineComposer />} />
              </>
            )}
          </Routes>
        </BrowserRouter>
      </Provider>
    </ThemeProvider>
  );
};

export default App;
