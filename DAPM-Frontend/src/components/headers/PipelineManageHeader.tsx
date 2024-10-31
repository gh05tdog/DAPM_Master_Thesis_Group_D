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
            <Toolbar sx={{ justifyContent: 'space-between' }}>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    <Typography variant="h6" sx={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}>
                        {pipelineName}
                    </Typography>

                    <Button variant="contained"
                            color="primary"
                            sx={{marginRight: 2}}
                            onClick = {returnToOverview}>
                        Go back
                    </Button>
                </Box>
            </Toolbar>
        </AppBar>

    );
};

export default PipelineManageHeader;
    