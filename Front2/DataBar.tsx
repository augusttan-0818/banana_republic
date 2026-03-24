import { Box, Typography } from "@mui/material";
import CloseButton from '@/components/CloseButton';

export default function DataBar ({ /*rowData,*/ handleClick, children, label }: { /*rowData: any,*/ handleClick: () => void, children: React.ReactNode, label?: string }) {
  /*if (!rowData) return null;
  console.log(rowData)*/
  return(
    <>
      <Box className="form-overlay">
          <Box className="form-container" sx={{ width: '40.5%', pl: 'auto', pr: 'auto' }}>
            <Typography variant="h6" sx={{padding: '10px 0'}}> {label || "Details"} </Typography>
            {/* <Box style={{display: 'flex', flexDirection: 'row', height: '90%', marginTop: '5%'}}> */}
              {children}
            {/* </Box> */}
            {/* Need to put style in child container: see example in public-reviewGrid */}
            <Box sx={{position: "absolute", top: "10px", right: "25px"}}>
              <CloseButton size="small" color="black" onClick={handleClick} />
            </Box>
          </Box>
      </Box>
      
    </>
  )
}