import { getReferenceDocumentRecords } from "./dataUtils";
import { ReferenceDocumentRecord } from "./dataTypes";

/**
 * Converts an array of reference document reports to CSV format and triggers a download.
 *
 * @param data - Array of reference document reports to export.
 * @param filename - The download filename.
 */
export function exportDataToExcel(
    data: ReferenceDocumentRecord[],
    filename: string = 'ReferenceDocuments.csv'
): void {
    // Update CSV header with properties that exist on ReferenceDocumentReport
    let csv = `Issuing Agency,Document Number,Title,Referenced In,Responsible Committees,Status\n`;
    data.forEach(row => {
        const referencedInStr = Array.isArray(row.referencedIn)
            ? row.referencedIn.join("; ")
            : row.referencedIn;
        csv += `"${row.issuingAgency}","${row.documentNumber}","${row.titleOfDocument}","${referencedInStr}","${row.responsibleCommittees}","${row.status}"\n`;
    });
    const blob = new Blob([csv], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.setAttribute("href", url);
    link.setAttribute("download", filename);
    link.style.visibility = "hidden";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

/**
 * Fetches *all* reference document reports across all pages and then triggers an export of the complete data to CSV.
 *
 * @param cookie - The auth cookie.
 * @param appliedFilters - Search/filter parameters as a JSON string.
 * @param pageSize - Number of rows to fetch per page (defaults to 1000).
 * @param filename - The filename for the exported CSV.
 */
export async function exportAllDataToExcel(
    cookie: string,
    appliedFilters: string,
    pageSize: number = 1000,
    filename: string = 'ReferenceDocuments.csv'
): Promise<void> {
    let allData: ReferenceDocumentRecord[] = [];
    let page = 0;
    let hasMore = true;
    while (hasMore) {
        const [rows, count] = await getReferenceDocumentRecords(cookie, page + 1, pageSize);
        allData = allData.concat(rows);
        page++;
        if (rows.length < pageSize) {
            hasMore = false;
        }
    }
    exportDataToExcel(allData, filename);
}
