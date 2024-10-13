import { Box } from "@mui/material";
import Header from '../components/MainpageComponents/Header.tsx';
import Sidebar from '../components/MainpageComponents/Sidebar.tsx'
import MainContent from '../components/MainpageComponents/MainContent.tsx'
import BackButton from "../components/OverviewPage/Buttons/BackButton.tsx";
import OrganizationSidebar from "../components/OverviewPage/OrganizationSidebar.tsx";
import PipelineAppBar from "../components/PipeLineComposer/PipelineAppBar.tsx";
import PipelineGrid from "../components/OverviewPage/PipelineGrid.tsx";
import Stack from '@mui/material/Stack';

const PipelineOverviewPage: React.FC = () => {
    return (
        <Box sx={{backgroundColor: 'whitesmoke', display: 'flex', flexDirection: 'column', height: '100vh'}}>
            <Header />
            <Box sx={{ display: 'flex', flex: 1, overflow: 'hidden'}}>
                <Sidebar />
                <Box sx={{flex: 1, overflowY: 'hidden'}}>
                    <MainContent />
                </Box>
            </Box>
        </Box>
    );
}

export default PipelineOverviewPage;