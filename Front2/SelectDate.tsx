import * as React from 'react';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { createTheme } from '@mui/material/styles';
import { ThemeProvider } from '@mui/material/styles';
import dayjs, { Dayjs } from 'dayjs';
import { DateTimePicker } from '@mui/x-date-pickers';

const emptyTheme = createTheme();

interface SelectDateProps {
  label: string;
  required?: boolean;
  width?: string;
  value?: Date | string;
  disabled?: boolean;
  time? : boolean;
  name?: string;
  handleChangeExternal?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function SelectDate({label, required = false, width, value, disabled = false, time=false, name, handleChangeExternal} : SelectDateProps) {
  const [dateValue, setValue] = React.useState<Dayjs | null>(value ? dayjs(value): null);

    // Custom handler to wrap the Dayjs value into a synthetic event
    const handleChange = (newValue: Dayjs | null) => {
      // Create a fake event to pass to the external handler
      if (handleChangeExternal) {
        const fakeEvent: React.ChangeEvent<HTMLInputElement> = {
          target: {
            name: name || label,
            value: newValue ? newValue.format('YYYY-MM-DD HH:mm:ss') : '', // Convert Dayjs to string (or null)
          },
        } as React.ChangeEvent<HTMLInputElement>;

        handleChangeExternal(fakeEvent); // Pass the fake event to the external handler
      }
      setValue(newValue); // Update the internal state as well
    };
  return (  
    <ThemeProvider theme={emptyTheme}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        {time ? (<DateTimePicker sx={{width: width || '100%'}} label={label}
          name={name}
          disabled={disabled}
          slotProps={{
            textField: {
              required: required
            }
          }}
          value={dateValue}
          onChange={handleChange}
          />
        ):(
        <DatePicker sx={{width: width || '100%'}} label={label}
        disabled={disabled}
        slotProps={{
          textField: {
            required: required
          }
        }}
        value={dateValue}
        onChange={handleChange}
        />
        )}
      </LocalizationProvider>
    </ThemeProvider>
  );
}