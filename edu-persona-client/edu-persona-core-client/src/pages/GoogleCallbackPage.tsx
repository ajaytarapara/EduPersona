import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { validateSession, googleLogin, isCompleteProfile } from "../api";
import { AppRoutes, Roles } from "../utils";
import { Box, Typography } from "@mui/material";
import FallbackLoader from "../components/common/FallbackLoader";
import { setSession } from "../store/features";
import { useAppDispatch } from "../store/hook";

const GoogleCallbackPage = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

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
        const profileResponse = await isCompleteProfile();
        const isProfileCompleted = profileResponse?.data;
        const userInfo = {
          userName: validateResponse.data?.userName || "",
          role: validateResponse.data?.role || "",
          isProfileCompleted: isProfileCompleted,
        };
        dispatch(setSession({ userInfo: userInfo, sessionId: sessionId }));
        if (!isProfileCompleted) {
          navigate(AppRoutes.CompleteProfile, { replace: true });
          return;
        }
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
      <FallbackLoader />
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
