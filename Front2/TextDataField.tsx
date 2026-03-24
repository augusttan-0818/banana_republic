import * as React from 'react';
import TextField from '@mui/material/TextField';
 
interface TextDataFieldProps {
    label: string;
    value: string | number | Date | null | undefined;
    name?: string;
    required?: boolean;
    read?: boolean;
    width?: string;
    type?: string;
    handleChangeExternal?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export const TextDataField: React.FC<TextDataFieldProps> = ({
  label,
  value,
  name,
  required = false,
  read = true,
  width,
  type,
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
      type={type}
      onChange={handleChange}
      sx={{
        width: width || { xs: '100%', sm: '300px' }, // 100% on small screens, 300px on larger screens
        '& .MuiInputBase-root': {
            '& > fieldset': {
            },
            width: '100%'
        },
      }}
      slotProps={{
        input: {
          readOnly: read
        }
      }}
      
      required = {required}
      // contentEditable={!read}
      />
  );
}
// import * as React from 'react';
// import Box from '@mui/material/Box';
// import TextField from '@mui/material/TextField';

// interface TextDataFieldProps {
//     label: string;
//     value: string | number | Date | undefined;
//     read?: boolean;
// }

// export const TextDataField: React.FC<TextDataFieldProps> = ({ 
//   label, 
//   value, 
//   read = true,
// }) => {
//   const [formData, setFormData] = React.useState(value);
//   const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
//     const { name, value } = e.target;
//     setFormData(value);
//   };
//   return (
//         <Box>
//             <TextField
//               label={label}
//               name={label}
//               value={value}
//               onChange={handleChange}
//               sx={{
//                 width: { xs: '100%', sm: '300px' }, // 100% on small screens, 300px on larger screens
      
//               }} 
//               InputProps={{readOnly: read}}
//               required
//               />
//         </Box>
//   );
// }
