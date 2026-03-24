import { Modal, Box } from '@mui/material';
import * as React from 'react';
import CloseButton from './CloseButton';

interface ModalPopupProps {
  handleClick?: () => void;
  children: React.ReactNode;
  open: boolean;
  height?: string;
}

export default function ModalPopup({ handleClick, children, open, height }: ModalPopupProps) {
  // The Modal is controlled by the parent via the "open" prop
  return (
    <Modal open={open} onClose={handleClick}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 400,
          bgcolor: 'background.paper',
          boxShadow: 24,
          p: 4,
          height: height,
          borderRadius: 4, // Rounded edges
        }}
      >
        {/* CloseButton triggers the parent handler */}
        <Box sx={{position: "absolute", top: "10px", right: "25px"}}>
            <CloseButton size="small" color="black" onClick={handleClick} />
        </Box>
        {children}
      </Box>
    </Modal>
  );
}
