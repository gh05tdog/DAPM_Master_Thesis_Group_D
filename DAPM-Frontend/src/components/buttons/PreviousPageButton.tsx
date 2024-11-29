import { Button } from "@mui/material";
import React from "react";

interface PreviousPageButtonProps {
    currentPage: number;
    setPage: (page: number) => void;
    totalPages: number;
}

export default function PreviousPageButton({currentPage, setPage, totalPages}: PreviousPageButtonProps) {
    const handlePreviousPage = () => {
        setPage(Math.min(currentPage - 1, totalPages));
    };

    return (
        <Button
            variant="contained"
            color="primary"
            disabled={currentPage === totalPages || totalPages === 0}
            onClick={handlePreviousPage}
        >
            Previous Page
        </Button>
    );
}