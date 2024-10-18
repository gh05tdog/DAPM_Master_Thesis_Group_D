import React from "react";
import { Resource } from "../../state_management/states/apiState.ts"

interface ResourceCardProps {
    resource: Resource;
}

const ResourceCard: React.FC<ResourceCardProps> = ({ resource }) => {
    return (
        <li key={resource.id}>Repository: {resource.repositoryId} Resource: {resource.name} Type: {resource.type} </li>
    )
}

export default ResourceCard;