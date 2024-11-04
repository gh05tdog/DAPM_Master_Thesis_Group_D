import React from 'react';
import {AppBar, Box, Button, Toolbar, Typography} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import {useSelector} from "react-redux";
import {getActivePipeline} from "../../state_management/selectors/index.ts";

const PipelineManageHeader: React.FC = () => {

    const navigate = useNavigate();

    const pipelineName = useSelector((state: any) => getActivePipeline(state)?.name)

    const returnToOverview = () => {
        navigate("/user");
    };

    return (
        <AppBar
            position="static"
            sx={{ bgcolor: 'rgba(54,55,56,1)', paddingX: 3 }}
            elevation={3}
        >
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <Typography variant="h6" sx={{maringLeft: 2, fontWeight: 'bold', whiteSpace: 'nowrap' }}>
                        {pipelineName}
                    </Typography>

                    <Button variant="contained"
                            color="primary"
                            sx={{marginRight: 2}}
                            onClick = {returnToOverview}>
                        Go forward my trusty dusty
                    </Button>
                </Box>
        </AppBar>

    );
};

export default PipelineManageHeader;
    