import { NextRequest, NextResponse } from "next/server";
import { createAxiosInstanceWithToken } from "@/utils/axiosInstance";
import { getValidAccessToken } from "@/utils/getValidAccessToken";

// This file should be placed at: src/app/api/committees/type/[committeeType]/route.ts

export async function GET(
    request: NextRequest,
    { params }: { params: Promise<{ committeeType: string }> }
) {
    try {
        const token = await getValidAccessToken(request);
        if (!token) {
            return NextResponse.json(
                { error: "Unauthorized or token expired" },
                { status: 401 }
            );
        }

        const { committeeType } = await params;
        const axios = createAxiosInstanceWithToken(token);

        const response = await axios.get(`/committees/type/${committeeType}`);

        return NextResponse.json(response.data);
    } catch (error: any) {
        console.error("Error fetching committees by type:", error);
        return NextResponse.json(
            { error: error.message ?? "Internal Server Error" },
            { status: 500 }
        );
    }
}
