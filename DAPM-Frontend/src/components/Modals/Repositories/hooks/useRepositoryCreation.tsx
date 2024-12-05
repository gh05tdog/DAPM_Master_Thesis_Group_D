import { useCallback,  useState } from "react";
import { useSelector } from "react-redux";

import { Repository } from "../../../../state_management/states/apiState.ts";
import { putRepository } from "../../../../services/backendAPI.tsx";
import { getActiveOrganisation } from "../../../../state_management/slices/indexSlice.ts";

interface RepositoryCreationForm {
    repository: Repository;
}

const initialFormState: RepositoryCreationForm = {
    repository: {
        id: "",
        name: "",
        organizationId: "",

    },
};

const useRepositoryCreation = (onClose: () => void) => {
    const getSelectedorganizationId =  useSelector(getActiveOrganisation);
    const [isLoading, setIsLoading] = useState(false);
    const [formState, setFormState] = useState<RepositoryCreationForm>(initialFormState);
    const sleep = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));
    const handleCreateRepository = useCallback(async () => {
        //await sleep(1000);

        formState.repository.organizationId= await getSelectedorganizationId?.id;
        try {
            if (!formState.repository.organizationId) return alert("Please select an Organization");
            
            if (!formState.repository.name) return alert("Please enter a repository name");
            setIsLoading(true);
            const response = await putRepository(formState.repository.organizationId ,formState.repository.name);
            if (response.status=== 1)
                {
                     alert("Repository created successfully") ;
                } else {
                    alert("Failed to create repository") ;
                }

        } catch (error) {
            console.error("Failed to create repository:", error);
            alert("Failed to create repository");

        } finally {
            setIsLoading(false);
            onClose();

        }
    }, [formState.repository, onClose]);

    return {
        formState,
        handleCreateRepository,
        setFormState,
        isLoading,
    };
};

export default useRepositoryCreation;
