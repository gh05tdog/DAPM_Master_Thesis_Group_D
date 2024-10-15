import { RootState } from "../states/index.ts";

export const getOrganizations = (state: RootState) => state.apiState.organizations
export const getRepositories = (state: RootState) => state.apiState.repositories
export const getResources = (state: RootState) => state.apiState.resources