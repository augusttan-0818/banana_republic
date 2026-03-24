import React from "react";
import { IconButton } from "@mui/material";
import CloseRoundedIcon from '@mui/icons-material/CloseRounded';
    
export const CloseButton = ({size, color, onClick }: {
    size: "small" | "medium" | "large", 
    color?: string, 
    onClick?: () => void}) => {
    return (
        <IconButton  
            size={size}
            onClick={onClick}
        > 
            <CloseRoundedIcon sx={{color: {color}}} /> 
        </IconButton>
    );
};

export default CloseButton