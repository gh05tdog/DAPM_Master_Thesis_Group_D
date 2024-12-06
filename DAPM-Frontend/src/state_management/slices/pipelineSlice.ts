import { addEdge as addFlowEdge, applyEdgeChanges, applyNodeChanges, Connection, Edge, EdgeChange, Node, NodeChange } from "reactflow";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { EdgeData, NodeData, NodeState, PipelineData, PipelineState } from "../states/pipelineState.ts";
import { Organization, Repository } from "../states/apiState.ts";
import {fetchPipeline, fetchRepositoryPipelines} from "../../services/backendAPI.tsx";

export const initialState: PipelineState = {
  pipelines: [],
  activePipelineId: "",
  activeOrganisationId: "",
  activeRepositoryId: ""
}

const takeSnapshot = (state: PipelineState) => {
  var activePipeline = state.pipelines.find((pipeline: { id: any; }) => pipeline.id === state.activePipelineId)
  if (!activePipeline) return
  activePipeline?.history?.past?.push({nodes: activePipeline.pipeline.nodes, edges: activePipeline.pipeline.edges})
}


export const pipelineThunk = createAsyncThunk<
    PipelineData[],
    { organizations: any[]; repositories: any[] }
>(
    "pipelines/fetchPipelines",
    async ({ organizations, repositories }, thunkAPI) => {
      try {
        const pipelinePromises: Promise<PipelineData[]>[] = [];

        for (const org of organizations) {
          for (const repo of repositories) {
            if (org.id === repo.organizationId) {
              const pipelinePromise = fetchRepositoryPipelines(org.id, repo.id).then(
                  async (pipes) => {
                    const pipelineDetailsPromises = pipes.result.pipelines.map(
                        async (pipeline: PipelineData) => {
                          const pipelineData = await fetchPipeline(
                              org.id,
                              repo.id,
                              pipeline.id
                          );

                          // Log pipeline data before transformation
                          console.log("Pipeline Data (before transformation):", pipelineData.result.pipelines);

                          const transformedPipelines = pipelineData.result.pipelines.map((pipelineDetails: any) => {
                            const transformedPipeline = {
                              id: pipelineDetails.id,
                              name: pipelineDetails.name,
                              orgId: org.id,
                      repoId: repo.id,status: "unknown",
                              pipeline: transformPipelineData(pipelineDetails.pipeline),
                              history: {
                                past: [],
                                future: [],
                              },
                            };

                            // Log pipeline data after transformation
                            console.log("Pipeline Data (after transformation):", transformedPipeline);

                            return transformedPipeline;
                          });

                          return transformedPipelines;
                        }
                    );

                    const detailsArray = await Promise.all(pipelineDetailsPromises);
                  return detailsArray.flat();
                  }
              );

              pipelinePromises.push(pipelinePromise);
            }
          }
        }

        const pipelines = await Promise.all(pipelinePromises);
        return pipelines.flat();
      } catch (error) {
        return thunkAPI.rejectWithValue(error);
      }
    }
);

// Transform pipeline data to match the expected state structure
const transformPipelineData = (pipeline: any) => ({
  nodes: pipeline.nodes.map((node: any) => ({
    id: node.id,
    type: node.type,
    position: node.position,
    data: {
      label: node.data.label,
      templateData: {
        sourceHandles: node.data.templateData.sourceHandles.map((handle: any) => ({
          id: handle.id,
          type: handle.type,
        })),
        targetHandles: node.data.templateData.targetHandles.map((handle: any) => ({
          id: handle.id,
          type: handle.type,
        })),
        hint: node.data.templateData.hint,
      },
      instantiationData: node.data.instantiationData,
    },
    width: node.width,
    height: node.height,
  })),
  edges: pipeline.edges.map((edge: any) => ({
    id: `${edge.source}-${edge.sourceHandle}-${edge.target}-${edge.targetHandle}`,
    source: edge.source,
    target: edge.target,
    sourceHandle: edge.sourceHandle,
    targetHandle: edge.targetHandle,
  })),
});



