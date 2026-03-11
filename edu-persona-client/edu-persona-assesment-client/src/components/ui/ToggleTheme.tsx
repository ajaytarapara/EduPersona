import { IconButton } from "@mui/material";
import { LightMode, DarkMode } from "@mui/icons-material";
import { useTheme } from "../../contexts";

export const ToggleTheme = () => {
  const { toggleTheme, theme } = useTheme();
  return (
    <IconButton onClick={toggleTheme}>
      {theme === "dark" ? <LightMode /> : <DarkMode />}
    </IconButton>
  );
};
