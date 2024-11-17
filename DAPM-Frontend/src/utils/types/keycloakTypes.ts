export interface UserRepresentation {
  username: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  enabled: boolean;
  clientRoles: { [clientId: string]: string[] };
}
