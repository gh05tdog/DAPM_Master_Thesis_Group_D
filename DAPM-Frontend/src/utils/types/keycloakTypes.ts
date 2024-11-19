export interface UserRepresentation {
  username: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  enabled: boolean;
  clientRoles: { [clientId: string]: string[] };
}

export interface RoleRepresentation {
  id?: string;
  name?: string;
  description?: string;
  clientRole?: boolean;
}
