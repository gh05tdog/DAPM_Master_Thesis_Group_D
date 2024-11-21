import React from "react";
import { Repository } from "../../state_management/states/apiState.ts"
import { Checkbox, FormControlLabel } from "@mui/material";

interface RepositoryCardProps {
    repository: Repository;
    isChecked: boolean;
    handleToggle: () => void,
}

const RepositoryCard: React.FC<RepositoryCardProps> = ({ repository, isChecked, handleToggle }) => {
    return (
        <FormControlLabel
            key={repository.id}
            control={
                <Checkbox
                    checked={isChecked}
                    onChange={handleToggle}
                    color="primary"
                />
            }
            label={repository.name}
        />
    )
}

export default RepositoryCard;