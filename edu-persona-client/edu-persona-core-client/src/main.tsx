import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { ThemeProvider } from "@mui/material/styles";
import { CssBaseline } from "@mui/material";
import App from "./App";
import { lightTheme, darkTheme } from "../theme";
import { ThemeContextProvider, useTheme } from "./contexts";

function Root() {
  const { theme } = useTheme();
  return (
    <ThemeProvider theme={theme === "light" ? lightTheme : darkTheme}>
      <CssBaseline />
      <App />
    </ThemeProvider>
  );
}

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeContextProvider>
      <Root />
    </ThemeContextProvider>
  </StrictMode>
);
