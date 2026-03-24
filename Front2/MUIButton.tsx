import React from "react";
import Button from "@mui/material/Button";
import { CircularProgress } from "@mui/material";

type MUIButtonProps = {
  text: string;
  onClick: () => void;
  variant?: "text" | "outlined" | "contained"; // Default is "contained"
  color?: "primary" | "secondary" | "success" | "error" | "info" | "warning"; // Default is "primary"
  disabled?: boolean;
  loading?: boolean; // Optional loading state
  startIcon?: React.ReactNode; // Icon to display at the start of the button
  endIcon?: React.ReactNode; // Icon to display at the end of the button
};

const MUIButton: React.FC<MUIButtonProps> = ({
  text,
  onClick,
  variant = "contained",
  color = "primary",
  disabled = false,
  loading = false,
  startIcon,
  endIcon,
}) => {
  return (
    <Button
      variant={variant}
      color={color}
      onClick={onClick}
      disabled={disabled || loading}
      startIcon={loading ? <CircularProgress size={20} color="inherit" /> : startIcon}
      endIcon={endIcon}
    >
      {loading ? "Loading..." : text}
    </Button>
  );
};

export default MUIButton;
