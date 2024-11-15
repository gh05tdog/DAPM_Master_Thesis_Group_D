import React, { useEffect, useState } from 'react';
import {
  Button, ThemeProvider,
} from '@mui/material';
import Header from '../../components/headers/Header.tsx';
import PipelineManageSearch from '../../components/searchFields/PipelineManageSearch.tsx';
import PipelineMangePopup from '../../components/searchFields/ManagePipelinePopup.tsx';
import { Box } from "@mui/material";
import PipelineManageTable from '../../components/overviews/PipelineManageTable.tsx';
import {createTheme} from "@mui/material/styles";

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineManager: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
  const [info, setInfo] = useState<any>(null);
  const [selectedPipeline, setSelectedPipeline] = useState<{ pipelineId: string } | null>(null);
  const [openPopup, setOpenPopup] = useState(false);
  const [mode, setMode] = useState<'light' | 'dark'>('light');
  
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

  const theme = createTheme({
    palette: {
      mode: mode,
    },
  });
  
  return (
      <ThemeProvider theme={theme}>
      <Header setMode={setMode} currentMode={mode} />
      {/* Pass setSelectedPipeline to PipelineManageSearch */}
      
      <Box sx={{display: 'flex', flexDirection: 'row', justifyContent: 'space-between'}}>
        <PipelineManageSearch setSelectedPipeline={setSelectedPipeline} />

        <Button
            variant="contained"
            color="primary"
            sx={{ width: '10%' }}
            onClick={handleOpenPopup}
        >
          Add user
        </Button>
        
      </Box>
      
      
      <Box data-qa = "pipeline-manager"
          sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
        {/* Pass selectedPipeline to PipelineManageTable */}
        <PipelineManageTable selectedPipeline={selectedPipeline} />
        <PipelineMangePopup open={openPopup} onClose={handleClosePopup} selectedPipeline={selectedPipeline} />
      </Box>
      </ThemeProvider>
  )
};

export default PipelineManager;