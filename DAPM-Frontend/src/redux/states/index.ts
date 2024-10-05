import { ApiState } from './apiState.js';
import { PipelineState } from './pipelineState.js';

export interface RootState {
  pipelineState: PipelineState
  apiState: ApiState
}
