// src/App.tsx
import React, { useState, useEffect } from 'react';
import { ThemeProvider, createTheme } from '@mui/material';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './state_management/store/stores.ts';

import PipelineOverviewPage from './views/PipelineOverviewPage.tsx';
import PipelineComposer from './views/old_PipeLineComposer.tsx';
import LoginPage from './views/LoginPage.tsx';
import keycloak, { initKeycloak } from '../src/utils/keycloak.ts';
import { environment } from './configs/environments.ts';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const lightTheme = createTheme({ 
  palette: {
    mode: 'light',
  },
});

console.log('Client api url:', environment.clientapi_url);
console.log('Keycloak url:', environment.keycloak_url);
console.log('Peer api url:', environment.peerapi_url);
console.log('Access control url:', environment.accesscontrol_url);

const App: React.FC = () => {


  return (
    <ThemeProvider theme={lightTheme}>
      <Provider store={store}>
        <BrowserRouter>
          <Routes>
              <>
                <Route path="/" element={<Navigate to="/user" />} />
                <Route path="/user" element={<PipelineOverviewPage user={null}/>} />
                <Route path="/pipeline" element={<PipelineComposer />} />
              </>
          </Routes>
        </BrowserRouter>
      </Provider>
    </ThemeProvider>
  );
};

export default App;
