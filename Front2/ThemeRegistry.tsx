// components/ThemeRegistry.tsx
"use client";

import { ThemeProvider, CssBaseline } from "@mui/material";
import theme from "../styles/theme";

const ThemeRegistry = ({
  children,
}: {
  children: React.ReactNode;
}) => 
 {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {children}
    </ThemeProvider>
  );
}
export default ThemeRegistry;