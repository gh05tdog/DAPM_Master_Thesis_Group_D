import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations, getResources } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk, organizationThunk, resourceThunk } from "../../state_management/slices/apiSlice.ts";
import { Accordion, AccordionSummary, AccordionDetails, Typography, Box, List, ListItem } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const ResourceList: React.FC = () => {
  const dispatch = useDispatch();
  
  const organizations = useSelector(getOrganizations);
  const repositories = useSelector(getRepositories);
  const resources = useSelector(getResources);

  useEffect(() => {
    dispatch(organizationThunk());
  }, [dispatch]);

  useEffect(() => {
    if (organizations.length > 0) {
      try {
        dispatch(repositoryThunk(organizations));
      } catch (error) {
        console.error(error);
      }
    }
  }, [dispatch, organizations]);

  useEffect(() => {
    if (repositories.length > 0) {
      dispatch(resourceThunk({ organizations, repositories }));
    }
  }, [dispatch, organizations, repositories]);

  return (
    <Accordion defaultExpanded sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="resource-list-content"
        id="resource-list-header"
        sx={{ bgcolor: 'primary.main', color: 'white', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Resources</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <List sx={{ width: '100%' }}>
          {resources?.map((resource) => (
            <ListItem key={resource.id} sx={{ borderBottom: '1px solid lightgray', padding: 1 }}>
              <Typography variant="body1">{resource.name}</Typography>
            </ListItem>
          ))}
        </List>
      </AccordionDetails>
    </Accordion>
  );
};

export default ResourceList;
