// Definition: Contains the styles for the components
//
import { styled } from '@mui/material/styles';
import { Box } from '@mui/material';

export const Container = styled(Box)`
  display: flex;
  flex-direction: column;
  height: 100vh;
  background-color: #e0e0e0;
  border-radius: 10px;
`;

export const TopBar = styled(Box)`
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #607d8b;
  padding: 20px;
  border-top-left-radius: 10px;
  border-top-right-radius: 10px;
`;

export const BottomContainer = styled(Box)`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  background-color: #f0f0f0;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
  overflow-y: auto;
  padding: 20px;
`;