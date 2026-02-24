import { Navigate, Outlet } from "react-router-dom";
import { AppRoutes } from "../utils";

interface IProtectedRouteProps {
  allowedRoles: string[];
}

const ProtectedRoutes = ({ allowedRoles }: IProtectedRouteProps) => {
  const userRole = localStorage.getItem("role");
  if (!userRole) {
    return <Navigate to={AppRoutes.Login} replace />;
  }
  if (allowedRoles && !allowedRoles.includes(userRole)) {
    return <Navigate to={AppRoutes.Login} replace />;
  }
  return <Outlet />;
};

export default ProtectedRoutes;
