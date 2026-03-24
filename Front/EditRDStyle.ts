import { styled } from "@mui/material/styles";
import StepLabel from "@mui/material/StepLabel";

export const stepperContainerStyle = {
    width: "100%"
};

export const CustomStepLabel = styled(StepLabel)(({ theme }) => ({
    "& .MuiStepLabel-label, & .MuiStepLabel-label.Mui-active, & .MuiStepLabel-label.Mui-completed, & .MuiStepLabel-label.Mui-disabled, & .MuiStepLabel-alternativeLabel": {
        color: "#fff !important",
    },
    cursor: "pointer",
}));