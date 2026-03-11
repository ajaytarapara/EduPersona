import { Box, Skeleton } from "@mui/material";
import { useEffect, useState } from "react";
import { CustomButton } from "../components";
import { ExternalAppRoute } from "../utils";
import { useAppDispatch, useAppSelector } from "../store/hook";
import { checkProfileCompleted, logout } from "../api";
import { logoutUser } from "../store/features";

const ProfilePage = () => {
  const dispatch = useAppDispatch();
  const { sessionId, userInfo } = useAppSelector((state) => state.auth);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const handleExamPortalNavigation = () => {
    const endPoint = `${import.meta.env.VITE_EXAM_APP_WEB_URL}${
      ExternalAppRoute.ExamDashboard
    }`;
    window.open(endPoint, "_blank");
  };
  const handleClick = async () => {
    await checkProfileCompleted();
  };
  const handleLogout = async () => {
    await logout(userInfo.userId ?? 0);
    dispatch(logoutUser());
  };
  return (
    <Box>
      {!isLoading ? (
        <>
          <h4>this is profile page</h4>
          <CustomButton
            variant="contained"
            fullWidth
            onClick={handleExamPortalNavigation}
          >
            Exam Portal
          </CustomButton>
          <CustomButton
            variant="outlined"
            onClick={handleClick}
            sx={{ marginTop: "15px" }}
          >
            Check Access
          </CustomButton>
          <CustomButton
            variant="outlined"
            onClick={handleLogout}
            sx={{ marginTop: "15px" }}
          >
            Logout
          </CustomButton>
        </>
      ) : (
        <Skeleton variant="rectangular" width={210} height={118} />
      )}
    </Box>
  );
};

export default ProfilePage;
