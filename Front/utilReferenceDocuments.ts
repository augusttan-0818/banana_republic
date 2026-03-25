"use client";
import { StandardsInfo, Organization, OrganizationAndStandards } from "@/app/api/referencedocuments/ReferenceDocument";
import { APP_BASE_URL } from "@/constants/nrcconstants";
import { ReferenceDocumentRecord, statusType, RDUpdate } from "@/app/[locale]/reference-documents/utils/dataTypes";

export interface StatusOption {
    id: number;
    name: string;
    nameFR: string | null;
    sortOrder: number;
}

export interface SubStatusOption {
    id: number;
    name: string;
    nameFR: string | null;
    sortOrder: number | null;
}

export interface CommitteeOption {
    committeeId: number;
    committeeName: string | null;
    committeeShortName: string | null;
    committeeType: number;
} 


export async function fetchAllStandardsAndOrgs(cookieHeader: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> { 
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    // Convert to 1-based pagination for the API (consistency)
    const apiPage = page + 1;
    const url = `${baseUrl}/api/referencedocuments/AllstandardsAndOrgs?page=${apiPage}&pageSize=${pageSize}`;

    try {
        const response = await fetch(url, { 
            headers: { 
                Cookie: cookieHeader,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }, 
            cache: 'no-store' 
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
            throw new Error('Invalid response content-type. Expected JSON.');
        }

        let data: OrganizationAndStandards[];
        try {
            const rawData = await response.json();
            if (!Array.isArray(rawData)) {
                throw new Error('Expected array of organizations datatype');
            }
            data = rawData;
        } catch (parseError) {
            throw new Error('Failed to parse response as JSON');
        }

        // Transform the data into ReferenceDocumentRecord format
        const transformedData: ReferenceDocumentRecord[] = data.flatMap(org => 
            (org.standards?.standardGrp || []).map(standard => ({
                issuingAgency: org.orgGroupID || '',
                documentNumber: standard.standardRefNumber || '',
                titleOfDocument: standard.standardRefTitle || '',
                referencedIn: standard.applyTo ? standard.applyTo.split(' ').filter(Boolean) : [],
                responsibleCommittees: '',
                status: '' as statusType
            }))
        );

        const headersInfo = response.headers.get('x-pagination');
        const rowCount = headersInfo ? parseInt(JSON.parse(headersInfo).TotalItemCount) : 0;
        
        return [transformedData, rowCount];
    } catch (error) {
        throw error;
    }
}

export async function fetchAllApplyToStandards(cookieHeader: string, page: number, pageSize: number): Promise<[RDUpdate[], number]> { 
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    // Convert to 1-based pagination for the API (consistency)
    const apiPage = page + 1;
    const url = `${baseUrl}/api/referencedocumentupdates/standardupdates/list?page=${apiPage}&pageSize=${pageSize}`;

    try {
        const response = await fetch(url, { 
            headers: { 
                Cookie: cookieHeader,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }, 
            cache: 'no-store' 
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
            throw new Error('Invalid response content-type. Expected JSON.');
        }

        // Parse the PagedResult response
        let pagedResult: { items: any[]; totalCount: number };
        try {
            const rawData = await response.json();
            if (!rawData.items || typeof rawData.totalCount !== 'number') {
                throw new Error('Expected PagedResult with items and totalCount');
            }
            pagedResult = rawData;
        } catch (parseError) {
            throw new Error('Failed to parse response as JSON');
        }

        // Transform the data to match the DataGrid columns
        // API returns: Id, DocNumber, AgencyName, ReferencedIn, WithdrawnDate, PublicationDate, RequestDate, UpdatedBy
        const transformedData = pagedResult.items.map((item: any) => ({
            id: item.id ?? 0,
            issuingAgency: item.agencyName ?? '',
            documentNumber: item.docNumber ?? '',
            referencedIn: item.referencedIn ?? '',
            withdrawnDate: item.withdrawnDate ?? null,
            publicationDate: item.publicationDate ?? null,
            submittedDate: item.requestDate ?? null,
            submittedBy: item.updatedBy ?? '',
            // Empty fields for now
            SCs: [],
            status: [],
            PR: null,
            job: null,
        }));

        return [transformedData as unknown as RDUpdate[], pagedResult.totalCount];
    } catch (error) {
        console.error(`Error fetching data from ${url}:`, error);
        throw error;
    }
}

export async function fetchAllStandardOrgs(cookieHeader: string, page: number, pageSize: number): Promise<[Organization[], number]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/referencedocuments/AllStandardOrgs?page=${page}&pageSize=${pageSize}`;

    try {
        const response = await fetch(url, { 
            headers: { 
                Cookie: cookieHeader,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }, 
            cache: 'no-store' 
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
            throw new Error('Invalid response content-type. Expected JSON.');
        }

        const data: Organization[] = await response.json();
        const headersInfo = response.headers.get('x-pagination');
        const rowCount = headersInfo ? parseInt(JSON.parse(headersInfo).TotalItemCount) : 0;
        
        return [data, rowCount];
    } catch (error) {
        console.error(`Error fetching data from ${url}:`, error);
        throw error;
    }
}

export async function fetchStandardsByOrg(cookieHeader: string, orgGroupID: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/referencedocuments/StandardsByOrg/${orgGroupID}?page=${page}&pageSize=${pageSize}`;

    try {
        const response = await fetch(url, { 
            headers: { 
                Cookie: cookieHeader,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }, 
            cache: 'no-store' 
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
            throw new Error('Invalid response content-type. Expected JSON.');
        }

        const data: OrganizationAndStandards = await response.json();
        const transformedData: ReferenceDocumentRecord[] = (data.standards?.standardGrp || []).map(standard => ({
            issuingAgency: data.orgGroupID || '',
            documentNumber: standard.standardRefNumber || '',
            titleOfDocument: standard.standardRefTitle || '',
            referencedIn: standard.applyTo ? standard.applyTo.split(' ').filter(Boolean) : [],
            responsibleCommittees: '',
            status: '' as statusType
        }));
        const headersInfo = response.headers.get('x-pagination');
        const rowCount = headersInfo ? parseInt(JSON.parse(headersInfo).TotalItemCount) : 0;
        return [transformedData, rowCount];
    } catch (error) {
        console.error(`Error fetching data from ${url}:`, error);
        throw error;
    }
}

export async function fetchStandardByStandID(cookieHeader: string, StandID: string, page: number, pageSize: number): Promise<[ReferenceDocumentRecord[], number]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/referencedocuments/StandardLookup?standId=${StandID}`;

    try {
        const response = await fetch(url, { 
            headers: { 
                Cookie: cookieHeader,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }, 
            cache: 'no-store' 
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
            throw new Error('Invalid response content-type. Expected JSON.');
        }

        const data: StandardsInfo = await response.json();
        // Map the correct property for issuingAgency (if available)
        const transformedData: ReferenceDocumentRecord = {
            issuingAgency: (data as any).orgGroupID || '',
            documentNumber: data.standardRefNumber || '',
            titleOfDocument: data.standardRefTitle || '',
            referencedIn: data.applyTo ? data.applyTo.split(' ').filter(Boolean) : [],
            responsibleCommittees: '',
            status: '' as statusType
        };
        return [[transformedData], 1];
    } catch (error) {
        console.error(`Error fetching data from ${url}:`, error);
        throw error;
    }
}

export async function fetchAllStatuses(): Promise<StatusOption[]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/referencedocumentupdates/statuses`;

    try {
        const response = await fetch(url, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            cache: 'no-store'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data: StatusOption[] = await response.json();
        return data;
    } catch (error) {
        console.error(`Error fetching statuses from ${url}:`, error);
        throw error;
    }
}

export async function fetchSubStatusesByStatusId(statusId: number): Promise<SubStatusOption[]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/referencedocumentupdates/statuses/${statusId}/substatuses`;

    try {
        const response = await fetch(url, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            cache: 'no-store'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data: SubStatusOption[] = await response.json();
        return data;
    } catch (error) {
        console.error(`Error fetching sub-statuses from ${url}:`, error);
        throw error;
    }
}

export async function fetchCommitteesByType(committeeType: number): Promise<CommitteeOption[]> {
    const isServer = typeof window === 'undefined';
    const baseUrl = isServer ? APP_BASE_URL : '';
    const url = `${baseUrl}/api/committee/type/${committeeType}`;

    try {
        const response = await fetch(url, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            cache: 'no-store'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data: CommitteeOption[] = await response.json();
        return data;
    } catch (error) {
        console.error(`Error fetching committees from ${url}:`, error);
        throw error;
    }
}
