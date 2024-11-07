import Header from '../components/headers/Header.tsx';
import PipelineManagerSearch from '../components/searchFields/PipelineManageSearch.tsx';
import { useEffect, useState } from "react";
import PipelineManageTable from '../components/overviews/PipelineManageTable.tsx';
import {Box} from "@mui/material";
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
          <Box sx={{ display: 'static', minHeight: '100dvh', padding: '10px' }}>
           <PipelineManageTable/>
            </Box>
      </>
    )
};



export default PipelineManager;