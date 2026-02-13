import { createTheme } from "@mui/material/styles";

export const lightTheme = createTheme({
  palette: {
    mode: "light",

    primary: {
      light: "#818CF8", // lighter indigo
      main: "#4F46E5", // deep indigo
      dark: "#3730A3", // darker indigo
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

    divider: "#E2E8F0", // slate-200
  },
});

export const darkTheme = createTheme({
  palette: {
    mode: "dark",

    primary: {
      light: "#818CF8",
      main: "#6366F1", // slightly brighter for dark mode
      dark: "#4338CA",
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
});
