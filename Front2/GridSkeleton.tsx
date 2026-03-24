import { Box, Skeleton } from "@mui/material";
import { GridColDef } from "@mui/x-data-grid";

const GridSkeleton = ({columns}: {columns:GridColDef[]}) => (
    // Skeleton Placeholder
    <Box
      sx={{
        flexGrow: 1,
        overflow: 'hidden',
        marginLeft:1,
        marginRight:1
      }}
    >
      <Box sx={{
                flexGrow: 1, // Ensures the DataGrid takes up available space
                overflow: 'hidden', // Prevents scrollbars if needed
                marginLeft:1,
                marginRight:1
              }}>
        {/* <Typography variant="body1">Search Label: {topic}</Typography>
        <Typography variant="body1">Code Year:{topicYear}</Typography> */}
      </Box>
      {/* Skeleton Header */}
      <Box display="flex" sx={{ marginBottom: 1 , marginLeft:1}}>
        {columns.map((col, index) => (
          <Skeleton
            key={index}
            variant="rectangular"
            height={40} // Matches DataGrid header height
            width={col.width || 100} // Use column width or a fallback
            sx={{ marginRight: 1 }}
            animation="wave"
          />
        ))}
      </Box>
      {/* Skeleton Rows */}
      {[...Array(5)].map((_, rowIndex) => (
        <Box display="flex" key={rowIndex} sx={{ marginBottom: 1, marginLeft:1 }}>
          {columns.map((col, colIndex) => (
            <Skeleton
              key={`${rowIndex}-${colIndex}`}
              variant="rectangular"
              height={40} // Matches DataGrid row height
              width={col.width || 100} // Use column width or a fallback
              sx={{ marginRight: 1 }}
              animation="wave"
            />
          ))}
        </Box>
      ))}
    </Box>
  );
  
export  default GridSkeleton;