import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories, getOrganizations,selectLoadingRepositories } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk, organizationThunk } from "../../state_management/slices/apiSlice.ts";
import Spinner from '../cards/SpinnerCard.tsx';
import RepositoryCard from "../cards/RepositoryCard.tsx";

const RepoList: React.FC = () => {
    const dispatch = useDispatch();
    
    // Get organizations and repositories from the store
    const organizations = useSelector(getOrganizations);
    const repositories = useSelector(getRepositories);
    const loading = useSelector(selectLoadingRepositories); // Get loading state

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
    if (loading) {
        return(
            <div>
                <h1>Repositories</h1>
                <Spinner />
            </div>
        )
    }

    return (
        <div>
            <h1>Repositories</h1>
            <ul data-qa="Repositories:">
                {repositories?.map((repository) => (
                    <RepositoryCard key={repository.id} repository={repository} />
                ))}
            </ul>
        </div>
    );
};

export default RepoList;
