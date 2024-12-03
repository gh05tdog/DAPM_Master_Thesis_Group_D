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
import ManagerPage from './views/ManagerPage.tsx';
import LogoutPage from "./views/LogoutPage.tsx";
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

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

const App: React.FC = () => {
    const [initialized, setInitialized] = useState(false);
    const [authenticated, setAuthenticated] = useState(false);
    const [user, setUser] = useState<any>(null);

    useEffect(() => {
        const initialize = async () => {
            await initKeycloak();
            setInitialized(true);
            // Assuming initKeycloak sets the keycloakConfig.authenticated value
            setAuthenticated(keycloak.authenticated ?? false); // Update this based on your keycloak logic
            setUser(keycloak.loadUserInfo());
        };
        initialize();
    }, []);

  if (!initialized) {
    return <div>Loading...</div>;
  }
  
  
  return (
    <ThemeProvider theme={lightTheme}>
      <Provider store={store}>
        <BrowserRouter>
          <ToastContainer />
          <Routes>
            {!authenticated ? (
              <Route path="/user" element={<LoginPage />} />
            ) : (
              <>
                {/* Automatically redirect to /user when authenticated */}
                <Route path="/" element={<Navigate to="/user" />} />
                <Route path="/user" element={<PipelineOverviewPage user={user}/>} />
                <Route path="/pipelineEditor" element={<PipelineComposer />} />
                <Route path="/manage" element = {<ManagerPage />} />
                <Route path={"/logout"} element = {<LogoutPage user = {user} />} />
              </>
            )}
          </Routes>
        </BrowserRouter>
      </Provider>
    </ThemeProvider>
  );
};

export default App;
