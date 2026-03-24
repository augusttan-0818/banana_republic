"use client";

import React from "react";
import Tabs from "../../../../components/Tabs";
import { usePathname } from "next/navigation";

const GeneralNavBar: React.FC = () => {
    const pathname = usePathname();
    const locale = pathname.split("/")[1] || "en";

    const tabs = [
        { label: "RD Record", path: `/${locale}/reference-documents/rd-record` },
        { label: "RD Updates", path: `/${locale}/reference-documents/rd-updates` },
        { label: "Committee Responsibilities", path: `/${locale}/reference-documents/committee-responsibility` },
    ];

    return (
        <Tabs tabs={tabs} ariaLabel={"Tabbed Navigation for Reference Documents"} />
    );
};

export default GeneralNavBar;
