import React from "react";
import { Organization } from "../../state_management/states/apiState.ts"

interface OrganizationCardProps {
    organization: Organization;
}

const OrganizationCard: React.FC<OrganizationCardProps> = ({ organization }) => {
    return (
        <li key={organization.id}>Organization: {organization.name} - {organization.apiUrl}</li>
    )
}

export default OrganizationCard;