import * as React from 'react';
import {Card, CardActions, CardContent, CardMedia, Button, Typography, Box} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setActivePipeline } from '../../state_management/slices/pipelineSlice.ts';

export interface PipelineCardProps {
  id: string;
  name: string;
  imgData: string;
}

export default function PipelineCard({ id, name, imgData }: PipelineCardProps) {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const navigateToPipeline = () => {
    dispatch(setActivePipeline(id));
    navigate('/pipeline');
  };

    const navigateToManage = () => {
        dispatch(setActivePipeline(id));
        navigate('/manage-pipeline');
    };

  return (
    <Card sx={{ width: '100%', borderRadius: 2, boxShadow: 3 }}>
      <CardMedia
        sx={{ height: 180 }}
        image={imgData || 'https://via.placeholder.com/150'}  // Fallback image if imgData is missing
        title="Pipeline Preview"
      />
      
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {name || 'Unnamed Pipeline'} 
        </Typography>
      </CardContent>
      <CardContent> 
      <Button variant="contained" color="success">
        Success
      </Button>
      </CardContent>
      <CardActions>
          <Box sx={{ width: '100%', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <Button variant="outlined" size="small" color="primary" onClick={navigateToPipeline}>
                  Edit Pipeline
              </Button>
              <Button variant="contained" color="primary" sx={{ marginRight: 2 }} onClick={navigateToManage}>
                  Pipeline Manager
              </Button>
          </Box>
      </CardActions>
    </Card>
  );
}
