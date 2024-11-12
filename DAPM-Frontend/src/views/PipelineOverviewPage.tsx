import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import MainContentList from '../components/overviews/PipelineOverviewList.tsx'
import { useEffect, useState } from "react";
import PageLayout from './PageLayout.tsx';
import {Box} from "@mui/material";

interface PipelineOverviewPageProps {
  user: any;
  children?: React.ReactNode;
}


const PipelineOverviewPage: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
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
    
    return (
             <PageLayout user={info}>
               <Sidebar />
                 <Box sx={{marginLeft: '250px', display: 'static', minHeight: '100dvh', padding: '10px' }}>
               <MainContentList />
                </Box>
             </PageLayout>

    )
}
export default PipelineOverviewPage;