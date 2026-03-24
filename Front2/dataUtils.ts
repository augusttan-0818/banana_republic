"use client";
import { fetchAllStandardsAndOrgs, fetchAllApplyToStandards, fetchAllStandardOrgs, fetchStandardsByOrg, fetchStandardByStandID} from "@/utils/utilReferenceDocuments";
import type { Organization } from "@/app/api/referencedocuments/ReferenceDocument";
import { ReferenceDocumentRecord, RDUpdate } from "./dataTypes"


// This function is a placeholder for fetching all RD's which have the status "Completed"
export async function getReferenceDocumentRecords(cookie: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> {
    const [reports, count] = await fetchAllStandardsAndOrgs(cookie, page, pageSize);
    // TODO: Combine multiple API calls into a grid component for rd-record and to show relevant data
    return [reports, count];
}
// This function is a placeholder for fetching all RD's which have the status "In Progress"
export async function getReferenceDocumentUpdates(cookie: string, page: number, pageSize: number): Promise<[RDUpdate[], number]> {
    const [updates, count] = await fetchAllApplyToStandards(cookie, page, pageSize);
    // TODO: Combine multiple API calls into a grid component for rd-upload to show relevant data
    return [updates, count];
}

export async function getAllStandardOrgs(cookie: string, page: number, pageSize: number): Promise<[Organization[], number]> {
    return await fetchAllStandardOrgs(cookie, page, pageSize);
}

// This function returns all standards given an organization ID.
export async function getStandardsByOrg(cookie: string, orgGroupID: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> {
    return await fetchStandardsByOrg(cookie, orgGroupID, page, pageSize);
}

// This function returns a single standard by its standID.
export async function getStandardByStandID(cookie: string, standID: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> {
    return await fetchStandardByStandID(cookie, standID, page, pageSize);
}
