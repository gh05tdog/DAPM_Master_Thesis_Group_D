import React, { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getOrganizations, selectLoadingOrganisation } from "../../state_management/selectors/apiSelector.ts";
import { setActiveOrganisation } from "../../state_management/slices/pipelineSlice.ts";
import { organizationThunk } from "../../state_management/slices/apiSlice.ts";
import OrganizationCard from "../cards/OrganizationCard.tsx";
import Spinner from '../cards/SpinnerCard.tsx';
import { Accordion, AccordionSummary, AccordionDetails, Checkbox, FormControlLabel, Typography, Box } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { getActiveOrganisation } from "../../state_management/slices/indexSlice.ts";

const OrgList: React.FC = () => {

  const dispatch = useDispatch();
  const organizations = useSelector(getOrganizations); // Adjust state path as needed
  const selectedorg = useSelector(getActiveOrganisation);
  const loading = useSelector(selectLoadingOrganisation); // Get loading state
  const [selectedOrgs, setSelectedOrgs] = useState<string[]>([]); // State for selected organizations

  const handleToggleOrg = useCallback((orgId: string) => {
    setSelectedOrgs((prevSelectedOrgs) =>
      prevSelectedOrgs.includes(orgId)
        ? prevSelectedOrgs.filter((id) => id !== orgId) // Deselect
        : [...prevSelectedOrgs, orgId] // Select
    );
    dispatch(setActiveOrganisation(orgId));
  },[selectedorg]);

  useEffect(() => {
    dispatch(organizationThunk());
  }, [dispatch]);

  if (loading) {
    return (
      <Box>
        <Accordion disabled sx={{ boxShadow: 3, borderRadius: 2 }}>
          <AccordionSummary aria-controls="org-list-content" id="org-list-header" sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}>
            <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Organizations</Typography>
          </AccordionSummary>
        </Accordion>
        <Box sx={{ mt: 2, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
          <Spinner />
        </Box>
      </Box>

    )
  }

  

  return (
    <Accordion defaultExpanded sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="org-list-content"
        id="org-list-header"
        sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Organizations</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          {organizations?.map((organization) => (
            
            <OrganizationCard key={organization.id} organization={organization}
              isChecked={selectedOrgs.includes(organization.id)}
              handleToggle={() => handleToggleOrg(organization.id)} />
          ))}
        </Box>
      </AccordionDetails>
    </Accordion>
  );
};

export default OrgList;
