import { GridToolbarContainer, GridToolbarExport, GridToolbarColumnsButton, GridToolbarFilterButton } from '@mui/x-data-grid';

export default function CustomToolbar() {
    return (
        <>
            <GridToolbarContainer>
                <GridToolbarColumnsButton />
                <GridToolbarExport />
                <GridToolbarFilterButton />
            </GridToolbarContainer>
        </>
    )
}