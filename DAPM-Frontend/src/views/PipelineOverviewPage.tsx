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
      <div>
        <Box sx={{ display: 'flex' }}>
          <Sidebar />
          <Box
          component="main"
          sx={(theme) => ({
            flexGrow: 1,
            overflow: 'auto',
          })}
        >
          <Stack
            spacing={2}
            sx={{
              alignItems: 'center',
              mx: 3,
              pb: 5,
              mt: { xs: 8, md: 0 },
            }}
          >
            <Header setMode={setMode} currentMode={mode} />
            <MainContent />
          </Stack>
        </Box>
        </Box>
      </div>
    </ThemeProvider>

    )
}
export default PipelineOverviewPage;