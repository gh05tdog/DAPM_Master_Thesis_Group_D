// src/store.ts
import { configureStore } from '@reduxjs/toolkit';
import rootReducer from './redux/slices/index.ts'; // Assume this is where your root reducer is defined

const store = configureStore({
  reducer: rootReducer,
  // Add middleware, devTools, etc., as needed
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
