import LoginPage from "../../pages/Login";
import RegisterPage from "../../pages/Register";
import { AppRoutes } from "../../utils";

export const PublicAppRoutesConfig = [
  { path: AppRoutes.Login, element: <LoginPage /> },
  { path: AppRoutes.Register, element: <RegisterPage /> },
];
