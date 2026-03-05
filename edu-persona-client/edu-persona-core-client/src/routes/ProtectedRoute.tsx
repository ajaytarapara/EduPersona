import { Navigate, Outlet, useLocation } from "react-router-dom";
import { AppRoutes } from "../utils";
import { useAppSelector } from "../store/hook";

interface IProtectedRouteProps {
  allowedRoles: string[];
}

const ProtectedRoutes = ({ allowedRoles }: IProtectedRouteProps) => {
  const { userInfo } = useAppSelector((state) => state.auth);
  const location = useLocation();

  const userRole = userInfo.role || "";

  if (!userInfo) {
    return <Navigate to={AppRoutes.Login} replace />;
  }

  if (
    !userInfo.isProfileCompleted &&
    location.pathname !== AppRoutes.CompleteProfile
  ) {
    return <Navigate to={AppRoutes.CompleteProfile} replace />;
  }

  if (allowedRoles && !allowedRoles.includes(userRole)) {
    return <Navigate to={AppRoutes.Login} replace />;
  }

  return <Outlet />;
};

export default ProtectedRoutes;
