import { Navigate, Outlet } from "react-router-dom";
import { AppRoutes, ExternalAppRoute } from "../utils";
import { useAppDispatch, useAppSelector } from "../store/hook";
import { useEffect } from "react";
import { setSession } from "../store/features";
import Cookies from "js-cookie";

interface IProtectedRouteProps {
  allowedRoles: string[];
}

const ProtectedRoutes = ({ allowedRoles }: IProtectedRouteProps) => {
  const { userInfo } = useAppSelector((state) => state.auth);
  const userRole = userInfo?.role || "";

  if (!userInfo?.userId) {
    window.location.replace(ExternalAppRoute.CoreLogin);
    return null;
  }
  if (allowedRoles && !allowedRoles.includes(userRole)) {
    window.location.replace(ExternalAppRoute.CoreLogin);
    return null;
  }
  return <Outlet />;
};

export default ProtectedRoutes;
