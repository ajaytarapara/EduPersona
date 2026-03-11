import { Route, Routes } from "react-router-dom";
import { ProtectedRoutesConfig } from "./routeConfig/ProtectedRoutesConfig";
import ProtectedRoutes from "./ProtectedRoute";
import { AppRoutes, ExternalAppRoute } from "../utils";
import { Suspense, useEffect } from "react";
import FallbackLoader from "../components/common/FallbackLoader";

const ApplicationRoutes = () => {
  return (
    <Suspense fallback={<FallbackLoader />}>
      <Routes>
        {ProtectedRoutesConfig.map((route, index) => (
          <Route
            key={index}
            element={<ProtectedRoutes allowedRoles={route.allowRoles} />}
          >
            <Route path={route.path} element={route.element} />
          </Route>
        ))}
      </Routes>
    </Suspense>
  );
};

export default ApplicationRoutes;
