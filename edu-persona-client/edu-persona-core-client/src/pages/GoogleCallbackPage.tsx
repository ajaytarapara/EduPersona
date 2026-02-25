import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { validateSession, googleLogin } from "../api";
import { AppRoutes, Roles } from "../utils";
import { Box, Typography } from "@mui/material";
import FallbackLoader from "../components/common/FallbackLoader";

const GoogleCallbackPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const loginWithGoogle = async () => {
      const params = new URLSearchParams(window.location.search);
      const code = params.get("code");

      if (!code) {
        navigate(AppRoutes.Login);
        return;
      }

      try {
        const loginResponse = await googleLogin(code);
        const sessionId = loginResponse.data.sessionId;
        const validateResponse = await validateSession(sessionId);
        localStorage.setItem("sessionId", sessionId?.toString() || "");
        localStorage.setItem("role", validateResponse?.data?.role || "");
        if (validateResponse?.data?.role === Roles.USER) {
          navigate(AppRoutes.Profile);
        } else {
          navigate(AppRoutes.Admin);
        }
      } catch {
        navigate(AppRoutes.Login);
      }
    };

    loginWithGoogle();
  }, [navigate]);

  return (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      minHeight="100vh"
      gap={3}
      bgcolor="background.default"
    >
      <FallbackLoader/>
      <Typography
        variant="h6"
        color="text.primary"
        fontWeight={600}
        textAlign="center"
      >
        Signing you in...
      </Typography>

      <Typography variant="body2" color="text.secondary" textAlign="center">
        Please wait while we securely authenticate your account.
      </Typography>
    </Box>
  );
};

export default GoogleCallbackPage;
