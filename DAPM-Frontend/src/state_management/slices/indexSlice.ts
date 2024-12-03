import { Reducer, combineReducers } from "redux"
import nodeReducer from "./pipelineSlice.ts"
import apiReducer from "./apiSlice.ts"
import pipelineReducer from "./pipelineSlice.ts"
import { RootState } from "../states/index.ts"

const rootReducer: Reducer<RootState> = combineReducers({
    apiState: apiReducer,
    pipelineState: pipelineReducer,
})

export default rootReducer
export const getActivePipeline = (state) =>   state.pipelineState.pipelines.find(pipeline => pipeline.id === state.pipelineState.activePipelineId);
export const getActiveFlowData = (state) => state.pipelineState.pipelines.find(pipeline => pipeline.id === state.pipelineState.activePipelineId)?.pipeline
