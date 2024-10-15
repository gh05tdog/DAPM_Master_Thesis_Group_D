// src/keycloak.ts
import Keycloak from 'keycloak-js';

const keycloakConfig = new Keycloak({
  url: 'http://localhost:8888',
  realm: 'test',
  clientId: 'test-client',
});

let keycloakInitialized = false; // Track initialization state

// Initialize Keycloak
const initKeycloak = async () => {
  try {
    const authenticated = await keycloakConfig.init({ onLoad: 'login-required' });
    keycloakInitialized = true;
    if (!authenticated) {
      keycloakConfig.login();
    }
  } catch (error) {
    console.error('Keycloak initialization failed:', error);
  }
};

// Directly call Keycloak's login and logout functions
const login = () => {
  if (keycloakInitialized) {
    keycloakConfig.login();
  } else {
    console.error('Keycloak is not initialized');
  }
};

const logout = () => {
  if (keycloakInitialized) {
    keycloakConfig.logout();
  } else {
    console.error('Keycloak is not initialized');
  }
};

export { initKeycloak, login, logout };
export default keycloakConfig;
