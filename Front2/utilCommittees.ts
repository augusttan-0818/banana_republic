// src/utils/apiHelpers.ts

import { CommitteesResponse } from "@/app/api/committees/all-committees/CommitteesResponse";
import { ExistingParentCommitteeResponse } from "@/app/api/committees/existing-parentcommittees/ExistingParentCommitteeResponse";
import { APP_BASE_URL, SelectType, serverBase } from '../constants/nrcconstants';

export type ParentCommittee = {
    value: string;
    label: string;
};

export type  Committee = {
    committeeId: number;
    committeeName: string;
    committeeShortName: string;
    codesCycleId: string;
    committeeType: string;
    parentCommitteeId?: string | null;
  }

export async function fetchCommittees(cookieHeader: string): Promise<CommitteesResponse[]> {
  // Determine if we're running on the server or client
  const isServer = typeof window === 'undefined';
  
  const orgurl = `/api/committees/all-committees`;
  // Use absolute URL on the server and relative URL on the client
  const url = isServer ? `${serverBase}${orgurl}` : orgurl;
    
  try {
    const response = await fetch(url, {
        headers: {
          Cookie: cookieHeader,
        },
        cache: 'no-store',
      });
   // console.log(" we are in response in fetchcommittees ", response)
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    // Check if response body has content
    const contentType = response.headers.get('content-type');
    if (!contentType || !contentType.includes('application/json')) {
        throw new Error('Invalid response content-type. Expected JSON.');
    }
    // Parse the JSON
    const data: CommitteesResponse[] = await response.json();
    return data;
  } catch (error) {
    console.error(`Error fetching data from ${url}:`, error);
    throw error;
  }
}

export async function fetchCommitteesByType(cookieHeader: string, typeId:string): Promise<CommitteesResponse[]> {
    // Determine if we're running on the server or client
    const isServer = typeof window === 'undefined';
    const orgurl = `/api/committees/committee-bytype?type=${typeId}`;
    // Use absolute URL on the server and relative URL on the client
    const url = isServer ? `${serverBase}${orgurl}` : orgurl;
  
    try {
        const response = await fetch(url, {
            headers: {
              Cookie: cookieHeader,
            },
            cache: 'no-store',
          });
  
      if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
      }
      // Check if response body has content
      const contentType = response.headers.get('content-type');
      if (!contentType || !contentType.includes('application/json')) {
          throw new Error('Invalid response content-type. Expected JSON.');
      }
      // Parse the JSON
      const data: CommitteesResponse[] = await response.json();
      return data;
    } catch (error) {
      console.error(`Error fetching data from ${url}:`, error);
      throw error;
    }
  }

export async function fetchCommittee(cookieHeader: string, id: string): Promise<Committee> {
    const isServer = typeof window === 'undefined';
    const orgurl = `/api/committees/${id}`;
    // Use absolute URL on the server and relative URL on the client
    const url = isServer ? `${serverBase}${orgurl}` : orgurl;
   
    const committeeResponse = await fetch(url, {
        headers: {
          Cookie: cookieHeader,
        },
        cache: 'no-store',
      });
    
    const committee = await committeeResponse.json()
    return committee;
  }
  
export async function fetchExistingParentCommittees(cookieHeader: string): Promise<ParentCommittee[]> {
    const isServer = typeof window === 'undefined';
    const orgurl = `/api/committees/existing-parentcommittees`;
    // Use absolute URL on the server and relative URL on the client
    const url = isServer ? `${serverBase}${orgurl}` : orgurl;
   
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                Cookie: cookieHeader,
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            console.error('Failed to fetch existing parent committees:', response.statusText);
            return [];
        }
        const parentCommitteesRaw = await response.json();
        const parentCommittees = Array.isArray(parentCommitteesRaw)
            ? parentCommitteesRaw.map((committee: ExistingParentCommitteeResponse) => ({
                value: (committee.committeeId ?? '').toString(),
                label: committee.committeeName,
            })) : [];
        // Add "None" option for parent committee dropdown
        parentCommittees.unshift({ value: '', label: 'None' });

        // Transform API data into desired format for dropdowns
        return parentCommittees;
    } catch (error) {
        console.error('Error fetching parent committees:', error);
        return [];
    }
}

export async function fetchAllParentCommittees(cookieHeader: string): Promise<ParentCommittee[]> {
    
    try {
        const isServer = typeof window === 'undefined';
        const orgurl = `/api/committees/all-parentcommittees`;
        // Use absolute URL on the server and relative URL on the client
        const url = isServer ? `${serverBase}${orgurl}` : orgurl;
      
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                Cookie: cookieHeader,
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            console.error('Failed to fetch parent committees:', response.statusText);
            return [];
        }
        const parentCommitteesRaw = await response.json();
        const parentCommittees = Array.isArray(parentCommitteesRaw)
            ? parentCommitteesRaw.map((committee: ExistingParentCommitteeResponse) => ({
                value: (committee.committeeId ?? '').toString(),
                label: committee.committeeName,
            })) : [];
        // Add "None" option for parent committee dropdown
        parentCommittees.unshift({ value: '', label: 'None' });

        // Transform API data into desired format for dropdowns
        return parentCommittees;
    } catch (error) {
        console.error('Error fetching parent committees:', error);
        return [];
    }
}

export const formatCommitteeSelect = (data : CommitteesResponse[]) : SelectType[] => {
    const committeeSelect = data.map((committee) => ({
        value: committee.committeeId.toString(),
        label: committee.committeeName.toString()
    }))
    return committeeSelect;
}

export async function deleteCommittee(cookieHeader: string, committeeId: number): Promise<void> {
  const isServer = typeof window === 'undefined';
  const orgurl = `/api/committees/${committeeId}`;
  const url = isServer ? `${serverBase}${orgurl}` : orgurl;

  try {
    const response = await fetch(url, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        ...(isServer ? { Cookie: cookieHeader } : {}), // only include cookies manually on server
      },
      credentials: isServer ? 'omit' : 'include', // ✅ ensure browser sends session cookies
    });

    if (!response.ok) {
      throw new Error(`Failed to delete committee. HTTP status: ${response.status}`);
    }

    console.log(`✅ Committee ${committeeId} deleted successfully.`);
  } catch (error) {
    console.error(`❌ Error deleting committee ${committeeId}:`, error);
    throw error;
  }
}

