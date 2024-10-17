import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getOrganizations } from "../../state_management/selectors/apiSelector.ts";
import { repositoryThunk } from "../../state_management/slices/apiSlice.ts";
import OrganizationCard from "../cards/OrganizationCard.tsx";

const RepoList: React.FC = () => {
    
    const dispatch = useDispatch();
    const organizations = useSelector(getOrganizations); // Adjust state path as needed
    
    useEffect(()=> {
        dispatch(repositoryThunk());
    }, [dispatch]);
    

    return (
        <div>
            <h1>Organizations</h1>
            <ul data-qa="organiztionList">
            {
                organizations?.map((repository) => {
                    return (
                        <OrganizationCard organization={organization} />
                    );
                })
            }
            </ul>
        </div>
    )
}

export default OrgList;