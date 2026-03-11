import { Box, Typography } from "@mui/material";
import { CustomButton } from "../components/common";
import { checkProfileCompleted, logout } from "../api";
import { logoutUser } from "../store/features";
import { useAppDispatch, useAppSelector } from "../store/hook";

const DashboardPage = () => {
  const dispatch = useAppDispatch();
  const { sessionId, userInfo } = useAppSelector((state) => state.auth);
  const handleClick = async () => {
    await checkProfileCompleted();
  };
  const handleLogout = async () => {
    await logout(userInfo.userId ?? 0);
    dispatch(logoutUser());
  };
  return (
    <Box>
      <Typography>This is exam dashboard page.</Typography>
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
    </Box>
  );
};

export default DashboardPage;
