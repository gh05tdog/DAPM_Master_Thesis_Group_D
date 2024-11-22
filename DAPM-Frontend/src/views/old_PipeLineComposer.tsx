import {  Box } from "@mui/material";
import Flow from "../components/pipelineComposers/Flow.tsx";
import Sidebar from "../components/pipelineComposers/NodesSidebar.tsx";

import PipelineAppBar from "../components/pipelineComposers/PipelineAppBar.tsx";
import { Controls} from "reactflow";
import { ReactFlowProvider } from "reactflow";
import {useParams} from "react-router-dom";


export default function PipelineComposer(){
    const { id } = useParams<{ id: string }>();
    return (
        <ReactFlowProvider>
            <Flow />
            <Box sx={{ display: 'flex' }}>
            <PipelineAppBar pipelineId={id} />
            <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                <Sidebar />
                <Controls style={{ position: 'fixed', bottom: '0px', left: '240px' }} />
            </Box>
            </Box>
        </ReactFlowProvider>
    )
}