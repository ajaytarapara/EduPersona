import { Navigate, Route, Routes as Routers } from "react-router-dom";
import "./App.css";
import LoginPage from "./pages/Login";
import RegisterPage from "./pages/Register";
import { Routes } from "./utils";
import GoogleCallbackPage from "./pages/GoogleCallbackPage";

function App() {
  return (
    <>
      <Routers>
        <Route path={Routes.Home} element={<Navigate to="/login" replace />} />
        <Route path={Routes.Login} element={<LoginPage />} />
        <Route path={Routes.Register} element={<RegisterPage />} />
        <Route path={Routes.Home} element={<LoginPage />} />
        <Route path="/auth/google-callback" element={<GoogleCallbackPage />} />
      </Routers>
    </>
  );
}

export default App;
