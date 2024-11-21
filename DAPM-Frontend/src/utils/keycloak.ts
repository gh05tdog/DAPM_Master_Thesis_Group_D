import Keycloak from 'keycloak-js';
import { environment } from '../configs/environments.ts';

const keycloak = new Keycloak({
  url: environment.keycloak_url || '',
  realm: 'test',
  clientId: 'test-client'
});

let keycloakInitialized = false;

const initKeycloak = async () => {
  try {
    const authenticated = await keycloak.init({ onLoad: 'login-required' });
    keycloakInitialized = true;
    if (!authenticated) {
      keycloak.login();
    }
  } catch (error) {
    console.error('Keycloak initialization failed:', error);
  }
};

const login = () => {
  if (keycloakInitialized) {
    keycloak.login();
  } else {
    console.error('Keycloak is not initialized');
  }
};

const logout = async () => {
  if (keycloakInitialized) {
    keycloak.logout({redirectUri: environment.keycloak_url?.replace("8888", "3000").replace("https","http") });
  } else {
    console.error('Keycloak is not initialized');
  }
};

async function getToken() {
  try {
    if (!keycloak.token || keycloak.isTokenExpired()) {
      await keycloak.updateToken(30);
    }
    return keycloak.token;    
  } catch (error) {
    console.error('Failed to refresh token:', error);
    throw error;
  }
}

export { initKeycloak, login, logout, getToken };
export default keycloak;
