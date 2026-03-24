"use client";

import {
  DataGrid,
  GridColDef,
  GridRowParams,
} from "@mui/x-data-grid";
import { useEffect, useState, useCallback } from "react";
import { getReferenceDocumentUpdates } from "../../utils/dataUtils";
import { useMenu } from "@/app/context/AppStateContext";
import { Box } from "@mui/material";

type RDUpdatesDataGridProps = {
    editButtonCallback: (row: GridRowParams) => void
    refreshKey?: number
}

export default function RDUpdatesDataGrid({ editButtonCallback, refreshKey }: RDUpdatesDataGridProps) {

    const columns: GridColDef[] = [
        { field: 'id', headerName: 'ID', width: 80 },
        { field: 'issuingAgency', headerName: 'Issuing Agency', flex: 1, minWidth: 150 },
        { field: 'documentNumber', headerName: 'Document Number', flex: 1, minWidth: 150 },
        { field: 'referencedIn', headerName: 'Referenced In', flex: 1, minWidth: 200 },
        { field: 'withdrawnDate', headerName: 'Withdrawn Date', width: 150, valueFormatter: (value: unknown) => {
            if (!value) return '';
            return new Date(value as string).toLocaleDateString();
        }},
        { field: 'publicationDate', headerName: 'Publication Date', width: 150, valueFormatter: (value: unknown) => {
            if (!value) return '';
            return new Date(value as string).toLocaleDateString();
        }},
        { field: 'submittedDate', headerName: 'Submitted Date', width: 150, valueFormatter: (value: unknown) => {
            if (!value) return '';
            return new Date(value as string).toLocaleDateString();
        }},
        { field: 'submittedBy', headerName: 'Submitted By', flex: 1, minWidth: 150 },
        { field: 'SCs', headerName: 'SCs', width: 150 },
        { field: 'status', headerName: 'Status', flex: 1, minWidth: 150 },
        { field: 'PR', headerName: 'PR', width: 80 },
        { field: 'job', headerName: 'Job', width: 100 },
    ];

    const { setSelectedRDRowIds } = useMenu();

    const [page, setPage] = useState<number>(0);
    const [pageSize, setPageSize] = useState<number>(10);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [rowCount, setRowCount] = useState<number>(0);
    const [rows, setRows] = useState<any[]>([]);

    const fetchData = useCallback(async () => {
        setIsLoading(true);
        const cookieHeader = document.cookie;
        
        try {
            const [data, totalCount] = await getReferenceDocumentUpdates(cookieHeader, page, pageSize);

            setRowCount(totalCount);
            
            // Transform the data to match the DataGrid columns
            // API returns: id, issuingAgency, documentNumber, referencedIn, withdrawnDate, publicationDate, submittedDate, submittedBy
            const transformedRows = data.map((record: any, index: number) => ({
                id: record.id ?? index,
                issuingAgency: record.issuingAgency ?? '',
                documentNumber: record.documentNumber ?? '',
                referencedIn: record.referencedIn ?? '',
                withdrawnDate: record.withdrawnDate ?? null,
                publicationDate: record.publicationDate ?? null,
                submittedDate: record.submittedDate ?? null,
                submittedBy: record.submittedBy ?? '',
                SCs: [],
                status: [],
                PR: null,
                job: null,
            }));

            setRows(transformedRows);
        } catch (error) {
            console.error("Error fetching data:", error);
        } finally {
            setIsLoading(false);
        }
    }, [page, pageSize]);

    useEffect(() => {
        fetchData();
    }, [fetchData, refreshKey]);

    const handlePaginationModelChange = (model: { page: number; pageSize: number }) => {
        setPage(model.page);
        setPageSize(model.pageSize);
    };

    const handleSelectionChange = (params: any) => {
        setSelectedRDRowIds(params as number[]);
    };

    return (
        <Box
            sx={{
                width: "100%",
                height: 600,
                "& .MuiDataGrid-root": {
                    border: "none",
                },
            }}
        >
            <DataGrid
                sx={{
                    "& .MuiDataGrid-virtualScroller": {
                        overflowX: "auto",
                    },
                }}
                slotProps={{
                    pagination: {
                        showFirstButton: true,
                        showLastButton: true,
                    },
                }}
                columns={columns}
                rows={rows}
                loading={isLoading}
                localeText={{
                    noRowsLabel: "No Reference Document Updates found",
                    footerRowSelected: (count: number) =>
                        `${count.toLocaleString()} Document${count !== 1 ? "s" : ""} selected`,
                }}
                pagination
                paginationMode="server"
                pageSizeOptions={[10, 20, 50]}
                rowCount={rowCount}
                paginationModel={{ page, pageSize }}
                onPaginationModelChange={handlePaginationModelChange}
                disableRowSelectionOnClick
                checkboxSelection
                onRowSelectionModelChange={handleSelectionChange}
            />
        </Box>
    );
}
