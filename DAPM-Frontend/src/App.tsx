import { useKeycloak } from '@react-keycloak/web';
import { ThemeProvider, createTheme } from "@mui/material";
import { Provider } from "react-redux";
import { configureStore } from "@reduxjs/toolkit";
import rootReducer from "./redux/slices";
import { persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import PipelineComposer from './routes/PipeLineComposer'; // Ensure this path is correct
import UserPage from "./routes/OverviewPage";
import LoginPage from './routes/LoginPage'; // Import the LoginPage
import { loadState, saveState } from "./redux/browser-storage";

const persistConfig = {
  key: 'root',
  storage,
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

const store = configureStore({
  reducer: persistedReducer,
  preloadedState: loadState(),
});

store.subscribe(() => saveState(store.getState()));

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

const router = createBrowserRouter([
  {
    path: "/",
    element: <UserPage />,
  },
  {
    path: "/pipeline",
    element: <PipelineComposer />,
  }
]);

const App: React.FC = () => {
  const { keycloak, initialized } = useKeycloak();

  // Check if Keycloak is initialized and authenticated
  if (!initialized) {
    return <div>Loading...</div>; // Show a loading indicator while Keycloak is initializing
  }

  // If not authenticated, show the login page
  if (!keycloak.authenticated) {
    return <LoginPage />;
  }

  // If authenticated, render the main application
  return (
    <ThemeProvider theme={darkTheme}>
      <Provider store={store}>
        <RouterProvider router={router} />
      </Provider>
    </ThemeProvider>
  );
};

export default App;
