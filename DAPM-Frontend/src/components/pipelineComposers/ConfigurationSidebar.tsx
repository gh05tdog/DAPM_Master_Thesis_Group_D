import { styled } from '@mui/material/styles';
import Drawer from '@mui/material/Drawer/Drawer';
import Typography from '@mui/material/Typography/Typography';
import Divider from '@mui/material/Divider/Divider';
import { Edge, Node } from "reactflow";
import { NodeData } from '../../state_management/states/pipelineState.ts';
import { getEdges, getNodes } from '../../state_management/selectors/indexSelector.ts';
import { useSelector } from 'react-redux';
import AlgorithmConfiguration from './Configurations/AlgorithmConfiguration.tsx';
import DataSourceConfiguration from './Configurations/DataSourceConfiguration.tsx';
import DataSinkConfiguration from './Configurations/DataSinkConfiguration.tsx';
import OrganizationConfiguration from './Configurations/OrganizationConfiguration.tsx';
import EdgeConfiguration from './Configurations/EdgeConfiguration.tsx';

const drawerWidth = 240;

const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
  justifyContent: 'flex-end',
}));

export interface ConfigurationSidebarProps {
  selectableProp: Node<NodeData> | Edge | undefined;
}

export default function PersistentDrawerRight({ selectableProp }: ConfigurationSidebarProps) {

  const node = useSelector(getNodes)?.find(node => node.id === selectableProp?.id);
  const edge = useSelector(getEdges)?.find(edge => edge.id === selectableProp?.id);

  const edgeEndNode = useSelector(getNodes)?.find(node => node.data.templateData.targetHandles.find(handle => handle.id === edge?.targetHandle));

  if (edgeEndNode !== undefined && edgeEndNode?.type !== "dataSink")
    return (null)

  return (
    <Drawer
      PaperProps={{
        sx: {
          backgroundColor: '#292929',
          position: 'fixed',
          top: '64px',
          height: 'calc(100vh - 64px)',
        }
      }}
      sx={{
        width: drawerWidth,
        position: 'static',
        flexGrow: 1,
      }}
      variant="permanent"
      anchor="right"
    >
      <Divider />
      <DrawerHeader>
        <Typography sx={{ width: '100%', textAlign: 'center' }} variant="h6" noWrap component="div">
          Configuration
        </Typography>
      </DrawerHeader>
      <Divider />
      {node?.type === "operator" && <AlgorithmConfiguration nodeprop={selectableProp as Node<NodeData>} />}
      {node?.type === "dataSource" && <DataSourceConfiguration nodeprop={selectableProp as Node<NodeData>} />}
      {node?.type === "dataSink" && <DataSinkConfiguration nodeprop={selectableProp as Node<NodeData>} />}
      {node?.type === "organization" && <OrganizationConfiguration nodeprop={selectableProp as Node<NodeData>} />}
      {edge && <EdgeConfiguration edgeProp={selectableProp as Edge} />}
    </Drawer>
  );
}
