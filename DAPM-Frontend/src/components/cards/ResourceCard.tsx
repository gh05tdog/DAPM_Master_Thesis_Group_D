import React from "react";
import { Resource } from "../../state_management/states/apiState.ts"
import { ListItem, Typography } from "@mui/material";

interface ResourceCardProps {
    resource: Resource;
}

const ResourceCard: React.FC<ResourceCardProps> = ({ resource }) => {
    return (
        <ListItem sx={{ borderBottom: '1px solid lightgray', padding: 1 }}>
            <Typography variant="body1" color="text.primary">{resource.name}</Typography>
        </ListItem>
    )
}

export default ResourceCard;