import {
  Box,
  FormControl,
  FormHelperText,
  MenuItem,
  Select,
  Typography,
  styled,
} from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";

interface SelectFieldProps {
  name: string;
  label: string;
  options: { label: string; value: number }[];
  placeholder?: string;
  required?: boolean;
  disabled?: boolean;
}

export const SelectField = ({
  name,
  label,
  options,
  placeholder = "Select option",
  required = false,
  disabled = false,
}: SelectFieldProps) => {
  const { control } = useFormContext();

  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => (
        <FormControl fullWidth error={!!fieldState.error}>
          {/* Label */}
          <LabelWrapper>
            <StyledLabel>
              {label}
              {required && <RequiredStar>*</RequiredStar>}
            </StyledLabel>
          </LabelWrapper>

          {/* Select */}
          <StyledSelect
            {...field}
            displayEmpty
            disabled={disabled}
            IconComponent={KeyboardArrowDownIcon}
            renderValue={(selected: any) => {
              if (!selected || selected === 0) {
                return (
                  <Typography color="text.secondary">{placeholder}</Typography>
                );
              }

              const selectedOption = options.find((o) => o.value === selected);

              return selectedOption?.label;
            }}
          >
            <MenuItem value={0} disabled>
              {placeholder}
            </MenuItem>

            {options.map((option) => (
              <MenuItem key={option.value} value={option.value}>
                {option.label}
              </MenuItem>
            ))}
          </StyledSelect>

          {/* Error (always reserve space) */}
          {fieldState.error?.message && (
            <FormHelperText>{fieldState.error?.message || " "}</FormHelperText>
          )}
        </FormControl>
      )}
    />
  );
};

const StyledSelect = styled(Select)(({ theme }) => ({
  borderRadius: 12,
  height: 46,

  "& .MuiOutlinedInput-notchedOutline": {
    borderRadius: 12,
  },

  "& .MuiSelect-select": {
    display: "flex",
    alignItems: "center",
  },

  "& .MuiSvgIcon-root": {
    color: "#6a7181",
    transition: "0.2s ease",
  },

  "&.Mui-focused .MuiSvgIcon-root": {
    color: theme.palette.primary.main,
    transform: "rotate(180deg)",
  },
}));

const LabelWrapper = styled(Box)({
  marginBottom: 4,
});

const StyledLabel = styled("label")({
  fontSize: 14,
  fontWeight: 500,
  display: "block",
});

const RequiredStar = styled("span")({
  color: "#d32f2f",
  marginLeft: 4,
});
