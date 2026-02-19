import { Box, Divider, styled, Typography } from "@mui/material";

interface DividerWithLabelProps {
  label: string;
}

export const DividerWithLabel = ({ label }: DividerWithLabelProps) => {
  return (
    <StyledLabelBox>
      <StyledDivider />
      <Label variant="body2">{label}</Label>
      <StyledDivider />
    </StyledLabelBox>
  );
};

const StyledLabelBox = styled(Box)(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  width: "100%",
  margin: `${theme.spacing(3)} 0`,
}));

const StyledDivider = styled(Divider)(({ theme }) => ({
  flexGrow: 1,
  borderColor: theme.palette.divider,
}));

const Label = styled(Typography)(({ theme }) => ({
  margin: `0 ${theme.spacing(2)}`,
  color: theme.palette.text.secondary,
  fontWeight: 500,
  whiteSpace: "nowrap",
}));
