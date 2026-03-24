import * as React from 'react';
import TextField from '@mui/material/TextField';

interface LargeTextDataFieldProps {
    label: string;
    value: string | number | Date | undefined;
    width?: string;
    required?: boolean;
    read?: boolean;
    name?: string;
    handleChangeExternal?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export const LargeTextDataField: React.FC<LargeTextDataFieldProps> = ({ 
  label, 
  value,
  width = '',
  required = false, 
  read = false,
  name,
  handleChangeExternal,
}) => {
  const [formData, setFormData] = React.useState(value);
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (handleChangeExternal) {
      const fakeEvent: React.ChangeEvent<HTMLInputElement> = {
        target: {
          name: name || label,
          value: e.target.value.toString(),
        },
      } as React.ChangeEvent<HTMLInputElement>;

      handleChangeExternal(fakeEvent); // Pass the fake event to the external handler
    }
    setFormData(e.target.value);
  };
  return (
    <TextField
      label={label}
      name={name || label}
      value={formData}
      multiline
      minRows={5}
      onChange={handleChange}
      sx={{
          width: width || { xs: '100%', sm: '900px' }, 
          // bgcolor: "red",
          '& .MuiOutlinedInput-root': {
              width: '100%', 
              height: '100%',
              padding: '1.5%',     
          },
      }} 
      slotProps = {{
        input: {
          readOnly: read
        }
      }}
      required={required}
    />
  );
}

