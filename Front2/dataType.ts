export type AxisPosition = 'top' | 'bottom' | undefined;
export type AxisScaleType = 'time' | 'band' | 'point' | 'log' | 'pow' | 'sqrt' | 'utc' | 'linear' | undefined;

export type PublicReview = {
    publicReviewTitle: string,
    PCFsDueDate: Date
};

export type statusType = 
    "Received" | "Sorted" | "Analyzed" | "Reviewed" | "Posted for pre-Public Review" |
    "Posted for Public Review" | "Posted for Public Review Comments" | "Posted for post-Public Review"| 
    "Ready for P&M Processing" | "Published" | "Completed";

// The committee type remains available if needed elsewhere
export type committee = {
    cbhcc: boolean;
    divac: boolean;
    ed: boolean;
    ee: boolean;
    es: boolean;
    fp: boolean;
    hma: boolean;
    hp: boolean;
    hsb: boolean;
    nmccAEB: boolean;
    nmccAccess: boolean;
    nmccCCA: boolean;
    nmccCD: boolean;
    nmccFLS: boolean;
    nmccHS: boolean;
    nmccHrmzn: boolean;
    nmccIndE: boolean;
    nmccMiti: boolean;
    nmccPBS: boolean;
    nmccRefDocs: boolean;
    nmccSD: boolean;
    sccc: boolean;
    sd: boolean;
    ttvc: boolean;
    ue: boolean;
};

export type ReferenceDocumentRecord = {
    issuingAgency: string;      // The agency that issued the document
    documentNumber: string;     // The document number
    titleOfDocument: string;    // The title of the document 
    referencedIn: string[];     // List of codes which make reference to this document
    responsibleCommittees: string; // Comma-separated string from the API (e.g., "DIVAC, NMCC-RefDocs, NMCC-Hrmzn, CBHCC")
    status: statusType;         // The status of the document
};

export type ReferenceDocumentUpdate = {
    id: Number;                 // The unique identifier for database purposes
    issuingAgency: string;      // The agency that issued the document
    documentNumber: string;     // The document number
    referencedIn: string[];     // List of codes which make reference to this document
    withdrawnDate: [Date, Date]; // [EN,FR]
    publicationDate: [Date, Date]; // [EN,FR]
    submittedDate: Date;        // The date when the document was submitted into the system
    submittedBy: string;        // The email address of the person who submitted the document
    SCs: string[];             // List of standard committees (SCs) associated with the document
    status: statusType[];         // The status of the document in the workflow of being approved
    PR: null;                  // TODO: Investigate what this data will look like
    job: null;                 // TODO: Investigate what this data will look like 
    version: Number;           // The version of the document in question
}

export type CommitteesCodeResponsibility = {
    committee: string;        // The name of the committee
    ta: string[];             // List of technical areas (TA) associated with the committee
    teamCoordinator: string;  // The name of the team coordinator
    sortingDoneBy: string;    // The name of the person who did the sorting
    codeResponsibility: string; // Section of the code that the committee is responsible
}

export type RelatedCommittees = {
    DocumentNumber: string;  // The document number 
    IssuingAgency: string;   // The agency that issued the document
    responsibleCommittees: string[]; // List of committees related to this document number
    code: string;            // These could be in FR or EN so need to convert to EN for consistency
    reference: string;       // Division, article, section, etc. of the code that the committee is responsible for
}

// RD Update interface holds the main update instance details.
export interface RDUpdate {
    id: number; // Unique identifier for database purposes.
    issuingAgency: string; // The agency that issued the document.
    referencedIn: string[]; // List of codes which make reference to this document.
    withdrawn: [boolean, boolean]; // Indicates if the document is withdrawn (EN, FR).
    publicationDate: [Date, Date]; // Publication date (EN, FR).
    originalDocumentNumber: string; // The original document number.
    updatedDocumentNumberFromIssuer: string; // Updated document number as submitted by the issuing agency.
    updatedDocumentNumberPublicReview: string; // Updated document number for the public review version.
    updatedDocumentNumberCurrent: string; // Updated document number for the current version.
    changeToDocumentNumber: string; // Any change to the document number.
    originalDocumentTitle: string; // The original document title.
    updatedDocumentTitleFromIssuer: string; // Updated document title as submitted by the issuing agency.
    updatedDocumentTitlePublicReview: string; // Updated document title for the public review version.
    updatedDocumentTitleCurrent: string; // Updated document title for the current version.
    changeToDocumentTitle: string; // Any change to the document title.
    significantChanges: [string, string]; // Significant changes described (EN, FR).
    rationale: [string, string]; // Rationale or justification for changes (EN, FR).
    supportingDocument?: [File, File]; // Supporting document (EN, FR).
    dateSubmitted: Date; // Date when the update was submitted.
    language: "English" | "French"; // Submission language (English or French).
    submittedBy: string; // Email of the person who submitted the document.
}

// RDUpdatePRVersion interface holds details specific to the Public Review version of the update.
export interface RDUpdatePRVersion {
    updatedDocumentNumber: [string, string]; // Updated document number (EN, FR).
    updatedDocumentTitle: [string, string]; // Updated document title (EN, FR).
    withdrawnDate: Date; // Withdrawn date (EN, FR)
    noteOnPCF: [string, string]; // Note on PCF (EN, FR).
}

// RDUpdateAnalysis interface holds analysis-related details for the update instance.
// TODO: Confirm if certain fields can be optional.
export interface RDUpdateAnalysis {
    referencedInCommittees: [string, string]; // Committees referencing this document (EN, FR).
    noteOnPCF: [string, string] // Note on PCF (EN, FR).
    internalNotes: string; // Internal notes about the update.
    workflowInformation: string; // Workflow information.
}

// StatusObject interface defines each update status entry.
// The structure follows the same style as defined in dataTypes.ts.
export interface StatusObject {
    date: Date; // Date of the status update.
    committee: string; // Committee involved in the status update.
    status: statusType; // Current status of the document update.
    decision: boolean; // Decision: true if approved, false if disapproved.
    minutesReference: string; // Reference for minutes.
    discussion: string; // Discussion details.
    secretaryNotes: string; // Secretary's notes regarding the status.
    action: string; // Action taken in this update step.
    jobComment: string; // Job comment related to the status.
}

// RDUpdateStatuses interface groups all status entries for the update.
export interface RDUpdateStatuses {
    statuses: StatusObject[]; // Array of status objects.
}

// Main form interface that aggregates all sections of the reference document update.
// This aligns with the separation of main update, public review, analysis, and status details.
export interface ReferenceDocumentUpdateForm {
    update: RDUpdate; // Main update instance details.
    prVersion: RDUpdatePRVersion; // Public Review version details.
    analysis: RDUpdateAnalysis; // Analysis-related details.
    statuses: RDUpdateStatuses; // Grouped status entries.
    lastUpdated?: Date; // Metadata: Last updated date/time.
}
