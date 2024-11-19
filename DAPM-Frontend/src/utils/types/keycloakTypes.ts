export interface UserRepresentation {
  username: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  enabled: boolean;
  credentials: CredentialRepresentation[];
}

interface CredentialRepresentation {
  type: string;
  value: string;
  temporary: boolean;
}

export interface RoleRepresentation {
  id?: string;
  name?: string;
  description?: string;
  clientRole?: boolean;
}
