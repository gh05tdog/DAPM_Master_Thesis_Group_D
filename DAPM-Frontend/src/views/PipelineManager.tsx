import Header from '../components/headers/Header.tsx';
import PipelineManagerSearch from '../components/searchFields/PipelineManageSearch.tsx';
import { useEffect, useState } from "react";

interface PipelineOverviewPageProps {
  user: any;
}

const PipelineManager: React.FC<PipelineOverviewPageProps> = ({ user }) => { 
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
           <PipelineManagerSearch />
      </>
    )
};



export default PipelineManager;