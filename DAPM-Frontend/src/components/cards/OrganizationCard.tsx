import React from "react";
import { Organization } from "../../state_management/states/apiState.ts"
import { FormControlLabel, Checkbox } from "@mui/material";

interface OrganizationCardProps {
    organization: Organization;
    isChecked: boolean;
    handleToggle: () => void,
}

const OrganizationCard: React.FC<OrganizationCardProps> = ({ organization, isChecked, handleToggle }) => {
    return (
        <FormControlLabel
            key={organization.id}
            control={
                <Checkbox
                    checked={isChecked}
                    onChange={handleToggle}
                    color="primary"
                />
            }
            label={organization.name}
        />
    )
}

export default OrganizationCard;