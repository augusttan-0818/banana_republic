import { Select, MenuItem, FormControl, InputLabel, SelectChangeEvent } from '@mui/material';
import * as React from 'react';

export type SelectType = {
  value: string;
  label: string;
}

export interface SelectMenuProps {
    itemList: (string | SelectType)[];
    label?: string;
    className?: string;
    id?: string;
    required?: boolean;
    width?: string;
    name?: string;
    disabled?: boolean;
    data?: string;
    handleChangeExternal?: (
        e: React.ChangeEvent<HTMLInputElement> | {
            target: {
                name: string;
                value: any;
                startDate?: string | Date | null;
                endDate?: string | Date | null;
            };
        }
    ) => void;
}

export default function SelectMenu({itemList, label = '', className = '', id = '', required=false, width, name, disabled = false, data, handleChangeExternal}: SelectMenuProps) {

    const [selectedValue, setSelectedValue] = React.useState<string>(data || '');

    // Handle the change event
    const handleChange = (event: SelectChangeEvent<unknown>,) => {
        const newValue = event.target.value as string

        let selectedItem: any = null;
        if (typeof itemList[0] !== "string") {
            selectedItem = (itemList as SelectType[]).find(i => i.value === newValue);
        }

        // Create a fake event to pass to the external handler
        if (handleChangeExternal) {
            const fakeEvent = {
                target: {
                    name: name || label,
                    value: newValue,
                    ...(selectedItem ? { startDate: selectedItem.startDate, endDate: selectedItem.endDate } : {})
                }
            };
            handleChangeExternal(fakeEvent); // Works for both standard and custom events
        }

        setSelectedValue(newValue);
    };

    return (
        <FormControl sx={{width: width || '100%', minWidth: '250px'}} required={required}>
            <InputLabel>{label} </InputLabel>
            <Select label={label} name={name || label} className={className} id={id} onChange={handleChange} value={selectedValue} disabled={disabled}>
                {itemList.map((item, index) =>
                    typeof item === 'string' ? (
                        <MenuItem key={index} value={item}>
                        {item}
                        </MenuItem>
                    ) : (
                        <MenuItem key={index} value={item.value}>
                        {item.label}
                        </MenuItem>
                    )
                )}
            </Select>
        </FormControl>
    )
}