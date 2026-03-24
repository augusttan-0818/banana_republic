import React, { useState, ChangeEvent, useImperativeHandle, forwardRef, useEffect } from "react";
import {
    Box,
    TextField,
    Checkbox,
    FormControlLabel,
    Button,
    MenuItem,
    Stepper,
    Step,
    StepLabel,
    Stack,
    Typography,
    Alert,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    IconButton
} from "@mui/material";
import {
    RDUpdate,
    RDUpdatePRVersion,
    RDUpdateAnalysis,
    RDUpdateStatuses,
    ReferenceDocumentUpdateForm,
    StatusObject
} from "../../utils/dataTypes";
import { stepperContainerStyle } from "../style/EditRDStyle";
import { GridValidRowModel } from "@mui/x-data-grid";
import { statusType } from "../../utils/dataTypes";
import { SelectType } from "../../../../../constants/nrcconstants";
import { fetchCommittees, formatCommitteeSelect } from "../../../../../utils/utilCommittees";
import CloseIcon from '@mui/icons-material/Close';

import DeleteIcon from "@mui/icons-material/Delete";

// Expose a ref interface so that parent can trigger close confirmation.
export interface RDUpdatesEditRef {
    triggerClose: () => void;
}

//prop to allow parent to be notified of a confirmed close.
type RDUpdatesEditProps = {
    metadata: GridValidRowModel;
    onRequestClose: () => void;
};

// Use a CSS Grid layout with a fixed column for labels (250px) and a flexible column for inputs.
const renderStaticAndInput = (label: string, element: React.ReactNode, description?: string) => (
    <Box
        sx={{
            display: "grid",
            gridTemplateColumns: "250px 1fr",
            gap: 2,
            alignItems: "center",
            mb: 2,
        }}
    >
        <Box sx={{ textAlign: "left", pl: 2 }}>
            <Typography sx={{ fontWeight: 500 }}>
                {label}:
            </Typography>
            {description && (
                <Typography variant="caption" color="text.secondary">
                    {description}
                </Typography>
            )}
        </Box>
        <Box>
            {element}
        </Box>
    </Box>
);

const dateNonEmpty = (date: string) => date !== '';

const initializeFormData = (metadata: GridValidRowModel) => {
    const initialRdUpdate: RDUpdate = {
        id: metadata.id,
        issuingAgency: metadata.row.issuingAgency ? metadata.row.issuingAgency : "",
        referencedIn: metadata.row.referencedIn ? metadata.row.referencedIn : [],
        withdrawn: metadata.row.withdrawnDate 
            ? [dateNonEmpty(metadata.row.withdrawnDate[0]), dateNonEmpty(metadata.row.withdrawnDate[1])] 
            : [false, false],
        publicationDate: metadata.row.publicationDate 
            ? [new Date(metadata.row.publicationDate[0]), new Date(metadata.row.publicationDate[1])] 
            : [new Date(), new Date()],
        originalDocumentNumber: "",
        updatedDocumentNumberFromIssuer: "",
        updatedDocumentNumberPublicReview: "",
        updatedDocumentNumberCurrent: "",
        changeToDocumentNumber: "",
        originalDocumentTitle: "",
        updatedDocumentTitleFromIssuer: "",
        updatedDocumentTitlePublicReview: "",
        updatedDocumentTitleCurrent: "",
        changeToDocumentTitle: "",
        significantChanges: ["", ""],
        rationale: ["", ""],
        supportingDocument: undefined,
        dateSubmitted: metadata.row.submittedDate 
            ? new Date(metadata.row.submittedDate) 
            : new Date(),
        language: "English",
        submittedBy: metadata.row.submittedBy ? metadata.row.submittedBy : ""
    };

    const initialPrVersion: RDUpdatePRVersion = {
        updatedDocumentNumber: ["", ""],
        updatedDocumentTitle: ["", ""],
        withdrawnDate: new Date(),
        noteOnPCF: ["", ""]
    };

    const initialAnalysis: RDUpdateAnalysis = {
        referencedInCommittees: ["", ""],
        noteOnPCF: ["", ""],
        internalNotes: "",
        workflowInformation: ""
    };

    const initialStatuses: RDUpdateStatuses = {
        statuses: metadata.row.status ? metadata.row.status : []
    };

    const initialFormData: ReferenceDocumentUpdateForm = {
        update: initialRdUpdate,
        prVersion: initialPrVersion,
        analysis: initialAnalysis,
        statuses: initialStatuses,
        lastUpdated: new Date()
    };

    return initialFormData;
};

