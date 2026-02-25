import { Navigate, Outlet } from "react-router-dom";
import { Roles, AppRoutes } from "../utils";

const PublicRoute = () => {
  const userRole = localStorage.getItem("role");

  if (userRole) {
    if (userRole === Roles.ADMIN)
      return <Navigate to={AppRoutes.Admin} replace />;
    if (userRole === Roles.USER)
      return <Navigate to={AppRoutes.Profile} replace />;
  }

  return <Outlet />;
};

export default PublicRoute;
