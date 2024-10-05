import Keycloak from 'keycloak-js';

// Define and export the Keycloak instance
const keycloakConfig = new Keycloak({
  url: 'http://localhost:8080/',
  realm: 'myrealm',
  clientId: 'myclient',
});

// Export a function to initialize Keycloak
export const initializeKeycloak = async () => {
  try {
    const authenticated = await keycloakConfig.init({ onLoad: 'login-required' });
    if (authenticated) {
      console.log('User is authenticated');
    } else {
      console.log('User is not authenticated');
    }
    return authenticated;
  } catch (error) {
    console.error('Failed to initialize Keycloak:', error);
    return false;
  }
};

export default keycloakConfig;
