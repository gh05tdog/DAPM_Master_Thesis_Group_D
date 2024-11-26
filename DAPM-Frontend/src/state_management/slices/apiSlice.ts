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
    
    const fetchAllRepositories = async () => {
      const fetchPromises = organizations.map((organization) =>
        fetchOrganisationRepositories(organization.id)
      );
    
      const results = await Promise.all(fetchPromises);
    
      const repositories = results.flatMap((result) => result.result.repositories);
    
      return repositories;
    }
    return fetchAllRepositories();
  } catch (error) {
    return thunkAPI.rejectWithValue(error); // Handle error
  }
});
export const selectLoadingRepositories = (state) =>  state.loadingRepositories;

export const resourceThunk = createAsyncThunk<
  Resource[],
  { organizations: Organization[]; repositories: Repository[] }
>(
  "api/fetchResources",
  async ({ organizations, repositories }, thunkAPI) => {
    try {
      const fetchPromises: Promise<any>[] = [];
      for (const org of organizations) {
        for (const repo of repositories) {
          if (org.id === repo.organizationId) {
            fetchPromises.push(fetchRepositoryResources(org.id, repo.id));
          }
        }
      }
      // Wait for all promises to resolve
      const results = await Promise.all(fetchPromises);
      const resources = results.flatMap((res) => res.result.resources);
      return resources;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);