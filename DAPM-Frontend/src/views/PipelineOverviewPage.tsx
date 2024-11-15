import { Box } from "@mui/material";
import Header from '../components/headers/Header.tsx';
import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import { useEffect, useState } from "react";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Stack } from "@mui/material";

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineOverviewPage: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
    const [mode, setMode] = useState<'light' | 'dark'>('light');
    const [info, setInfo] = useState<any>(null);
    console.log(user);
    useEffect(() => {
      const getUserInfo = async () => {
        const response = await user;
        console.log(response);
        setInfo(response);
      };

      getUserInfo();
    }, [user]);

    console.log(info);

    const theme = createTheme({
        palette: {
            mode: mode,
        },
    });

    return (
      <ThemeProvider theme={theme}>
        <Box sx={{ display: 'flex', height: '100vh'  }}>
          {/* Sidebar */}
          <Sidebar />

          {/* Main Area */}
          <Box sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
            {/* Header at the top */}
            <Header setMode={setMode} currentMode={mode} />
            {/* Main Content below the Header */}
            <MainContent />
          </Box>
        </Box>
      </ThemeProvider>
    )
}
export default PipelineOverviewPage;