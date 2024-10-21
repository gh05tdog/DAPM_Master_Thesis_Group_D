import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const BackButton = () => {
    const navigate = useNavigate();

    return (
        <Button
            variant="contained"
            onClick={() => navigate(-1)} // Navigates back to the previous page
        >
            Go Back
        </Button>
    );
};

export default BackButton;
