import { Button } from "@mui/material";
import React from "react";

interface NextPageButtonProps {
    currentPage: number;
    setPage: (page: number) => void;
    totalPages: number;
}

export default function NextPageButton({currentPage, setPage, totalPages}: NextPageButtonProps) {
    const handleNextPage = () => {
        setPage(Math.min(currentPage + 1, totalPages));
    };

    return (
        <Button
            variant="contained"
            color="primary"
            disabled={currentPage === totalPages || totalPages === 0}
            onClick={handleNextPage}
        >
            Next Page
        </Button>
    );
}