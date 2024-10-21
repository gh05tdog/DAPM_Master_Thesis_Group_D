import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations,selectLoadingRepositories } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk, organizationThunk } from "../../state_management/slices/apiSlice.ts";
import Spinner from '../cards/SpinnerCard.tsx';
import RepositoryCard from "../cards/RepositoryCard.tsx";
import { Accordion, AccordionSummary, AccordionDetails, Checkbox, FormControlLabel, Typography, Box } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
const RepoList: React.FC = () => {
   const dispatch = useDispatch();
  // Get organizations and repositories from the store
    const organizations = useSelector(getOrganizations);
    const repositories = useSelector(getRepositories);
    const loading = useSelector(selectLoadingRepositories); // Get loading state
  const [selectedRepos, setSelectedRepos] = useState<string[]>([]);

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
  
 

  const handleToggleRepo = (repoId: string) => {
    setSelectedRepos((prevSelectedRepos) =>
      prevSelectedRepos.includes(repoId)
        ? prevSelectedRepos.filter((id) => id !== repoId) // Deselect
        : [...prevSelectedRepos, repoId] // Select
    );
  };
  
 if (loading) {
        return(
            <div>
                <h1>Repositories</h1>
                <Spinner />
            </div>
        )
    }
  return (
    <Accordion defaultExpanded sx={{ boxShadow: 3, borderRadius: 2 }}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls="repo-list-content"
        id="repo-list-header"
        sx={{ bgcolor: 'primary.main', color: 'white', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Repositories</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          {repositories?.map((repository) => (
            <FormControlLabel
              key={repository.id}
              control={
                <Checkbox
                  checked={selectedRepos.includes(repository.id)}
                  onChange={() => handleToggleRepo(repository.id)}
                  color="primary"
                />
              }
              label={repository.name}
            />
          ))}
        </Box>
      </AccordionDetails>
    </Accordion>
  );
};

export default RepoList;
