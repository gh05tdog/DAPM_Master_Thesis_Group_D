import React from 'react';
import { ReactFlow, Handle, Node } from 'reactflow';
import { Edge } from 'reactflow';
import DataSourceNode from '../pipelineComposers/Nodes/DataSourceNode.tsx';
import DataSinkNode from '../pipelineComposers/Nodes/DataSinkNode.tsx';
import CustomNode from '../pipelineComposers/Nodes/CustomNode.tsx';
import OrganizationImageNode from './OrganizationImageNode.tsx';

interface FlowDiagramProps {
    nodes: Node[];
    edges: Edge[];
}

export const nodeTypes = {
    dataSource: DataSourceNode,
    dataSink: DataSinkNode,
    operator: CustomNode,
    organization: OrganizationImageNode
  };

const FlowDiagram: React.FC<FlowDiagramProps> = ({ nodes, edges }) => (
    <ReactFlow nodeTypes={nodeTypes} nodes={nodes} edges={edges} fitView />
);

export default FlowDiagram;