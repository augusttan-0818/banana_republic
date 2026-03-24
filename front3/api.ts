// lib/api/ccrApi.ts
import axios from "axios";
import { normalizeSearch } from "@/features/ccrs/search/utils/lib";
import { ccrSearch } from "@/features/ccrs/search/types/schema";

export async function searchCcrs(form: ccrSearch, pageNumber: number, pageSize: number) {
  const normalized = normalizeSearch(form);
  console.log("Normalized search parameters:", normalized);
  const cleanedQuery = Object.fromEntries(
    Object.entries({
      ...normalized,
        pageNumber,
        pageSize,
      }).filter(([_, value]) => value !== null && value !== undefined)
  );

  const response = await axios.get("/api/ccrs/search", {
    params: cleanedQuery,
  });
 console.log("API response data:", response.data);
  return response.data;
}
