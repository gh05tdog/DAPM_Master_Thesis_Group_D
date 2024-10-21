// src/keycloak.ts
import Keycloak from 'keycloak-js';
import { environment } from '../configs/environments.ts';

const keycloak = new Keycloak({
  config: {
  url: environment.keycloak_url,
  realm: 'test',
  clientId: 'test-client',
  },
  init: {
    checkLoginIframe: false
  },
});

let keycloakInitialized = false; // Track initialization state

// Initialize Keycloak
const initKeycloak = async () => {
  try {
    console.log('Initializing Keycloak on url:', environment.keycloak_url);
    const authenticated = await keycloak.init({ onLoad: 'login-required' });
    keycloakInitialized = true;
    if (!authenticated) {
      keycloak.login();
    }
  } catch (error) {
    console.error('Keycloak initialization failed:', error);
  }
};

// Directly call Keycloak's login and logout functions
const login = () => {
  if (keycloakInitialized) {
    keycloak.login();
  } else {
    console.error('Keycloak is not initialized');
  }
};

const logout = () => {
  if (keycloakInitialized) {
    keycloak.logout();
  } else {
    console.error('Keycloak is not initialized');
  }
};

async function getToken(){
  try {
    if (keycloak.isTokenExpired()) {
      await keycloak.updateToken(30);
    }
    return keycloak.token;    
  } catch (error) {
    console.error('Failed to refresh token:', error);
  }
}

export { initKeycloak, login, logout, getToken };
export default keycloak;

