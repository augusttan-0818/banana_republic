import React, { useState, useEffect, ChangeEvent } from "react";
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Box,
    TextField,
    Button,
    Typography,
    FormControlLabel,
    Checkbox,
    MenuItem
} from "@mui/material";
import { statusType } from "../../utils/dataTypes";

// Define the StatusObject interface (same as in RDUpdatesEdit.tsx)
export interface StatusObject {
    date: Date;
    committee: string;
    status: statusType;
    decision: boolean;
    minutesReference: string;
    discussion: string;
    secretaryNotes: string;
    action: string;
    jobComment: string;
}

// Default status value – adjust the default values as needed.
const defaultStatus: StatusObject = {
    date: new Date(),
    committee: "",
    status: "Received",
    decision: false,
    minutesReference: "",
    discussion: "",
    secretaryNotes: "",
    action: "",
    jobComment: ""
};

// List of valid status options.
const statusOptions: statusType[] = [
    "Received",
    "Sorted",
    "Analyzed",
    "Reviewed",
    "Posted for pre-Public Review",
    "Posted for Public Review",
    "Posted for Public Review Comments",
    "Posted for post-Public Review",
    "Ready for P&M Processing",
    "Published",
    "Completed"
];

type RDUpdateStatusDialogProps = {
    open: boolean;
    documents: number[]; // changed from DocumentIdentifier[]
    onClose: () => void;
};

export default function RDUpdateStatusDialog({ open, documents, onClose }: RDUpdateStatusDialogProps) {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [newStatus, setNewStatus] = useState<StatusObject>(defaultStatus);

    useEffect(() => {
        if (open) {
            setCurrentIndex(0);
            setNewStatus(defaultStatus);
        }
    }, [open, documents]);

    // Guard: if documents is empty, do not render the dialog
    if (!documents || documents.length === 0) {
        return null;
    }

    // Format a Date to YYYY-MM-DD for an input[type="date"]
    const formatDate = (date: Date): string => {
        if (!date || isNaN(date.getTime())) return "";
        return date.toISOString().split("T")[0];
    };

    // Handlers for updating each status field
    const handleFieldChange = (field: keyof StatusObject) => (e: ChangeEvent<HTMLInputElement>) => {
        if (field === "date") {
            setNewStatus({ ...newStatus, date: new Date(e.target.value) });
        } else if (field === "decision") {
            setNewStatus({ ...newStatus, decision: e.target.checked });
        } else {
            setNewStatus({ ...newStatus, [field]: e.target.value });
        }
    };

    const handleSave = () => {
        const currentDocId = documents[currentIndex];
        console.log(`Saving status for Document ID ${currentDocId}:`, newStatus);
        // Insert save logic here.
        if (documents.length === 1 || currentIndex === documents.length - 1) {
            onClose();
        }
    };

    const handleNext = () => {
        const currentDocId = documents[currentIndex];
        console.log(`Saving status for Document ID ${currentDocId}:`, newStatus);
        // Insert save logic for the current document here.
        if (currentIndex < documents.length - 1) {
            setCurrentIndex(currentIndex + 1);
            setNewStatus(defaultStatus);
        } else {
            onClose();
        }
    };

    const currentDocId = documents[currentIndex];

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>
                {`Update Status for Document ID: ${currentDocId}`}
            </DialogTitle>
            <DialogContent>
                <Box sx={{ mt: 2 }}>
                    <Box sx={{ display: "grid", gridTemplateColumns: "150px 1fr", gap: 2, mb: 2 }}>
                        <Typography variant="body1">Date:</Typography>
                        <TextField
                            type="date"
                            size="small"
                            value={formatDate(newStatus.date)}
                            onChange={handleFieldChange("date")}
                            InputLabelProps={{ shrink: true }}
                        />

                        <Typography variant="body1">Committee:</Typography>
                        <TextField
                            size="small"
                            variant="outlined"
                            value={newStatus.committee}
                            onChange={handleFieldChange("committee")}
                            placeholder="Enter committee"
                        />

                        <Typography variant="body1">Status:</Typography>
                        <TextField
                            select
                            size="small"
                            variant="outlined"
                            value={newStatus.status}
                            onChange={handleFieldChange("status")}
                        >
                            {statusOptions.map((option) => (
                                <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>
                            ))}
                        </TextField>

                        <Typography variant="body1">Decision:</Typography>
                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={newStatus.decision}
                                    onChange={handleFieldChange("decision")}
                                />
                            }
                            label="Approved"
                        />

                        <Typography variant="body1">Minutes Ref.:</Typography>
                        <TextField
                            size="small"
                            variant="outlined"
                            value={newStatus.minutesReference}
                            onChange={handleFieldChange("minutesReference")}
                            placeholder="Enter minutes reference"
                        />

                        <Typography variant="body1">Discussion:</Typography>
                        <TextField
                            multiline
                            rows={3}
                            size="small"
                            variant="outlined"
                            value={newStatus.discussion}
                            onChange={handleFieldChange("discussion")}
                            placeholder="Enter discussion details"
                        />

                        <Typography variant="body1">Secretary Notes:</Typography>
                        <TextField
                            multiline
                            rows={3}
                            size="small"
                            variant="outlined"
                            value={newStatus.secretaryNotes}
                            onChange={handleFieldChange("secretaryNotes")}
                            placeholder="Enter secretary notes"
                        />

                        <Typography variant="body1">Action:</Typography>
                        <TextField
                            size="small"
                            variant="outlined"
                            value={newStatus.action}
                            onChange={handleFieldChange("action")}
                            placeholder="Enter action"
                        />

                        <Typography variant="body1">Job Comment:</Typography>
                        <TextField
                            multiline
                            rows={2}
                            size="small"
                            variant="outlined"
                            value={newStatus.jobComment}
                            onChange={handleFieldChange("jobComment")}
                            placeholder="Enter job comment"
                        />
                    </Box>
                </Box>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Cancel</Button>
                {documents.length > 1 && currentIndex < documents.length - 1 ? (
                    <Button onClick={handleNext} variant="contained">
                        Save & Next
                    </Button>
                ) : (
                    <Button onClick={handleSave} variant="contained">
                        Save
                    </Button>
                )}
            </DialogActions>
        </Dialog>
    );
}