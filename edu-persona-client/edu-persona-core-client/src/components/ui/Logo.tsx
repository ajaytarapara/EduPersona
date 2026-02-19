import { Box, Typography } from "@mui/material";
import SchoolIcon from "@mui/icons-material/School";

export const EduPersonaLogo = () => {
  return (
    <Box display="flex" alignItems="center" gap={2}>
      {/* Icon Box */}
      <Box
        display="flex"
        alignItems="center"
        justifyContent="center"
        sx={{
          width: 40,
          height: 40,
          borderRadius: "10px",
          backgroundColor: "rgba(255,255,255,0.15)",
        }}
      >
        <SchoolIcon sx={{ color: "#fff", fontSize: 22 }} />
      </Box>

      {/* Text */}
      <Typography
        variant="h6"
        sx={{
          color: "#fff",
          fontWeight: 600,
          letterSpacing: 0.3,
        }}
      >
        EduPersona
      </Typography>
    </Box>
  );
};
