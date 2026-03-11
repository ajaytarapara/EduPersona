import {
  Button,
  CircularProgress,
  styled,
  type ButtonProps,
} from "@mui/material";
import type { ReactNode } from "react";

interface CustomButtonProps extends ButtonProps {
  loading?: boolean;
  startIcon?: ReactNode;
  endIcon?: ReactNode;
}

export const CustomButton = ({
  children,
  loading = false,
  startIcon,
  endIcon,
  disabled,
  ...props
}: CustomButtonProps) => {
  return (
    <StyledButton
      {...props}
      disabled={disabled || loading}
      startIcon={!loading ? startIcon : undefined}
      endIcon={!loading ? endIcon : undefined}
    >
      {loading ? <CircularProgress size={20} /> : children}
    </StyledButton>
  );
};

const StyledButton = styled(Button)(({ theme }) => ({
  textTransform: "none",
  height: "46px",
  fontWeight: 500,
  fontSize: 16,
  borderRadius: 12,
  backgroundImage: `linear-gradient(
    90deg,
    ${theme.palette.primary.main},
    ${theme.palette.secondary.main}
  )`,
  color: theme.palette.primary.contrastText,

  "&:hover": {
    backgroundImage: `linear-gradient(
      135deg,
      ${theme.palette.primary.dark},
      ${theme.palette.secondary.dark}
    )`,
  },

  "&.Mui-disabled": {
    backgroundImage: "none",
    backgroundColor: theme.palette.action.disabledBackground,
  },
}));
