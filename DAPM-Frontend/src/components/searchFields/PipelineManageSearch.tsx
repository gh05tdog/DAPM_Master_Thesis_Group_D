import * as React from 'react';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

export default function BasicSelect() {
    const [age, setAge] = React.useState('');

    const handleChange = (event: SelectChangeEvent) => {
        setAge(event.target.value as string);
    };

    return (
        <Box sx={{bgcolor: 'white', color: 'primary.main', minWidth: 120, marginLeft: '10px', marginRight: '10px' }}>
            <FormControl fullWidth>
                <InputLabel>Select pipeline</InputLabel>
                <Select
                    value={age}
                    onChange={handleChange}
                >
                    <MenuItem value=""> <em>None</em> </MenuItem>
                    <MenuItem value={10}>Ten</MenuItem>
                    <MenuItem value={20}>Twenty</MenuItem>
                    <MenuItem value={30}>Thirty</MenuItem>
                </Select>
            </FormControl>
        </Box>
    );
}

