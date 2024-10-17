import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getRepositories } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk } from "../../state_management/slices/apiSlice.ts";
import RepositoryCard from "../cards/RepositoryCard.tsx";

const OrgList: React.FC = () => {
    
    const dispatch = useDispatch();
    const repositories = useSelector(getRepositories); // Adjust state path as needed
    
    useEffect(()=> {
        dispatch(repositoryThunk());
    }, [dispatch]);
    

    return (
        <div>
            <h1>Repositories</h1>
            <ul data-qa="Repositories:">
            {
                repositories?.map((repository) => {
                    return (
                        <RepositoryCard repository={repository} />
                    );
                })
            }
            </ul>
        </div>
    )
}

export default OrgList;