"use client";

import {
  DataGrid,
  GridColDef,
  GRID_DATE_COL_DEF,
  GridColTypeDef,
} from "@mui/x-data-grid";
import { Box, Button, Tooltip } from "@mui/material";
import LibraryBooksIcon from "@mui/icons-material/LibraryBooks";
import { format } from "date-fns";
import { enUS } from "date-fns/locale";
import { useTheme, useMediaQuery } from "@mui/material";

type CCRDataGridViewProps = {
  rows: any[];
  loading: boolean;
  rowCount: number;
  paginationModel: { page: number; pageSize: number };
  onPaginationModelChange: (model: { page: number; pageSize: number }) => void;
  locale: string;
};

const dateColumnType: GridColTypeDef<Date, string> = {
  ...GRID_DATE_COL_DEF,
  resizable: false,
  valueFormatter: (value) => {
    if (value) {
      return format(value, "MM/dd/yyyy", { locale: enUS });
    }
    return "";
  },
};

export default function CCRDataGridView({
  rows,
  loading,
  rowCount,
  paginationModel,
  onPaginationModelChange,
  locale,
}: CCRDataGridViewProps) {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
  const columns: GridColDef[] = [
    {
      field: "ccrId",
      headerName: "CCR ID",
      type: "string",
      align: "left",
      headerAlign: "left",
      flex: 0.75,
    },
    {
      field: "createdOn",
      headerName: "Date",
      ...dateColumnType,
      flex: 1,
    },
    { field: "codeReference", headerName: "Code Reference", flex: 2 },
    { field: "subject", headerName: "Proponent Title", flex: 1 },
    { field: "ccrTitle", headerName: "Title", flex: 3, minWidth: 200 },
    {
      field: "proponentName",
      headerName: "Proponent",
      flex: 0.75,
    },
    { field: "submittedByFPT", headerName: "P/T", flex: 0.5 },
    { field: "leadTAName", headerName: "Lead TA", flex: 0.75 },
    { field: "techcommittee", headerName: "Technical Committee", flex: 0.5 },
    {
      field: "tools",
      headerName: "Tools",
      renderCell: (params) => (
        <Tooltip title="View CCR Details">
          <Button
            startIcon={<LibraryBooksIcon />}
            size="small"
            variant="text"
            href={`/${locale}/ccrs/view-all-ccrs/${params.row.ccrId}/`}
          />
        </Tooltip>
      ),
      flex: 0.75,
    },
  ];

  const columnVisibilityModel = {
    leadTA: !isMobile,
    techcommittee: !isMobile,
    submittedByFPT: !isMobile,
    proponentName: !isMobile,
    codeReference: !isMobile,
    subject: !isMobile,
    createdOn: !isMobile,
  };

  return (
    <Box
      sx={{
        width: "100%",
        minHeight: {
          xs: 400,
          sm: 400,
          md: 600,
          xl: 700,
        },
        height: {
          xs: 400,
          sm: 400,
          md: 600,
          xl: 700,
        },
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
        columnVisibilityModel={columnVisibilityModel}
        rows={rows}
        loading={loading}
        columns={columns}
        paginationMode="server"
        pageSizeOptions={[10, 20, 50]}
        rowCount={rowCount}
        paginationModel={paginationModel}
        onPaginationModelChange={onPaginationModelChange}
        getRowId={(row) => row.ccrId}
        localeText={{
          noRowsLabel: "No CCRs found",
          footerRowSelected: (count) =>
            `${count.toLocaleString()} CCR${count !== 1 ? "s" : ""} selected`,
        }}
      />
    </Box>
  );
}
