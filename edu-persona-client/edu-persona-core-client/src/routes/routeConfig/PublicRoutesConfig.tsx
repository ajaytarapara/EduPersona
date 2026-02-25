import { lazy } from "react";
import { AppRoutes } from "../../utils";
import GoogleCallbackPage from "../../pages/GoogleCallbackPage";

const LoginPage = lazy(() => import("../../pages/Login"));
const RegisterPage = lazy(() => import("../../pages/Register"));

export const PublicAppRoutesConfig = [
  { path: AppRoutes.Login, element: <LoginPage /> },
  { path: AppRoutes.Register, element: <RegisterPage /> },
  { path: "/auth/google-callback", element: <GoogleCallbackPage /> },
];
