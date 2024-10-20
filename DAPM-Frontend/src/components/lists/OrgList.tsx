import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getOrganizations ,selectLoadingOrganisation} from "../../state_management/selectors/apiSelector.ts";
import { organizationThunk } from "../../state_management/slices/apiSlice.ts";
import OrganizationCard from "../cards/OrganizationCard.tsx";
import Spinner from '../cards/SpinnerCard.tsx';

const OrgList: React.FC = () => {
    
    const dispatch = useDispatch();
    const organizations = useSelector(getOrganizations); // Adjust state path as needed
    const loading = useSelector(selectLoadingOrganisation); // Get loading state
    
    useEffect(()=> {
        dispatch(organizationThunk());
    }, [dispatch]);
    
    if (loading) {
        return(
            <div>
                <h1>Organizations</h1>
                <Spinner />
            </div>
        )
    }


    return (
        <div>
            <h1>Organizations</h1>
            <ul data-qa="organiztionList">
            {
                organizations?.map((organization) => {
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