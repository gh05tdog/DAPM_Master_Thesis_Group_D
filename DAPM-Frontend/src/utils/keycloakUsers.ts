import axios from 'axios';
import { getToken } from './keycloak.ts'; // Import the getToken utility
import { environment } from '../configs/environments.ts'; 

const getUsersFromKeycloak = async () => {
  try {
    const token = await getToken();
    if (!token) throw new Error('No token available');

    const response = await axios.get(
      `${environment.keycloak_url}/admin/realms/test/users`, // Construct URL directly
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    return response.data; // Returns an array of users
  } catch (error) {
    console.error('Error fetching users from Keycloak:', error);
    throw error;
  }
};

export default getUsersFromKeycloak;
