import { RootState } from "../states/index.ts";

export const getOrganizations = (state: RootState) => state.apiState.organizations
export const selectLoadingRepositories = (state: RootState) =>  state.apiState.loadingRepositories
export const getRepositories = (state: RootState) => state.apiState.repositories
export const selectLoadingOrganisation = (state: RootState) => state.apiState.loadingOrganizations
export const getResources = (state: RootState) => state.apiState.resources
export const selectLoadingResources = (state: RootState) => state.apiState.loadingResources


