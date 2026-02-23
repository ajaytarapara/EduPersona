import { Box, Grid, styled, Typography } from "@mui/material";
import { EduPersonaLogo } from "./Logo";
import authHero from "../../../src/assets/auth-hero.jpg";
import type { ReactNode } from "react";

interface IAuthLayoutProps {
  leftTitle?: string;
  leftSubtitle?: string;
  authForm: ReactNode;
}

export const AuthLayout = ({
  leftSubtitle,
  leftTitle,
  authForm,
}: IAuthLayoutProps) => {
  return (
    <Box minHeight="100vh">
      <Grid container minHeight="100vh">
        <StyledLeftGird size={{ xs: 12, md: 6 }}>
          <StyledImg src={authHero} alt="auth-image" />
          <StyledImgGradient></StyledImgGradient>
          <StyledTitleBox
            position="absolute"
            padding={6}
            height="100%"
            width="100%"
          >
            <Box display="flex" alignItems="center">
              <EduPersonaLogo />
            </Box>
            <Box
              display="flex"
              flexDirection="column"
              marginTop="40%"
              gap="10px"
            >
              <Typography variant="h3" fontWeight={600} maxWidth="500px">
                {leftTitle}
              </Typography>
              <Typography variant="body1" maxWidth="600px">
                {leftSubtitle}
              </Typography>
            </Box>
          </StyledTitleBox>
        </StyledLeftGird>
        <StyledRightGrid size={{ xs: 12, md: 6 }}>{authForm}</StyledRightGrid>
      </Grid>
    </Box>
  );
};

const StyledLeftGird = styled(Grid)(({ theme }) => ({
  height: "100vh",
  position: "relative",
  [theme.breakpoints.down("md")]: {
    display: "none",
  },
}));

const StyledImg = styled(`img`)({
  position: "absolute",
  inset: 0,
  width: "100%",
  height: "100%",
  objectFit: "cover",
});

const StyledTitleBox = styled(Box)({
  position: "absolute",
  top: 0,
  color: "#fff",
});

const StyledImgGradient = styled(Box)(({ theme }) => ({
  position: "absolute",
  inset: 0,
  background: `linear-gradient(
    135deg,
    ${theme.palette.primary.main},
    ${theme.palette.secondary.main}
  )`,
  opacity: 0.4,
}));

const StyledRightGrid = styled(Grid)(({ theme }) => ({
  backgroundColor: "#f6f7f9",
  padding: theme.spacing(4),
  [theme.breakpoints.down("sm")]: {
    padding: theme.spacing(3),
  },
}));
