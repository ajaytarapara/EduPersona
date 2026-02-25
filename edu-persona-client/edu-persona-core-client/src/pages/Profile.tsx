import { Box, Skeleton } from "@mui/material";
import { useState } from "react";

const ProfilePage = () => {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  return (
    <Box>
      {!isLoading ? (
        <h4>this is profile page</h4>
      ) : (
        <Skeleton variant="rectangular" width={210} height={118} />
      )}
    </Box>
  );
};

export default ProfilePage;
