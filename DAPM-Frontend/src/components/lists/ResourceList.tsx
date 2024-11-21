import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations, getResources, selectLoadingRepositories, selectLoadingResources } from "../../state_management/selectors/apiSelector.ts";
import { resourceThunk } from "../../state_management/slices/apiSlice.ts";
import Spinner from '../cards/SpinnerCard.tsx';
import { Accordion, AccordionSummary, AccordionDetails, Typography, List } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ResourceCard from "../cards/ResourceCard.tsx";

const ResourceList: React.FC = () => {
  const dispatch = useDispatch();

  const organizations = useSelector(getOrganizations);
  const repositories = useSelector(getRepositories);
  const resources = useSelector(getResources);
  const repoLoading = useSelector(selectLoadingRepositories); // Get loading state
  const loading = useSelector(selectLoadingResources); // Get loading state

  useEffect(() => {
    if ((!repoLoading) && repositories.length > 0) {
      dispatch(resourceThunk({ organizations, repositories }));
    }
  }, [dispatch, organizations, repositories]);

  if (loading) {
    return (
      <div>
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
          <h2>Resources</h2>
        </div>
        <Spinner />

      </div>
    )
  }

  return (
    <Accordion sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="resource-list-content"
        id="resource-list-header"
        sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Resources</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <List sx={{ width: '100%' }}>
          {resources?.map((resource) => (
            <ResourceCard  key={resource.id} resource={resource} />
          ))}
        </List>
      </AccordionDetails>
    </Accordion>
  );
};

export default ResourceList;
