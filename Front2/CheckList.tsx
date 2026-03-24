import { Checkbox, FormControlLabel, Box } from '@mui/material';
import * as React from 'react';

interface CheckListProps {
    itemList: string[];
    direction?: string;
    className?: string;
    id?: string;
    name?: string;
    styling?: React.CSSProperties;
}

export default function CheckList({itemList, direction = "column", className = '', id = '', name, styling={}} : CheckListProps) {

    return (
        <Box className={className} id={id} sx={{display: 'flex', flexDirection: {direction}, justifyContent: 'space-between', alignItems: 'left', ...styling}}>
            {itemList.map((index, item) => (
                <FormControlLabel name={name} key={index} control={<Checkbox />} label={item}/>
            ))}
        </Box>
    )
}