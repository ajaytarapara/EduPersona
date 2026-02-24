import {
  Box,
  Typography,
  Stepper,
  Step,
  StepLabel,
  styled,
} from "@mui/material";
import {
  EmailOutlined,
  LockOutline,
  PersonOutline,
  VisibilityOffOutlined,
  VisibilityOutlined,
  AppRegistrationOutlined,
} from "@mui/icons-material";
import { FormProvider, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { AuthLayout, EduPersonaLogo } from "../components/ui";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { CustomButton, InputField } from "../components";
import { registerSchema, AppRoutes, type IRegisterPayload } from "../utils";
import { registerUser } from "../api";

const steps = ["Basic Info", "Security"];

/* ---------------- VALIDATION SCHEMA ---------------- */

const RegisterPage = () => {
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = useState(0);
  const [isPwdVisible, setIsPwdVisible] = useState(false);
  const [isConfirmPwdVisible, setIsConfirmPwdVisible] = useState(false);

  const methods = useForm<IRegisterPayload>({
    mode: "onTouched",
    resolver: yupResolver(registerSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      confirmPassword: "",
    },
  });

  const { trigger } = methods;

  /* ---------------- STEP VALIDATION ---------------- */

  const handleNext = async () => {
    const valid = await trigger(["firstName", "lastName", "email"]);

    if (valid) {
      setActiveStep(1);
    }
  };

  const handleBack = () => {
    setActiveStep(0);
  };

  const onSubmit = async (data: IRegisterPayload) => {
    const response = await registerUser(data);
    if (response.success) {
      navigate(AppRoutes.Login);
    }
  };

  return (
    <AuthLayout
      leftTitle="Join Us Today"
      leftSubtitle="Create your account and start your journey toward academic excellence."
      authForm={
        <StyledFormContainer>
          <StyledLogoBox>
            <EduPersonaLogo
              color="text.primary"
              bgColor="rgba(15,23,42,0.15)"
            />
          </StyledLogoBox>
          <Typography variant="h4" fontWeight={700}>
            Create Account
          </Typography>

          {/* Stepper */}
          <Stepper
            activeStep={activeStep}
            sx={{ mt: 3, mb: 4, width: "70%", mx: "auto" }}
          >
            {steps.map((label) => (
              <Step key={label}>
                <StepLabel>{label}</StepLabel>
              </Step>
            ))}
          </Stepper>

          <FormProvider {...methods}>
            <StyledForm onSubmit={methods.handleSubmit(onSubmit)}>
              {/* ---------------- STEP 1 ---------------- */}
              {activeStep === 0 && (
                <>
                  <InputField
                    name="firstName"
                    label="First Name"
                    startIcon={<PersonOutline />}
                    required
                    fullWidth
                    style={{ marginBottom: "20px" }}
                    placeholder="John"
                  />

                  <InputField
                    name="lastName"
                    label="Last Name"
                    startIcon={<PersonOutline />}
                    required
                    fullWidth
                    style={{ marginBottom: "20px" }}
                    placeholder="Doe"
                  />

                  <InputField
                    name="email"
                    label="Email Address"
                    startIcon={<EmailOutlined />}
                    required
                    fullWidth
                    style={{ marginBottom: "24px" }}
                    placeholder="your@example.com"
                  />

                  <CustomButton
                    variant="contained"
                    fullWidth
                    onClick={handleNext}
                  >
                    Next
                  </CustomButton>
                </>
              )}

              {/* ---------------- STEP 2 ---------------- */}
              {activeStep === 1 && (
                <>
                  <InputField
                    name="password"
                    label="Password"
                    startIcon={<LockOutline />}
                    type={isPwdVisible ? "text" : "password"}
                    required
                    fullWidth
                    style={{ marginBottom: "20px" }}
                    placeholder="Enter your password"
                    endIcon={
                      isPwdVisible ? (
                        <VisibilityOffOutlined />
                      ) : (
                        <VisibilityOutlined />
                      )
                    }
                    endIconClick={() => setIsPwdVisible(!isPwdVisible)}
                  />

                  <InputField
                    name="confirmPassword"
                    label="Confirm Password"
                    startIcon={<LockOutline />}
                    type={isConfirmPwdVisible ? "text" : "password"}
                    required
                    fullWidth
                    style={{ marginBottom: "24px" }}
                    placeholder="Re-enter your password"
                    endIcon={
                      isConfirmPwdVisible ? (
                        <VisibilityOffOutlined />
                      ) : (
                        <VisibilityOutlined />
                      )
                    }
                    endIconClick={() =>
                      setIsConfirmPwdVisible(!isConfirmPwdVisible)
                    }
                  />

                  <Box
                    display="flex"
                    gap={2}
                    flexDirection={{ xs: "column", sm: "row" }}
                  >
                    <CustomButton
                      variant="outlined"
                      fullWidth
                      onClick={handleBack}
                    >
                      Back
                    </CustomButton>

                    <CustomButton
                      type="submit"
                      variant="contained"
                      endIcon={<AppRegistrationOutlined />}
                      fullWidth
                    >
                      Register
                    </CustomButton>
                  </Box>
                </>
              )}
            </StyledForm>
          </FormProvider>

          <Typography
            variant="body1"
            color="text.secondary"
            textAlign="center"
            mt="20px"
          >
            Already have an account?{" "}
            <StyledSpan onClick={() => navigate(AppRoutes.Login)}>
              Login
            </StyledSpan>
          </Typography>
        </StyledFormContainer>
      }
    />
  );
};

export default RegisterPage;

/* ---------------- STYLES ---------------- */

const StyledFormContainer = styled(Box)(({ theme }) => ({
  height: "100%",
  display: "flex",
  justifyContent: "center",
  gap: theme.spacing(0.5),
  flexDirection: "column",
  maxWidth: theme.breakpoints.values.sm,
  margin: "0 auto",
}));

const StyledForm = styled("form")({
  marginTop: "5px",
});

const StyledSpan = styled("span")(({ theme }) => ({
  color: theme.palette.primary.main,
  fontWeight: 600,
  cursor: "pointer",
}));

const StyledLogoBox = styled(Box)(({ theme }) => ({
  display: "none",
  padding: theme.spacing(4),
  [theme.breakpoints.down("md")]: {
    display: "flex",
    marginBottom: theme.spacing(2),
    padding: 0,
  },
}));
