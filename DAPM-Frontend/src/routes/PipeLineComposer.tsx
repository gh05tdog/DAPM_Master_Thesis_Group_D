import { AppBar, Box, ThemeProvider, createTheme } from "@mui/material";
import Flow from "../components/PipeLineComposer/Flow.tsx";
import Sidebar from "../components/PipeLineComposer/NodesSidebar.tsx";

import PipelineAppBar from "../components/PipeLineComposer/PipelineAppBar.tsx";
import { Controls, Position } from "reactflow";
import { ReactFlowProvider } from "reactflow";


export default function PipelineComposer(){
    return (
        <ReactFlowProvider>
            <Flow />
            <Box sx={{ display: 'flex' }}>
            <PipelineAppBar />
            <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                <Sidebar />
                <Controls style={{ position: 'fixed', bottom: '0px', left: '240px' }} />
            </Box>
            </Box>
        </ReactFlowProvider>
    )
}