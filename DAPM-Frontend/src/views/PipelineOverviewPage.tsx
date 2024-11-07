import Sidebar from '../components/sidebars/Sidebar.tsx'
import MainContent from '../components/overviews/PipelineOverview.tsx'
import { useEffect, useState } from "react";
import PageLayout from './PageLayout.tsx';

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
               <MainContent />
             </PageLayout>

    )
}
export default PipelineOverviewPage;