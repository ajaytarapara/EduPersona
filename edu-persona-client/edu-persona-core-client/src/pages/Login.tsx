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
import { AuthLayout, EduPersonaLogo } from "../components/ui";
import { useState } from "react";
import { GoogleIcon } from "../assets";
import { useNavigate } from "react-router-dom";
import { loginSchema, Roles, AppRoutes, type ILoginPayload } from "../utils";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginUser, validateSession } from "../api";

const LoginPage = () => {
  const navigate = useNavigate();
  const [isPwdVisible, setIsPwdVisible] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const methods = useForm<ILoginPayload>({
    defaultValues: {
      email: "",
      password: "",
    },
    resolver: yupResolver(loginSchema),
    mode: "onTouched",
  });
  const handlePwdVisibility = () => {
    setIsPwdVisible(!isPwdVisible);
  };

  const onSubmit = async (data: ILoginPayload) => {
    setIsLoading(true);
    const response = await loginUser(data);
    if (response.success) {
      const sessionId = response.data?.sessionId;
      const validateSessionResponse = await validateSession(sessionId || 0);
      setIsLoading(false);
      if (validateSessionResponse.success) {
        localStorage.setItem("sessionId", sessionId?.toString() || "");
        localStorage.setItem("role", validateSessionResponse.data?.role || "");
        if (validateSessionResponse.data?.role === Roles.USER) {
          navigate(AppRoutes.Profile);
        } else {
          navigate(AppRoutes.Admin);
        }
      }
    }
  };


  const handleGoogleLogin = () => {
    const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;
    const redirectUri = import.meta.env.VITE_GOOGLE_REDIRECT_URI;

    const scope = "openid email profile";

    const authUrl =
      `https://accounts.google.com/o/oauth2/v2/auth?` +
      `client_id=${clientId}` +
      `&redirect_uri=${redirectUri}` +
      `&response_type=code` +
      `&scope=${encodeURIComponent(scope)}` +
      `&access_type=offline` +
      `&prompt=consent`;

    window.location.href = authUrl;
  };

  return (
    <>
      <AuthLayout
        leftSubtitle="Track your progress, ace your exams, and build the confidence to reach your dream career."
        leftTitle="Unlock Your Academic Potential"
        authForm={
          <StyledFormContainer>
            <StyledLogoBox>
              <EduPersonaLogo
                color="text.primary"
                bgColor="rgba(15,23,42,0.15)"
              />
            </StyledLogoBox>
            <Typography variant="h4" fontSize="2rem" fontWeight={700}>
              Welcome back
            </Typography>
            <Typography variant="h6" fontSize="1rem" color="text.secondary">
              Sign in to continue your learning journey
            </Typography>
            <FormProvider {...methods}>
              <StyledForm onSubmit={methods.handleSubmit(onSubmit)}>
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
                  loading={isLoading}
                  fullWidth
                  type="submit"
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
              onClick={handleGoogleLogin}
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
              <StyledSpan
                color="text.primary"
                onClick={() => navigate(AppRoutes.Register)}
              >
                Create one
              </StyledSpan>
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
  cursor: "pointer",
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

const StyledLogoBox = styled(Box)(({ theme }) => ({
  display: "none",
  padding: theme.spacing(4),
  [theme.breakpoints.down("md")]: {
    display: "flex",
    marginBottom: theme.spacing(2),
    padding: 0,
  },
}));
