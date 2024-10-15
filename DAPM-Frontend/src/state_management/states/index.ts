import { ApiState } from './apiState.ts';
import { PipelineState } from './pipelineState.ts';

export interface RootState {
  pipelineState: PipelineState
  apiState: ApiState
}
