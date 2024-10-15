import { Box } from "@mui/material";
import OrganizationSidebar from "../components/OverviewPage/OrganizationSidebar.tsx";
import PipelineAppBar from "../components/PipeLineComposer/PipelineAppBar.tsx";
import PipelineGrid from "../components/OverviewPage/PipelineGrid.tsx";
import Stack from '@mui/material/Stack';

export default function UserPage() {
    return (
        <Stack direction="column" spacing={20}>
           <div>
                <PipelineAppBar />
            </div>

            <div>
                <Box sx={{display: 'flex'}}>
                    <OrganizationSidebar />
                    <PipelineGrid />
                </Box>
            </div>
         </Stack>
    )
}