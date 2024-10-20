import React from "react";
import { Box, Typography, Stack } from "@mui/material";
import PipelineGrid from "../overviews/old_PipelineOverview.tsx";

const Index: React.FC = () => {
  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        height: '100vh',
        backgroundColor: '#e0e0e0',
        borderRadius: 2,
        padding: 3,
      }}
    >
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          backgroundColor: '#607d8b',
          padding: 2,
          borderRadius: '10px 10px 0 0',
        }}
      >
        <Typography variant="h5" sx={{ color: 'white', fontWeight: 'bold' }}>
          Build
        </Typography>
      </Box>

      <Box
        sx={{
          flexGrow: 1,
          backgroundColor: '#f0f0f0',
          padding: 2,
          borderRadius: '0 0 10px 10px',
          overflowY: 'auto',
        }}
      >
        <Stack direction="column" spacing={2}>
          <PipelineGrid />
        </Stack>
      </Box>
    </Box>
  );
};

export default Index;
