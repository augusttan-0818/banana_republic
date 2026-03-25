import { NextRequest, NextResponse } from "next/server";
import { createAxiosInstanceWithToken } from "@/utils/axiosInstance";
import { getValidAccessToken } from "@/utils/getValidAccessToken";

// This file should be placed at: src/app/api/referencedocumentupdates/statuses/[statusId]/substatuses/route.ts

export async function GET(
    request: NextRequest,
    { params }: { params: { statusId: string } }
) {
    try {
        const token = await getValidAccessToken(request);
        if (!token) {
            return NextResponse.json(
                { error: "Unauthorized or token expired" },
                { status: 401 }
            );
        }

        const axios = createAxiosInstanceWithToken(token);
        const statusId = params.statusId;

        const response = await axios.get(`/referencedocumentupdates/statuses/${statusId}/substatuses`);

        return NextResponse.json(response.data);
    } catch (error: any) {
        console.error("Error fetching sub-statuses:", error);
        return NextResponse.json(
            { error: error.message ?? "Internal Server Error" },
            { status: 500 }
        );
    }
}
