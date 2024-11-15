import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import {ApiState, Organization, Repository, Resource} from "../states/apiState.ts";
import {
  fetchOrganisationRepositories,
  fetchOrganisations,
  fetchRepositoryResources
} from "../../services/backendAPI.tsx";


export const initialState: ApiState = {
    organizations: [],
    loadingOrganizations: true,
    repositories: [],
    loadingRepositories: true,
    resources: []
  }

const apiSlice = createSlice({
    name: 'api',
    initialState: initialState,
    reducers: {},
      extraReducers(builder) {
        builder
          .addCase(organizationThunk.pending, (state, action) => {
            state.loadingOrganizations=true;
          })
          .addCase(organizationThunk.fulfilled, (state, action) => {
            state.loadingOrganizations=false;
            // Add any fetched posts to the array
            state.organizations = action.payload.organizations
          })
          .addCase(organizationThunk.rejected, (state, action) => {
            state.loadingOrganizations=false;
            console.log("org thunk failed")
          })
          .addCase(repositoryThunk.pending, (state, action) => {
            state.loadingRepositories=true;
          })
          .addCase(repositoryThunk.fulfilled, (state, action) => {
            state.loadingRepositories=false;
            // Add any fetched posts to the array
            state.repositories = action.payload
          })
          .addCase(repositoryThunk.rejected, (state, action) => {
            state.loadingRepositories=false;

            console.log("repo thunk failed")
          })
          .addCase(resourceThunk.pending, (state, action) => {
            state.loadingResources= true;
          })
          .addCase(resourceThunk.fulfilled, (state, action) => {
            state.loadingResources= false;
            // Add any fetched posts to the array
            state.resources = action.payload
          })
          .addCase(resourceThunk.rejected, (state, action) => {
            state.loadingResources= false;
            console.log("resorce thunk failed")
          })
      }
    
})

export default apiSlice.reducer 

// Define the return type of the thunk
interface FetchOrganizationsResponse {
  organizations: Organization[]; // Update this type based on your actual organization type
}

interface FetchRepositoriesResponse {
  repositories: Repository[]; // Update this type based on your actual organization type
}

// Define the thunk action creator
export const organizationThunk = createAsyncThunk<
  FetchOrganizationsResponse
>("api/fetchOrganizations", async (_, thunkAPI) => {
  try {
    const organizations = await fetchOrganisations(); // Fetch organizations from the backend API
    return organizations.result; // Return data fetched from the API
  } catch (error) {
    return thunkAPI.rejectWithValue(error); // Handle error
  }
});
export const selectLoadingOrganisation = (state) => state.loadingOrganizations;


export const repositoryThunk = createAsyncThunk<
  Repository[],
  Organization[]
>("api/fetchRespositories", async (organizations: Organization[], thunkAPI) => {
  if (!organizations || organizations.length === 0) {
    return thunkAPI.rejectWithValue("Organizations list is null or empty.");
  }
  try {
    
    const repositories = [];
      for (const organization of organizations) {
        const repos = await fetchOrganisationRepositories(organization.id);
        repositories.push(...repos.result.repositories);
      }
      return repositories;
  } catch (error) {
    return thunkAPI.rejectWithValue(error); // Handle error
  }
});
export const selectLoadingRepositories = (state) =>  state.loadingRepositories;


export const resourceThunk = createAsyncThunk<
  Resource[],
  { organizations: Organization[]; repositories: Repository[] }
>("api/fetchResources", async ({organizations, repositories}, thunkAPI) => {
  try {
    
    const resources: Resource[] = [];
    for (const org of organizations) {
      for (const repo of repositories) {
        if (org.id === repo.organizationId) {
          const res = await fetchRepositoryResources(org.id, repo.id);
          resources.push(...res.result.resources);
        }
      }
    }
    const result = await Promise.all(resources)
      return result;
  } catch (error) {
    return thunkAPI.rejectWithValue(error); // Handle error
  }
});


