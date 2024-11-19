import axios from "axios";
import { getToken } from "./keycloak.ts"; // Import the getToken utility
import { environment } from "../configs/environments.ts";
import {
  RoleRepresentation,
  UserRepresentation,
} from "./types/keycloakTypes.ts";

export const getUsersFromKeycloak = async () => {
  try {
    const token = await getToken();
    if (!token) throw new Error("No token available");

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
    console.error("Error fetching users from Keycloak:", error);
    throw error;
  }
};

export const createUser = async (user: UserRepresentation) => {
  try {
    const token = await getToken();
    if (!token) throw new Error("No token available");
    const response = await axios.post(
      `${environment.keycloak_url}/admin/realms/test/users`,
      { user },
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    return response.data; // Returns the created user
  } catch (error) {
    console.error("Error creating user to Keycloak:", error);
    throw error;
  }
};

export const roleMapUser = async (
  userId: string,
  roles: RoleRepresentation[]
) => {
  try {
    const token = await getToken();
    if (!token) throw new Error("No token available");

    const response = await axios.post(
      `${environment.keycloak_url}/admin/realms/test/users/${userId}/role-mappings/realm`,
      roles,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );

    return response.data; // Returns the created user
  } catch (error) {
    console.error("Error creating user to Keycloak:", error);
    throw error;
  }
};

export const getRolesFromKeycloak = async (): Promise<RoleRepresentation[]> => {
  try {
    const token = await getToken();
    if (!token) throw new Error("No token available");

    const response = await axios.get(
      `${environment.keycloak_url}/admin/realms/test/roles`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    return response.data.map(
      (x: any): RoleRepresentation => ({
        id: x.id,
        name: x.name,
        description: x.description,
        clientRole: x.clientRole,
      })
    ); // Maps the response data to RoleRepresentation
  } catch (error) {
    console.error("Error fetching roles from Keycloak:", error);
    throw error;
  }
};
