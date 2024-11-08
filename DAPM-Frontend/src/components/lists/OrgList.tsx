import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getOrganizations ,selectLoadingOrganisation} from "../../state_management/selectors/apiSelector.ts";
import { organizationThunk } from "../../state_management/slices/apiSlice.ts";
import OrganizationCard from "../cards/OrganizationCard.tsx";
import Spinner from '../cards/SpinnerCard.tsx';
import { Accordion, AccordionSummary, AccordionDetails, Checkbox, FormControlLabel, Typography, Box } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const OrgList: React.FC = () => {
    
    const dispatch = useDispatch();
    const organizations = useSelector(getOrganizations); // Adjust state path as needed
    const loading = useSelector(selectLoadingOrganisation); // Get loading state
    const [selectedOrgs, setSelectedOrgs] = useState<string[]>([]); // State for selected organizations
    
    useEffect(()=> {
        dispatch(organizationThunk());
    }, [dispatch]);
    
    if (loading) {
        return(
          <Box>
          <Accordion disabled sx={{ boxShadow: 3, borderRadius: 2 }}>
            <AccordionSummary aria-controls="org-list-content" id="org-list-header" sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}>
                <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Organizations</Typography>
            </AccordionSummary>
          </Accordion>
          <Box sx={{ mt: 2, display:'flex', justifyContent:'center', alignItems:'center' }}>
            <Spinner />
          </Box>
        </Box>
          
            <div>
              <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <h2>Organizations</h2>
              </div>
              <Spinner />
            </div>
        )
    }

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
        sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}
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
