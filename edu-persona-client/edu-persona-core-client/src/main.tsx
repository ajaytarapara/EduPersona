import { createRoot } from "react-dom/client";
import { ThemeProvider } from "@mui/material/styles";
import { CssBaseline } from "@mui/material";
import App from "./App";
import { lightTheme, darkTheme } from "../theme";
import { ThemeContextProvider, useTheme } from "./contexts";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { persistor, store } from "./store";
import GlobalLoader from "./components/common/GlobalLoader";

function Root() {
  const { theme } = useTheme();
  return (
    <Provider store={store}>
      <PersistGate persistor={persistor}>
        <BrowserRouter>
          <ThemeProvider theme={theme === "light" ? lightTheme : darkTheme}>
            <CssBaseline />
            <GlobalLoader />
            <App />
            <ToastContainer position="top-right" autoClose={3000} />
          </ThemeProvider>
        </BrowserRouter>
      </PersistGate>
    </Provider>
  );
}

createRoot(document.getElementById("root")!).render(
  <ThemeContextProvider>
    <Root />
  </ThemeContextProvider>
);
