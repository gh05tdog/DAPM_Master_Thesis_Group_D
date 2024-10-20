import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box/Box';
import Paper from '@mui/material/Paper/Paper';
import { Button, Card, CardActions, CardContent, CardMedia, Typography } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { addNewPipeline, setImageData } from '../../state_management/slices/pipelineSlice.ts';
import { getPipelines } from '../../state_management/selectors/index.ts';
import FlowDiagram from '../imageGeneration/FlowDiagram.tsx';
import ReactDOM from 'react-dom';
import { toPng } from 'html-to-image';
import { getNodesBounds, getViewportForBounds } from 'reactflow';
import { v4 as uuidv4 } from 'uuid';
import PipelineCard from '../cards/oldPipelineCard.tsx';

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));

export default function AutoGrid() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const pipelines = useSelector(getPipelines);

  const createNewPipeline = () => {
    dispatch(addNewPipeline({ id: `pipeline-${uuidv4()}`, flowData: { nodes: [], edges: [] } }));
    navigate("/pipeline");  // Navigate to pipeline editor after creation
  };

 
  pipelines.map(({ pipeline: flowData, id, name }) => {
    const nodes = flowData.nodes;
    const edges = flowData.edges;
    const pipelineId = id;
    const container = document.createElement('div');
    container.style.position = 'absolute';
    container.style.top = '-10000px';
    container.id = pipelineId;
    document.body.appendChild(container);

    ReactDOM.render(
      <FlowDiagram nodes={nodes} edges={edges} />,
      container,
      () => {
        const width = 800;
        const height = 600;
        const nodesBounds = getNodesBounds(nodes!);
        const { x, y, zoom } = getViewportForBounds(nodesBounds, width, height, 0.5, 2, 1);

        toPng(document.querySelector(`#${pipelineId} .react-flow__viewport`) as HTMLElement, {
          backgroundColor: '#333',
          width: width,
          height: height,
          style: {
            width: `${width}`,
            height: `${height}`,
            transform: `translate(${x}px, ${y}px) scale(${zoom})`,
          },
        }).then((dataUrl) => {
          dispatch(setImageData({ id: pipelineId, imgData: dataUrl }));
          document.body.removeChild(container);
        });
      }
    );
  });

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
      {/* Render the PipelineCard for each pipeline */}
      {pipelines.map(({ id, name, imgData }) => (
        <PipelineCard key={id} id={id} name={name} imgData={imgData} />
      ))}

      {/* New Pipeline Button */}
      <Box sx={{ textAlign: 'center', marginTop: 2 }}>
        <Button
          variant="contained"
          color="success"
          startIcon={<AddIcon />}
          sx={{ borderRadius: 50, backgroundColor: '#4caf50', "&:hover": { backgroundColor: '#388e3c' } }}
          onClick={createNewPipeline}
        >
          Create New
        </Button>
      </Box>
    </Box>
  );
}
