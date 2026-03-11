import {
  Autocomplete,
  Box,
  Chip,
  FormControl,
  FormHelperText,
  TextField,
} from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

interface OptionType {
  label: string;
  value: number;
}

interface MultiSelectChipFieldProps {
  name: string;
  label: string;
  options: OptionType[];
  required?: boolean;
  disabled?: boolean;
  maxVisibleChips?: number;
}

export const MultiSelectChipField = ({
  name,
  label,
  options,
  required = false,
  disabled = false,
  maxVisibleChips = 2,
}: MultiSelectChipFieldProps) => {
  const { control } = useFormContext();

  return (
    <Controller
      name={name}
      control={control}
      defaultValue={[]}
      render={({ field, fieldState }) => {
        const selectedOptions = options.filter((o) =>
          (field.value || []).includes(o.value)
        );

        return (
          <FormControl fullWidth error={!!fieldState.error}>
            {/* Label */}
            <Box sx={{ mb: 0.5 }}>
              <label style={{ fontSize: 14, fontWeight: 500 }}>
                {label}
                {required && (
                  <span style={{ color: "#d32f2f", marginLeft: 4 }}>*</span>
                )}
              </label>
            </Box>

            <Autocomplete<OptionType, true, false, false>
              multiple
              options={options}
              disableCloseOnSelect
              getOptionLabel={(option) => option.label}
              value={selectedOptions}
              disabled={disabled}
              onChange={(_, newValue) => {
                field.onChange(newValue.map((v) => v.value));
              }}
              renderTags={(value, getTagProps) => {
                const visible = value.slice(0, maxVisibleChips);
                const remaining = value.length - maxVisibleChips;

                return (
                  <>
                    {visible.map((option, index) => (
                      <Chip
                        {...getTagProps({ index })}
                        key={option.value}
                        label={option.label}
                        sx={{
                          borderRadius: 2,
                          height: 28,
                          fontWeight: 500,
                        }}
                      />
                    ))}

                    {remaining > 0 && (
                      <Chip
                        label={`+${remaining}`}
                        sx={{
                          borderRadius: 2,
                          height: 28,
                          backgroundColor: "#e0e0e0",
                        }}
                      />
                    )}
                  </>
                );
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="Select option"
                  error={!!fieldState.error}
                  sx={{
                    "& .MuiOutlinedInput-root": {
                      borderRadius: 3,
                      minHeight: 46,
                    },
                  }}
                />
              )}
            />

            <FormHelperText>{fieldState.error?.message || " "}</FormHelperText>
          </FormControl>
        );
      }}
    />
  );
};
