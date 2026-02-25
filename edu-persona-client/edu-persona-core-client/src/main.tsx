import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { ThemeProvider } from "@mui/material/styles";
import { CssBaseline } from "@mui/material";
import App from "./App";
import { lightTheme, darkTheme } from "../theme";
import { ThemeContextProvider, useTheme } from "./contexts";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function Root() {
  const { theme } = useTheme();
  return (
    <BrowserRouter>
      <ThemeProvider theme={theme === "light" ? lightTheme : darkTheme}>
        <CssBaseline />
        <App />
        <ToastContainer position="top-right" autoClose={3000} />
      </ThemeProvider>
    </BrowserRouter>
  );
}

createRoot(document.getElementById("root")!).render(
  <ThemeContextProvider>
    <Root />
  </ThemeContextProvider>
);
