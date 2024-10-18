import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations, getResources } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk, organizationThunk, resourceThunk } from "../../state_management/slices/apiSlice.ts";
import ResourceCard from "../cards/ResourceCard.tsx";
import { Resource } from "../../state_management/states/apiState.ts";

const ResourceList: React.FC = () => {
    const dispatch = useDispatch();
    
    // Get organizations and repositories from the store
    const organizations = useSelector(getOrganizations);
    const repositories = useSelector(getRepositories);
    const resources = useSelector(getResources)
    
    // Fetch organizations on component mount
    useEffect(() => {
        dispatch(organizationThunk());
    }, [dispatch]);

    // Fetch repositories once organizations are available
    useEffect(() => {
        if (organizations.length > 0) {
            try {
                dispatch(repositoryThunk(organizations));
            }
            catch (error) {
                console.error(error);    
            }
        }

    }, [dispatch, organizations]);

    useEffect(() => {
        dispatch(resourceThunk({ organizations, repositories }));
    }, [dispatch, organizations, repositories]);

    return (
        <div>
            <h1>Resources</h1>
            <ul data-qa="Resources:">
                {resources?.map((resource: Resource) => (
                    <ResourceCard key={resource.id} resource={resource} />
                ))}
            </ul>
        </div>
    );
};

export default ResourceList;
