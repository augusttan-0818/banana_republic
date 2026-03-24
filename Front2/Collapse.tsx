import * as React from 'react';
import Box from '@mui/material/Box';
import { Collapse, MenuItem } from '@mui/material';

interface TextDataFieldProps {
    // key: number,
    children: React.ReactNode,
    title: string,
    titleStyle?: React.CSSProperties,
    isOpen?: boolean,
}

const CustomCollapse: React.FC<TextDataFieldProps> = ({ 
  children,
  title,
  titleStyle,
  isOpen = true,
}) => {
  const [open, setOpen] = React.useState(isOpen)

  return (
    <Box sx={{ gap: "5px", flexDirection: "column"}}>
      <MenuItem onClick={() => setOpen(!open)} sx={{...titleStyle}}>
          {title}
      </MenuItem>
      <Collapse in={open} sx={{padding: "10px"}}>
          {children}
      </Collapse>
    </Box>
  );
}

export default CustomCollapse;

