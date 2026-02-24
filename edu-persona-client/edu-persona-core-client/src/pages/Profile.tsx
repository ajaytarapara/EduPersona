import { Box } from "@mui/material";
import { testAccess } from "../api";
import { useEffect } from "react";

const ProfilePage = () => {
  const fetch = async () => {
    await testAccess();
  };
  useEffect(() => {
    fetch();
  }, []);
  return (
    <Box>
      <h4>this is profile page</h4>
    </Box>
  );
};

export default ProfilePage;
