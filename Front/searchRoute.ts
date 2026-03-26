import { NextRequest, NextResponse } from "next/server";
import { createAxiosInstanceWithToken } from "@/utils/axiosInstance";
import { getValidAccessToken } from "@/utils/getValidAccessToken";

// This file should be placed at: src/app/api/referencedocumentupdates/standardupdates/search/route.ts

export async function GET(request: NextRequest) {
    try {
        const token = await getValidAccessToken(request);
        if (!token) {
            return NextResponse.json(
                { error: "Unauthorized or token expired" },
                { status: 401 }
            );
        }

        const axios = createAxiosInstanceWithToken(token);

        // Get all query parameters from the request URL
        const searchParams = request.nextUrl.searchParams;
        const queryString = searchParams.toString();

        // Forward the query to the backend
        const response = await axios.get(`/referencedocumentupdates/standardupdates/search?${queryString}`);

        return NextResponse.json(response.data);
    } catch (error: any) {
        console.error("Error searching standard updates:", error);
        return NextResponse.json(
            { error: error.message ?? "Internal Server Error" },
            { status: 500 }
        );
    }
}
