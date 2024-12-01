import List from '@mui/material/List/List';
import ListItem from '@mui/material/ListItem/ListItem';
import { Node } from "reactflow";
import { Box, InputLabel, ListItemText, MenuItem, Select } from '@mui/material';
import { DataSinkNodeData, NodeData, OrganizationNodeData } from '../../../state_management/states/pipelineState.ts';
import { useDispatch, useSelector } from 'react-redux';
import { updateNode } from '../../../state_management/slices/pipelineSlice.ts';
import { getNodes } from '../../../state_management/selectors/indexSelector.ts';
import { getRepositories } from '../../../state_management/selectors/apiSelector.ts';


export interface AlgorithmConfugurationProps {
  nodeprop: Node<NodeData> | undefined;
}

export default function DataSinkConfiguration({ nodeprop }: AlgorithmConfugurationProps) {

  const dispatch = useDispatch()

  const node = useSelector(getNodes)?.find(node => node.id === nodeprop?.id)  as Node<DataSinkNodeData> | undefined;;

  const parentNode = useSelector(getNodes)?.find(n => n.id === node?.parentNode) as Node<OrganizationNodeData> | undefined;

  const repositories = useSelector(getRepositories).filter(repository => repository.organizationId === parentNode?.data?.instantiationData?.organization?.id);

  const setLogData = (repository: string) => {
    dispatch(updateNode(
      {
        ...node!,
        data: {
          ...node?.data!,
          instantiationData: {
            repository: repositories.find(r => r.id === repository)
          }
        }
      }))
  }

  return (
    <List>
      <>
        <ListItem>
          <ListItemText primary={`Organization - ${parentNode?.data?.label}`} />
        </ListItem>
        <ListItem>
          <Box sx={{ width: '100%', display: "flex", flexDirection: "column" }}>
            <InputLabel id="demo-simple-select-standard-label">Please select the repository</InputLabel>
            <Select
              labelId="algorithm-simple-select-label"
              id="algorithm-simple-select"
              value={node?.data.instantiationData?.repository?.id ?? ""}
              sx={{ width: '100%' }}
              onChange={(event) => setLogData(event?.target.value as string)}
            >
              {repositories.map((repository) => <MenuItem value={repository.id}>{repository.name}</MenuItem>)}
            </Select>
          </Box>
        </ListItem>
      </>
    </List>
  );
}
