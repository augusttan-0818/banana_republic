"use client";

import {
  DataGrid,
  GridColDef,
  GridRowParams,
} from "@mui/x-data-grid";
import { useEffect, useState, useCallback } from "react";
import { getReferenceDocumentUpdates, searchReferenceDocumentUpdates, RdUpdateSearchParams } from "../../utils/dataUtils";
import { useMenu } from "@/app/context/AppStateContext";
import { Box } from "@mui/material";

export type RDUpdateSearchCriteria = {
    documentNumberFrom?: string;
    documentNumberTo?: string;
    submittedDateFrom?: Date;
    submittedDateTo?: Date;
    having?: string;
    statusValue?: string;
    decision?: string;
    statusCommittee?: string;
    minutesReference?: string;
    additionalCommittee?: string;
    additionalAgency?: string;
    additionalCode?: string;
    additionalCodeReference?: string;
    publicReview?: string;
    includeExclude?: string;
};

type RDUpdatesDataGridProps = {
    editButtonCallback: (row: GridRowParams) => void
    refreshKey?: number
    searchCriteria?: RDUpdateSearchCriteria
}

export default function RDUpdatesDataGrid({ editButtonCallback, refreshKey, searchCriteria }: RDUpdatesDataGridProps) {

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

    // Reset page to 0 when search criteria changes
    useEffect(() => {
        setPage(0);
    }, [searchCriteria]);

    const fetchData = useCallback(async () => {
        setIsLoading(true);
        const cookieHeader = document.cookie;

        try {
            let data: any[];
            let totalCount: number;

            // Check if search criteria is provided
            if (searchCriteria) {
                // Build search params from criteria
                const searchParams: RdUpdateSearchParams = {
                    pageNumber: page + 1, // API expects 1-based page
                    pageSize: pageSize,
                    documentNumberFrom: searchCriteria.documentNumberFrom || undefined,
                    documentNumberTo: searchCriteria.documentNumberTo || undefined,
                    submittedDateFrom: searchCriteria.submittedDateFrom?.toISOString() || undefined,
                    submittedDateTo: searchCriteria.submittedDateTo?.toISOString() || undefined,
                    having: searchCriteria.having || undefined,
                    statusValue: searchCriteria.statusValue || undefined,
                    decision: searchCriteria.decision || undefined,
                    statusCommittee: searchCriteria.statusCommittee || undefined,
                    minutesReference: searchCriteria.minutesReference || undefined,
                    additionalCommittee: searchCriteria.additionalCommittee || undefined,
                    additionalAgency: searchCriteria.additionalAgency || undefined,
                    additionalCode: searchCriteria.additionalCode || undefined,
                    additionalCodeReference: searchCriteria.additionalCodeReference || undefined,
                    publicReview: searchCriteria.publicReview || undefined,
                    includeExclude: searchCriteria.includeExclude || undefined,
                };

                [data, totalCount] = await searchReferenceDocumentUpdates(cookieHeader, searchParams);
            } else {
                [data, totalCount] = await getReferenceDocumentUpdates(cookieHeader, page, pageSize);
            }

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
    }, [page, pageSize, searchCriteria]);

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
