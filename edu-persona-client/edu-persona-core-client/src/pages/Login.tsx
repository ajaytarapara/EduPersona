import { Box, styled, Typography } from "@mui/material";
import {
  CustomButton,
  DividerWithLabel,
  InputField,
} from "../components/index";
import {
  EmailOutlined,
  LockOutline,
  LoginOutlined,
  VisibilityOffOutlined,
  VisibilityOutlined,
} from "@mui/icons-material";
import { FormProvider, useForm } from "react-hook-form";
import { AuthLayout } from "../components/ui";
import { useState } from "react";
import { GoogleIcon } from "../assets";

const LoginPage = () => {
  const [isPwdVisible, setIsPwdVisible] = useState<boolean>(false);
  const methods = useForm({
    defaultValues: {
      email: "",
      password: "",
    },
  });
  const handlePwdVisibility = () => {
    setIsPwdVisible(!isPwdVisible);
  };
  return (
    <>
      <AuthLayout
        leftSubtitle="Track your progress, ace your exams, and build the confidence to reach your dream career."
        leftTitle="Unlock Your Academic Potential"
        authForm={
          <StyledFormContainer>
            <Typography variant="h4" fontSize="2rem" fontWeight={700}>
              Welcome back
            </Typography>
            <Typography variant="h6" fontSize="1rem" color="text.secondary">
              Sign in to continue your learning journey
            </Typography>
            <FormProvider {...methods}>
              <StyledForm onSubmit={methods.handleSubmit(console.log)}>
                <InputField
                  name="email"
                  label="Email Address"
                  startIcon={<EmailOutlined />}
                  placeholder="your@example.com"
                  required
                  fullWidth
                  style={{ marginBottom: "24px" }}
                />
                <InputField
                  name="password"
                  label="Password"
                  startIcon={<LockOutline />}
                  required
                  type={isPwdVisible ? "text" : "password"}
                  fullWidth
                  placeholder="Enter your password"
                  endIcon={
                    isPwdVisible ? (
                      <VisibilityOffOutlined />
                    ) : (
                      <VisibilityOutlined />
                    )
                  }
                  endIconClick={handlePwdVisibility}
                  style={{ marginBottom: "24px" }}
                />
                <CustomButton
                  variant="contained"
                  endIcon={<LoginOutlined />}
                  loading={false}
                  fullWidth
                >
                  Sign In
                </CustomButton>
              </StyledForm>
            </FormProvider>
            <DividerWithLabel label="OR" />
            <StyledGoogleButton
              variant="contained"
              startIcon={<GoogleIcon height="20px" width="20px" />}
              loading={false}
              fullWidth
            >
              Sign In With Google
            </StyledGoogleButton>
            <Typography
              variant="body1"
              color="text.secondary"
              textAlign="center"
              marginTop="16px"
            >
              Don't have an account?{" "}
              <StyledSpan color="text.primary">Create one</StyledSpan>
            </Typography>
          </StyledFormContainer>
        }
      />
    </>
  );
};

export default LoginPage;

const StyledFormContainer = styled(Box)(({ theme }) => ({
  height: "100%",
  display: "flex",
  justifyContent: "center",
  gap: theme.spacing(0.5),
  flexDirection: "column",
  maxWidth: theme.breakpoints.values.sm,
  margin: "0 auto",
}));

const StyledForm = styled(`form`)({
  marginTop: "24px",
});

const StyledSpan = styled(`span`)(({ theme }) => ({
  color: theme.palette.primary.main,
  fontWeight: 600,
}));

const StyledGoogleButton = styled(CustomButton)({
  backgroundImage: "none",
  backgroundColor: "#eaecf0",
  boxShadow: "none",
  color: "black",
  border: "1px solid #00000024",
  "&:hover": {
    backgroundColor: "#d9d9da",
    backgroundImage: "none",
    boxShadow: "none",
  },
});
