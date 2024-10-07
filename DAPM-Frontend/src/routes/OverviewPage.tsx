import { Box } from "@mui/material";
import OrganizationSidebar from "../components/OverviewPage/OrganizationSidebar.tsx";
import PipelineAppBar from "../components/PipeLineComposer/PipelineAppBar.tsx";
import PipelineGrid from "../components/OverviewPage/PipelineGrid.tsx";

export default function UserPage() {
    return (
        <div>
            <Box sx={{display: 'flex'}}>
                <OrganizationSidebar />
                <PipelineGrid />
            </Box>
        </div>
    )
}