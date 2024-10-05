async function initializeKeycloak() {
  const Keycloak = (await import('keycloak-js')).default;

  // Configuration for the Keycloak instance
  const keycloakConfig = {
    url: 'https://localhost:8080/auth',  // Replace with your Keycloak server URL
    realm: 'myrealms',                       // Replace with your Keycloak realm name
    clientId: 'myclient',                // Replace with your Keycloak client ID
  };

  // Initialize Keycloak instance with the configuration
  const keycloak = new Keycloak(keycloakConfig);

  return keycloak;
}

(async () => {
  const keycloak = await initializeKeycloak();
  module.exports = keycloak;
})();
