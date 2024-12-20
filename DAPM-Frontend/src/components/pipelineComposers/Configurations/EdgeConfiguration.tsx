import List from '@mui/material/List/List';
import ListItem from '@mui/material/ListItem/ListItem';
import { Edge } from "reactflow";
import { Box, TextField } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { updateEdge } from '../../../state_management/slices/pipelineSlice.ts';
import { getActiveFlowData, getNodes } from '../../../state_management/selectors/indexSelector.ts';


export interface AlgorithmConfugurationProps {
    edgeProp: Edge | undefined;
}

export default function EdgeConfiguration({ edgeProp }: AlgorithmConfugurationProps) {

    const dispatch = useDispatch()

    const edge = useSelector(getActiveFlowData)?.edges.find(edge => edge.id === edgeProp?.id);

    const setFilename = (edgeText: string) => {
        dispatch(updateEdge({ ...edge!, data: { filename: edgeText } }))
    }
    return (
        <List>
            <>
                <ListItem>
                    <Box sx={{ width: '100%', display: "flex", flexDirection: "column" }}>
                        <TextField value={edge?.data?.filename} id="outlined-basic" label="Filename" variant="outlined" onChange={(event) => setFilename(event?.target.value as string)} />
                    </Box>
                </ListItem>
            </>
        </List >
    );
}
