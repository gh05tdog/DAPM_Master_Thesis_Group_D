import { AppBar, Box, Button, TextField, Toolbar, Typography } from "@mui/material";
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { getActiveFlowData, getActivePipeline } from "../../state_management/selectors/index.ts";
import { useState } from "react";
import { updatePipelineName } from "../../state_management/slices/pipelineSlice.ts";
import { Edit as EditIcon } from '@mui/icons-material';
import { Node } from "reactflow";
import { DataSinkNodeData, DataSourceNodeData, OperatorNodeData } from "../../state_management/states/pipelineState.ts";
import { putCommandStart, putExecution, putPipeline } from "../../services/backendAPI.tsx";
import { getOrganizations, getRepositories } from "../../state_management/selectors/apiSelector.ts";
import { getHandleId, getNodeId } from "./Flow.tsx";

interface PipelineAppBarProps {
  pipelineId: string;
}

export default function PipelineAppBar({ pipelineId }: PipelineAppBarProps) {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [isEditing, setIsEditing] = useState(false);

  const handleStartEditing = () => {
    setIsEditing(true);
  };

  const handleFinishEditing = () => {
    setIsEditing(false);
  };

  const organizations = useSelector(getOrganizations)
  const repositories = useSelector(getRepositories)

  const pipelineName = useSelector((state: any) => getActivePipeline(state)?.name)

  const setPipelineName = (name: string) => {
    dispatch(updatePipelineName(name))
  }

  const flowData = useSelector(getActiveFlowData) as { edges: any[], nodes: any[] } | undefined

  const generateJson = async () => {

    //console.log(flowData)

    var edges = flowData!.edges.map(edge => {
      return { sourceHandle: edge.sourceHandle, targetHandle: edge.targetHandle }
    })

    console.log("copied", edges)

    const dataSinks = flowData?.edges.map((edge) => {
      if (edge.data?.filename) {
        const newTarget = getHandleId()
        const egeToModify = edges.find(e => e.sourceHandle === edge.sourceHandle && e.targetHandle === edge.targetHandle)
        egeToModify!.targetHandle = newTarget

        const originalDataSink = flowData!.nodes.find(node => node.id === edge.target) as Node<DataSinkNodeData>
        return {
          type: originalDataSink?.type,
          data: {
            ...originalDataSink?.data,
            templateData: { sourceHandles: [], targetHandles: [{ id: newTarget }] },
            instantiationData: {
              resource: {
                //...originalDataSink?.data?.instantiationData.repository, 
                organizationId: originalDataSink?.data?.instantiationData.repository?.organizationId,
                repositoryId: originalDataSink?.data?.instantiationData.repository?.id,
                name: edge?.data?.filename
              }
            }
          },
          position: { x: 100, y: 100 },
          id: getNodeId(),
          width: 100,
          height: 100,
        }
      }
    }).filter(node => node !== undefined) as any

    console.log(JSON.stringify(dataSinks))

    const requestData = {
      name: pipelineName,
      pipeline: {
        nodes: flowData?.nodes?.filter(node => node.type === 'dataSource').map(node => node as Node<DataSourceNodeData>).map(node => {
          return {
            type: node.type,
            data: {
              ...node.data,
              instantiationData: {
                resource: {
                  //...node?.data?.instantiationData.resource,
                  organizationId: node?.data?.instantiationData.resource?.organizationId,
                  repositoryId: node?.data?.instantiationData.resource?.repositoryId,
                  resourceId: node?.data?.instantiationData.resource?.id,
                },
              }
            },
            width: 100, height: 100, position: { x: 100, y: 100 }, id: node.id, label: "",
          } as any
        }).concat(
          flowData?.nodes?.filter(node => node.type === 'operator').map(node => node as Node<OperatorNodeData>).map(node => {
            return {
              type: node.type, data: {
                ...node.data,
                instantiationData: {
                  resource: {
                    //...node?.data?.instantiationData.algorithm,
                    organizationId: node?.data?.instantiationData.algorithm?.organizationId,
                    repositoryId: node?.data?.instantiationData.algorithm?.repositoryId,
                    resourceId: node?.data?.instantiationData.algorithm?.id,
                  }
                }
              },
              width: 100, height: 100, position: { x: 100, y: 100 }, id: node.id, label: "",
            } as any
          })
        ).concat(dataSinks),
        edges: edges.map(edge => {
          return { sourceHandle: edge.sourceHandle, targetHandle: edge.targetHandle }
        })
      }
    }

    console.log(JSON.stringify(requestData))

    const selectedOrg = organizations[0]
    const selectedRepo = repositories.filter(repo => repo.organizationId === selectedOrg.id)[0]
    
    const executionId = await putExecution(selectedOrg.id, selectedRepo.id, pipelineId)
    await putCommandStart(selectedOrg.id, selectedRepo.id, pipelineId, executionId)
  }

  //Post pipeline to backend
  const savePipeline = async () => {
    console.log("Starting savePipeline function...");

    const requestData = {
      name: pipelineName,
      pipeline: {
        nodes: flowData?.nodes?.map((node, index) => {
          console.log(`Processing node #${index + 1} - ID: ${node.id}, Type: ${node.type}`);

          const nodeData = {
            id: node.id,
            type: node.type,
            width: node.width || 100,
            height: node.height || 100,
            position: {
              x: node.position?.x || 0,
              y: node.position?.y || 0,
            },
            data: {
              label: node.label || "",
              instantiationData: {
                resource: {
                  organizationId: node?.data?.instantiationData?.resource?.organizationId,
                  repositoryId: node?.data?.instantiationData?.resource?.repositoryId,
                  resourceId: node?.data?.instantiationData?.resource?.id,
                },
              },
              templateData: {
                sourceHandles: node.data?.templateData?.sourceHandles || [],
                targetHandles: node.data?.templateData?.targetHandles || [],
                hint: node.data?.templateData?.hint || "",
              },
            },
          };

          // Log the node data and its instantiation/template details
          console.log(`Node Data for ID ${node.id}:`, JSON.stringify(nodeData, null, 2));
          console.log(`Instantiation Data for ID ${node.id}:`, JSON.stringify(nodeData.data.instantiationData, null, 2));
          console.log(`Template Data for ID ${node.id}:`, JSON.stringify(nodeData.data.templateData, null, 2));

          return nodeData;
        }),

        edges: flowData?.edges?.map((edge, index) => {
          console.log(`Processing edge #${index + 1} - Source: ${edge.source}, Target: ${edge.target}`);
          return {
            source: edge.source,
            target: edge.target,
            sourceHandle: edge.sourceHandle,
            targetHandle: edge.targetHandle,
          };
        }),
      },
    };

    // Debug the organization and repository selection
    const selectedOrg = organizations[0];
    console.log("Selected Organization:", selectedOrg);

    const selectedRepo = repositories.find((repo) => repo.organizationId === selectedOrg.id);
    console.log("Selected Repository:", selectedRepo);

    // Include organizationId and repositoryId in the request
    const pipelineDTO = {
      organizationId: selectedOrg.id,
      repositoryId: selectedRepo?.id,
      id: pipelineId || '',
      ...requestData,
    };

    // Log the entire request payload before sending
    console.log("Final Pipeline DTO:", JSON.stringify(pipelineDTO, null, 2));

    try {
      // Attempt to save the pipeline
      console.log("Sending save request...");
      await putPipeline(selectedOrg.id, selectedRepo.id, pipelineDTO);
      console.log("Pipeline saved successfully!");
    } catch (error) {
      console.error("Error saving pipeline:", error);
    }
  };



  return (
    <AppBar position="fixed">
      <Toolbar sx={{ flexGrow: 1 }}>
        <Button onClick={() => navigate('/')}>
          <ArrowBackIosNewIcon sx={{ color: "white" }} />
        </Button>
        <Box sx={{ width: '100%', textAlign: 'center' }}>
          {isEditing ? (
            <TextField
              value={pipelineName}
              onChange={(event) => setPipelineName(event?.target.value as string)}
              autoFocus
              onBlur={handleFinishEditing}
              inputProps={{ style: { textAlign: 'center', width: 'auto' } }}
            />
          ) : (
            <Box onClick={handleStartEditing} sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'center', width: '100%' }}>
              <Typography>{pipelineName}</Typography>
              <EditIcon sx={{ paddingLeft: '10px' }} />
            </Box>
          )}
        </Box>
        <Button onClick={() => generateJson()}>
          <Typography variant="body1" sx={{ color: "white" }}>Deploy pipeline</Typography>
        </Button>
        <Button onClick={savePipeline}>
        <Typography variant="body1" sx={{ color: "white" }}>Save pipeline</Typography>
        </Button>
      </Toolbar>
    </AppBar>
  )
}

