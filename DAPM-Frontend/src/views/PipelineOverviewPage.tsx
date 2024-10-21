import { Box } from "@mui/material";
import Header from '../components/headers/Header.tsx';
import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import BackButton from "../components/buttons/BackButton.tsx";
import OrganizationSidebar from "../components/sidebars/OrganizationSidebar.tsx";
import PipelineAppBar from "../components/pipelineComposers/PipelineAppBar.tsx";
import PipelineGrid from "../components/overviews/old_PipelineOverview.tsx";
import Stack from '@mui/material/Stack';

const PipelineOverviewPage: React.FC = () => { 
    return (
        <>
             <Header />
             <Box sx={{ display: 'flex', height: '100vh' }}>
               <Sidebar />
               <MainContent />
             </Box>
        </>

    )
}
export default PipelineOverviewPage;