const pipelineSlice = createSlice({
  name: 'pipelines',
  initialState: initialState,
  reducers: {
    addNewPipeline: (state, { payload }: PayloadAction<{ id: string, flowData: NodeState }>) => {
      state.pipelines.push({ id: payload.id, name: 'unnamed pipeline', pipeline: payload.flowData, history: { past: [], future: []}, imgData: '', status: 'not started'} as PipelineData)
      state.activePipelineId = payload.id
    },
    setActivePipeline: (state, { payload }: PayloadAction<string>) => {
      state.activePipelineId = payload
    },
    setActiveOrganisation: (state, { payload }: PayloadAction<string>) => {
      state.activeOrganisationId = payload
    },
    setActiveRepository: (state, { payload }: PayloadAction<string>) => {
      state.activeRepositoryId = payload
    },
    // actions for undo and redo

    undo(state){
      var activePipeline = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)
      if (!activePipeline) return
      const pastState = activePipeline?.history?.past?.pop()
      if (!pastState) return

      activePipeline.history.future.push({nodes: activePipeline.pipeline.nodes, edges: activePipeline.pipeline.edges})
      activePipeline.pipeline.nodes = pastState.nodes
      activePipeline.pipeline.edges = pastState.edges
    },
    redo(state){
      var activePipeline = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)
      if (!activePipeline) return
      const futureState = activePipeline?.history?.future?.pop()
      if (!futureState) return

      activePipeline.pipeline.nodes = futureState.nodes
      activePipeline.pipeline.edges = futureState.edges
    },
    createSnapShot(state){
      takeSnapshot(state)
    },
    
    // actions for the active pipeline
    
    updatePipelineName: (state, { payload }: PayloadAction<string>) => {
      var activePipeline = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)
      if (!activePipeline) return
      activePipeline!.name = payload
    },
    addHandle: (state, { payload }: PayloadAction<string>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      activeFlowData?.nodes.find(node => node.id === payload)?.data?.templateData?.sourceHandles.push({ type: 'source', id: "1" })
    },
    updateSourceHandle: (state, { payload }: PayloadAction<{ nodeId?: string, handleId?: string, newType?: string }>) => {
      const { nodeId, handleId, newType } = payload;
      // Find the active pipeline based on the activePipelineId
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      
      if (!activeFlowData) return; // Early exit if no active pipeline is found
    
      // Find the node within the active pipeline's flowData that matches the nodeId
      const targetNode = activeFlowData.nodes.find(node => node.id === nodeId);

      if (!targetNode) return; // Early exit if no matching node is found
    
      // Initialize templateData and sourceHandles if they are not defined
      if (!targetNode.data.templateData?.sourceHandles) return; // Early exit if templateData or sourceHandles are not defined
    
      // Find the handle to update within the sourceHandles
      const handleToUpdate = targetNode.data.templateData.sourceHandles.find(handle => handle.id === handleId);
    
      if (!handleToUpdate) return; // Early exit if no matching handle is found

      // Update the handle's type
      handleToUpdate.type = newType;
    },
    updateTargetHandle: (state, { payload }: PayloadAction<{ nodeId?: string, handleId?: string, newType?: string }>) => {
      const { nodeId, handleId, newType } = payload;
      // Find the active pipeline based on the activePipelineId
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      
      if (!activeFlowData) return; // Early exit if no active pipeline is found
    
      // Find the node within the active pipeline's flowData that matches the nodeId
      const targetNode = activeFlowData.nodes.find(node => node.id === nodeId);

      if (!targetNode) return; // Early exit if no matching node is found
    
      // Initialize templateData and sourceHandles if they are not defined
      if (!targetNode.data.templateData?.targetHandles) return; // Early exit if templateData or sourceHandles are not defined
    
      // Find the handle to update within the sourceHandles
      const handleToUpdate = targetNode.data.templateData.targetHandles.find(handle => handle.id === handleId);
    
      if (!handleToUpdate) return; // Early exit if no matching handle is found

      // Update the handle's type
      handleToUpdate.type = newType;
    },
    
    updateNode: (state, { payload }: PayloadAction<Node<NodeData> | undefined>) => {
      if (!payload) return
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      const index = activeFlowData?.nodes.findIndex(node => node.id === payload.id)
      activeFlowData.nodes[index] = payload
    },
    addNode: (state, { payload }: PayloadAction<Node>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      
      activeFlowData.nodes.push(payload)
    },
    removeNode: (state, { payload }: PayloadAction<Node<NodeData>>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      //takeSnapshot(state)

      activeFlowData.nodes = activeFlowData.nodes.filter(node => node.id !== payload.id && node.parentNode !== payload.id)
      activeFlowData.edges = activeFlowData.edges.filter(edge =>
        !payload.data?.templateData?.sourceHandles.find(data => data.id === edge.sourceHandle) &&
        !payload.data?.templateData?.targetHandles.find(data => data.id === edge.targetHandle))
    },
    removeEdge: (state, { payload }: PayloadAction<Edge>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      //takeSnapshot(state)

      activeFlowData!.edges = activeFlowData?.edges.filter(edge => edge.id !== payload.id)
    },
    updateEdge: (state, { payload }: PayloadAction<Edge<EdgeData> | undefined>) => {
      if (!payload) return
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      const index = activeFlowData.edges.findIndex(edge => edge.id === payload.id)
      const strokeColor = payload.data?.filename === undefined || payload.data?.filename === '' || payload.data?.filename === null ? 'red' : 'white'
      activeFlowData.edges[index] = { ...payload }
    },
    // From react flow example
    onNodesChange: (state, { payload }: PayloadAction<NodeChange[]>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return

      activeFlowData.nodes = applyNodeChanges(payload, activeFlowData.nodes);
    },
    onEdgesChange: (state, { payload }: PayloadAction<EdgeChange[]>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return

      activeFlowData.edges = applyEdgeChanges(payload, activeFlowData.edges);
    },
    onConnect: (state, { payload }: PayloadAction<Connection>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return
      takeSnapshot(state)

      const strokeColor = activeFlowData.nodes.find(node => node.id == payload.target)?.type === 'dataSink' ? 'red' : 'white'

      activeFlowData.edges = addFlowEdge({ ...payload, type: 'default'}, activeFlowData.edges);
    },
    setNodes: (state, { payload }: PayloadAction<Node[]>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return

      activeFlowData.nodes = payload;
    },
    setEdges: (state, { payload }: PayloadAction<Edge[]>) => {
      var activeFlowData = state.pipelines.find(pipeline => pipeline.id === state.activePipelineId)?.pipeline
      if (!activeFlowData) return

      activeFlowData.edges = payload;
    },
  },
  extraReducers: (builder) => {
    builder
        .addCase(pipelineThunk.fulfilled, (state, action) => {
          state.pipelines = action.payload;
        })
        .addCase(pipelineThunk.rejected, (state, action) => {
          console.error("Pipeline thunk failed", action.error);
        });
  },
});

export const { 
  //actions for all pipelines
  addNewPipeline, 
  setActivePipeline, 
  setActiveOrganisation,
  setActiveRepository,
  
  // actions for undo and redo
  undo,
  redo,
  createSnapShot,

  // actions for the active pipeline
  updateSourceHandle,
  updateTargetHandle,
  updatePipelineName, 
  addHandle, 
  updateNode, 
  addNode, 
  removeNode, 
  removeEdge, 
  updateEdge, 
  onNodesChange, 
  onEdgesChange, 
  onConnect, 
  setNodes, 
  setEdges 
} = pipelineSlice.actions

export default pipelineSlice.reducer;
