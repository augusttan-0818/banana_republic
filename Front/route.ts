import { NextRequest, NextResponse } from "next/server";
import { createAxiosInstanceWithToken } from "@/utils/axiosInstance";
import { getValidAccessToken } from "@/utils/getValidAccessToken";

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
        
        // Get pagination parameters from query string
        const { searchParams } = new URL(request.url);
        const page = parseInt(searchParams.get("page") || "1");
        const pageSize = parseInt(searchParams.get("pageSize") || "10");

        // Call the new backend endpoint
        const response = await axios.get(`/referencedocumentupdates/standardupdates/list`, {
            params: {
                page,
                pageSize
            }
        });

        return NextResponse.json(response.data);
    } catch (error: any) {
        console.error("Error fetching standard updates list:", error);
        return NextResponse.json(
            { error: error.message ?? "Internal Server Error" },
            { status: 500 }
        );
    }
}

