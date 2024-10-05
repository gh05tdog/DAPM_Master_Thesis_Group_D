import { ThemeProvider, createTheme } from "@mui/material";
import { Provider } from "react-redux";
import { configureStore } from "@reduxjs/toolkit";
import rootReducer from "./redux/slices";
import { persistReducer } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import PipelineComposer from './routes/PipeLineComposer'; // Ensure this path is correct
import UserPage from "./routes/OverviewPage";
import { loadState, saveState } from "./redux/browser-storage";
import LandingPage from './routes/LandingPage';
import LoginPage from './routes/LoginPage';

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
    element: <LandingPage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/user",
    element: <UserPage />,
  },
  {
    path: "/pipeline",
    element: <PipelineComposer />,
  }
]);

const App: React.FC = () => {

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
