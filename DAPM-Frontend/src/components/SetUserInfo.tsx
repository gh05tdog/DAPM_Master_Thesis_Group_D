import { useEffect } from "react";

interface SetUserInfoProps {
    user: any;
    onInfoUpdate: (info: any) => void;
}

const SetUserInfo: React.FC<SetUserInfoProps> = ({ user, onInfoUpdate }) => {
    useEffect(() => {
        const getUserInfo = async () => {
            const response = await user; 
            onInfoUpdate(response); 
        };

        getUserInfo();
    }, [user, onInfoUpdate]);

    return null;
};

export default SetUserInfo;