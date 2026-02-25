import { lazy } from "react";
import { AppRoutes } from "../../utils";

const LoginPage = lazy(() => import("../../pages/Login"));
const RegisterPage = lazy(() => import("../../pages/Register"));

export const PublicAppRoutesConfig = [
  { path: AppRoutes.Login, element: <LoginPage /> },
  { path: AppRoutes.Register, element: <RegisterPage /> },
];
