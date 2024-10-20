import * as React from 'react';
import { Card, CardActions, CardContent, CardMedia, Button, Typography } from '@mui/material';
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

  return (
    <Card sx={{ width: '100%', borderRadius: 2, boxShadow: 3 }}>
      {/* CardMedia for pipeline preview image */}
      <CardMedia
        sx={{ height: 180 }}
        image={imgData || 'https://via.placeholder.com/150'}  // Fallback image if imgData is missing
        title="Pipeline Preview"
      />
      
      {/* Card content for pipeline name */}
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {name || 'Unnamed Pipeline'} {/* Fallback if no name */}
        </Typography>
      </CardContent>
      <CardContent> 
      <Button variant="contained" color="success">
        Success
      </Button>
      </CardContent>
      {/* CardActions with Edit button */}
      <CardActions>
        <Button variant="outlined" size="small" color="primary" onClick={navigateToPipeline}>
          Edit Pipeline
        </Button>
      </CardActions>
    </Card>
  );
}
