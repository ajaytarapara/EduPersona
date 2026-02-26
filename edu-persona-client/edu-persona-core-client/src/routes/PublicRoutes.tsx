import { Navigate, Outlet } from "react-router-dom";
import { Roles, AppRoutes } from "../utils";
import { useAppSelector } from "../store/hook";

const PublicRoute = () => {
  const { userInfo } = useAppSelector((state) => state.auth);
  const userRole = userInfo.role;
  if (userInfo) {
    if (userRole === Roles.ADMIN)
      return <Navigate to={AppRoutes.Admin} replace />;
    if (userRole === Roles.USER)
      return <Navigate to={AppRoutes.Profile} replace />;
  }

  return <Outlet />;
};

export default PublicRoute;
