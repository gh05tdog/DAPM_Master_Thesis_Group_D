import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import {
    Box,
    Button,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography
} from "@mui/material";

const statuses = ["build", "ready", "in progress", "finished"];

const Pipelines = [
    { name: 'Data Ingestion Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'ETL Process Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Sales Data Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Customer Analytics Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Marketing Automation Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Financial Reporting Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Data Cleansing Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Real-Time Monitoring Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Machine Learning Training Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Data Warehouse Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'Product Usage Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
    { name: 'User Activity Pipeline', status: statuses[Math.floor(Math.random() * statuses.length)] },
];

const ITEMS_PER_PAGE = 10;

export default function SimpleTable() {
    const [rows, setRows] = useState(Pipelines);
    const [page, setPage] = useState(1);

    const handleRemove = (name: string) => {
        setRows((prevRows) => prevRows.filter((row) => row.name !== name));
    };

    // Pagination controls
    const totalPages = Math.ceil(rows.length / ITEMS_PER_PAGE);
    const handleNextPage = () => {
        setPage((prevPage) => Math.min(prevPage + 1, totalPages));
    };
    const handlePrevPage = () => {
        setPage((prevPage) => Math.max(prevPage - 1, 1));
    };

    const navigate = useNavigate();

    const startIndex = (page - 1) * ITEMS_PER_PAGE;
    const currentRows = rows.slice(startIndex, startIndex + ITEMS_PER_PAGE);


    return (
        <Box sx={{ width: '100%', margin: 'auto', mt: 4 }}>
            <TableContainer component={Paper}>
                <Table aria-label="Simple Table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell align="right">Action</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {currentRows.map((row) => (
                            <TableRow key={row.name}>
                                <TableCell>{row.name}</TableCell>
                                <TableCell>{row.status}</TableCell>
                                <TableCell align="right">
                                    <Button
                                        variant="outlined"
                                        color="primary"
                                        onClick={() => handleRemove(row.name)}
                                    >
                                        Edit
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 2 }}>
                <Button
                    variant="contained"
                    color="primary"
                    disabled={page === 1}
                    onClick={handlePrevPage}
                >
                    Previous Page
                </Button>
                <Typography variant="body1">
                    Page {page} of {totalPages}
                </Typography>
                <Button
                    variant="contained"
                    color="primary"
                    disabled={page === totalPages}
                    onClick={handleNextPage}
                >
                    Next Page
                </Button>
            </Box>
        </Box>
    )
}