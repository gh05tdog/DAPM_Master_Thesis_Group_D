import {
    type EdgeProps,
  BezierEdge} from 'reactflow';

import { EdgeData } from '../../../state_management/states/pipelineState.ts';
import { useSelector } from 'react-redux';
import { getNodes } from '../../../state_management/selectors/indexSelector.ts';


export function DefaultEdge({id, data, style, selected, source, target, sourceHandleId, targetHandleId, ...delegated}: EdgeProps<EdgeData>) {

  const nodes = useSelector(getNodes);
  
  const sourceNode = nodes?.find(node => node.id === source);
  const targetNode = nodes?.find(node => node.id === target);

  const sourceHandle = sourceNode?.data.templateData.sourceHandles.find(handle => handle.id === sourceHandleId);
  const targetHandle = targetNode?.data.templateData.targetHandles.find(handle => handle.id === targetHandleId)

  const strokeColor = ((targetNode?.type === 'dataSink' && (!data?.filename || data.filename === '')) || (targetNode?.type !== 'dataSink' && (sourceHandle?.type !== targetHandle?.type))) ? 'red' : 'white';

  return (
    <>
      <BezierEdge
        id={id}
        target={target}
        source={source}
        {...delegated}
        style={{
          ...style,
          strokeWidth: 2,
          stroke: selected ? '#007bff' : strokeColor,
        }}
      />
    </>
  );
}

