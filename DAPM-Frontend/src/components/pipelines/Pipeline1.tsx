import React from "react";
import styled from "styled-components";
import { Box, Typography } from "@mui/material";
import PipelineGrid from "../overviews/old_PipelineOverview.tsx";

function Index(props) {
  return (
    <Container>
      <TopBar>
        <Typography variant="h5" sx={{ color: 'white', fontWeight: 'bold' }}>Build</Typography>
      </TopBar>
      
      <BottomContainer>
        <Box sx={{ flexGrow: 1, padding: 3, display: 'flex', flexDirection: 'column', gap: '20px' }}>
          <PipelineGrid />
        </Box>
      </BottomContainer>
    </Container>
  );
}

const Container = styled(Box)`
  display: flex;
  flex-direction: column;
  height: 100vh;
  background-color: #e0e0e0;
  border-radius: 10px;
`;

const TopBar = styled(Box)`
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #607d8b;
  padding: 20px;
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
`;

const BottomContainer = styled(Box)`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  background-color: #f0f0f0;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
  overflow-y: auto;
  padding: 20px;
`;

export default Index;
