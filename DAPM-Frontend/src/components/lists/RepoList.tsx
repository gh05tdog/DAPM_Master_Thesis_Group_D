import React, { useCallback, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations, selectLoadingOrganisation, selectLoadingRepositories } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk } from "../../state_management/slices/apiSlice.ts";
import Spinner from '../cards/SpinnerCard.tsx';
import RepositoryCard from "../cards/RepositoryCard.tsx";
import { Accordion, AccordionSummary, AccordionDetails, Checkbox, FormControlLabel, Typography, Box } from "@mui/material";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { setActiveRepository } from "../../state_management/slices/pipelineSlice.ts";
import { getActiveRepository } from "../../state_management/slices/indexSlice.ts";
const RepoList: React.FC = () => {
  const dispatch = useDispatch();
  // Get organizations and repositories from the store
  const organizations = useSelector(getOrganizations);
  const repositories = useSelector(getRepositories);
  const selectedRepo = useSelector(getActiveRepository);
  const Orgsloading = useSelector(selectLoadingOrganisation); // Get loading state
  const loading = useSelector(selectLoadingRepositories); // Get loading state
  const [selectedRepos, setSelectedRepos] = useState<string[]>([]);

  useEffect(() => {
    if ((!Orgsloading) && organizations.length > 0) {
      try {
        dispatch(repositoryThunk(organizations));
      } catch (error) {
        console.error(error);
      }
    }
  }, [dispatch, organizations]);



  const handleToggleRepo = useCallback((repoId: string) => {
    setSelectedRepos((prevSelectedRepos) =>
      prevSelectedRepos.includes(repoId)
        ? prevSelectedRepos.filter((id) => id !== repoId) // Deselect
        : [...prevSelectedRepos, repoId] // Select
    );
    dispatch(setActiveRepository(repoId));
  },[selectedRepo]);
  
  if (loading) {
    return (
      <Box>
        <Accordion disabled sx={{ boxShadow: 3, borderRadius: 2 }}>
          <AccordionSummary aria-controls="org-list-content" id="org-list-header" sx={{ bgcolor: 'primary.main', color: 'primary.contrastText', borderRadius: '4px' }}>
            <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Repositories</Typography>
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
        aria-controls="repo-list-content"
        id="repo-list-header"
        sx={{ bgcolor: 'primary.main', color: 'white', borderRadius: '4px' }}
      >
        <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Repositories</Typography>
      </AccordionSummary>
      <AccordionDetails>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
          {repositories?.map((repository) => (
            <RepositoryCard
              key={repository.id}
              repository={repository}
              isChecked={selectedRepos.includes(repository.id)}
              handleToggle={() => handleToggleRepo(repository.id)}
            />
          ))}
        </Box>
      </AccordionDetails>
    </Accordion>
  );
};

export default RepoList;
