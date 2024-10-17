import React from "react";
import { Repository } from "../../state_management/states/apiState.ts"

interface RepositoryCardProps {
    repository: Repository;
}

const RepositoryCard: React.FC<RepositoryCardProps> = ({ repository }) => {
    return (
        <li key={repository.id}>Repository: {repository.name}</li>
    )
}

export default RepositoryCard;