import React, { useEffect, useState } from 'react';
import { Card, CardContent, Typography } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { fetchExecutionStatus } from '../../services/backendAPI.tsx';
import { getOrganizations, getRepositories,  } from '../../state_management/selectors/apiSelector.ts';
import { organizationThunk, repositoryThunk } from '../../state_management/slices/apiSlice.ts';

const PipelineStatusCard: React.FC<{ pipelineId: string }> = ({ pipelineId }) => {
  const [status, setStatus] = useState<string>('Loading...');
  const [error, setError] = useState<string | null>(null);

    const dispatch = useDispatch();
    
    // Get organizations and repositories from the store
    const organizations = useSelector(getOrganizations);
    const repositories = useSelector(getRepositories);

  // Polling function to fetch the pipeline status every 30 seconds
  const fetchPipelineStatus = async () => {
    try {
            for (const repo of repositories) {
                const executionId = localStorage.getItem('pipelineId') || '';
                console.log('Execution ID:', executionId);
                const response = await fetchExecutionStatus(repo.organizationId, repo.id, pipelineId, executionId);
                setStatus(response.data.status);
            }
    } catch (err) {
      setError('Failed to fetch status');
    }
  };
    // Fetch organizations on component mount
    useEffect(() => {
    dispatch(organizationThunk());
}, [dispatch]);

    // Fetch repositories once organizations are available
    useEffect(() => {
        if (organizations.length > 0) {
            try {
                dispatch(repositoryThunk(organizations));
            }
            catch (error) {
                console.error(error);    
            }
        }

    }, [dispatch, organizations]);
    useEffect(() => {
    // Fetch the status immediately when the component is mounted
    fetchPipelineStatus();

    // Set up a polling interval (e.g., every 30 seconds)
    const interval = setInterval(() => {
      fetchPipelineStatus();
    }, 30000); // 30 seconds

    // Clear the interval when the component is unmounted
    return () => clearInterval(interval);
  },);

  return (
    <Card sx={{ maxWidth: 300, margin: '20px' }}>
      <CardContent>
        <Typography variant="h5">Pipeline Status</Typography>
        {error ? (
          <Typography color="error">{error}</Typography>
        ) : (
          <Typography>{status}</Typography>
        )}
      </CardContent>
    </Card>
  );
};

export default PipelineStatusCard;
