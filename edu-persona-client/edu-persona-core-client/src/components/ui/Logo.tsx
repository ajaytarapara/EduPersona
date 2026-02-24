import { Box, Typography, type PaletteColorOptions } from "@mui/material";
import SchoolIcon from "@mui/icons-material/School";

interface IEduPersonaLogo {
  color?: keyof PaletteColorOptions | string;
  bgColor?: string;
}

export const EduPersonaLogo = ({
  color = "#fff",
  bgColor = "rgba(255,255,255,0.15)",
}: IEduPersonaLogo) => {
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
          backgroundColor: `${bgColor}`,
        }}
      >
        <SchoolIcon sx={{ color: { color }, fontSize: 22 }} />
      </Box>

      {/* Text */}
      <Typography
        variant="h6"
        sx={{
          color: { color },
          fontWeight: 600,
          letterSpacing: 0.3,
        }}
      >
        EduPersona
      </Typography>
    </Box>
  );
};
