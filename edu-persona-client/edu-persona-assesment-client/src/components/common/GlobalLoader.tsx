import { styled } from "@mui/material";
import { Box, CircularProgress } from "@mui/material";
import { useAppSelector } from "../../store/hook";

const GlobalLoader = () => {
  const loading = useAppSelector((state) => state.loader.loading);

  if (!loading) return null;
  return (
    <LoaderOverlay>
      <CircularProgress size={60} thickness={4} />
    </LoaderOverlay>
  );
};

export default GlobalLoader;

const LoaderOverlay = styled(Box)(({ theme }) => ({
  position: "fixed",
  top: 0,
  left: 0,
  right: 0,
  bottom: 0,
  backgroundColor: "rgba(0, 0, 0, 0.5)",
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  zIndex: 9999,
}));
