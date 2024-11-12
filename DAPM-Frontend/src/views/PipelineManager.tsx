import React, { useEffect, useState } from 'react';
import Header from '../components/headers/Header.tsx';
import PipelineManageSearch from '../components/searchFields/PipelineManageSearch.tsx';
import { Box } from "@mui/material";
import PipelineManageTable from '../components/overviews/PipelineManageTable.tsx';

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineManager: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
  const [info, setInfo] = useState<any>(null);
  const [selectedPipeline, setSelectedPipeline] = useState(null); // Added state

  useEffect(() => {
    const getUserInfo = async () => {
      const response = await user;
      setInfo(response);
    };
    getUserInfo();
  }, [user]);

  return (
    <>
      <Header userInfo={info}/>
      {/* Pass setSelectedPipeline to PipelineManageSearch */}
      <PipelineManageSearch setSelectedPipeline={setSelectedPipeline} />
      <Box sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
        {/* Pass selectedPipeline to PipelineManageTable */}
        <PipelineManageTable selectedPipeline={selectedPipeline} />
      </Box>
    </>
  )
};

export default PipelineManager;