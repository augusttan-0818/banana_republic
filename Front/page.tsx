"use client";
import React, { useState, useRef } from "react";
import { Box, Button, Dialog, IconButton, Stack } from "@mui/material";
import { Card, CardContent } from "@mui/material";
import RefreshIcon from '@mui/icons-material/Refresh';
import GetAppIcon from '@mui/icons-material/GetApp';
import RDSearchSection from "./components/RDSearchSection";
import RDUpdatesDataGrid from "./components/RDUpdatesDataGrid";
import RDUpdatesEdit, { RDUpdatesEditRef } from "./components/RDUpdatesEdit";
import RDUpdateStatusDialog from "./components/RDUpdateStatusDialog";
import { GridValidRowModel } from "@mui/x-data-grid";
import { generateRDAgendaPackage } from "../utils/generateRDDocumentation";
import { useMenu } from "@/app/context/AppStateContext";
import { exportAllDataToExcel } from "../utils/exportToExcel";
import GeneralNavBar from "../components/GeneralNavBar";
import GlobalNavigation from "@/components/GlobalNavigation";

const startSendForReviewWorkflow = () => {
    console.log(`SENT FOR REVIEW!!`);
};

export default function TasksPage() {
    const { selectedRDRowIds } = useMenu();
    const [openEdit, setOpenEdit] = useState(false);
    const [openStatus, setOpenStatus] = useState(false);
    const [currentRow, setCurrentRow] = useState<GridValidRowModel>({});
    const [refreshKey, setRefreshKey] = useState<number>(0);
    const editRef = useRef<RDUpdatesEditRef>(null);

    // Updated close handler that calls the RDUpdatesEdit confirmation via ref.
    const handleCloseEdit = () => {
        editRef.current?.triggerClose();
    };

    // onRequestClose passed to the RDUpdatesEdit will actually close the dialog.
    const onRequestClose = () => {
        setOpenEdit(false);
    };

    // Handle Set Status click: if no rows are selected, alert the user.
    const handleSetStatusClick = () => {
        if (!selectedRDRowIds || selectedRDRowIds.length === 0) {
            alert("No Reference Document selected. Please select one.");
            return;
        }
        setOpenStatus(true);
    };

    const startGenerateWorkflow = () => {
        const packageBody = generateRDAgendaPackage([]);
        console.log(packageBody);
        window.open(packageBody)
    };

    const startExportWorkflow = async () => {
        const cookieHeader = document.cookie;
        exportAllDataToExcel(cookieHeader, '', 10);
    }

    const handleRefreshChange = (newKey: number) => {
        setRefreshKey(newKey);
    }

    return (
        <div className="paddedDiv">
            <Card sx={{ maxWidth: 1480, mx: 'auto' }}>
                <CardContent>
                    <Box sx={{ mb: 7 }}>
                        <RDSearchSection 
                            refreshKey={refreshKey} 
                            onRefreshChange={handleRefreshChange}
                        />
                    </Box>

                    <Box sx={{ mb: -2 }}>
                        <Stack direction="row" spacing={3}>
                            <Button onClick={startGenerateWorkflow} variant="contained">Generate</Button>
                            <Button onClick={handleSetStatusClick} variant="contained">Set Status</Button>
                            <Button onClick={startSendForReviewWorkflow} variant="contained">Send for Review</Button>
                        </Stack>
                    </Box>

                    <Box sx={{ display: 'flex', flexDirection: 'row', mb: 1 }}>
                        <Box sx={{ width: '50%', display: 'flex', justifyContent: 'flex-start', alignContent: 'center' }} />
                        <Box sx={{ width: '50%', display: 'flex', justifyContent: 'flex-end', alignContent: 'center' }} >
                            <IconButton 
                                color="primary"
                                size="small"
                                sx={{ color: 'primary.light' }}
                                onClick={() => setRefreshKey(prev => prev + 1)}
                            >
                                <RefreshIcon />
                            </IconButton>
                            <IconButton onClick={startExportWorkflow} size="small" color="primary" sx={{ color: 'primary.light' }}>
                                <GetAppIcon />
                            </IconButton>
                        </Box>
                    </Box>
                    <RDUpdatesDataGrid 
                        editButtonCallback={(row) => { setOpenEdit(true); setCurrentRow(row); }} 
                        refreshKey={refreshKey}
                    />
                </CardContent>
            </Card>

            <Dialog fullScreen open={openEdit} onClose={handleCloseEdit}>
                <GlobalNavigation />
                <GeneralNavBar />
                <Box sx={{ p: 2 }}>
                    <RDUpdatesEdit ref={editRef} metadata={currentRow} onRequestClose={onRequestClose} />
                </Box>
            </Dialog>

            <RDUpdateStatusDialog open={openStatus} documents={selectedRDRowIds} onClose={() => setOpenStatus(false)} />        
        </div>
    );
}
