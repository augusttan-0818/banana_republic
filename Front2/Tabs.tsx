"use client"

import React from "react";
import { Tabs, Tab, Box } from "@mui/material";
import { usePathname, useRouter } from "next/navigation";

const CustomTabs: React.FC<{ tabs: {label: string, path: string}[], value?: (targetTab: number) => number, ariaLabel?: string }> = ({ tabs, value, ariaLabel }) => {
  const pathname = usePathname();
  const router = useRouter();

  const targetTab = tabs.findIndex((tab) => pathname === tab.path);
  const activeTab = value ? value(targetTab) : ( (targetTab > -1) ? targetTab : 0 ); // Pass the current tab to MUI using a custom function if available. 
  // const activeTab= (targetTab>-1)?targetTab: 0;
 
  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    router.push(tabs[newValue].path); // Navigate to the selected tab's path
  };

  return (
    <Box
      sx={{
        position: "sticky", // Tabs remain at the top
        top: "64px",
        left: 0,
        width: "100%", // Full-width tabs
        zIndex: 1000,
        backgroundColor: "#ffffff", // Prevent overlap with transparent backgrounds
        borderBottom: 1,
        borderColor: "divider",
      }}
    >
      <Tabs
        value={activeTab}
        onChange={handleTabChange}
        aria-label={ariaLabel ?? "Tabbed Navigation"}
        textColor="primary"
        indicatorColor="primary"
      >
        {tabs.map((tab, index) => (
          <Tab
            key={index}
            label={tab.label}
            sx={{
              minWidth: 120,
              textTransform: "none",
            }}
          />
        ))}
      </Tabs>
    </Box>
  );
};

export default CustomTabs;
