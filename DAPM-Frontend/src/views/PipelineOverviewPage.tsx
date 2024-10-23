import { Box } from "@mui/material";
import Header from '../components/headers/Header.tsx';
import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import { useEffect, useState } from "react";

interface PipelineOverviewPageProps {
  user: any;
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
        <>
             <Header userInfo={info}/>
             <Box sx={{ display: 'flex', height: '100vh' }}>
               <Sidebar />
               <MainContent />
             </Box>
        </>

    )
}
export default PipelineOverviewPage;