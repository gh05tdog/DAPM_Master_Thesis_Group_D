import React, { useEffect, useState } from 'react';
import {
  Autocomplete,
  TextField,
  FormControl,
  Button,
  MenuItem,
  Select,
  InputLabel,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from '@mui/material';
import Header from '../components/headers/Header.tsx';
import PipelineManageSearch from '../components/searchFields/PipelineManageSearch.tsx';
import PipelineMangePopup from '../components/searchFields/ManagePipelinePopup.tsx';
import { Box } from "@mui/material";
import PipelineManageTable from '../components/overviews/PipelineManageTable.tsx';

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineManager: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
  const [info, setInfo] = useState<any>(null);
  const [selectedPipeline, setSelectedPipeline] = useState<{ pipelineId: string } | null>(null);
  const [openPopup, setOpenPopup] = useState(false); 

  useEffect(() => {
    const getUserInfo = async () => {
      const response = await user;
      setInfo(response);
    };
    getUserInfo();
  }, [user]);

  const handleClosePopup = () => {
    setOpenPopup(false);
};

const handleOpenPopup = () => {
  setOpenPopup(true);
};


  return (
    <>
      <Header userInfo={info}/>
      {/* Pass setSelectedPipeline to PipelineManageSearch */}
      <PipelineManageSearch setSelectedPipeline={setSelectedPipeline} />
      <Box data-qa = "pipeline-manager"
          sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
        {/* Pass selectedPipeline to PipelineManageTable */}
        <PipelineManageTable selectedPipeline={selectedPipeline} />
        <Button
                variant="contained"
                color="primary"
                sx={{ width: '10%' }}
                onClick={handleOpenPopup}
            >
                Add user
            </Button>
        <PipelineMangePopup open={openPopup} onClose={handleClosePopup} selectedPipeline={selectedPipeline} />
      </Box>
    </>
  )
};

export default PipelineManager;