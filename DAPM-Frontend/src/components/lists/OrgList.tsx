import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getOrganizations } from "../../state_management/selectors/apiSelector.ts";
import { organizationThunk } from "../../state_management/slices/apiSlice.ts";
import { Accordion, AccordionSummary, AccordionDetails, Checkbox, FormControlLabel, Typography, Box } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const OrgList: React.FC = () => {
  const dispatch = useDispatch();
  const organizations = useSelector(getOrganizations); // Get organizations from Redux store
  const [selectedOrgs, setSelectedOrgs] = useState<string[]>([]); // State for selected organizations

  useEffect(() => {
    dispatch(organizationThunk()); // Fetch organizations on mount
  }, [dispatch]);

  const handleToggleOrg = (orgId: string) => {
    setSelectedOrgs((prevSelectedOrgs) =>
      prevSelectedOrgs.includes(orgId)
        ? prevSelectedOrgs.filter((id) => id !== orgId) // Deselect
        : [...prevSelectedOrgs, orgId] // Select
    );
  };

  return (
    <Accordion defaultExpanded sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="org-list-content"
        id="org-list-header"
        sx={{ bgcolor: 'primary.main', color: 'white', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Organizations</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          {organizations?.map((organization) => (
            <FormControlLabel
              key={organization.id}
              control={
                <Checkbox
                  checked={selectedOrgs.includes(organization.id)}
                  onChange={() => handleToggleOrg(organization.id)}
                  color="primary"
                />
              }
              label={organization.name}
            />
          ))}
        </Box>
      </AccordionDetails>
    </Accordion>
  );
};

export default OrgList;
