import { Box } from "@mui/material";
import Header from '../components/MainpageComponents/Header.tsx';
import BackButton from "../components/OverviewPage/Buttons/BackButton.tsx";
import OrganizationSidebar from "../components/OverviewPage/OrganizationSidebar.tsx";
import PipelineAppBar from "../components/PipeLineComposer/PipelineAppBar.tsx";
import PipelineGrid from "../components/OverviewPage/PipelineGrid.tsx";
import Stack from '@mui/material/Stack';

export default function PipelineOverviewPage() {
    return (
        <>
            <Header />
            <BackButton />
        </>

    )
}