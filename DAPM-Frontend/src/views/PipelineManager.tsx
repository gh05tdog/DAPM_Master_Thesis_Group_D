import Header from '../components/headers/Header.tsx';
import PipelineManagerSearch from '../components/searchFields/PipelineManageSearch.tsx';
import { useEffect, useState } from "react";
import PipelineManageTable from '../components/overviews/PipelineManageTable.tsx';
import {Box} from "@mui/material";
interface PipelineOverviewPageProps {
  user: any;
}

const Users = [
  { id: 1, surfsup: 'gg', name: 'Olivia Rhye' },
  { id: 2, name: 'Ciaran Murray' },
  { id: 3, name: 'Marina Macdonald' },
  { id: 4, name: 'Charles Fulton' },
  { id: 5, name: 'Jay Hoper' },
  { id: 6, name: 'Steve Hampton' },
  { id: 7, name: 'Liam Peterson' },
  { id: 8, name: 'Ava Martinez' },
  { id: 9, name: 'Mia Robinson' },
  { id: 10, name: 'Sophia Johnson' },
  { id: 11, name: 'James Brown' },
  { id: 12, name: 'Emily Davis' },
];

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
           <PipelineManageTable data = {Users}/>
            </Box>
      </>
    )
};



export default PipelineManager;