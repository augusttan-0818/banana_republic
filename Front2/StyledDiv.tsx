// components/StyledDiv.tsx
import { styled } from '@mui/material/styles';

const StyledDiv = styled('div')(({ theme }) => ({
  padding: theme.spacing(2), // 16px
  backgroundColor: theme.palette.background.paper, // Optional styling
  borderRadius: theme.shape.borderRadius, // Optional styling
}));

export default StyledDiv;
