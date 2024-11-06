import Header from '../components/headers/Header.tsx';
import { useEffect, useState } from "react";
import PageLayout from './PageLayout.tsx';

interface PipelineOverviewPageProps {
    user: any;
}

const PipelineManager: React.FC<PipelineOverviewPageProps> = ({ user }) => {
    const [info, setInfo] = useState<any>(null);

    useEffect(() => {
        const fetchUserInfo = async () => {
            // Assuming `user` is an API call or some promise
            const response = await user;
            setInfo(response);
        };

        fetchUserInfo();
    }, [user]); // Runs the effect whenever `user` changes

    return (
        <PageLayout user={info}>
            <text> Pipeline Manager </text>
        </PageLayout>
    );
};

export default PipelineManager;