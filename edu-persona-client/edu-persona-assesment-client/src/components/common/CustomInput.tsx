import {
  Box,
  InputAdornment,
  InputLabel,
  styled,
  TextField,
  type SxProps,
  type TextFieldVariants,
  type Theme,
} from "@mui/material";
import type { ReactNode } from "react";
import { Controller, useFormContext } from "react-hook-form";

interface InputFieldProps {
  label: string;
  startIcon?: ReactNode;
  endIcon?: ReactNode;
  endIconClick?: () => void;
  placeholder?: string;
  required?: boolean;
  disabled?: boolean;
  fullWidth?: boolean;
  size?: "small" | "medium";
  variant?: TextFieldVariants;
  name: string;
  type?: string;
  style?: SxProps<Theme>;
}

export const InputField = ({
  label,
  startIcon,
  endIcon,
  endIconClick,
  placeholder,
  required = false,
  fullWidth = true,
  size,
  variant = "outlined",
  name,
  type = "text",
  style,
}: InputFieldProps) => {
  const { control } = useFormContext();
  return (
    <>
      <StyledInputLabel>
        {required ? (
          <Box>
            {label} <span style={{ color: "red" }}> * </span>
          </Box>
        ) : (
          label
        )}
      </StyledInputLabel>
      <Controller
        name={name}
        control={control}
        render={({ field, fieldState }) => (
          <StyledTextField
            {...field}
            placeholder={placeholder}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position="start">{startIcon}</InputAdornment>
                ),
                endAdornment: (
                  <InputAdornment position="end" onClick={endIconClick}>
                    {endIcon}
                  </InputAdornment>
                ),
              },
            }}
            error={!!fieldState.error}
            helperText={fieldState.error?.message}
            fullWidth={fullWidth}
            size={size}
            variant={variant}
            type={type}
            sx={style}
          />
        )}
      />
    </>
  );
};

const StyledTextField = styled(TextField)({
  "& .MuiOutlinedInput-root": {
    borderRadius: 12,
    BorderColor: "#6a7181",
    height: "46px",
  },
  "& .MuiInputAdornment-root": {
    color: "#6a7181",
  },
});

const StyledInputLabel = styled(InputLabel)({
  fontSize: 14,
  marginBottom: 8,
});
