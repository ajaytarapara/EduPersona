import { lazy } from "react";
import { Roles, AppRoutes } from "../../utils";

const ProfilePage = lazy(() => import("../../pages/Profile"));
const AdminPage = lazy(() => import("../../pages/AdminPage"));
const CompleteProfilePage = lazy(() => import("../../pages/CompleteProfile"));

export const ProtectedRoutesConfig = [
  {
    path: AppRoutes.Profile,
    element: <ProfilePage />,
    allowRoles: [Roles.USER],
  },
  {
    path: AppRoutes.Admin,
    element: <AdminPage />,
    allowRoles: [Roles.ADMIN],
  },
  {
    path: AppRoutes.CompleteProfile,
    element: <CompleteProfilePage />,
    allowRoles: [Roles.USER],
  },
];