const steps = ["Update", "Version", "Analysis", "Status"];

// Helper to format a Date into YYYY-MM-DD
const formatDate = (date: Date): string => {
    if (!date || isNaN(date.getTime())) return "";
    return date.toISOString().split("T")[0];
};

// Update the valid status options to match the union defined in dataTypes.ts.
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

// Also update the default status value to one of the valid options.
const defaultStatus: StatusObject = {
    date: new Date(),
    committee: "",
    status: "Received" as statusType,
    decision: false,
    minutesReference: "",
    discussion: "",
    secretaryNotes: "",
    action: "",
    jobComment: ""
};

const RDUpdatesEdit = forwardRef<RDUpdatesEditRef, RDUpdatesEditProps>(({ metadata, onRequestClose }, ref) => {
    const initialFormData = initializeFormData(metadata);
    const [activeStep, setActiveStep] = useState(0);
    const [formData, setFormData] = useState<ReferenceDocumentUpdateForm>(initialFormData);
    const [dirty, setDirty] = useState<boolean>(false); // dirty controls whether any form input has been modified.
    // New state for entering a Status entry:
    const [newStatus, setNewStatus] = useState<StatusObject>(defaultStatus);
    const [confirmOpen, setConfirmOpen] = useState(false);  // Confirmation dialog state
    const [confirmMessage, setConfirmMessage] = useState("");
    const [pendingAction, setPendingAction] = useState<null | (() => void)>(null);
    // Add a list of valid committees to select from.
    const [committeeOptions, setCommitteeOptions] = useState<SelectType[]>([]);

    // Inside the component, add local state for the committees drop-down.
    const [selectedCommittees, setSelectedCommittees] = useState<string[]>(() => {
        // Convert the initial comma-separated value (if any) to an array.
        const initial = formData.analysis.referencedInCommittees[0] || "";
        return initial ? initial.split(",").map(s => s.trim()) : [];
    });
    const [newCommittee, setNewCommittee] = useState<string>("");

    // Expose a close trigger to parent via ref.
    useImperativeHandle(ref, () => ({
        triggerClose: handleCloseRequest
    }));

    useEffect(() => {
        const cookieHeader = document.cookie;
        fetchCommittees(cookieHeader)
          .then((data) => {
              const options = formatCommitteeSelect(data);
              setCommitteeOptions(options);
          })
          .catch((error) => {
              console.error("Failed to fetch committees: ", error);
          });
    }, []);

    const requestNavigation = (action: () => void, message: string) => {
        if (dirty) {
            setConfirmMessage(message);
            setPendingAction(() => {
                setDirty(false);    // If user confirms, discard unsaved changes.
                return action;
            });
            setConfirmOpen(true);
        } else {
            action();
        }
    };

    // All onChange handlers set dirty = true
    const handleUpdateChange = (field: keyof RDUpdate) => (event: ChangeEvent<HTMLInputElement>) => {
        const value = field === "dateSubmitted" ? new Date(event.target.value) : event.target.value;
        setFormData({
            ...formData,
            update: { ...formData.update, [field]: value }
        });
        setDirty(true);
    };

    const handleWithdrawnChange = (index: number) => (event: ChangeEvent<HTMLInputElement>) => {
        const updatedWithdrawn = [...formData.update.withdrawn] as [boolean, boolean];
        updatedWithdrawn[index] = event.target.checked;
        setFormData({
            ...formData,
            update: { ...formData.update, withdrawn: updatedWithdrawn }
        });
        setDirty(true);
    };

    const handlePublicationDateChange = (index: number) => (event: ChangeEvent<HTMLInputElement>) => {
        const updatedPublicationDate = [...formData.update.publicationDate] as [Date, Date];
        updatedPublicationDate[index] = new Date(event.target.value);
        setFormData({
            ...formData,
            update: { ...formData.update, publicationDate: updatedPublicationDate }
        });
        setDirty(true);
    };

    const handleArrayChange = (
        section: "update" | "prVersion" | "analysis",
        field: "significantChanges" | "rationale" | "updatedDocumentNumber" | "updatedDocumentTitle" | "noteOnPCF" | "referencedInCommittees",
        index: number
    ) => (event: ChangeEvent<HTMLInputElement>) => {
        const updatedSection = { ...formData[section] } as any;
        const currentArray = updatedSection[field] as [string, string];
        const updatedArray = [...currentArray] as [string, string];
        updatedArray[index] = event.target.value;
        updatedSection[field] = updatedArray;
        setFormData({ ...formData, [section]: updatedSection });
        setDirty(true);
    };

    const handleVersionDateChange = (event: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            prVersion: { ...formData.prVersion, withdrawnDate: new Date(event.target.value) }
        });
        setDirty(true);
    };

    const handleAnalysisChange = (field: keyof RDUpdateAnalysis) => (event: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            analysis: { ...formData.analysis, [field]: event.target.value }
        });
        setDirty(true);
    };

    const handleStatusesChange = (event: ChangeEvent<HTMLInputElement>) => {
        setDirty(true); // If needed, update statuses and mark dirty.
    };

    const doNextStep = () => {
        setActiveStep((prev) => Math.min(prev + 1, steps.length - 1));
    };

    const doPreviousStep = () => {
        setActiveStep((prev) => Math.max(prev - 1, 0));
    };

    const doStepChange = (newStep: number) => {
        setActiveStep(newStep);
    };

    const handleNextClick = () => {
        requestNavigation(doNextStep, "You have unsaved changes. Do you want to continue without saving?");
    };

    const handleBackClick = () => {
        requestNavigation(doPreviousStep, "You have unsaved changes. Do you want to continue without saving?");
    };

    const handleStepLabelClick = (index: number) => {
        requestNavigation(() => doStepChange(index), "You have unsaved changes. Do you want to continue without saving?");
    };

    const handlePreview = () => {
        console.log("Preview action triggered!");
    };

    // On Save, assume changes are committed so mark dirty as false.
    const handleSave = () => {
        console.log(`Step ${activeStep} saved`);
        setDirty(false);
    };

    // This function is used by the parent X button to trigger a close confirmation if needed.
    const handleCloseRequest = () => {
        requestNavigation(onRequestClose, "You have unsaved changes. Do you want to close without saving?");
    };

    const handleConfirmYes = () => {
        setConfirmOpen(false);
        if (pendingAction) pendingAction();
        setPendingAction(null);
    };

    const handleConfirmNo = () => {
        setConfirmOpen(false);
        setPendingAction(null);
    };

    const handleAddStatus = () => {
        const updatedStatuses = [...formData.statuses.statuses, newStatus];
        setFormData({
            ...formData,
            statuses: { statuses: updatedStatuses }
        });
        setNewStatus(defaultStatus);
        setDirty(true);
    };

    const handleAddCommittee = () => {
        if (newCommittee && !selectedCommittees.includes(newCommittee)) {
            const updated = [...selectedCommittees, newCommittee];
            setSelectedCommittees(updated);
            // Update the analysis field with the updated committees list as a comma-separated string.
            setFormData({
                ...formData,
                analysis: {
                    ...formData.analysis,
                    referencedInCommittees: [updated.join(", "), formData.analysis.referencedInCommittees[1]]
                }
            });
            setNewCommittee("");
            setDirty(true);
        }
    };

    const handleDeleteCommittee = (index: number) => {
        const updated = selectedCommittees.filter((_, i) => i !== index);
        setSelectedCommittees(updated);
        setFormData({
            ...formData,
            analysis: {
                ...formData.analysis,
                referencedInCommittees: [updated.join(", "), formData.analysis.referencedInCommittees[1]]
            }
        });
        setDirty(true);
    };

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return (
                    <Box>
                        {renderStaticAndInput("ID", <Typography>{formData.update.id}</Typography>, "Auto-generated identifier")}
                        {renderStaticAndInput("Agency", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.issuingAgency}
                                onChange={handleUpdateChange("issuingAgency")}
                            />
                        ), "Enter issuing agency")} 
                        {renderStaticAndInput(
                            "Withdrawn",
                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                <FormControlLabel
                                    control={
                                        <Checkbox 
                                            checked={formData.update.withdrawn[0]} 
                                            onChange={handleWithdrawnChange(0)}
                                        />
                                    }
                                    label="EN"
                                />
                                <FormControlLabel
                                    control={
                                        <Checkbox 
                                            checked={formData.update.withdrawn[1]} 
                                            onChange={handleWithdrawnChange(1)}
                                        />
                                    }
                                    label="FR"
                                />
                            </Box>,
                            "Select for each language"
                        )}
                        {renderStaticAndInput("Publication Date (EN)", (
                            <TextField
                                size="small"
                                type="date"
                                InputLabelProps={{ shrink: true }}
                                value={formatDate(formData.update.publicationDate[0])}
                                onChange={handlePublicationDateChange(0)}
                            />
                        ), "Select publication date for English")}
                        {renderStaticAndInput("Original Document Number", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.originalDocumentNumber}
                                onChange={handleUpdateChange("originalDocumentNumber")}
                            />
                        ), "Enter the original document number")}
                        {renderStaticAndInput("Updated Document Number - Issuer", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentNumberFromIssuer}
                                onChange={handleUpdateChange("updatedDocumentNumberFromIssuer")}
                            />
                        ), "Enter the updated document number as provided by the issuer")}
                        {renderStaticAndInput("Updated Document Number (Public Review)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentNumberPublicReview}
                                onChange={handleUpdateChange("updatedDocumentNumberPublicReview")}
                            />
                        ), "Enter the updated document number for public review")}
                        {renderStaticAndInput("Updated Document Number (Current)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentNumberCurrent}
                                onChange={handleUpdateChange("updatedDocumentNumberCurrent")}
                            />
                        ), "Enter the current document number")}
                        {renderStaticAndInput("Change to Document Number", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.changeToDocumentNumber}
                                onChange={handleUpdateChange("changeToDocumentNumber")}
                            />
                        ), "Specify if changing the document number")}
                        {renderStaticAndInput("Original Document Title", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.originalDocumentTitle}
                                onChange={handleUpdateChange("originalDocumentTitle")}
                            />
                        ), "Enter the original document title")}
                        {renderStaticAndInput("Updated Document Title - Issuer", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentTitleFromIssuer}
                                onChange={handleUpdateChange("updatedDocumentTitleFromIssuer")}
                            />
                        ), "Enter the updated title from issuer")}
                        {renderStaticAndInput("Updated Document Title (Public Review)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentTitlePublicReview}
                                onChange={handleUpdateChange("updatedDocumentTitlePublicReview")}
                            />
                        ), "Enter the title used during public review")}
                        {renderStaticAndInput("Updated Document Title (Current)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.updatedDocumentTitleCurrent}
                                onChange={handleUpdateChange("updatedDocumentTitleCurrent")}
                            />
                        ), "Enter the current document title")}
                        {renderStaticAndInput("Change to Document Title", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.changeToDocumentTitle}
                                onChange={handleUpdateChange("changeToDocumentTitle")}
                            />
                        ), "Specify if changing the document title")}
                        {renderStaticAndInput("Significant Changes (EN)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.significantChanges[0]}
                                onChange={handleArrayChange("update", "significantChanges", 0)}
                            />
                        ), "Describe any significant changes in English")}
                        {renderStaticAndInput("Significant Changes (FR)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.significantChanges[1]}
                                onChange={handleArrayChange("update", "significantChanges", 1)}
                            />
                        ), "Describe any significant changes in French")}
                        {renderStaticAndInput("Rationale/Justification (EN)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.rationale[0]}
                                onChange={handleArrayChange("update", "rationale", 0)}
                            />
                        ), "Provide the rationale in English")}
                        {renderStaticAndInput("Rationale/Justification (FR)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.rationale[1]}
                                onChange={handleArrayChange("update", "rationale", 1)}
                            />
                        ), "Provide the rationale in French")}
                        {renderStaticAndInput("Date Submitted", (
                            <TextField
                                size="small"
                                type="date"
                                InputLabelProps={{ shrink: true }}
                                value={formatDate(formData.update.dateSubmitted)}
                                onChange={handleUpdateChange("dateSubmitted")}
                            />
                        ), "Select the date of submission")}
                        {renderStaticAndInput("Language", (
                            <TextField
                                select
                                size="small"
                                variant="outlined"
                                value={formData.update.language}
                                onChange={handleUpdateChange("language")}
                            >
                                <MenuItem value="English">English</MenuItem>
                                <MenuItem value="French">French</MenuItem>
                            </TextField>
                        ), "Choose the document language")}
                        {renderStaticAndInput("Submitted By", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.update.submittedBy}
                                onChange={handleUpdateChange("submittedBy")}
                            />
                        ), "Enter the submitter's name")}
                    </Box>
                );
            case 1:
                return (
                    <Box component="form">
                        {renderStaticAndInput("Updated Document Number (EN)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.updatedDocumentNumber[0]}
                                onChange={handleArrayChange("prVersion", "updatedDocumentNumber", 0)}
                            />
                        ), "Enter updated document number in English")}
                        {renderStaticAndInput("Updated Document Number (FR)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.updatedDocumentNumber[1]}
                                onChange={handleArrayChange("prVersion", "updatedDocumentNumber", 1)}
                            />
                        ), "Enter updated document number in French")}
                        {renderStaticAndInput("Updated Document Title (EN)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.updatedDocumentTitle[0]}
                                onChange={handleArrayChange("prVersion", "updatedDocumentTitle", 0)}
                            />
                        ), "Enter updated document title in English")}
                        {renderStaticAndInput("Updated Document Title (FR)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.updatedDocumentTitle[1]}
                                onChange={handleArrayChange("prVersion", "updatedDocumentTitle", 1)}
                            />
                        ), "Enter updated document title in French")}
                        {renderStaticAndInput("Withdrawn Date", (
                            <TextField
                                size="small"
                                type="date"
                                InputLabelProps={{ shrink: true }}
                                onChange={handleVersionDateChange}
                            />
                        ), "Select the withdrawn date")}
                        {renderStaticAndInput("Note on PCF (EN)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.noteOnPCF[0]}
                                onChange={handleArrayChange("prVersion", "noteOnPCF", 0)}
                            />
                        ), "Enter note on PCF in English")}
                        {renderStaticAndInput("Note on PCF (FR)", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={formData.prVersion.noteOnPCF[1]}
                                onChange={handleArrayChange("prVersion", "noteOnPCF", 1)}
                            />
                        ), "Enter note on PCF in French")}
                    </Box>
                );
            case 2:
                return (
                    <Box component="form">
                        <Box sx={{ mb: 2 }}>
                            <Box
                                sx={{
                                    display: "grid",
                                    gridTemplateColumns: "250px 1fr",
                                    gap: 2,
                                    mb: 2,
                                    alignItems: "start" // top align so label doesn't get pushed down
                                }}
                            >
                                <Box sx={{ textAlign: "left", pl: 2 }}>
                                    <Typography sx={{ fontWeight: 500 }}>Committees:</Typography>
                                    <Typography variant="caption" color="text.secondary">
                                        Select and add committees
                                    </Typography>
                                </Box>
                                <Box>
                                    <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
                                        <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                                            <TextField
                                                select
                                                size="small"
                                                label="Select Committee"
                                                value={newCommittee}
                                                onChange={(e) => setNewCommittee(e.target.value)}
                                                sx={{ flexGrow: 1, maxWidth: 300 }}
                                            >
                                                {committeeOptions.map((option) => (
                                                    <MenuItem key={option.value} value={option.label}>
                                                        {option.label}
                                                    </MenuItem>
                                                ))}
                                            </TextField>
                                            <Button variant="contained" onClick={handleAddCommittee}>
                                                Add
                                            </Button>
                                        </Box>
                                        <Box sx={{ mt: 1 }}>
                                            {selectedCommittees.length === 0 ? (
                                                <Typography variant="body2" color="text.secondary">
                                                    No committees selected.
                                                </Typography>
                                            ) : (
                                                selectedCommittees.map((committee, index) => (
                                                    <Box
                                                        key={index}
                                                        sx={{
                                                            display: "flex",
                                                            alignItems: "center",
                                                            justifyContent: "space-between",
                                                            border: "1px solid #ccc",
                                                            borderRadius: 1,
                                                            p: 1,
                                                            my: 0.5,
                                                            maxWidth: 300
                                                        }}
                                                    >
                                                        <Typography variant="body2">
                                                            {committee}
                                                        </Typography>
                                                        <IconButton size="small" onClick={() => handleDeleteCommittee(index)}>
                                                            <DeleteIcon fontSize="small" />
                                                        </IconButton>
                                                    </Box>
                                                ))
                                            )}
                                        </Box>
                                    </Box>
                                </Box>
                            </Box>
                            {renderStaticAndInput("Note on PCF (EN)", (
                                <TextField
                                    size="small"
                                    variant="outlined"
                                    value={formData.analysis.noteOnPCF[0]}
                                    onChange={handleArrayChange("analysis", "noteOnPCF", 0)}
                                />
                            ), "Enter note on PCF in English")}
                            {renderStaticAndInput("Note on PCF (FR)", (
                                <TextField
                                    size="small"
                                    variant="outlined"
                                    value={formData.analysis.noteOnPCF[1]}
                                    onChange={handleArrayChange("analysis", "noteOnPCF", 1)}
                                />
                            ), "Enter note on PCF in French")}
                            {renderStaticAndInput("Internal Notes", (
                                <TextField
                                    size="small"
                                    multiline
                                    variant="outlined"
                                    value={formData.analysis.internalNotes}
                                    onChange={handleAnalysisChange("internalNotes")}
                                />
                            ), "Add any internal notes")}
                            {renderStaticAndInput("Workflow Information", (
                                <TextField
                                    size="small"
                                    multiline
                                    variant="outlined"
                                    value={formData.analysis.workflowInformation}
                                    onChange={handleAnalysisChange("workflowInformation")}
                                />
                            ), "Provide workflow information")}
                        </Box>
                    </Box>
                );
            case 3:
                return (
                    <Box component="form">
                        {renderStaticAndInput("Status Date", (
                            <TextField
                                size="small"
                                type="date"
                                InputLabelProps={{ shrink: true }}
                                value={formatDate(newStatus.date)}
                                onChange={(e) =>
                                    setNewStatus({ ...newStatus, date: new Date(e.target.value) })
                                }
                            />
                        ), "Enter the status update date")}
                        {renderStaticAndInput("Committee", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={newStatus.committee}
                                onChange={(e) =>
                                    setNewStatus({ ...newStatus, committee: e.target.value })
                                }
                            />
                        ), "Enter committee involved")}
                        {renderStaticAndInput("Status", (
                            <TextField
                                select
                                size="small"
                                variant="outlined"
                                value={newStatus.status}
                                onChange={(e) =>
                                    setNewStatus({ ...newStatus, status: e.target.value as statusType })
                                }
                            >
                                {statusOptions.map((option) => (
                                    <MenuItem key={option} value={option}>
                                        {option}
                                    </MenuItem>
                                ))}
                            </TextField>
                        ), "Select current status")}
                        {renderStaticAndInput("Decision", (
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={newStatus.decision}
                                        onChange={(e) => setNewStatus({ ...newStatus, decision: e.target.checked })}
                                    />
                                }
                                label="Approved"
                            />
                        ), "Check if approved")}
                        {renderStaticAndInput("Minutes Reference", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={newStatus.minutesReference}
                                onChange={(e) => setNewStatus({ ...newStatus, minutesReference: e.target.value })}
                            />
                        ), "Enter reference for minutes")}
                        {renderStaticAndInput("Discussion", (
                            <TextField
                                size="small"
                                multiline
                                variant="outlined"
                                value={newStatus.discussion}
                                onChange={(e) => setNewStatus({ ...newStatus, discussion: e.target.value })}
                            />
                        ), "Enter discussion details")}
                        {renderStaticAndInput("Secretary Notes", (
                            <TextField
                                size="small"
                                multiline
                                variant="outlined"
                                value={newStatus.secretaryNotes}
                                onChange={(e) => setNewStatus({ ...newStatus, secretaryNotes: e.target.value })}
                            />
                        ), "Enter secretary's notes")}
                        {renderStaticAndInput("Action", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={newStatus.action}
                                onChange={(e) => setNewStatus({ ...newStatus, action: e.target.value })}
                            />
                        ), "Enter action taken")}
                        {renderStaticAndInput("Job Comment", (
                            <TextField
                                size="small"
                                variant="outlined"
                                value={newStatus.jobComment}
                                onChange={(e) => setNewStatus({ ...newStatus, jobComment: e.target.value })}
                            />
                        ), "Enter job comment")}
                        <Button onClick={handleAddStatus} variant="contained" sx={{ mt: 2 }}>
                            Add Status
                        </Button>

                        {/* Display added statuses as static text */}
                        <Box sx={{ mt: 4 }}>
                            <Typography variant="subtitle1" sx={{ mb: 1 }}>
                                Added Status Entries:
                            </Typography>
                            {formData.statuses.statuses.length === 0 && (
                                <Typography variant="body2" color="text.secondary">
                                    No statuses added.
                                </Typography>
                            )}
                            {formData.statuses.statuses.map((statusEntry, index) => (
                                <Box 
                                    key={index} 
                                    sx={{ 
                                        p: 1, 
                                        mb: 1, 
                                        border: "1px solid #ccc", 
                                        borderRadius: 1,
                                        backgroundColor: "#f9f9f9"
                                    }}
                                >
                                    <Typography variant="body2">
                                        <strong>Date:</strong> {formatDate(statusEntry.date)}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Committee:</strong> {statusEntry.committee}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Status:</strong> {statusEntry.status}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Decision:</strong> {statusEntry.decision ? "Approved" : "Rejected"}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Minutes Reference:</strong> {statusEntry.minutesReference}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Discussion:</strong> {statusEntry.discussion}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Secretary Notes:</strong> {statusEntry.secretaryNotes}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Action:</strong> {statusEntry.action}
                                    </Typography>
                                    <Typography variant="body2">
                                        <strong>Job Comment:</strong> {statusEntry.jobComment}
                                    </Typography>
                                </Box>
                            ))}
                        </Box>
                    </Box>
                );
            default:
                return <div>Unknown Step</div>;
        }
    };

    return (
        <Box sx={{ p: 0 }}>
            {/* Banner with Preview Button and Stepper */}
            <Box sx={{ display: 'flex', alignContent: 'flex-start',  mb: 4 }}>
                <IconButton edge="start" color="inherit" sx={{ mr: 2, height: '50%' }} onClick={handleCloseRequest} aria-label="close">
                    <CloseIcon />
                </IconButton>
                <Button size="small" variant="outlined" sx={{ mr: 1, height: '50%' }} onClick={handlePreview}>
                    Preview
                </Button>
                <Box sx={{ alignContent: 'center', width: '80%' }}>
                    <Stepper activeStep={activeStep} alternativeLabel sx={stepperContainerStyle}>
                        {steps.map((label, index) => (
                            <Step key={label}>
                                <StepLabel sx={{ cursor: "pointer" }} onClick={() => handleStepLabelClick(index)}>
                                    {label}
                                </StepLabel>
                            </Step>
                        ))}
                    </Stepper>
                </Box>
            </Box>

            {/* Persistent language warning */}
            {(formData.update.language === "English" || formData.update.language === "French") && (
                <Alert severity="warning" sx={{ mb: 3 }}>
                    This document is published in {formData.update.language} only.
                </Alert>
            )}

            {/* Form Content */}
            <Box sx={{ mb: 2 }}>
                {getStepContent(activeStep)}
            </Box>

            {/* Navigation Buttons */}
            <Stack direction="row" spacing={2} justifyContent="flex-end">
                <Button variant="contained" onClick={handleBackClick}>
                    Back
                </Button>
                <Button
                    variant="contained"
                    color="primary"
                    onClick={handleSave}
                    sx={{ px: 4, py: 1.5, minWidth: '120px' }} // Increased padding and minimum width for a wider Save button
                >
                    Save
                </Button>
                {activeStep < steps.length - 1 && (
                    <Button variant="contained" onClick={handleNextClick}>
                        Next
                    </Button>
                )}
            </Stack>

            {/* Confirmation Dialog */}
            <Dialog
                open={confirmOpen}
                onClose={handleConfirmNo}
                aria-labelledby="confirm-dialog-title"
                aria-describedby="confirm-dialog-description"
            >
                <DialogTitle id="confirm-dialog-title">Unsaved Changes</DialogTitle>
                <DialogContent>
                    <DialogContentText id="confirm-dialog-description">
                        {confirmMessage}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleConfirmNo} color="primary">No</Button>
                    <Button onClick={handleConfirmYes} color="primary" autoFocus>
                        Yes
                    </Button>
                </DialogActions>
            </Dialog>
        </Box>
    );
});

export default RDUpdatesEdit;