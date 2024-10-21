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

import { persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web
import { RouterProvider, createBrowserRouter, createHashRouter } from "react-router-dom";
import PipelineComposer from "./routes/PipeLineComposer";
import UserPage from "./routes/OverviewPage";
import { loadState, saveState } from "./redux/browser-storage";

// Configure redux-persist
const persistConfig = {
  key: 'root',
  storage,
};

const persistedReducer = persistReducer<ReturnType<typeof rootReducer>>(persistConfig, rootReducer);

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

    };

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch



const router = createBrowserRouter([
  {
    path: "/",
    element: <UserPage/>,

  },
  {
    path: "/pipeline",
    element: <PipelineComposer/>,
  }
]);

export default function App() {
  return (
    <ThemeProvider theme={lightTheme}>
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
}
