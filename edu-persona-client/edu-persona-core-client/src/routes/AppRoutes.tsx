import { Route, Routes as BrowserRouter, Navigate } from "react-router-dom";
import { ProtectedRoutesConfig } from "./routeConfig/ProtectedRoutesConfig";
import ProtectedRoutes from "./ProtectedRoute";
import { AppRoutes } from "../utils";
import { PublicAppRoutesConfig } from "./routeConfig/PublicRoutesConfig";
import PublicRoute from "./PublicRoutes";
import { Suspense } from "react";
import FallbackLoader from "../components/common/FallbackLoader";

const ApplicationRoutes = () => {
  return (
    <Suspense fallback={<FallbackLoader />}>
      <BrowserRouter>
        {PublicAppRoutesConfig.map((route, index) => (
          <Route key={index} element={<PublicRoute />}>
            <Route path={route.path} element={route.element} />
          </Route>
        ))}
        {ProtectedRoutesConfig.map((route, index) => (
          <Route
            key={index}
            element={<ProtectedRoutes allowedRoles={route.allowRoles} />}
          >
            <Route path={route.path} element={route.element} />
          </Route>
        ))}
        <Route path="*" element={<Navigate to={AppRoutes.Login} replace />} />
      </BrowserRouter>
    </Suspense>
  );
};

export default ApplicationRoutes;
