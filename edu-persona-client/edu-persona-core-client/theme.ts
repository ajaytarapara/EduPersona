import { createTheme } from "@mui/material/styles";
import RalewayWoff2 from "./src/fonts/space-grotesk-v22-latin-regular.woff2";

export const lightTheme = createTheme({
  palette: {
    mode: "light",

    primary: {
      light: "#7C6FE6", // soft violet highlight
      main: "#4F46E5", // deep indigo (matches image)
      dark: "#2E267A", // dark violet (top-left depth)
      contrastText: "#FFFFFF",
    },

    secondary: {
      light: "#5B7CFA", // lighter blue glow
      main: "#3B4FD8", // strong indigo-blue (image bottom-right)
      dark: "#2634A6", // deep blue
      contrastText: "#FFFFFF",
    },

    background: {
      default: "#F8FAFC", // very light slate
      paper: "#FFFFFF",
    },

    text: {
      primary: "#0F172A", // slate-900
      secondary: "#475569", // slate-600
    },

    success: {
      light: "#6EE7B7", // emerald light
      main: "#10B981", // emerald
      dark: "#047857",
      contrastText: "#FFFFFF",
    },

    warning: {
      light: "#FCD34D", // warm amber light
      main: "#F59E0B", // amber
      dark: "#B45309",
      contrastText: "#FFFFFF",
    },

    divider: "#E2E8F0", // slate-200,
  },
  typography: {
    fontFamily: "Raleway, Arial",
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: `
        @font-face {
          font-family: 'Raleway';
          font-style: normal;
          font-display: swap;
          font-weight: 400;
          src: local('Raleway'), local('Raleway-Regular'), url(${RalewayWoff2}) format('woff2');
          unicodeRange: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF;
        }
      `,
    },
  },
});

export const darkTheme = createTheme({
  palette: {
    mode: "dark",

    primary: {
      light: "#9AA5FF",
      main: "#6366F1",
      dark: "#4338CA",
      contrastText: "#FFFFFF",
    },

    secondary: {
      light: "#6C8CFF",
      main: "#4C63E6",
      dark: "#2E3AA8",
      contrastText: "#FFFFFF",
    },

    background: {
      default: "#0F172A", // slate-900
      paper: "#1E293B", // slate-800
    },

    text: {
      primary: "#F1F5F9", // slate-100
      secondary: "#94A3B8", // slate-400
    },

    success: {
      light: "#34D399",
      main: "#10B981",
      dark: "#059669",
      contrastText: "#000000",
    },

    warning: {
      light: "#FBBF24",
      main: "#F59E0B",
      dark: "#D97706",
      contrastText: "#000000",
    },

    divider: "#334155", // slate-700
  },
  typography: {
    fontFamily: "Raleway, Arial",
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: `
        @font-face {
          font-family: 'Raleway';
          font-style: normal;
          font-display: swap;
          font-weight: 400;
          src: local('Raleway'), local('Raleway-Regular'), url(${RalewayWoff2}) format('woff2');
          unicodeRange: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF;
        }
      `,
    },
  },
});
