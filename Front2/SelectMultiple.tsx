import { Box, Chip, FormControl, InputLabel, MenuItem, OutlinedInput, Select, SelectChangeEvent } from "@mui/material";
import React from "react";

interface MultipleSelectProps {
    dropdownOptions: string[];
    data: string;
    label: string;
    read?: boolean;
  }


const MultipleSelect: React.FC<MultipleSelectProps> = ({
        dropdownOptions,
        data,
        label,
        read = true, // Default to true if not provided
    }) => {
        const [formData, setFormData] = React.useState<string[]>(data != "" ? [data] : []);

        const handleChange = (event: SelectChangeEvent<typeof formData>) => {
            const {
            target: { value },
            } = event;
            setFormData(
            // On autofill we get a stringified value.
            typeof value === 'string' ? value.split(',') : value,
            );
        };

    return (
        <FormControl sx={{ m: 1, width: "100%" }}>
            <InputLabel id="SelectMultiple">{label}</InputLabel>
            <Select
                labelId="SelectMultiple"
                multiple
                value={formData}
                onChange={handleChange}
                disabled={read}
                input={<OutlinedInput id="select-multiple-chip" label={label} />}
                renderValue={(selected) => {
                    return(
                        <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                            {selected.map((value) => (
                            <Chip key={value} label={value} />
                            ))}
                        </Box>
                    )
                }}
            >
                {dropdownOptions.map((option) => (
                <MenuItem
                    key={option}
                    value={option}
                >
                    {option}
                </MenuItem>
                ))}
            </Select>
        </FormControl>
    )
}

export default MultipleSelect;