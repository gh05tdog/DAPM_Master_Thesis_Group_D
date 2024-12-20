import { Box } from "@mui/material";
import Header from '../components/headers/Header.tsx';
import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import { useEffect, useState } from "react";
import { createTheme, ThemeProvider } from "@mui/material/styles";

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineOverviewPage: React.FC<PipelineOverviewPageProps> = ({ user }) => {
  const [mode, setMode] = useState<'light' | 'dark'>('light');
  const [info, setInfo] = useState<any>(null);
  useEffect(() => {
    const getUserInfo = async () => {
      const response = await user;
      setInfo(response);
    };

    getUserInfo();
  }, [user]);


  const theme = createTheme({
    palette: {
      mode: mode,
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <Header setMode={setMode} currentMode={mode} />
      <Box
        data-qa="Pipeline Overview Page"
        sx={{ display: 'flex', height: '100vh' }}>
        <MainContent />
        <Sidebar />
      </Box>
    </ThemeProvider>
  )
}
export default PipelineOverviewPage;